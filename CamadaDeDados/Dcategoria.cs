using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CamadaDeDados
{
    public class Dcategoria { 

        private int _Idcategoria { get; set; }
        private string _Nome { get; set; } 
        private string _Descricao { get; set; }
        private string _Textobuscar { get; set; }

        public Dcategoria()
        {
        }

        public Dcategoria(int idcategoria)
        {
            _Idcategoria = idcategoria;
        }

        public Dcategoria(string textobuscar)
        {
            _Textobuscar = textobuscar;
        }

        public Dcategoria(string nome, string descricao)
        {
            _Nome = nome;
            _Descricao = descricao;
        }
        public Dcategoria(int idcategoria, string nome, string descricao)
        {
            _Idcategoria = idcategoria;
            _Nome = nome;
            _Descricao = descricao;
        }
        public Dcategoria(int idcategoria, string nome, string descricao, string textobuscar)
        {
            _Idcategoria = idcategoria;
            _Nome = nome;
            _Descricao = descricao;
            _Textobuscar = textobuscar;
        }
        //method para inserir dados
        public string Inserir (Dcategoria categoria)
        {
            string resp = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = Conexao.strCon;
                con.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = con;
                sqlcmd.CommandText = "inserircategoria;";
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterid = new SqlParameter();
                parameterid.ParameterName = "@idcategoria";
                parameterid.SqlDbType = SqlDbType.Int;
                parameterid.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(parameterid);
                
                SqlParameter parameterNome = new SqlParameter();
                parameterNome.ParameterName = "@nome";
                parameterNome.SqlDbType = SqlDbType.VarChar;
                parameterNome.Size = 50;
                parameterNome.Value = _Nome;
                sqlcmd.Parameters.Add(parameterNome);

                SqlParameter parametersDescricao = new SqlParameter();
                parametersDescricao.ParameterName = "@descricao";
                parametersDescricao.SqlDbType = SqlDbType.VarChar;
                parametersDescricao.Size = 100;
                parametersDescricao.Value = _Descricao;
                sqlcmd.Parameters.Add(parametersDescricao);

                resp = sqlcmd.ExecuteNonQuery() == 1 ? "ok" : "erro ao inserir";

            }
            catch (Exception ex)
            {
                return $"Erro: {ex.Message}";
            }
            finally {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                
            }

            return resp;
        }
        //method para Editar dados
        public string Editar(Dcategoria categoria)
        {
            string resp = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = Conexao.strCon;
                con.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = con;
                sqlcmd.CommandText = "editarcategoria;";
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterid = new SqlParameter();
                parameterid.ParameterName = "@idcategoria";
                parameterid.SqlDbType = SqlDbType.Int;
                parameterid.Value = _Idcategoria;
                sqlcmd.Parameters.Add(parameterid);

                SqlParameter parameterNome = new SqlParameter();
                parameterNome.ParameterName = "@nome";
                parameterNome.SqlDbType = SqlDbType.VarChar;
                parameterNome.Size = 50;
                parameterNome.Value = _Nome;
                sqlcmd.Parameters.Add(parameterNome);

                SqlParameter parametersDescricao = new SqlParameter();
                parametersDescricao.ParameterName = "@descricao";
                parametersDescricao.SqlDbType = SqlDbType.VarChar;
                parametersDescricao.Size = 100;
                parametersDescricao.Value = _Descricao;
                sqlcmd.Parameters.Add(parametersDescricao);

                resp = sqlcmd.ExecuteNonQuery() == 1 ? "ok" : "erro ao Editar";

            }
            catch (Exception ex)
            {
                return $"Erro: {ex.Message}";
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

            return resp;
        }
        //method para excluir dados
        public string Excluir(Dcategoria categoria)
        {
            string resp = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = Conexao.strCon;
                con.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = con;
                sqlcmd.CommandText = "deletarcategoria;";
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterid = new SqlParameter();
                parameterid.ParameterName = "@idcategoria";
                parameterid.SqlDbType = SqlDbType.Int;
                parameterid.Value = _Idcategoria;
                sqlcmd.Parameters.Add(parameterid);

                resp = sqlcmd.ExecuteNonQuery() == 1 ? "ok" : "erro ao Excluir";

            }
            catch (Exception ex)
            {
                return $"Erro: {ex.Message}";
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return resp;
        }
        //method para Mostrar dados
        public DataTable Mostrar()
        {
            DataTable DtResult = new DataTable("categoria");
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = Conexao.strCon;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "mostracategoria";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataadap = new SqlDataAdapter(cmd);
                dataadap.Fill(DtResult);

            }
            catch (Exception ex)
            {

                DtResult = null;
            }
            return DtResult;
        }
        //method para Buscar Nome dados
        public DataTable BuscarNome(Dcategoria categoria)
        {
            
            DataTable DtResult = new DataTable("categoria");
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = Conexao.strCon;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "buscanome";
                cmd.CommandType = CommandType.StoredProcedure;
                
                SqlParameter parametersBuscar = new SqlParameter();
                parametersBuscar.ParameterName = "@textobuscar";
                parametersBuscar.SqlDbType = SqlDbType.VarChar;
                parametersBuscar.Size = 100;
                parametersBuscar.Value = _Textobuscar;
                cmd.Parameters.Add(parametersBuscar);

                SqlDataAdapter dataadap = new SqlDataAdapter(cmd);
                dataadap.Fill(DtResult);

            }
            catch (Exception ex)
            {

                DtResult = null;
                return DtResult;
            }
            return DtResult;
        }



    }
}

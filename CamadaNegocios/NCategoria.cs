using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaDeDados;
using System.Data;

namespace CamadaNegocios
{
    public class NCategoria
    {
        public static string Inserir(string nome, string descricao) {

            Dcategoria dcatObj= new Dcategoria(nome,descricao);
            return dcatObj.Inserir(dcatObj);

        }

        public static string Editar(int IdCategoria, string nome, string descricao)
        {

            Dcategoria dcatObj = new Dcategoria(IdCategoria ,nome, descricao);
            return dcatObj.Editar(dcatObj);

        }

        public static string Excluir(int IdCategoria)
        {

            Dcategoria dcatObj = new Dcategoria(IdCategoria);
            return dcatObj.Excluir(dcatObj);

        }

        public static DataTable Mostrar() {

            return new Dcategoria().Mostrar();

        }
        public static DataTable BuscarNome(string textoBuscar)
        {
            Dcategoria dcObj = new Dcategoria(textoBuscar);
            
            return dcObj.BuscarNome(dcObj);

        }

    }
}

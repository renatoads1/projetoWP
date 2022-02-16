using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamadaNegocios;

namespace CamadaDeApresentacao
{
    public partial class frmCategoria : Form
    {
        private bool eNovo = false;
        private bool eEditar = false;
        public frmCategoria()
        {
            InitializeComponent();
            this.ttMessage.SetToolTip(this.txtNomeCategoria, "Insira o nome da categoria");
        }

        private void MessageOk(string message) {

            MessageBox.Show(message,"Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private void MessageErro(string message)
        {

            MessageBox.Show(message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LimparCampos()
        {
            this.txtIdCategoria.Text = string.Empty;
            this.txtNomeCategoria.Text = string.Empty;
            this.txtDescricaoCategoria.Text = string.Empty;
            
        }

        private void Habilitar(bool valor) {

            this.txtIdCategoria.ReadOnly = !valor;
            this.txtNomeCategoria.ReadOnly = !valor;
            this.txtDescricaoCategoria.ReadOnly = !valor;

        }

        private void Botoes()
        {
            if (this.eNovo || this.eEditar)
            {

                this.Habilitar(true);
                this.btnNovoCategoria.Enabled = false;
                this.btnSalvarDesc.Enabled = true;
                this.btnEditarDesc.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else {
                this.Habilitar(false);
                this.btnNovoCategoria.Enabled = true;
                this.btnSalvarDesc.Enabled = false;
                this.btnEditarDesc.Enabled = true;
                this.btnCancelar.Enabled = false;
            }

        }
        private void Mostrar() {

            this.dataLista.DataSource = NCategoria.Mostrar();
            this.OcultarColunas();
            lblTotal.Text = "Total de registros \n "+dataLista.Rows.Count.ToString();
        }

        private void BuscarNome()
        {

            this.dataLista.DataSource = NCategoria.BuscarNome(this.tbNomeBusca.Text);
            this.OcultarColunas();
            lblTotal.Text = "Total de registros \n " + dataLista.Rows.Count.ToString();
        }

        private void OcultarColunas() {

            this.dataLista.Columns[0].Visible = false;
            this.dataLista.Columns[1].Visible = false;
        }


        private void frmCategoria_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.Botoes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void tbNomeBusca_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNome();
        }

        private void btnNovoCategoria_Click(object sender, EventArgs e)
        {
            this.eNovo = true;
            this.eEditar = false;
            this.Botoes();
            this.LimparCampos();
            this.Habilitar(true);
            this.txtNomeCategoria.Focus();
            this.txtIdCategoria.Enabled = false;

        }

        private void btnSalvarDesc_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if (this.txtNomeCategoria.Text == string.Empty)
                {
                    MessageErro("O campo nome deve ser preenchido!!!");
                    errorIcon.SetError(txtNomeCategoria, "Insira o nome");
                }
                else {
                    if (this.eNovo)
                    {   //caso de inserir
                        resp = NCategoria.Inserir(this.txtNomeCategoria.Text.Trim().ToUpper(),
                            this.txtDescricaoCategoria.Text.Trim().ToUpper());
                    }
                    else { //caso de editar
                        resp = NCategoria.Editar(Convert.ToInt32(this.txtIdCategoria.Text),
                            this.txtNomeCategoria.Text.Trim().ToUpper(),
                            this.txtDescricaoCategoria.Text.Trim().ToUpper());
                    }
                    if (resp.Equals("ok"))
                    {
                        if (this.eNovo)
                        {

                            this.MessageOk("Registro salvo!!!");
                        }
                        else
                        {
                            this.MessageOk("Registro Editado!!!");
                        }
                    }
                    else {

                        this.MessageErro(resp);
                    }
                    this.eNovo = false;
                    this.eEditar = false;
                    this.Botoes();
                    this.LimparCampos();
                    this.Mostrar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void dataLista_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdCategoria.Text = this.dataLista.CurrentRow.Cells[1].Value.ToString();
            this.txtNomeCategoria.Text = this.dataLista.CurrentRow.Cells[2].Value.ToString();
            this.txtDescricaoCategoria.Text = this.dataLista.CurrentRow.Cells[3].Value.ToString();
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEditarDesc_Click(object sender, EventArgs e)
        {

        }
    }
}

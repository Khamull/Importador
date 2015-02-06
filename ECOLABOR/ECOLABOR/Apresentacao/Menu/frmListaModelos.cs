using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECOLABOR.Negocios;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Office.Interop.Excel;
using ECOLABOR.Dados;
using System.Data.OleDb;
using ECOLABOR.Negocios.funcoesUteis;
using System.Data.SqlClient;
using ECOLABOR.Apresentacao.Login;
using ECOLABOR.Apresentacao.Administração;
using ECOLABOR.Apresentacao.Menu;

namespace ECOLABOR.Apresentacao.Menu
{
    public partial class frmListaModelos : Form
    {
        public int estado;
        DataSet user = new DataSet();
        public string MODELO;
        public int ID_MODELO;
        //frmCadastroNovaPlanilha frmNova = new frmCadastroNovaPlanilha();
        csDados dados = new csDados();
        public frmListaModelos()
        {
            InitializeComponent();
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
                DialogResult result = MessageBox.Show("Deseja Excluir Modelo Selecionado?", "Exclusão de Modelos", MessageBoxButtons.OKCancel);
                switch (result)
                {
                    case DialogResult.OK:
                        {
                            if (!string.IsNullOrEmpty(MODELO) && !string.IsNullOrEmpty(ID_MODELO.ToString()))
                            {
                                try
                                {
                                    try
                                    {
                                        dados.ExecutarComando("DELETE FROM " + MODELO + "  WHERE ID_MODELO = " + ID_MODELO);
                                    }
                                    catch { }
                                    try
                                    {
                                        dados.ExecutarComando("DELETE FROM PARAMETROS WHERE ID_MODELO = " + ID_MODELO);
                                    }
                                    catch { }
                                    try
                                    {

                                        dados.ExecutarComando("DELETE FROM MODELOS WHERE ID_MODELO = " + ID_MODELO);

                                    }
                                    catch { }
                                    try
                                    {
                                        dados.ExecutarComando("DROP TABLE " + MODELO);
                                    }
                                    catch { }

                                    carregaGrid();
                                }
                                catch { }
                            }
                            else
                            {
                                MessageBox.Show("Selecione um Registro da Tabela para Excluir");
                            }
                            
                            //Delete Parametros
                            
                            //Delete Modelos
                            //DROP Tabelas
                            break;
                        }
                    case DialogResult.Cancel:
                        {

                            break;
                        }
                }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListaModelos_Load(object sender, EventArgs e)
        {
            user = csUserDataSet.USER_;
            validaCampos();
            dtGVModelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtGVModelos.ReadOnly = true;
            carregaGrid();

            if (dtGVModelos.Rows.Count <= 0)
            {
                btnexcluir.Enabled = false;
            }
            else
            {
                //dtGVModelos.Columns.Remove("ID_MODELO");
                dtGVModelos.Rows[0].Selected = false;
                dtGVModelos.FirstDisplayedScrollingRowIndex = dtGVModelos.RowCount - 1;
            }
        }
        public void validaCampos()
        {
            if (estado == 0)
            {
                btnexcluir.Enabled = false;
                btnImport.Enabled = true;
                //btnNovaPlanilha.Enabled = true;
            }
            else
            {
                btnexcluir.Enabled = true;
                btnImport.Enabled = false;
                //btnNovaPlanilha.Enabled = true;
            }
            if (Convert.ToInt32(user.Tables[0].Rows[0]["ID_ACESSO"].ToString()) == 0)
            {
                btnexcluir.Enabled = true;
            }
            else
            {
                btnexcluir.Enabled = false;
            }
            
        }
        public void carregaGrid()
        {
            DataSet modelos = new DataSet();
            DataView modelos_view = new DataView();
            modelos = dados.RetornarDataSet("SELECT ID_MODELO AS ID, NOME_MODELO AS MODELO, NOME_WORKSHEET AS PLANILHA, IT AS IT, DATA_CRIACAO AS DATA FROM MODELOS");
            modelos_view = modelos.Tables[0].DefaultView;
            dtGVModelos.DataSource = modelos_view;
            //dtGVModelos.Columns.Remove("ID");
            dtGVModelos.ReadOnly = true;
        }

        private void dtGVModelos_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtGVModelos.SelectedRows)
            {
                if (!string.IsNullOrEmpty(row.Cells["MODELO"].Value.ToString()))
                {
                    MODELO = row.Cells["MODELO"].Value.ToString();
                }
                if (!string.IsNullOrEmpty(row.Cells["ID"].Value.ToString()))
                {
                    ID_MODELO = (int)row.Cells["ID"].Value;
                }
            }
        }

        private void frmListaModelos_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}

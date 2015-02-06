using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECOLABOR.Apresentacao.Login;
using ECOLABOR.Negocios.funcoesUteis;
using ECOLABOR.Apresentacao.Administração;
using ECOLABOR.Apresentacao.Menu;
using ECOLABOR.Dados;


namespace ECOLABOR.Apresentacao
{
    public partial class frmPrincipal : Form
    {
        //Declaração de variaveis e objetos globais;
        public DataSet USUARIO = new DataSet();
        public string usuario;
        public string senha;
        //public List<string> user = new List<string>();
        csStrings querys = new csStrings();


        frmCadastroNovaPlanilha frmNovaPlanilhas;
        frmImportaDados frmImportaPlanilhas;
        frmListaModelos frmlistaplanilhas;
        frmConfiguracoes frmConfig;
        frmCadastroDeUsuarios frmUserCad;
        frmSenhaArquivos frmSenhaArq;
        
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            //user.Add(usuario);
            //user.Add(senha);
            //csDados dados = new csDados();
            //USUARIO = dados.RetornarDataSet(querys.criaString(1, user));
            csUserDataSet user = new csUserDataSet();
            USUARIO = csUserDataSet.USER_;
        }
        
        #region ---------------------- MENU -----------------------------
        private void novaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(USUARIO.Tables[0].Rows[0]["ID_ACESSO"].ToString()) == 0)
            {

                if (Application.OpenForms.OfType<frmCadastroNovaPlanilha>().Count() == 0)
                {
                    frmNovaPlanilhas = new frmCadastroNovaPlanilha();//Objeto do Formulário de Importação de Cadastro de nova planilha
                    frmNovaPlanilhas.TopLevel = false;
                    frmNovaPlanilhas.MdiParent = this;
                    frmNovaPlanilhas.Show();
                }
                else
                {
                    frmNovaPlanilhas.BringToFront();
                    frmNovaPlanilhas.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("Somente Administradores Possuem Acesso a Funcionalidade");
            }        
        }

        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmImportaDados>().Count() == 0)//Verifica se ja tem janela aberta, se não abre uma nova
            {
                frmImportaPlanilhas = new frmImportaDados();
                frmImportaPlanilhas.TopLevel = false;
                frmImportaPlanilhas.MdiParent = this;
                frmImportaPlanilhas.tipodeacesso = 1;
                frmImportaPlanilhas.Show();
            }
            else//caso contrário tras a antiga para frente
            {
                frmImportaPlanilhas.BringToFront();
                frmImportaPlanilhas.WindowState = FormWindowState.Normal;
            }
        }

        private void listaDePlanilhasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmListaModelos>().Count() == 0)
            {
                frmlistaplanilhas = new frmListaModelos();
                frmlistaplanilhas.TopLevel = false;
                frmlistaplanilhas.MdiParent = this;
                frmlistaplanilhas.estado = 1;
                frmlistaplanilhas.Show();
            }
            else
            {
                frmlistaplanilhas.BringToFront();
                frmlistaplanilhas.WindowState = FormWindowState.Normal;
            }
        }


        #endregion

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            csFechaExcel fecha = new csFechaExcel();
            fecha.fechaQualquerExcel();
        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void cadastroDeUsuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(USUARIO.Tables[0].Rows[0]["ID_ACESSO"].ToString()) == 0)
            {
                if (Application.OpenForms.OfType<frmCadastroDeUsuarios>().Count() == 0)
                {
                    frmUserCad = new frmCadastroDeUsuarios();
                    frmUserCad.TopLevel = false;
                    frmUserCad.MdiParent = this;
                    frmUserCad.Show();
                }
                else
                {
                    frmUserCad.BringToFront();
                    frmUserCad.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("Somente Administradores Possuem Acesso a Funcionalidade");
            }
        }

        private void baseDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(USUARIO.Tables[0].Rows[0]["ID_ACESSO"].ToString()) == 0)
            {

                if (Application.OpenForms.OfType<frmConfiguracoes>().Count() == 0)
                {

                    frmConfig = new frmConfiguracoes();
                    frmConfig.TopLevel = false;
                    frmConfig.MdiParent = this;
                    frmConfig.Show();
                }
                else
                {
                    frmConfig.BringToFront();
                    frmConfig.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("Somente Administradores Possuem Acesso a Funcionalidade");
            }
        }

        private void senhaArquivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(USUARIO.Tables[0].Rows[0]["ID_ACESSO"].ToString()) == 0)
            {

                if (Application.OpenForms.OfType<frmConfiguracoes>().Count() == 0)
                {
                    frmSenhaArq = new frmSenhaArquivos();
                    frmSenhaArq.TopLevel = false;
                    frmSenhaArq.MdiParent = this;
                    frmSenhaArq.Show();
                }
                else
                {
                    frmSenhaArq.BringToFront();
                    frmSenhaArq.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("Somente Administradores Possuem Acesso a Funcionalidade");
            }
        }
    }
}

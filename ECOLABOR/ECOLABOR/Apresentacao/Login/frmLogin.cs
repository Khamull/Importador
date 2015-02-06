using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECOLABOR.Dados;
using ECOLABOR.Negocios.funcoesUteis;

namespace ECOLABOR.Apresentacao.Login
{
    public partial class frmLogin : Form
    {
        csStrings querys = new csStrings();
        frmPrincipal frmprincipal = new frmPrincipal();
        public List<string> lista = new List<string>();
        public static DataSet user;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validaCampo())
            {
                csDados dados = new csDados();
                user = new DataSet();

                lista.Add(txtxUsrLogin.Text);
                lista.Add(txtPassWdLogin.Text);
                try
                {
                    user = dados.RetornarDataSet(querys.criaString(1, lista));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Probelmas de Conexão com o Banco :" + ex.ToString());
                    return;
                }
                if (user.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("Usuário ou Senha Inválido ou Usuário Inativo");
                    txtPassWdLogin.Text = "";
                    txtxUsrLogin.Text = "";
                    lista.Clear();
                    return;
                }
                else
                {
                    if (Convert.ToBoolean(user.Tables[0].Rows[0]["ATIVO"]) != true)
                    {
                        MessageBox.Show("Usuário não esta Ativo, Favor Verificar o Cadastro!");
                        txtPassWdLogin.Text = "";
                        txtxUsrLogin.Text = "";
                        user.Clear();
                        lista.Clear();
                        return;
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            else
            {
                MessageBox.Show("Favor Preencher todos os campos!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            lista.Clear();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        public bool validaCampo()
        {
            if (!string.IsNullOrEmpty(txtxUsrLogin.Text) && !string.IsNullOrEmpty(txtPassWdLogin.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

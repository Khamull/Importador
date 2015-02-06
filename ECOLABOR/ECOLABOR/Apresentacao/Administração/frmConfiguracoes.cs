using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECOLABOR.Negocios.funcoesUteis;
using ECOLABOR.Dados;


namespace ECOLABOR.Apresentacao.Administração
{
    public partial class frmConfiguracoes : Form
    {
        List<csConfiguracoes> configs = new List<csConfiguracoes>();
        string Arquivo = System.Environment.CurrentDirectory.ToString() + @"\config.xml";
        public int acesso = 0;
        public bool resultado;
        public string DB;
        public frmConfiguracoes()
        {
            InitializeComponent();
        }

        private void frmConfiguracoes_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(System.Environment.CurrentDirectory.ToString() + @"\config.xml");
            carregaConfigs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            csDados dados = new csDados();
            if (dados.StatusConexao(txtServer.Text, txtBasedeDados.Text, txtUser.Text, txtSenha.Text))
            {
                MessageBox.Show("Conexão Bem Sucedida");
                resultado = true;
            }
            else
            {
                MessageBox.Show("Conexão Não Foi Possivel. Por favor, valide as informações inseridas");
                resultado = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtServer.Text = "";
            txtBasedeDados.Text = "";
            txtUser.Text = "";
            txtSenha.Text = "";
        }
        public void carregaConfigs()
        {
            configs = csConfiguracoes.ListarConfiguracoes(Arquivo);
            try
            {
                if (configs != null)
                {
                    for (int i = 0; i < configs.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(configs[i].Server.ToString()))
                        {
                            txtServer.Text = configs[i].Server.ToString();
                        }
                        if (!string.IsNullOrEmpty(configs[i].Database.ToString()))
                        {
                            txtBasedeDados.Text = configs[i].Database.ToString();
                        }
                        if (!string.IsNullOrEmpty(configs[i].User.ToString()))
                        {
                            txtUser.Text = configs[i].User.ToString();
                        }
                        if (!string.IsNullOrEmpty(configs[i].Passwd.ToString()))
                        {
                            txtSenha.Text = configs[i].Passwd.ToString();
                        }
                    }
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (validaCampo())
            {
                csConfiguracoes c = new csConfiguracoes()
                {
                    Server = txtServer.Text,
                    Database = txtBasedeDados.Text,
                    User = txtUser.Text,
                    Passwd = txtSenha.Text
                };
                csConfiguracoes.EditarPessoa(c, Arquivo);
                MessageBox.Show("Alterações Salvas com Sucesso!");
                if (acesso == 1)
                {
                    button2_Click(this, null);
                    if (resultado)
                    {
                        DB = txtBasedeDados.Text;
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Favor Preencher Todos os Campos");
            }
        }
        public bool validaCampo()
        {
            if (string.IsNullOrEmpty(txtServer.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtBasedeDados.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                return false;
            }
            return true;
        }
    }
}

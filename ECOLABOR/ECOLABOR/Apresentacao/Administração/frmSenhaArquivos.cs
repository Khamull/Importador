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
    public partial class frmSenhaArquivos : Form
    {
        List<csConfiguracoes> configs = new List<csConfiguracoes>();
        string Arquivo = System.Environment.CurrentDirectory.ToString() + @"\config.xml";
        public frmSenhaArquivos()
        {
            InitializeComponent();
            carregaConfigs();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
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
                        if (!string.IsNullOrEmpty(configs[i].fPasswd.ToString()))
                        {
                            textBox1.Text = configs[i].fPasswd.ToString();
                        }
                    }
                }
            }
            catch { }
        }
        public void Salvar()
        {
            if (validaCampo())
            {
                csConfiguracoes c = new csConfiguracoes()
                {
                    fPasswd = textBox1.Text
                };
                csConfiguracoes.EditarfPasswd(c, Arquivo);
                MessageBox.Show("Alterações Salvas com Sucesso!");
                carregaConfigs();
            }
            else
            {
                MessageBox.Show("Favor Preencher o Campo");
            }
        }
        public bool validaCampo()
        {
            bool ok = false;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                ok = true;
            }
            return ok;
        }
    }
}

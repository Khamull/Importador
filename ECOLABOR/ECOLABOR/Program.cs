using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ECOLABOR.Apresentacao;
using ECOLABOR.Apresentacao.Login;
using ECOLABOR.Negocios.funcoesUteis;
using ECOLABOR.Dados;
using ECOLABOR.Apresentacao.Administração;

namespace ECOLABOR
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region----Carregar Configs do Banco
            string Arquivo = System.Environment.CurrentDirectory.ToString() + @"\config.xml";
            List<csConfiguracoes> configs = new List<csConfiguracoes>();
            configs = csConfiguracoes.ListarConfiguracoes(Arquivo);
            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmLogin frmlogin = new frmLogin();
            if (string.IsNullOrEmpty(configs[0].Server))
            {
                frmConfiguracoes configs_ = new frmConfiguracoes();
                configs_.acesso = 1;
                configs_.ShowDialog();
                bool resultado = configs_.resultado;
                if (resultado)
                {
                    try
                    {
                        csDados dados = new csDados();
                        dados.ExecutarComando(csStringCriaBase.criaBase(configs_.DB));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("DataBase Ja Existente");
                        frmLogin_();
                        return;
                    }
                    MessageBox.Show("Base de Dados Criada com Sucesso");
                    frmLogin_();
                }
            }
            else
            {
                frmLogin_();
                //if (frmlogin.ShowDialog() == DialogResult.OK)
                //{
                //    //csUserDataSet user = new csUserDataSet();
                //    csUserDataSet.USER_ = frmLogin.user;
                //    Application.Run(new frmPrincipal());
                //}
                //else
                //{
                //    Application.Exit();
                //}
            }
        }
        static void frmLogin_()
        {
            frmLogin frmlogin = new frmLogin();
            if (frmlogin.ShowDialog() == DialogResult.OK)
            {
                //csUserDataSet user = new csUserDataSet();
                csUserDataSet.USER_ = frmLogin.user;
                Application.Run(new frmPrincipal());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}

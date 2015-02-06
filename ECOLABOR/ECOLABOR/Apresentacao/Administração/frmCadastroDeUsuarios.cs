using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECOLABOR.Dados;
using System.Security;
using ECOLABOR.Negocios.funcoesUteis;

namespace ECOLABOR.Apresentacao.Administração
{
    public partial class frmCadastroDeUsuarios : Form
    {
        public int ativo;
        csDados dados = new csDados();
        public frmCadastroDeUsuarios()
        {
            InitializeComponent();
        }

        private void frmCadastroDeUsuarios_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sIPDataSet.ACESSOS' table. You can move, or remove it, as needed.
            this.aCESSOSTableAdapter.Fill(this.sIPDataSet.ACESSOS);

        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            frmListaUsuários frmUsuarios = new frmListaUsuários();
            frmUsuarios.ShowDialog();
            txtID.Text = frmUsuarios.ID;
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtID.Text))
                {
                   
                    DataSet user = new DataSet();
                    user = dados.RetornarDataSet(@"SELECT   A.ID_USUARIO AS ID,
	                                            A.NOME,
	                                            A.SOBRENOME,
	                                            B.LOGIN_USUARIO AS LOGIN,
	                                            B.ATIVO,
	                                            B.SENHA AS SENHA,
	                                            C.ID_ACESSO AS ID_ACESSO
                                                FROM USUARIO A, LOGIN_USUARIO B, ACESSOS C
                                                WHERE A.ID_USUARIO = B.ID_USUARIO
                                                AND B.ID_ACESSO = C.ID_ACESSO
                                                AND A.ID_USUARIO = " + txtID.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(user.Tables[0].Rows[0]["NOME"])))
                    {
                        txtNome.Text = Convert.ToString(user.Tables[0].Rows[0]["NOME"]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(user.Tables[0].Rows[0]["SOBRENOME"])))
                    {
                        txtSobrenome.Text = Convert.ToString(user.Tables[0].Rows[0]["SOBRENOME"]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(user.Tables[0].Rows[0]["LOGIN"])))
                    {
                        txtLoginUser.Text = Convert.ToString(user.Tables[0].Rows[0]["LOGIN"]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(user.Tables[0].Rows[0]["SENHA"])))
                    {
                        txtLogInSenha.Text = Convert.ToString(user.Tables[0].Rows[0]["SENHA"]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(user.Tables[0].Rows[0]["ATIVO"])))
                    {
                        if (Convert.ToBoolean(Convert.ToString(user.Tables[0].Rows[0]["ATIVO"])) == true)
                        {
                            radioButton1.Checked = true;
                            radioButton2.Checked = false;
                        }
                        else
                        {
                            radioButton1.Checked = false;
                            radioButton2.Checked = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(user.Tables[0].Rows[0]["ID_ACESSO"])))
                    {
                        comboBox1.SelectedValue = user.Tables[0].Rows[0]["ID_ACESSO"];
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.ToString()); }
        }

        private void button4_Click(object sender, EventArgs e)//Gerar usuário
        {
            if (!string.IsNullOrEmpty(txtNome.Text) && !string.IsNullOrEmpty(txtSobrenome.Text))
            {
                txtLoginUser.Text = txtNome.Text.Substring(0, 1).ToUpper() + txtSobrenome.Text.ToUpper();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtLogInSenha.Text = crip();
        }
        public string crip()
        {

            Random randNumber = new Random();
            
            if (!string.IsNullOrEmpty(txtLoginUser.Text))
                return csCriptografia.Encrypt(randNumber.NextDouble()+txtLoginUser.Text).Substring(0, 6);
            else
                return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                if (radioButton1.Checked == true)
                {
                    ativo = 1;
                }
                if (radioButton2.Checked == true)
                {
                    ativo = 0;
                }
                if (!string.IsNullOrEmpty(txtID.Text))
                {
                    //UPDATE
                    List<string> updates = new List<string>();
                    updates.Add(@" UPDATE LOGIN_USUARIO
                                        SET LOGIN_USUARIO = '" + txtLoginUser.Text
                                      + "',SENHA = '" + txtLogInSenha.Text
                                      + "',ID_ACESSO = " + comboBox1.SelectedValue
                                      + " ,ATIVO = '" + ativo.ToString()
                                      + "' WHERE ID_USUARIO = " + txtID.Text);
                    updates.Add(@"UPDATE USUARIO
                                    SET  NOME  = '" + txtNome.Text
                              + "' , SOBRENOME = '" + txtSobrenome.Text
                              + "' WHERE ID_USUARIO = " + txtID.Text);
                    for (int i = 0; i < updates.Count; i++)
                    {
                        try
                        {
                            dados.ExecutarComando(updates[i]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("UpdateError Lista " + i + " :" + ex.ToString());
                            return;
                        }
                    }
                    MessageBox.Show("Dados Salvos Com Sucesso!");
                }
                else
                {
                    //inserts
                    List<string> inserts = new List<string>();
                    //DataSet existsUser = new DataSet();
                    //existsUser = dados.RetornarDataSet(@"SELECT ID_USUARIO FROM USUARIO WHERE CPF = '" + txtCPF.Text+"'");
                    //if (existsUser.Tables[0].Rows.Count > 0)
                    //{
                    //    MessageBox.Show("Usuário Já Cadastrado!");
                    //    return;
                    //}
                    DataSet ID_USER = new DataSet();
                    try
                    {
                        ID_USER = dados.RetornarDataSet("SELECT MAX(ID_USUARIO)+1 AS ID FROM USUARIO");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao Gerar Novo ID de Usuário: " + ex.ToString());
                        return;
                    }
                    inserts.Add(@"INSERT INTO  USUARIO 
                                ( ID_USUARIO 
                                , NOME 
                                , SOBRENOME)
                            VALUES
                             (" + ID_USER.Tables[0].Rows[0]["ID"]
                             + ",'" + txtNome.Text
                             + "','" + txtSobrenome.Text + "')");
                    DataSet ID_LOGIN = new DataSet();
                    try
                    {
                        ID_LOGIN = dados.RetornarDataSet("SELECT MAX(ID_LOGIN)+1 AS ID FROM LOGIN_USUARIO");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao Gerar Novo ID de Login: " + ex.ToString());
                        return;
                    }
                    inserts.Add(@"INSERT INTO LOGIN_USUARIO 
                                   ( ID_LOGIN 
                                   , ID_USUARIO 
                                   , LOGIN_USUARIO 
                                   , SENHA 
                                   , ID_ACESSO 
                                   , ATIVO )
                             VALUES
                                   (" + ID_LOGIN.Tables[0].Rows[0]["ID"]
                                   + ",'" + ID_USER.Tables[0].Rows[0]["ID"]
                                   + "','" + txtLoginUser.Text
                                   + "','" + txtLogInSenha.Text
                                   + "'," + comboBox1.SelectedValue
                                   + "," + ativo + ")");
                    for (int i = 0; i < inserts.Count; i++)
                    {
                        try
                        {
                            dados.ExecutarComando(inserts[i]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao Inserir Passo " + (i + 1) + ": " + ex.ToString());
                            return;
                        }
                    }
                    MessageBox.Show("Usuário " + txtLoginUser.Text + " Cadastrado com Sucesso!");
                    txtID.Text = ID_USER.Tables[0].Rows[0]["ID"].ToString();
                }

            }
        }
        public bool validaCampos()
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Por Favor, Informe Nome!");
                return false;
            }
            if (string.IsNullOrEmpty(txtSobrenome.Text))
            {
                MessageBox.Show("Por Favor, Informe Sobrenome!");
                return false;
            }
            if (string.IsNullOrEmpty(txtLoginUser.Text))
            {
                MessageBox.Show("Por Favor, Gere um Login!");
                return false;
            }
            if (string.IsNullOrEmpty(txtLogInSenha.Text))
            {
                MessageBox.Show("Por Favor, Gere uma Senha!");
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtLogInSenha.Text = "";
            txtLoginUser.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Favor Selecionar Um Usuário para Excluir!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Deseja Excluir Usuário?", "Deletar Usuário", MessageBoxButtons.OKCancel);
                switch (result)
                {
                    case DialogResult.OK:
                        {
                            deletaUser();
                            break;
                        }
                    case DialogResult.Cancel:
                        {

                            break;
                        }
                }
            }
        }
        public void deletaUser()
        {
            try
            {
                dados.ExecutarComando("DELETE FROM LOGIN_USUARIO WHERE ID_USUARIO = " + txtID.Text);
                //dados.ExecutarComando("UPDATE USUARIO SET ATIVO = 0 WHERE ID_USUARIO = " + txtID.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Login: "+ ex.ToString());
                return;
            }
            MessageBox.Show("Login Excluido Com Sucesso, Usuário Inativado!");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

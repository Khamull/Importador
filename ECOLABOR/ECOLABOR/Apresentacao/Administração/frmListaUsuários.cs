using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECOLABOR.Dados;

namespace ECOLABOR.Apresentacao.Administração
{
    public partial class frmListaUsuários : Form
    {
        public string ID;
        csDados dados = new csDados();
        public frmListaUsuários()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListaUsuários_Load(object sender, EventArgs e)
        {
            dtUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtUsuarios.ReadOnly = true;
            carregaGrid();
        }
        public void carregaGrid()
        {
            DataSet user = new DataSet();
            DataView user_view = new DataView();
        user = dados.RetornarDataSet(@"SELECT   A.ID_USUARIO AS ID,
	                                            A.NOME,
	                                            A.SOBRENOME,
	                                            B.LOGIN_USUARIO AS LOGIN,
	                                            B.ATIVO,
	                                            B.SENHA AS SENHA,
	                                            C.DESCRICAO AS 'NÍVEL DE ACESSO'
                                                FROM USUARIO A, LOGIN_USUARIO B, ACESSOS C
                                                WHERE A.ID_USUARIO = B.ID_USUARIO
                                                AND B.ID_ACESSO = C.ID_ACESSO
                                                ORDER BY A.ID_USUARIO");
            user_view = user.Tables[0].DefaultView;
            dtUsuarios.DataSource = user_view;
            if(dtUsuarios.RowCount > 0)
            {
                //dtGVModelos.Columns.Remove("ID_MODELO");
                dtUsuarios.Rows[0].Selected = false;
                dtUsuarios.FirstDisplayedScrollingRowIndex = dtUsuarios.RowCount - 1;
            }
            //dtGVModelos.Columns.Remove("ID");
            //dtUsuarios.ReadOnly = true;
            
        }

        private void dtUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtUsuarios.SelectedRows)
            {
                if (!string.IsNullOrEmpty(row.Cells["ID"].Value.ToString()))
                {
                    ID = row.Cells["ID"].Value.ToString();
                }
            }
        }

    }
}

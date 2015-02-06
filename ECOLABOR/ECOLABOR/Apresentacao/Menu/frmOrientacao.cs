using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECOLABOR.Apresentacao.Menu
{
    public partial class frmOrientacao : Form
    {
        public string orientacao;
        public frmOrientacao()
        {
            InitializeComponent();
        }

        private void frmOrientacao_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                orientacao = "V";
            }
            if (radioButton2.Checked == true)
            {
                orientacao = "H";
            }
            this.Close();
        }
    }
}

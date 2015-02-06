using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOX;
using System.Data.OleDb;
using System.Data;

namespace ECOLABOR.Dados
{
    class csConectaExcel
    {
        OleDbConnection Conn_ = new OleDbConnection();
        //ADODB.Connection Conn = new ADODB.Connection();
        //public ADODB.Connection conectarExcel(string caminho_arquivo)
        //{
        //    Conn.Open("Provider=Microsoft.Jet.OLEDB.12.0; Data Source = " + caminho_arquivo + "; Extended Properties = \"Excel 12.0 Xml;HDR=Yes;IMEX=1\";", "", "", 0);
        //    return Conn;
        //}
        //public void FecharConexao()
        //{
        //    Conn.Close();
        //}
        public OleDbConnection conectarExcel1(string caminho_arquivo)
        {
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                       caminho_arquivo +
                       ";Extended Properties=\""+"Excel 12.0;HDR=YES;IMEX=2;"+"\"";
            Conn_.ConnectionString = constr;
            Conn_.Open();
            return Conn_;
        }
        public void FecharConexao_()
        {
            Conn_.Close();
        }

    }
}

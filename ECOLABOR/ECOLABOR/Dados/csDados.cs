using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ECOLABOR.Dados
{
    class csDados
    {
        //String de Conexão
        string myConnectionString = csConnectionString.retornaConnectionString(System.Environment.CurrentDirectory.ToString() + @"\config.xml");
        #region -- Abrir e Fechar Banco
        private SqlConnection AbrirBanco()
        {
            SqlConnection cn = new SqlConnection(myConnectionString);
            cn.Open();
            return cn;
        }
        public void FecharBanco(SqlConnection cn)
        {
            if (cn.State == ConnectionState.Open)
                cn.Close();
        }
        #endregion
        #region -- Executar comandos
        //Classe para execução de comando
        public void ExecutarComando(string strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }




        #endregion
        #region -- DataSet
        //Classe que retorna um objeto DataSet
        public DataSet RetornarDataSet(string strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                /*  Declarado um dataadapter e um dataset
                    passar o comando para o da (SqlDataAdapter) e
                    carregar o dataset com resultado da busca */
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }
        #endregion

        #region -- DataReader
        //Classe para retornar um DataReader()
        public SqlDataReader RetornarDataReader(string strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }
        #endregion
        #region --Exemplo
        //Classe para retornar um Id Numérico
        /*public int RetornarIdNumerico(string strQuery)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = AbrirBanco();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();
                int codigo;
                if (dr.Read())
                    codigo = Convert.ToInt16(dr[0]) + 1;
                else
                    codigo = 1;
                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharBanco(cn);
            }
        }*/

        public bool StatusConexao(string Servidor, string BD, string user, string passwd)
        {
            //
            string connectionstring = @"Server=" + Servidor + ";Database=" + BD + ";User Id=" + user + "; Password=" + passwd;
            try
            {
                SqlConnection cn_ = new SqlConnection(connectionstring);
                cn_.Open();
                if (cn_.State == ConnectionState.Open)
                {
                    cn_.Close();
                    return true;
                }
                else
                {
                    cn_.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        #endregion
    }
}

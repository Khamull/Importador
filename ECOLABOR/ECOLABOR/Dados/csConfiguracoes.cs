using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ECOLABOR.Negocios.funcoesUteis;

namespace ECOLABOR.Dados
{
    class csConfiguracoes
    {
        #region Atributos
        private string server;
        private string database;
        private string user;
        private string passwd;
        private string fpasswd;
        #endregion
 
        #region Propriedades
        public string Server
        {
            get { return server; }
            set { server = value; }
        }
         
        public string Database
        {
            get { return database; }
            set { database = value; }
        }
         
 
        public string User
        {
            get { return user; }
            set { user = value; }
        }
        public string Passwd
        {
            get { return passwd; }
            set { passwd = value; }
        }
        public string fPasswd
        {
            get { return fpasswd; }
            set { fpasswd = value; }
        }
        #endregion
 
        #region Métodos
        //Aqui ficarão os métodos
        public static List<csConfiguracoes> ListarConfiguracoes(String Arquivo)
        {
            List<csConfiguracoes> configs = new List<csConfiguracoes>();
            XElement xml = XElement.Load(Arquivo);
            foreach (XElement x in xml.Elements())
            {
                csConfiguracoes p = new csConfiguracoes()
                {
                    Server = csCriptografia.Decript(x.Attribute("server").Value),
                    Database = csCriptografia.Decript(x.Attribute("database").Value),
                    User = csCriptografia.Decript(x.Attribute("user").Value),
                    Passwd = csCriptografia.Decript(x.Attribute("passwd").Value),
                    fPasswd = csCriptografia.Decript(x.Attribute("fpasswd").Value)
                };
                configs.Add(p);
            }
            return configs;
        }
        public static void EditarPessoa(csConfiguracoes configs, string Arquivo)
        {
            XElement xml = XElement.Load(Arquivo);
            XElement x = xml.Elements().First();
            x.Attribute("server").SetValue(csCriptografia.Encrypt(configs.Server));
            x.Attribute("database").SetValue(csCriptografia.Encrypt(configs.Database));
            x.Attribute("user").SetValue(csCriptografia.Encrypt(configs.User));
            x.Attribute("passwd").SetValue(csCriptografia.Encrypt(configs.Passwd));
            xml.Save(Arquivo);
        }
        public static void EditarfPasswd(csConfiguracoes configs, string Arquivo)
        {
            XElement xml = XElement.Load(Arquivo);
            XElement x = xml.Elements().First();
            x.Attribute("fpasswd").SetValue(csCriptografia.Encrypt(configs.fPasswd));
            xml.Save(Arquivo);
        }
        //String pasta = System.Environment.Environment.CurrentDirectory.ToString();
        #endregion

        //internal List<csConfiguracoes> ListarConfiguracoes()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

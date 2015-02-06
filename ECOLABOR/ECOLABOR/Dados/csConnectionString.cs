using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Dados
{
    class csConnectionString
    {
        public static string retornaConnectionString(string Arquivo)
        {
            List<csConfiguracoes> configs = new List<csConfiguracoes>();
            string connectionstring = "";
            try
            {
                configs = csConfiguracoes.ListarConfiguracoes(Arquivo);

                if (configs != null)
                {
                    connectionstring = @"Server="+ configs[0].Server.ToString() +";Database="+ configs[0].Database.ToString() +";User Id="+ configs[0].User.ToString() +"; Password="+ configs[0].Passwd.ToString();
                }
            }
            catch{}
            return connectionstring;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Dados
{
    class csSenhaArquivo
    {
        public static string retornaSenhaArquivos(string Arquivo)
        {
            List<csConfiguracoes> configs = new List<csConfiguracoes>();
            string senha = "";
            try
            {
                configs = csConfiguracoes.ListarConfiguracoes(Arquivo);

                if (configs != null)
                {
                    senha = configs[0].fPasswd.ToString();
                }
            }
            catch { }
            return senha;
        }
    }
}

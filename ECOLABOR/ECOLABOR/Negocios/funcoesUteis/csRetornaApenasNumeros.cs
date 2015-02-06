using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Negocios.funcoesUteis
{
    static class csRetornaApenasNumeros
    {
        static public string ExtractNumbers(string expr)
        {
            return string.Join(null, System.Text.RegularExpressions.Regex.Split(expr, "[^\\d]"));
        }
    }
}

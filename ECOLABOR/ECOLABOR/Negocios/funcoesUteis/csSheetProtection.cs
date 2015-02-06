using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
namespace ECOLABOR.Negocios.funcoesUteis
{
    class csSheetProtection
    {
        public void unprotectSheet(Microsoft.Office.Interop.Excel.Application app)
        {
            try
            {
                ((Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet).Unprotect("1234567");
            }
            catch (Exception ex)
            {
                string ex_ = "";
                ex_ = ex.ToString();
            }

        }
    }
}

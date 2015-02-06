using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Negocios.funcoesUteis
{
    class csFechaExcel
    {
        public string FechaExcel(Microsoft.Office.Interop.Excel.Workbook xlWb, Microsoft.Office.Interop.Excel.Application xlApplication)
        {
           
            try
            {
                //webBrowser1.Dispose();
                if (xlWb != null)
                {
                    xlWb.Close(0);
                }
                if (xlApplication.Application != null)
                {
                    xlApplication.Application.Quit();
                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                    foreach (System.Diagnostics.Process p in process)
                    {
                        if (!string.IsNullOrEmpty(p.ProcessName))
                        {
                            try
                            {
                                p.Kill();
                            }
                            catch (Exception exk)
                            {
                                return exk.ToString();
                            }
                        }
                    }
                }
                return "true";//Se chegar até aqui, nada deu errado!
            }
            catch {
                fechaQualquerExcel();
                return "";
            }
        }
        public void fechaQualquerExcel()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch{}
                }
            }
        }
    }
}

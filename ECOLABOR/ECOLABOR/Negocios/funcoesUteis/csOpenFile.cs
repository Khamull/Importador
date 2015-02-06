using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace ECOLABOR.Negocios.funcoesUteis
{
    class csOpenFile
    {
        OpenFileDialog arquivo;
        public string caminho_arquivo;
        //public string nome_arquivo;
        //private Microsoft.Office.Interop.Excel.Application m_XlApplication = null;
        private Workbook m_Workbook = null;

        public string AbreArquivo()
        {
            arquivo = new OpenFileDialog();
            arquivo.Multiselect = false;
            arquivo.Filter = "Arquivos Excel (*.xls; *xlsx)|*.xls; *.xlsx|All files (*.*)|*.*";
            arquivo.RestoreDirectory = true;
            arquivo.ReadOnlyChecked = true;
            arquivo.ShowReadOnly = true;
            if (arquivo.ShowDialog() == DialogResult.OK)
            {
                if (!arquivo.CheckPathExists)
                {
                    MessageBox.Show("Diretório Selecionado Inexistente!");
                    return "";
                }
                if (!arquivo.CheckFileExists)
                {
                    MessageBox.Show("Arquivo Selecionado Inexistente!");
                    return "";
                }
                else
                {
                    caminho_arquivo = arquivo.FileName;
                    //nome_arquivo = System.IO.Path.GetFileName(arquivo.FileName);
                    if (!validarArquivo(caminho_arquivo))
                    {
                        MessageBox.Show("Arquivo Selecionado Inválido! Por favor, selecione um arquivo do MS Excel.");
                        return "";
                    }
                    else
                    {
                         return caminho_arquivo;
                        //webBrowser1.Navigate(caminho_arquivo, false);//Load do Arquivo
                        //EventDel_SelChange = new Excel.DocEvents_SelectionChangeEventHandler(SelChange);
                    }
                }
            }
            return caminho_arquivo;
        }
        public bool validarArquivo(string CaminhoArquivo)
        {
            if (CaminhoArquivo.Substring(CaminhoArquivo.Length - 3) == "xls" || CaminhoArquivo.Substring(CaminhoArquivo.Length - 4) == "xlsx")
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public Workbook retornaWorkBook(string CaminhoArquivo)
        {
            try
            {
                var exApp = new Microsoft.Office.Interop.Excel.Application();
                m_Workbook = exApp.Workbooks.Open(CaminhoArquivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Abrir Excel: " + ex.ToString());
            }
            return m_Workbook;
        }
    }
}

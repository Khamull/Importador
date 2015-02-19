using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECOLABOR.Negocios;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Excel = Microsoft.Office.Interop.Excel;
using ECOLABOR.Dados;
using System.Data.OleDb;
using ECOLABOR.Negocios.funcoesUteis;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace ECOLABOR.Apresentacao.Menu
{
    public partial class frmImportaDados : Form
    {

        public string IT;
        public string MODELO;
        public string id_modelo;
        public string sheetname;
        public string ModeloSelecionado;
        public int tipodeacesso = 0;
        private static DataSet user = new DataSet();
        public string anteriorArquivo;
        //csExcelWrapper ExWrapper = new csExcelWrapper();
        public Workbook Workbook = null;// -> Recebe o WorkBook Aberto na Tela Anterior, Preciso de outro para abrir um novo Excel pra modelo ja cadastrado
        // Contains a reference to the hosting application
        //public List<string> WorkingSheets;
        
        public string sheetname_;
        public Microsoft.Office.Interop.Excel.Application xlApp = new Excel.Application();// = new Microsoft.Office.Interop.Excel.Application();
        public Workbook Origem;
        public Workbook Destino;
        public Range OrigemRange;
        public Range DestinoRange;
        

        

        public string arquivo;
        public string modelo;
        #region Recebe dados do outro form
        public List<string> parametros = new List<string>(); 
        public List<string> coordenadas  = new List<string>();
        //public List<string> indices= new List<string>();
        //public List<bool> tipo = new List<bool>();



        //INTERVALOS
        public List<int> id_range = new List<int>();
        public List<List<int>> colunas = new List<List<int>>();
        public List<string> range_inicial = new List<string>();
        public List<string> range_final = new List<string>();


        //public csIntervalos intervalos = new csIntervalos();
        #endregion
        //List<int> Linhas = new List<int>();
        //List<int> Colunas = new List<int>();
        //System.Data.DataTable dt;
        //Object[,] values;
        //int lastColIgnoreFormulas;
        public frmImportaDados()
        {
            //csFechaExcel fecha = new csFechaExcel();
            //fecha.fechaQualquerExcel();
            InitializeComponent();
        }

        private void frmImportaDados_Load(object sender, EventArgs e)
        {
            validaCampos();
            user = csUserDataSet.USER_;
        }
        public void validaCampos()
        {
            if (string.IsNullOrEmpty(arquivo) && string.IsNullOrEmpty(modelo))
            {
                btnAbrirArquivo.Enabled = true;
                btnSelecionaModelo.Enabled = true;
            }
            else
            {
                btnAbrirArquivo.Enabled = false;
                btnSelecionaModelo.Enabled = false;
                txtCaminhoArquivo.Text = arquivo;
                txtModelo.Text = modelo;
                //dtViewImport.DataSource = ;
                ImportExcelToDataGrid_NovaPlanilha();
                //dtViewImport.Refresh();
            }
        }


        //public void testa()
        //{
        //    long lastCell;
        //    Microsoft.Office.Interop.Excel.Worksheet workSheet;
        //    if (tipodeacesso == 1)
        //    {
        //        workSheet = (Microsoft.Office.Interop.Excel.Worksheet)Workbook.Sheets[sheetname_];
        //    }
        //    else
        //    {
        //        workSheet = (Microsoft.Office.Interop.Excel.Worksheet)Workbook.ActiveSheet;
        //    }
        //    if ((lastCell = retriveLastFillCellValue(colunas[nivel], workSheet)) > 0)
        //    {
        //        Range src = Workbook.Sheets[workSheet.Name].Range(range_inicial[nivel] + ":" + range_final[nivel].Remove(1, range_final[nivel].Length - 1) + lastCell.ToString());
        //    }

        //}

        public void ImportExcelToDataGrid_NovaPlanilha()
        {
            lblIntervalos.Visible = true;
            long lastCell;
            try
            {
                if (Origem == null)
                {
                    Origem = xlApp.Workbooks.Open(anteriorArquivo);
                }
            }
            catch { }
            try
            {
                criaArquivo();
                //Destino = xlApp.Workbooks.Open(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls");
            }
            catch { }
            try
            {
                xlApp.DisplayAlerts = false;
            }
            catch { }
            Microsoft.Office.Interop.Excel.Worksheet workSheet;
            if (tipodeacesso == 1)
            {
                workSheet = (Microsoft.Office.Interop.Excel.Worksheet)Origem.Sheets[sheetname_];
            }
            else
            {
                workSheet = (Microsoft.Office.Interop.Excel.Worksheet)Origem.ActiveSheet;
            }
            
            try
            {
                List<int> col_final = new List<int>();
                lblIntervalos.Text = "Recuperando Dados ";
                progressBar.Minimum = 0;
                progressBar.Visible = true;
                for (int i = 0; i < colunas.Count; i++)
                {
                    for (int j = 0; j < colunas[i].Count; j++)
                    {
                        col_final.Add(colunas[i][j]);
                    }
                }
                    //for (int nivel = 0; nivel < colunas.Count; nivel++)
                    //{

                    //    lblIntervalos.Text = "Processando Intervalo: " + (nivel + 1) + " de " + (colunas.Count);
                    //    lblIntervalos.Visible = true;
                    //    importPgrbar.Maximum = colunas[nivel].Count() + 1;
                    //    importPgrbar.Value = 0;
                        //if ((lastCell = retriveLastFillCellValue(colunas[nivel], workSheet)) > 0)
                        //{

                        //object[,] valueArray=null;
                        //int total = range_inicial.Count;
                        //Range src = workBook.Sheets[workSheet.Name].Range(range_inicial[nivel] + ":" + range_final[nivel].Remove(1, range_final[nivel].Length - 1) + lastCell.ToString());
                        //Range src = Workbook.Sheets[workSheet.Name].Range(range_inicial[nivel] + ":" + range_inicial[nivel].Remove(1, range_inicial[nivel].Length - 1) + lastCell.ToString());
                        //Range src = Workbook.Sheets[workSheet.Name].Range(range_inicial[nivel] + ":" + range_final[nivel].Remove(1, range_final[nivel].Length - 1) + lastCell.ToString());
                        //Range src = retriveLastFillCellValue(colunas[nivel], workSheet, coordenadas);
                        retriveLastFillCellValue(col_final, workSheet, coordenadas);
                        //criar_tabela_temporaria(Workbook, src);
                        //importPgrbar.Value++;
                        //src_ = wb2.Sheets[sheets.Name].UsedRange();
                        //excelCell = wb2.Sheets[sheets.Name].UsedRange(); 
                        //values = (Object[,])excelCell.Cells.Value2;
                        //wb1.Close();
                        //Range src = workBook.Sheets[workSheet.Name].Range(coordenadas[0].ToString() + ":" + coordenadas[total - 1].Remove(1, coordenadas[total - 1].Length - 1) + lastCell.ToString());
                        //try
                        //{
                        //     valueArray= (object[,])src_.get_Value(XlRangeValueDataType.xlRangeValueDefault);
                        //}
                        //catch { }
                        //for (int i = 1; i < src.Rows.Count; i++)
                        //{
                        //    dtViewImport.Rows.Add(src.Rows[i].Value2);
                        //}
                        //try
                        //{
                        //    dtViewImport.DataSource = values;
                        //}
                        //catch(Exception ex)
                        //{
                        //    MessageBox.Show(ex.ToString());
                        //}
                        //System.Data.DataTable dt = ConvertRangeToDataTable();
                        //if (dt != null) { dtViewImport.DataSource = dt; }
                        //wb2.Close();
                        //    for (int row = 2; row <= src.Rows.Count; row++)//Daqui pra frente, como adicionar Linhas a uma coluna unica
                        //    {
                        //        for (int column = 1; column <= src.Columns.Count; column++)
                        //        {
                        //            draftCell = new DataGridViewTextBoxCell();
                        //            if (values[row, column] == null)
                        //            {
                        //                draftCell.Value = "";
                        //                draftRow.Cells.Add(draftCell);
                        //            }
                        //            else
                        //            {
                        //                draftCell.Value = values[row, column].ToString();
                        //                draftRow.Cells.Add(draftCell);
                        //            }
                        //        }
                        //        //DTR.Add(draftRow);
                        //        dtViewImport.Rows.Add(draftRow);
                        //        dtViewImport.Refresh();
                        //        draftRow = new DataGridViewRow();
                        //    }
                        //    //for (int i = 0; i < DTR.Count; i++)
                        //    //{
                        //    //    dtViewImport.Rows.Add(DTR[i]);
                        //    //}
                        // }

                    //}
            
                try
                {
                    //Destino.Close();
                    xlApp.Quit();
                }
                catch (Exception ex)
                {
                    //ex.ToString();
                }

                //src_ = wb2.Sheets[sheets.Name].UsedRange();

                //excelCell = wb2.Sheets[sheets.Name].UsedRange();

                //int vertical = 0;
                //int horizontal = 0;
                //Array myValues = (Array)excelCell.Cells.Value;
                ////wb2.Close();
                ////horizontal = myValues.GetLength(1);
                //for (int i = 1; i <= myValues.GetLength(1); i++)
                //{
                //    if(myValues.GetValue(1,i) != null)
                //    {
                //        horizontal++;
                //    }
                //}

                //vertical = myValues.GetLength(0);
                ////horizontal = myValues.GetLength(1);

                

                // must start with index = 1
                // get header information
                //try
                //{
                //    for (int i = 1; i <= horizontal; i++)
                //    {
                //        if (myValues.GetValue(1, i) != null)
                //        {
                //            dt.Columns.Add(new DataColumn(Convert.ToString(myValues.GetValue(1, i))));
                //        }
                //    }
                //}
                //catch(Exception ex) {
                //    MessageBox.Show(ex.ToString());
                //}

                // Get the row information
                //for (int a = 1; a <= vertical; a++)
                //{
                //    object[] poop = new object[horizontal];
                //    for (int b = 1; b <= horizontal; b++)
                //    {
                //        if (myValues.GetValue(a, b) == null)
                //        {
                //            //poop[b - 1] = "";
                //        }
                //        else
                //        {
                //            poop[b - 1] = myValues.GetValue(a, b);
                //        }

                //        DataRow row = dt.NewRow();
                //        row.ItemArray = poop;
                //        dt.Rows.Add(row);
                //    }
                //}
                // assign table to default data grid view
                //dtViewImport.DataSource = dt;
                //dtViewImport.DataBind();



                int rcount = 0;
                Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbook Destino = app.Workbooks.Open(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls");
                Excel.Worksheet worksheet = Destino.ActiveSheet;

                rcount = worksheet.UsedRange.Rows.Count;

                //int i = 0;

                //Initializing Columns
                dtViewImport.ColumnCount = worksheet.UsedRange.Columns.Count;
                for (int x = 0; x < dtViewImport.ColumnCount; x++)
                {
                    dtViewImport.Columns[x].Name = worksheet.Cells[1,x+1].Text;
                    for (int i = 0; i < rcount; i++)
                    {
                        //dataGridView1.Rows[i].Cells["Column1"].Value = worksheet.Cells[i + 1, 1].Value;
                        //dataGridView1.Rows[i].Cells["Column2"].Value = worksheet.Cells[i + 1, 2].Value;
                        //dtViewImport.Rows.Add(worksheet.Cells[i + 1, x+1].Text, worksheet.Cells[i + 1, x+1].Text);
                        dtViewImport.Rows.Add();
                        String teste = worksheet.Cells[i+2, x+1].Text;
                        dtViewImport.Rows[i].Cells[x].Value = teste.Replace(".", ",");
                    }
                   
                }
                //for (int i = 0; i < rcount; i++)
                //{
                //    //dataGridView1.Rows[i].Cells["Column1"].Value = worksheet.Cells[i + 1, 1].Value;
                //    //dataGridView1.Rows[i].Cells["Column2"].Value = worksheet.Cells[i + 1, 2].Value;
                //    dtViewImport.Rows.Add(worksheet.Cells[i + 1, 1].Text, worksheet.Cells[i + 1, 2].Text);
                //    //dtViewImport.Rows.Add(worksheet.Cells[i, x].Text, worksheet.Cells[i, x].Text);
                //}
                




                /*System.Data.DataTable dt = new System.Data.DataTable();
                csConectaExcel excel_datagrid = new csConectaExcel();
                OleDbConnection conn = new OleDbConnection();
                conn = excel_datagrid.conectarExcel1(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls");
                OleDbDataAdapter myAdapter = new OleDbDataAdapter("SELECT * FROM [aux$]", conn);
                System.Data.DataTable dt_ = new System.Data.DataTable();
                myAdapter.Fill(dt_);

                dtViewImport.DataSource = dt_;
                //dtViewImport.Columns.Remove
                OleDbDataAdapter myAdapter_ = new OleDbDataAdapter("DELETE FROM [aux$]", conn);
                excel_datagrid.FecharConexao_();
                //dtViewImport.DataSource = excelCell.Cells;
                dtViewImport.Refresh();
                //System.Data.DataTable dt = ConvertRangeToDataTable();
                //if (dt != null) { dtViewImport.DataSource = dt; }
                //wb2.Close();
                //values = (Object[,])excelCell.Cells.Value2;
                //wb2.Close();*/

                //Excel.Application xlApp_ = new Excel.Application();
                //Destino = xlApp_.Workbooks.Open(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls");
                Destino.ActiveSheet.Cells.ClearContents();
                Destino.Save();
                Destino.Close();
                xlApp.Quit();
                progressBar.Visible = false;
                lblIntervalos.Visible = false;
                DeletaArquivo();
                
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblIntervalos.Visible = false;
                MessageBox.Show(ex.ToString());
            }
            //Range src = workSheet.Range("A1:B3");
            //dt.Columns.Add("Column1"); //your column name
            //dt.Columns.Add("Column2");
            //dt.Columns.Add("Column3");

            //try
            //{
            //    DataRow row;
            //    for (int C = 0; C < Colunas.Count; C++)
            //    {
            //        for (int L = 0; L < Linhas.Count; L++)
            //        {
            //            for (int l = Linhas[L]; l < workSheet.Rows.Count; l++)
            //            {
            //                string teste = workSheet.Cells[l, Colunas[C]].Value2.ToString();
            //            }
            //        }
            //    }
            //}
            //catch { }
            //while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null)
            //{
            //    row = dt.NewRow();
            //    row[0] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2);
            //    row[1] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
            //    row[2] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 3]).Value2);
            //    index++;
            //    rowIndex = 2 + index;
            //    dt.Rows.Add(row);
            //}
            //m_XlApplication.Quit();
            //m_XlApplication.Workbooks.Close();
            //GC.Collect();
            //return dt;


        }
        
      

        public void retriveLastFillCellValue(List<int> Colunas, Microsoft.Office.Interop.Excel.Worksheet ws, List<string> rgInic)//retorna a maior Linha com dados!
        {
            string culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();//"en-GB";
            CultureInfo ci = new CultureInfo(culture);
            //Origem = xlApp.Workbooks.Open(anteriorArquivo);
            bool systemseparators = xlApp.UseSystemSeparators;
            if (xlApp.UseSystemSeparators == false)
            {

                xlApp.UseSystemSeparators = true;

            }
            //xlApp.Visible = true;
            //src.Formula = "=ROW()";
            int j = 0;
            //long UltimaMaiorCelula = 0;
            long lastRow;
            //long auxiliar = 0;
            long fullRow = ws.Rows.Count;
            //for (int i = 0; i < Colunas.Count; i++),
            //wb2 = excel.Workbooks.Open(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls");
            //var sheets = (Microsoft.Office.Interop.Excel.Worksheet)wb2.Worksheets.get_Item(1);//Corrigido para Evitar Excpetion!
            List<string> teste_ = new List<string>();
            for (int i = 0; i < rgInic.Count; i++)
            {
                Range src = null;
                try
                {
                    lblIntervalos.Text = ("Recueprando Dados: Passo " + (i+1) + " de " + rgInic.Count);
                    lastRow = ws.Cells[fullRow, Colunas[i]].End(Microsoft.Office.Interop.Excel.XlDirection.xlUp).Row;
                    src = (Origem.Sheets[ws.Name].Range(rgInic[i] + ":" + rgInic[i].Remove(1, rgInic[i].Length - 1) + lastRow.ToString()));
                    criar_tabela_temporaria(Origem, src);
                }
                catch { }
                    //object[,] valueArray = Workbook.Sheets[ws.Name].Range(rgInic[i] + ":" + rgInic[i].Remove(1, rgInic[i].Length - 1) + lastRow.ToString()).Value2;
                    //src = (Origem.Sheets[ws.Name].Range(rgInic[i] + ":" + rgInic[i].Remove(1, rgInic[i].Length - 1) + lastRow.ToString()));
                
            //    if (auxiliar < lastRow)
            //    {
            //        UltimaMaiorCelula = lastRow;
            //    }
            }
            //return UltimaMaiorCelula;
            //return src;

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }


        public void criar_tabela_temporaria(Microsoft.Office.Interop.Excel.Workbook workBook, Range src/*, object[,] valueArray*/)
        {
            try
            {
                //Range dest;
                try
                {
                   //xlApp.Visible = true;
                }
                catch { }
                //Microsoft.Office.Interop.Excel.Worksheet sheets = new Microsoft.Office.Interop.Excel.Worksheet();
                //try
                //{
                //    excel.DisplayAlerts = false;
                //}
                //catch { }
                
                //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                ////Workbook wb1 = excel.Workbooks.Open("c:\\temp\\me.xlsx");
                //Workbook wb2 = excel.Workbooks.Open(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls");
                //try
                //{
                //    wb2 = excel.Workbooks.Open(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls");
                //}
                //catch { }
                //Microsoft.Office.Interop.Excel.Worksheet sheets = new Microsoft.Office.Interop.Excel.Worksheet();
                var sheets = (Microsoft.Office.Interop.Excel.Worksheet)Destino.Worksheets.get_Item(1);//Corrigido para Evitar Excpetion!
                //dest = Destino.Sheets[1].Range(columnName(sheets)[0]);
                progressBar.Value = 0;
                progressBar.Maximum = src.Count;
                dtViewImport.ColumnCount++;
                for (int h = 1; h <= 1; h++)
                {
                    int col = lastColl(sheets);
                    for (int r = 1; r <= src.Count; r++)
                    {
                        //teste_.Add();
                        sheets.Cells[r, col + 1] = src.Cells[r, h].Text.ToString().Trim();
                        progressBar.Value++;
                        //dtViewImport.Rows.Add(src.Cells[r, h].Text, src.Cells[r, col+1].Text);
                    }
                }
                //excel.Selection.EntireColumn.Hidden = false;
                //Microsoft.Office.Interop.Excel.Range used = xlWs.UsedRange;
                //Microsoft.Office.Interop.Excel.Range excelCell = xlWs.UsedRange;
                //Microsoft.Office.Interop.Excel.Range oRng = xlWs.get_Range("A1").SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);
                //int lLastCol = oRng.Column;
                //int colCount = excelCell.Columns.Count;
                //int firstEmptyCols = lLastCol - colCount;
                //int lastColIncludeFormulas = wb2.ActiveSheet.UsedRange.Columns.Count;
                //int lascolIgnoreFormulas = x
                //try
                //{
                //    excel.Visible = true;
                //}
                //catch { }
                //src.Copy(Type.Missing);
                //dest.PasteSpecial(XlPasteType.xlPasteValuesAndNumberFormats, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                //string lastColIgnoreFormulas;
                //try
                //{
                //    lastColIgnoreFormulas = sheets.Cells.Find(
                //     "*",
                //     System.Reflection.Missing.Value,
                //     System.Reflection.Missing.Value,
                //     System.Reflection.Missing.Value,
                //     Microsoft.Office.Interop.Excel.XlSearchOrder.xlByColumns,
                //     Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious,
                //     false,
                //     System.Reflection.Missing.Value,
                //     System.Reflection.Missing.Value).Columns.Name;
                //}
                //catch 
                //{
                //    lastColIgnoreFormulas = "0";
                //}
               
                //dest = wb2.Sheets[1].Range();
                //int usedCol = used.Columns.Count;
                //src.ClearFormats();
                //src.NumberFormat = src.NumberFormat = "Text";
                //excel.Range[0].NumberFormat = "Text";
                //Microsoft.Office.Interop.Excel.Range ThisRange = sheets.UsedRange;
                //ThisRange.NumberFormat = "@";  
                //object[,] valueArray = (object[,])src.get_Value(XlRangeValueDataType.xlRangeValueDefault);
                //object[,] valueArray_ = (object[,])src.get_Value(XlRangeValueDataType.xlRangeValueDefault);

                //Excel.Range sourceRange = firstWorksheet.get_Range("A1", "J10");
                //for (int column = 1; column <= src.Columns.Count; column++)
                //{
                //    if (valueArray[1, column] != null && !string.IsNullOrEmpty(Convert.ToString(valueArray[1, column])) && !string.IsNullOrWhiteSpace(Convert.ToString(valueArray[1, column])))
                //    {
                //        sheets.Cells[1, lastColIgnoreFormulas + column] = CaracteresEspeciais.RemoveCaracteresEspeciais(Convert.ToString(valueArray[1, column]).Trim().Replace(" ", "_").ToUpper(), false, true);
                //        for (int row = 2; row <= src.Rows.Count; row++)
                //        {
                            
                //            if(!string.IsNullOrEmpty(Convert.ToString(valueArray[row, column])))
                //            {
                //                sheets.Cells[row, lastColIgnoreFormulas + column] = "'"+Convert.ToString(valueArray[row, column]);
                //            }
                //        }
                //    }
                //    Destino.Save();
                //    try
                //    {
                //        importPgrbar.Value++;
                //    }
                //    catch { }
                //}
                
                //src = workBook.ActiveSheet.Range("A1:B3");
                //int lastCol = wb2.ActiveSheet.Range("a1").End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight).Column;
                //int lastRow = wb2.Sheets["Sheet1"].Cells(65536, lastCol).End(Microsoft.Office.Interop.Excel.XlDirection.xlUp).Row;
                //Range dest = wb2.Sheets["Sheet1"].Range("A10");//"a1", wb2.Sheets["Sheet1"].Cells(lastRow, lastCol));
                //src.Copy(dest);
                try
                {
                    Destino.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao Salvar Tabela Temporária: " + ex.ToString());
                }
                try
                {
                    //Destino.Close();
                }
                catch { }
                //src_ = wb2.Sheets[sheets.Name].UsedRange();
                ////wb1.Close();
                //wb2.Close();
            }
            catch {}
            //excel.Quit();
        }
        public int lastColl(Microsoft.Office.Interop.Excel.Worksheet sheets)
        {
            int lastColIgnoreFormulas = 0;
            try
            {
                lastColIgnoreFormulas = sheets.Cells.Find(
                 "*",
                 System.Reflection.Missing.Value,
                 System.Reflection.Missing.Value,
                 System.Reflection.Missing.Value,
                 Microsoft.Office.Interop.Excel.XlSearchOrder.xlByColumns,
                 Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious,
                 false,
                 System.Reflection.Missing.Value,
                 System.Reflection.Missing.Value).Column;
            }
            catch
            {
                lastColIgnoreFormulas = 0;
            }
            return lastColIgnoreFormulas;
        }

        public List<string> columnName(Microsoft.Office.Interop.Excel.Worksheet wsDestino)
        {

            //int lastCol = wbDestino.Sheets[1].Cells.End(Microsoft.Office.Interop.Excel.XlDirection.xlToRight).Column;
            int columnCount = wsDestino.Columns.Count;
            List<string> columnNames = new List<string>();
            for (int c = 1; c < columnCount; c++)
            {
                string columnName = wsDestino.Cells[1].Columns[c].Address;
                if (wsDestino.Cells[1, c].Value2 == null && wsDestino.Cells[2, c].Value2 == null)
                {
                    columnNames.Add(columnName.Replace("$", ""));
                    return columnNames;
                }
            }
            return columnNames;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (dtViewImport.RowCount <= 0)
            {
                MessageBox.Show("Selecione um Modelo e um Arquivo!");
            }
            else
            {
                if (tipodeacesso == 0)
                {
                    csDados modelos = new csDados();
                    if (modeloexitente(modelos, txtModelo.Text))
                    {
                        id_modelo = Modelo(modelos);
                        //SELECT MAX(ID_MODELO) +1 FROM MODELOS
                        List<string> ParamModelos = new List<string>();

                        //CONECTA NA BASE DE DADOS E RETORNA O ULTIMO MODELO INSERIDO.

                        #region Insere na tabela Modelo, para se recuperado mais adiante quando formos Importar de um modelo ja Salvo.
                        if (!string.IsNullOrWhiteSpace(id_modelo))
                        {
                            csStrings str_comandos = new csStrings();
                            ParamModelos.Add(id_modelo);
                            ParamModelos.Add(CaracteresEspeciais.RemoveCaracteresEspeciais(txtModelo.Text.ToUpper(), false, true));
                            ParamModelos.Add(sheetname);
                            ParamModelos.Add(IT);
                            ParamModelos.Add(user.Tables[0].Rows[0]["ID_ACESSO"].ToString());//Inserir dados do Usuário aqui! Precias ver muito como faz isso!
                            string strSql = str_comandos.criaString(3, ParamModelos);
                            try
                            {
                                modelos.ExecutarComando(strSql);
                            }
                            catch { return; }
                            strSql = "";
                        #endregion
                            #region ------------- Carrega Parametros Para Tabela Parametros
                            List<string> parametros = new List<string>();

                            string id_parametros = "";
                            try
                            {
                                DataSet ID_PARAMETRO = new DataSet();
                                ID_PARAMETRO = modelos.RetornarDataSet("SELECT MAX(ID_PARAMETROS)+1 AS ID_PARAMETROS FROM PARAMETROS");
                                id_parametros = ID_PARAMETRO.Tables[0].Rows[0]["ID_PARAMETROS"].ToString();
                                if (string.IsNullOrEmpty(id_parametros))
                                {
                                    id_parametros = "0";
                                }
                            }
                            catch { return; }
                            parametros.Add(id_parametros);
                            parametros.Add(id_modelo);
                            parametros.Add(Colunas_DtView());
                            parametros.Add(ListaDeColunas());
                            parametros.Add(Ranges_IncialStr());
                            parametros.Add(Ranges_FinalStr());
                            parametros.Add(coordenadas_final());

                            try
                            {
                                modelos.ExecutarComando(str_comandos.criaString(4, parametros));
                            }
                            catch { return; }

                            //string param = str_comandos.criaString(2, parametros);
                            csCriaTabelas tabelas = new csCriaTabelas();
                            string CriarTabela = tabelas.ComandoSQL(str_comandos.criaString(2, ListaDeColunasParaTabela()), CaracteresEspeciais.RemoveCaracteresEspeciais(txtModelo.Text.ToUpper(), false, true), Convert.ToInt32(id_modelo));
                            try
                            {
                                modelos.ExecutarComando(CriarTabela);
                            }
                            catch { return; }
                            //SE CHEGOU AQUI  O PROCESSO TODO DEU CERTO
                            MessageBox.Show("Nova Tabela: " + txtModelo.Text.Trim().Replace(" ", "_") + " Criada com Sucesso!");
                            try
                            {

                                lblIntervalos.Visible = true;
                                lblIntervalos.Text = "Importando Dados para a Base";
                                insereDados();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erro na Inserção de Dados: " + ex.ToString());
                                return;
                            }
                            MessageBox.Show("Dados Inseridos Com Sucesso!");
                            progressBar.Visible = false;
                            lblIntervalos.Visible = false;
                            progressBar.Value = 0;
                            //limparObjectos();
                            this.Close();
                            #endregion
                        }


                    }
                    else
                    {
                        MessageBox.Show("Modelo " + txtModelo.Text + " Já Cadastrado! Favor Inserir Outro Nome ou Utilizar a Tela Apropriada Para Importar Dados");
                    }
                }
                else
                {
                    try
                    {
                        lblIntervalos.Visible = true;
                        lblIntervalos.Text = "Importando Dados para a Base";
                        insereDados();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro na Inserção de Dados: " + ex.ToString());
                        return;
                    }
                    MessageBox.Show("Dados Inseridos Com Sucesso!");
                    progressBar.Visible = false;
                    lblIntervalos.Visible = false;
                    progressBar.Value = 0;
                    limparObjectos();
                }
            }
        
        }
        public string Colunas_DtView()//Retorna Somente Coleção de Colunas que Existam no DTGridVw
        {
            string retorno = "";
            foreach (DataGridViewColumn coluna in dtViewImport.Columns)
            {

                //for (int i = 0; i < parametros.Count; i++)
                //{
                //    if (coluna.Name == parametros[i])
                //    {
                        retorno += "#"+coluna.Name;
                    //}
                //}
            }
            return retorno;
        }
        public string ListaDeColunas()
        {
            string retorno = "";
            for (int col1 = 0; col1 < colunas.Count; col1++)
            {
                retorno += "#";
                for (int col2 = 0; col2 < colunas[col1].Count; col2++)
                {
                    retorno += colunas[col1][col2].ToString() + ";";
                }
            }
            return retorno;

        }

        public List<string> ListaDeColunasParaTabela()//Continuar, Remover todos os Ascentos e passar para UPPER e Adicionar Coordenadas a base(não remover nada da lógica atual)
        {
            List<string> retorno = new List<string>();
            foreach (DataGridViewColumn coluna in dtViewImport.Columns)
            {
                retorno.Add(CaracteresEspeciais.RemoveCaracteresEspeciais(coluna.Name.Trim().Replace(" ", "_").ToUpper().ToString(), false, true));
                //retorno.Add(coluna.Name.Trim().Replace("/", "").Replace("%", "").Replace(" ", "_"));
            }
            
            //foreach (DataGridViewColumn coluna in dtViewImport.Columns)
            //{
            //    for (int i = 0; i < retorno.Count; i++)
            //    {
            //        if ("F" + i == coluna.Name)
            //        {
            //            retorno.Remove("F" + i);
            //        }
            //    }
            //}
            return retorno;
        }
        public string Ranges_IncialStr()
        {
            string retorno = "";
            for (int i = 0; i < range_inicial.Count; i++)
            {
                retorno += range_inicial[i] + "#";
            }
                return retorno;
        }

        public string Ranges_FinalStr()
        {
            string retorno = "";
            for (int i = 0; i < range_final.Count; i++)
            {
                retorno += range_final[i] + "#";
            }
            return retorno;
        }

        public string coordenadas_final()
        {
            string retorno = "";
            for (int i = 0; i < coordenadas.Count; i++)
            {
                retorno += coordenadas[i] + "#";
            }
            return retorno;
        }
        //private System.Data.DataTable ConvertRangeToDataTable()
        //{
        //    try
        //    {
        //        System.Data.DataTable dt = new System.Data.DataTable();
        //        int ColCount = excelCell.Columns.Count;
        //        int RowCount = excelCell.Rows.Count;

        //        for (int i = 0; i < ColCount; i++)
        //        {
        //            DataColumn dc = new DataColumn();
        //            dt.Columns.Add(dc);
        //        }
        //        for (int i = 1; i <= RowCount; i++)
        //        {
        //            DataRow dr = dt.NewRow();
        //            for (int j = 1; j <= ColCount; j++)
        //            {
        //                if (string.IsNullOrEmpty(Convert.ToString(excelCell.Cells[i, j].Value)))
        //                {
        //                    dr[j - 1] = "";
        //                }
        //                else
        //                {
        //                    dr[j - 1] = Convert.ToString(excelCell.Cells[i, j].Value);
        //                }
                        
        //            }
        //            dt.Rows.Add(dr);
        //        }
        //        return dt;
        //    }
        //    catch { return null; }
        //}
        #region Recupera ID_MAX do Modelo
        private string Modelo(csDados modelos)
        {
            string id_modelo = "";
            //SELECT MAX(ID_MODELO) +1 FROM MODELOS
            List<string> ParamModelos = new List<string>();

            //CONMECTA NA BASE DE DADOS E RETORNA O ULTIMO MODELO INSERIDO.
            try
            {
                DataSet ID_MODELO = new DataSet();
                ID_MODELO = modelos.RetornarDataSet("SELECT MAX(ID_MODELO) +1 AS ID_MODELO FROM MODELOS");
                id_modelo = ID_MODELO.Tables[0].Rows[0]["ID_MODELO"].ToString();
                if (string.IsNullOrEmpty(id_modelo))
                {
                    id_modelo = "0";
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao Recuperar ID Modelo MAX: " + ex.ToString());}

            return id_modelo;

        }
        #endregion
        #region Verifica se Nome do Modelo ja Existe
        private bool modeloexitente(csDados modelos, string nome_modelo)//Recebe uma instancia ja aberta da classe de dados!
        {
            bool ok = false;
            DataSet ID_MODELO = new DataSet();
            ID_MODELO = modelos.RetornarDataSet("SELECT * FROM MODELOS WHERE NOME_MODELO = " + "'"+ txtModelo.Text +"'");
            if (ID_MODELO.Tables[0].Rows.Count > 0)
            {
                ok = false;
            }
            else
            {
               ok = true;
            }
            return ok;
        }
        #endregion
        #region insere na base de dados
        public void insereDados(/*csDados dados, List<string> coluas, string NomeTabela*/)
        {
            csDados dados = new csDados();
            csStrings parametrosecolunas = new csStrings();
            //List<List<string>> values = new List<List<string>>(dtViewImport.Rows.Count);
            //List<string> val = new List<string>();
            //SqlConnection con = new SqlConnection(csConnectionString.retornaConnectionString(System.Environment.CurrentDirectory.ToString() + @"\config.xml"));

            lblIntervalos.Text = "Importando para Base";
            progressBar.Minimum = 0;
            progressBar.Maximum = dtViewImport.Rows.Count;
            progressBar.Value = 0;
            progressBar.Visible = true;
            List<string> parametros_insercao = new List<string>();
            try
            {
                for (int i = 0; i < dtViewImport.Rows.Count; i++)
                {
                    for (int j = 0; j < dtViewImport.Rows[i].Cells.Count; j++)
                    {
                        //sc.Parameters.AddWithValue("@" + ListaDeColunasParaTabela()[j], dtViewImport.Rows[i].Cells[j].Value);
                        parametros_insercao.Add(dtViewImport.Rows[i].Cells[j].Value.ToString());
                    }
                    string teste = @"INSERT INTO " + CaracteresEspeciais.RemoveCaracteresEspeciais(txtModelo.Text.ToUpper(), false, true) + " (ID_MODELO," + parametrosecolunas.criaString(5, ListaDeColunasParaTabela()) + " DATA_CRIACAO)" +
                           "VALUES ('" + id_modelo + "'," + parametrosecolunas.criaString(6, parametros_insercao) + " GETDATE())";
                    dados.ExecutarComando(teste);
                    teste = "";
                    parametros_insercao.Clear();
                    try
                    {
                        progressBar.Value++;
                    }
                    catch { }
                }
            }
            catch { }
        }
        #endregion

        private void btnSelecionaModelo_Click(object sender, EventArgs e)
        {
            frmListaModelos frmModelos = new frmListaModelos();
            //frmModelos.TopLevel = false;
            //frmModelos.MdiParent = this.MdiParent;
            frmModelos.estado = 0;
            frmModelos.ShowDialog();
            txtModelo.Text = frmModelos.MODELO;
        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            csOpenFile OpenFile = new csOpenFile();
            string arquivo = OpenFile.AbreArquivo();
            if (!string.IsNullOrEmpty(arquivo))
            {
                txtCaminhoArquivo.Text = arquivo;
                Origem = OpenFile.retornaWorkBook(arquivo);
                SelectDadosDoModelo();
            }
            else
            {
                txtCaminhoArquivo.Text = "";
            }
        }

        public void SelectDadosDoModelo()
        {
            if (!string.IsNullOrEmpty(txtModelo.Text))
            {
                csDados dados = new csDados();
                DataSet data = new DataSet();
                data = dados.RetornarDataSet(@"SELECT
                                        B.ID_MODELO,
	                                    A.NOME_MODELO, 
	                                    B.PARAMETRO,
	                                    B.COLUNAS,
	                                    B.RANGE_INICIAL,
	                                    B.RANGE_FINAL,
                                        B.COORDENADAS_FINAL,
                                        A.NOME_WORKSHEET
                                        FROM MODELOS A, PARAMETROS B
                                        WHERE A.ID_MODELO = B.ID_MODELO
                                        AND   A.NOME_MODELO = '" + txtModelo.Text + "'");

                id_modelo = Convert.ToString(data.Tables[0].Rows[0]["ID_MODELO"].ToString());
                string stringtosplit = Convert.ToString(data.Tables[0].Rows[0]["PARAMETRO"].ToString());
                string[] s = stringtosplit.Split('#');
                foreach (string str in s)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        parametros.Add(str);
                    }
                }
                stringtosplit = Convert.ToString(data.Tables[0].Rows[0]["RANGE_INICIAL"].ToString());
                s = stringtosplit.Split('#');
                foreach (string str in s)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        range_inicial.Add(str);
                    }
                }
                stringtosplit = Convert.ToString(data.Tables[0].Rows[0]["RANGE_FINAL"].ToString());
                s = stringtosplit.Split('#');
                foreach (string str in s)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        range_final.Add(str);
                    }
                }
                stringtosplit = Convert.ToString(data.Tables[0].Rows[0]["COORDENADAS_FINAL"].ToString());
                s = stringtosplit.Split('#');
                foreach (string str in s)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        coordenadas.Add(str);
                    }
                }

                stringtosplit = Convert.ToString(data.Tables[0].Rows[0]["COLUNAS"].ToString());
                s = stringtosplit.Split('#');
                List<string> teste = new List<string>();
                foreach (string str in s)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        teste.Add(str);
                    }
                }
                List<int> col;
                string[] teste_;
                foreach (string str in teste)
                {
                    teste_ = str.Split(';');
                    col = new List<int>();
                    foreach (string str_ in teste_)
                    {

                        if (!string.IsNullOrEmpty(str_))
                        {
                            col.Add(Convert.ToInt32(str_));
                        }
                    }
                    colunas.Add(col);
                }
                sheetname_ = Convert.ToString(data.Tables[0].Rows[0]["NOME_WORKSHEET"].ToString());
                ImportExcelToDataGrid_NovaPlanilha();
                //public List<int> id_range = new List<int>();
                //public List<List<int>> colunas = new List<List<int>>();
                //public List<string> range_inicial = new List<string>();
                //public List<string> range_final = new List<string>();
            }
            else
            {
                MessageBox.Show("Selecione um Modelo!");
            }
        }

        private void frmImportaDados_FormClosing(object sender, FormClosingEventArgs e)
        {
            csFechaExcel fecha = new csFechaExcel();
            colunas.Clear();
            coordenadas.Clear();
            parametros.Clear();
            txtModelo.Clear();
            dtViewImport.DataSource = null;
            dtViewImport.Refresh();
            txtCaminhoArquivo.Clear();
            fecha.fechaQualquerExcel();
        }
        public void DeletaArquivo()
        {
            if (System.IO.File.Exists(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls"))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls");
                }
                catch (System.IO.IOException e)
                {
                    //Console.WriteLine(e.Message);
                    //return;
                }
            }
        }
        public void criaArquivo()
        {
            DeletaArquivo();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = "aux";
            xlWorkBook.SaveAs(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls", Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
 Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Destino = xlApp.Workbooks.Open(System.Environment.CurrentDirectory.ToString() + @"\aux_sip.xls");
        }
        public void limparObjectos()
        {
            try
            {
                colunas.Clear();
                coordenadas.Clear();
                parametros.Clear();
                txtModelo.Clear();
                if (dtViewImport.DataSource != null)
                {
                    dtViewImport.DataSource = null;
                }
                dtViewImport.Rows.Clear();
                dtViewImport.Refresh();
                txtCaminhoArquivo.Clear();
                IT = "";
                MODELO = "";
                id_modelo = "";
                sheetname = "";
                ModeloSelecionado = "";
                //csExcelWrapper ExWrapper = new csExcelWrapper();
                //Workbook = null;// -> Recebe o WorkBook Aberto na Tela Anterior, Preciso de outro para abrir um novo Excel pra modelo ja cadastrado
                // Contains a reference to the hosting application
                //public List<string> WorkingSheets;
                try
                {
                    //Origem = null;
                    //Destino = null;
                    //OrigemRange = null;
                    //DestinoRange = null;
                    //try
                    //{
                    //    xlApp.Quit();
                    //}
                    //catch { }
                }
                catch { }
                arquivo = "";
                modelo = "";
                //INTERVALOS
                id_range.Clear();
                colunas.Clear();
                range_inicial.Clear();
                range_final.Clear();
            }
            catch { }
        }

    }

}

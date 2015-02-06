using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ADOX;
using System.Data.OleDb;
using ECOLABOR.Dados;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using ECOLABOR.Negocios;
using ECOLABOR.Negocios.funcoesUteis;
using System.Threading;
using ECOLABOR.Apresentacao.Menu;

namespace ECOLABOR.Apresentacao
{
    public partial class frmCadastroNovaPlanilha : Form
    {
        OpenFileDialog arquivo;
        public string caminho_arquivo;
        public string nome_arquivo;

        //Declarando objetos para manipular Excel
        Excel.Workbook xlWb;
        Excel.Worksheet xlWs;

        //Delegando Eventos para o C#
        //Excel.AppEvents_WorkbookBeforeCloseEventHandler EventDel_BeforeBookClose;//controla Eventos antes de fechar e limpeza do app
        Excel.DocEvents_SelectionChangeEventHandler EventDel_SelChange;//Controla Auteração de Seleção de Célula

        // Contains a reference to the hosting application
        private Microsoft.Office.Interop.Excel.Application m_XlApplication = null;

        // Contains a reference to the active workbook
        private Workbook m_Workbook = null;
        public Workbook AuxWorkbook;
        public List<string> WorkingSheets;

        //Controle de Intervalos
        private List<int> id_range = new List<int>();
        private List<List<int>> colunas = new List<List<int>>();
        private List<string> range_inicial = new List<string>();
        private List<string> range_final = new List<string>();



        public int count_id;
        //Lista de Celulas Selecionadas a serem exibidas na DataGrid
        BindingList<csCells> bindinglist = new BindingList<csCells>();//Lista que carrega o datagrid
        BindingSource source;
        public int index;
        int cont = 0; //controla exibição de mensagens

        csExcelWrapper ExcelWrapper = new csExcelWrapper();

        public frmCadastroNovaPlanilha()
        {
            InitializeComponent();
        }

        private void frmCadastroNovaPlanilha_Load(object sender, EventArgs e)
        {
            LimpaAoMudar(0);
            dtViewConfig.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //DEFINE TIPO DE SELE^ÇÃO
        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            if (txtCaminhoArquivo.Text == "")
            {
                DialogResult result = AvisaFechamento();
                switch (result)
                {
                    case DialogResult.OK:
                        {
                            csFechaExcel fecha = new csFechaExcel();
                            fecha.fechaQualquerExcel();

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
                                    return;
                                }
                                if (!arquivo.CheckFileExists)
                                {
                                    MessageBox.Show("Arquivo Selecionado Inexistente!");
                                    return;
                                }
                                else
                                {
                                    caminho_arquivo = arquivo.FileName;
                                    nome_arquivo = System.IO.Path.GetFileName(arquivo.FileName);
                                    if (!validarArquivo(caminho_arquivo))
                                    {
                                        MessageBox.Show("Arquivo Selecionado Inválido! Por favor, selecione um arquivo do MS Excel.");
                                        return;
                                    }
                                    else
                                    {
                                        txtCaminhoArquivo.Text = caminho_arquivo;
                                        webBrowser1.Navigate(caminho_arquivo, false);//Load do Arquivo
                                        EventDel_SelChange = new Excel.DocEvents_SelectionChangeEventHandler(SelChange);
                                    }
                                }
                            }

                            break;
                        }
                    case DialogResult.Cancel:
                        {

                            break;
                        }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Deseja abrir outra planilha? AVISO: Todo trabalho atual Será perdido!", "Trocar de Planilhas", MessageBoxButtons.OKCancel);
                switch (result)
                {
                    case DialogResult.OK:
                        {
                            csFechaExcel fecha = new csFechaExcel();
                            fecha.FechaExcel(m_Workbook, m_XlApplication);
                            txtCaminhoArquivo.Text = "";
                            treeView1.Nodes.Clear();
                            btnAbrirArquivo_Click(null, null);
                            break;
                        }
                    case DialogResult.Cancel:
                        {

                            break;
                        }
                }

            }
        }

        #region---------- VALIDAÇÂO DE ARQUIVO(Se é ou Não XLS ou XLSX) ---------------------
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
        #endregion
        #region ---------- TREEVIEW - CARREGA e CUIDA DE VERIFICAR QUAL FOI SELECIOANDA --------------
        public void carregaTreeView(string NomeArquivo, List<string> planilhas)
        {
            treeView1.Nodes.Add(NomeArquivo);

            for (int i = 0; i < planilhas.Count; i++)
            {
                treeView1.Nodes[0].Nodes.Add(planilhas[i].ToString().Replace("$", ""));
            }
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string sheetName = treeView1.SelectedNode.Text;
            if (sheetName != nome_arquivo)
            {
                DialogResult result = MessageBox.Show("Deseja Mudar de Planilha Ativa?\n AVISO: Todo trabalho atual será perdido!", "Mudança de Planilha Ativa", MessageBoxButtons.OKCancel);
                switch (result)
                {
                    case DialogResult.OK:
                        {

                            xlWs = (Excel.Worksheet)xlWb.Sheets[sheetName];
                            //m_XlApplication.Selection.EntireColumn.Hidden = false;
                            //xlWs.SelectionChange += EventDel_SelChange;
                            foreach (Excel.Worksheet sheet in xlWb.Worksheets)
                            {
                                sheet.SelectionChange -= EventDel_SelChange;
                            }
                            foreach (Excel.Worksheet sheet in xlWb.Worksheets)
                            {
                                sheet.SelectionChange += EventDel_SelChange;
                            }
                            xlWs.Activate();
                            LimpaAoMudar(0);
                            break;
                        }
                    case DialogResult.Cancel:
                        {

                            break;
                        }
                }

            }
        }
        #endregion
        #region ---------- Carrega a primeira vez o excel -- Não mais utilizado
        //public List<string> RecuperaTabelasPlanilha(string caminhoArquivo)
        //{
        //    csConectaExcel conecta = new csConectaExcel();
        //    List<string> strTables = new List<string>();
        //    Catalog oCatlog = new Catalog();
        //    ADOX.Table oTable = new ADOX.Table();
        //    //ADODB.Connection oConn = new ADODB.Connection();
        //    //oConn.Open("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + caminhoArquivo + "; Extended Properties = \"Excel 8.0;HDR=Yes;IMEX=1\";", "", "", 0);
        //    //oConn.Open("Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + caminhoArquivo + "; Extended Properties = \"Excel 12.0 Xml;HDR=Yes;IMEX=1\";", "", "", 0);
        //    oCatlog.ActiveConnection = conecta.conectarExcel(caminhoArquivo);//oConn;
        //    if (oCatlog.Tables.Count > 0)
        //    {
        //        int item = 0;
        //        foreach (ADOX.Table tab in oCatlog.Tables)
        //        {
        //            if (tab.Type == "TABLE")
        //            {
        //                strTables.Add(tab.Name);
        //                item++;
        //            }
        //        }
        //    }
        //    conecta.FecharConexao();
        //    return strTables;
        //}
        #endregion
        #region ---------- Eventos para execução do Delegate e Recuperação de dados
        private void SelChange(Excel.Range Target)
        {


            //Excel.Range usedrange = m_XlApplication.ActiveSheet.UsedRange() as Excel.Range;
            Excel.Range range = m_XlApplication.Selection as Excel.Range;
            //usedrange.EntireColumn.Hidden = false; 
            //string MensagemComposta = "";
            /*Debug.WriteLine("Delegate: You Changed Cells " +
               Target.get_Address(Missing.Value, Missing.Value,
               Excel.XlReferenceStyle.xlA1, Missing.Value, Missing.Value) +
               " on " + Target.Worksheet.Name);*/
            if (Target.Count == 1)
            {
                int Row = Target.Row;
                int Colum = Target.Column;
                List<int> colunas_list;
                Excel.Range oRng = (Excel.Range)xlWs.Cells[Row, Colum];
                if (oRng.get_Value() != null)
                {
                    try
                    {

                        //oRng.get_Value().ToString();
                        DialogResult result = MessageBox.Show("Deseja Aciocionar o Campo " + oRng.get_Value() + " como Parametro?", "Adicionar Parametros", MessageBoxButtons.OKCancel);
                        switch (result)
                        {
                            case DialogResult.OK:
                                {
                                    colunas_list = new List<int>();
                                    colunas_list.Add(Colum);
                                    id_range.Add(count_id);
                                    count_id++;
                                    colunas.Add(colunas_list);
                                    range_inicial.Add(Target.get_Address(Missing.Value, Missing.Value, Excel.XlReferenceStyle.xlA1, Missing.Value, Missing.Value).Replace("$", ""));
                                    range_final.Add(Target.get_Address(Missing.Value, Missing.Value, Excel.XlReferenceStyle.xlA1, Missing.Value, Missing.Value).Replace("$", ""));
                                    //adicionaCamposDG(oRng.get_Value().ToString(), Target.get_Address(Missing.Value, Missing.Value, Excel.XlReferenceStyle.xlA1, Missing.Value, Missing.Value).Replace("$", ""), Colum + "x" + Row, false);
                                    adicionaCamposDG(oRng.get_Value().ToString(), Target.get_Address(Missing.Value, Missing.Value, Excel.XlReferenceStyle.xlA1, Missing.Value, Missing.Value).Replace("$", ""), false);
                                    //frmOrientacao frmOri = new frmOrientacao();
                                    //frmOri.ShowDialog();
                                    //orientacao.Add(frmOri.orientacao);

                                    break;
                                }
                            case DialogResult.Cancel:
                                {

                                    break;
                                }
                        }
                    }
                    catch { }
                }

            }
            else//Delegar Métodos para Melhorar Código
            {

                //m_XlApplication.Selection.EntireColumn.Hidden = false;
                List<string> ListaParametros = new List<string>();
                List<string> Adress = new List<string>();
                List<int> colum_ = new List<int>();
                List<int> row_ = new List<int>();
                List<string> indice = new List<string>();
                //object myValue = (Excel.Range)Target.get_Value(Type.Missing);
                //List<Range> teste = new List<Range>();
                //teste = Target.get_Value(Type.Missing);
                object[,] myArrayOfvalues = Target.get_Value(Type.Missing);
                //Carrega Endereço das Células
                //try
                //{

                //    string test_ = "";
                //    //for (int y = 0; y <= range.Areas.Count; y++)//Iidex das Areas(Celulas?)
                //    //{
                //    try
                //    {
                //        foreach (Excel.Range areas in Target.Areas)//OK, ele ignora as colunas com dados Ocultos
                //        {
                //            for (int j = 0; j < areas.Cells.Count; j++)
                //            {
                //                if (!areas.Cells[j + 1].EntireColumn.Hidden && !areas.Cells[j + 1].EntireRow.Hidden)
                //                {
                //                    try
                //                    {

                //                        if (!string.IsNullOrEmpty(Convert.ToString(areas.Cells[j + 1].Address))/*!areas.Cells[j + 1].MergeCells*/)
                //                        {
                //                            try
                //                            {
                //                                Adress.Add(areas.Cells[j + 1].Address.ToString());//Continuar Daqui
                //                            }
                //                            catch { }
                //                            try
                //                            {
                //                                ListaParametros.Add(areas.Cells[j + 1].Value.ToString());
                //                            }
                //                            catch { }
                //                        }
                //                    }
                //                    catch { }
                //                }
                //            }
                //        }
                //    }
                //    catch { }
                //    //for (int j = 1; j <= range.Areas[1].Cells.Count; j++)
                //    //{
                //    //    if (!range.EntireRow.Hidden)
                //    //    {
                //    //        Adress.Add(range.Areas[1].Cells[j].Address.ToString());//LIstas e Arrays no Excel começam com 1 e não com 0
                //    //    }
                //    //    //Target.get_Address(Missing.Value, Missing.Value,Excel.XlReferenceStyle.xlA1, Missing.Value, Missing.Value));
                //    //}
                //    //}
                //    foreach (Excel.Range curCol in Target.Columns)
                //    {
                //        //if (curCol.Value2 != null)
                //        //{
                //        //As far as each column only has one row, each column can be associated with a cell
                //        if (!curCol.EntireColumn.Hidden)
                //        {
                //            colum_.Add(curCol.Column);
                //        }
                //        //}
                //    }
                //    foreach (Excel.Range curRow in Target.Rows)
                //    {
                //        //if (curRow.Value2 != null)
                //        //{
                //        //As far as each column only has one row, each column can be associated with a cell
                //        if (!curRow.EntireRow.Hidden)
                //        {
                //            row_.Add(curRow.Row);
                //        }
                //        //}
                //    }
                //    for (int i = 0; i < row_.Count; i++)
                //    {
                //        for (int j = 0; j < colum_.Count; j++)
                //        {
                //            indice.Add(row_[i] + " x " + colum_[j]);
                //        }
                //    }

                //    //for (int row = 1; row <= myArrayOfvalues.GetLength(0); row++)
                //    //{
                //    //    for (int column = 1; column <= myArrayOfvalues.GetLength(1); column++)
                //    //    {
                //    //        //if (MensagemComposta == "")
                //    //        //{
                //    //        //    MensagemComposta = Convert.ToString(myArrayOfvalues[row, column]);
                //    //        //Carrega Listas para Método DG

                //    //        if (!string.IsNullOrEmpty(Convert.ToString(myArrayOfvalues[row, column])))
                //    //        {
                //    //            ListaParametros.Add(Convert.ToString(myArrayOfvalues[row, column]));
                //    //        }

                //    //        //colum_.Add(range.Column);
                //    //        //row_.Add(range.Row);
                //    //        //}
                //    //        //else
                //    //        //{
                //    //        //    //MessageBox.Show(myArray[row, column].ToString());
                //    //        //    //MensagemComposta += "\n" + myArrayOfvalues[row, column];
                //    //        //    //Carrega Listas para Método DG
                //    //        //    //ListaParametros.Add(Convert.ToString(myArrayOfvalues[row, column]));
                //    //        //    //Adress.Add(Target.get_Address(Missing.Value, Missing.Value,Excel.XlReferenceStyle.xlA1, Missing.Value, Missing.Value));
                //    //        //    //colum_.Add(range.Column);
                //    //        //    //row_.Add(range.Row);
                //    //        //}
                //    //    }
                //    //}
                //    //DialogResult result = MessageBox.Show("Deseja Aciocionar os Campos: \n" + MensagemComposta + "\ncomo Parametros?", "Adicionar Parametros", MessageBoxButtons.OKCancel);
                //    if (cont == 1)
                //    {
                //        cont = 0;
                //        return;
                //    }
                DialogResult result = MessageBox.Show("Deseja Aciocionar os Campos Selecionados como Parametros?", "Adicionar Parametros", MessageBoxButtons.OKCancel);
                cont = 1;
                switch (result)
                {
                    case DialogResult.OK:
                        {
                            carregaValores(Target, Adress, ListaParametros, colum_, row_, indice);
                            id_range.Add(count_id);
                            colunas.Add(colum_);
                            count_id++;
                            //adicionaCamposListaDG(ListaParametros, Adress , colum_, row_);
                            for (int i = 0; i < ListaParametros.Count; i++)
                            {
                                //adicionaCamposDG(ListaParametros[i], Adress[i].Replace("$", ""), /*indice[i],*/ true);
                                adicionaCamposDG(ListaParametros[i], Adress[i].Replace("$", ""), true);
                                if (i == 0)
                                {
                                    range_inicial.Add(Adress[i].Replace("$", ""));
                                }
                                if (i == ListaParametros.Count - 1)
                                {
                                    range_final.Add(Adress[i].Replace("$", ""));
                                }
                            }
                            //frmOrientacao frmOri = new frmOrientacao();
                            //frmOri.ShowDialog();
                            //orientacao.Add(frmOri.orientacao);
                            break;
                        }
                    case DialogResult.Cancel:
                        {

                            break;
                        }
                }
                //MessageBox.Show(MensagemComposta);
            }
            //string strData = oRng.Value.ToString();
            cont = 0;
        }
        #endregion
        #region ---------- Carrega WorkBook e Excel Aplication After WebBrowser receber o excel(navigated)

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // Creation of the workbook object

            if ((m_Workbook = ExcelWrapper.RetrieveWorkbook(caminho_arquivo)) == null)
            {
                return;
            }
            else
            {

                // Create the Excel.Application
                m_XlApplication = (Microsoft.Office.Interop.Excel.Application)m_Workbook.Application;
                setaUI(m_XlApplication);
                xlWb = m_XlApplication.ActiveWorkbook;
                carregaTreeView(nome_arquivo, carregaWs(xlWb));
                //definir Worksheet Ativa no Treview
                //xlWs.SelectionChange += EventDel_SelChange;
                xlWs = xlWb.ActiveSheet;
                //m_XlApplication.ActiveSheet.Columns("C:C").Select();
                xlWs.SelectionChange += EventDel_SelChange;
                //Range usedRng1 = (Range)xlWs.UsedRange;
                try
                {
                    // xlWs.UsedRange.EntireColumn.Hidden = false;
                }
                catch { }
            }
        }
        #endregion
        #region ---------- Seta o Que não vai aparecer no Excel
        public void setaUI(Excel.Application App)
        {
            //App.Visible = true;
            try
            {

                try
                {
                    m_XlApplication.DisplayFormulaBar = false;
                }
                catch { }
                try
                {
                    m_XlApplication.DisplayStatusBar = false;
                }
                catch { }
                try
                {
                    m_XlApplication.ActiveWindow.DisplayWorkbookTabs = false;
                }
                catch { }
                //m_XlApplication.ScreenUpdating = false;

                try
                {
                    App.DisplayAlerts = false;
                    // m_XlApplication.ActiveSheet.Selection.EntireColumn.Hidden = false;
                }
                catch { }
                try
                {
                    string senha = csSenhaArquivo.retornaSenhaArquivos(System.Environment.CurrentDirectory.ToString() + @"\config.xml");
                    m_XlApplication.ActiveWorkbook.Unprotect(senha);
                    //unprotected.unprotectSheet(App);
                }
                catch { }
            }
            catch { }
        }
        #endregion
        #region ---------- Carrega quais programas estão ativos e "binda" o excel no sistema
        [DllImport("ole32.dll")]
        static extern int GetRunningObjectTable
            (uint reserved, out IRunningObjectTable pprot);
        [DllImport("ole32.dll")]
        static extern int CreateBindCtx(uint reserved, out IBindCtx pctx);

        public Workbook RetrieveWorkbook(string xlfile)
        {
            Workbook Wb;
            csExcelWrapper ExcelWrapper = new csExcelWrapper();
            try
            {
                Wb = ExcelWrapper.RetrieveWorkbook(xlfile);
            }
            catch
            {
                MessageBox.Show("Erro ao Abrir o Arquivo!");
                return null;
            }

            //IRunningObjectTable prot = null;
            //IEnumMoniker pmonkenum = null;
            //try
            //{
            //    IntPtr pfetched = IntPtr.Zero;
            //    // Query the running object table (ROT)
            //    if (GetRunningObjectTable(0, out prot) != 0 || prot == null) return null;
            //    prot.EnumRunning(out pmonkenum); pmonkenum.Reset();
            //    IMoniker[] monikers = new IMoniker[1];
            //    while (pmonkenum.Next(1, monikers, pfetched) == 0)
            //    {
            //        IBindCtx pctx; string filepathname;
            //        CreateBindCtx(0, out pctx);
            //        // Get the name of the file
            //        monikers[0].GetDisplayName(pctx, null, out filepathname);
            //        // Clean up
            //        Marshal.ReleaseComObject(pctx);
            //        // Search for the workbook
            //        if (filepathname.IndexOf(xlfile) != -1)
            //        {
            //            object roval;
            //            // Get a handle on the workbook
            //            prot.GetObject(monikers[0], out roval);
            //            return roval as Workbook;
            //        }
            //    }
            //}
            //catch
            //{
            //    return null;
            //}
            //finally
            //{
            //    // Clean up
            //    if (prot != null) Marshal.ReleaseComObject(prot);
            //    if (pmonkenum != null) Marshal.ReleaseComObject(pmonkenum);
            //}
            return Wb;
        }
        #endregion
        #region -- Fecha o Excel quando o Form é fechado

        private void frmCadastroNovaPlanilha_FormClosing(object sender, FormClosingEventArgs e)
        {
            csFechaExcel fecha = new csFechaExcel();
            string fechamento = fecha.FechaExcel(xlWb, m_XlApplication);
        }
        #endregion
        public void adicionaCamposDG(string conteudoCampo, string coordenadas, /*string indice*/ bool tipo)
        {

            try
            {
                csCells Cells = new csCells();
                if (!string.IsNullOrEmpty(conteudoCampo))
                {
                    Cells.Parametro = conteudoCampo;
                    Cells.Coordenda = coordenadas;
                }
                //Cells.Indice = indice;
                //Cells.Intervalo = tipo;

                dtViewConfig.Invoke((MethodInvoker)delegate
                {
                    //Thread.Sleep(200);
                    try
                    {
                        //auxiliar.Add(Cells);
                        bindinglist.Add(Cells);

                        //source = new BindingSource(bindinglist, null); //Trounouse desnecessário
                        carregaGrid(bindinglist);
                    }
                    catch (Exception ex)
                    {
                        //Aqui tratamos todos os eventos quando o estado da Bindinglist se perde.
                        List<csCells> teste = new List<csCells>();
                        teste = bindinglist.ToList<csCells>();
                        LimpaAoMudar(1);
                        //Reinsere os dados após exception
                        //bindinglist = auxiliar;
                        foreach (csCells cell in teste)
                        {
                            if (!string.IsNullOrEmpty(cell.Coordenda) && !string.IsNullOrEmpty(cell.Parametro)/* && !string.IsNullOrEmpty(cell.Indice)*/)
                            {
                                bindinglist.Add(cell);
                            }
                        }
                        //source = new BindingSource(bindinglist, null);
                        carregaGrid(bindinglist);
                    }

                });
            }
            catch { }
        }

        //public void adicionaCamposListaDG(List<string> conteudoCampo, List<string> coordenadas, List<int> coluna, List<int> Linha)
        //{
        //    dtViewConfig.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    csListOfCells Cells = new csListOfCells();
        //    for (int i = 0; i < conteudoCampo.Count; i++)
        //    {
        //        Cells.Cells_(conteudoCampo[i], coordenadas[i], coluna[i] + "x" + Linha[i]);
        //        //Cells.Parametro.Add(conteudoCampo[i]);
        //        //Cells.Coordenda.Add(coordenadas[i]);
        //        //Cells.Indice.Add(coluna[i] + "x" + Linha[i]);
        //    }
        //    dtViewConfig.Invoke((MethodInvoker)delegate
        //    {
        //        Thread.Sleep(1000);
        //        bindinglist_.Add(Cells);
        //        source = new BindingSource(bindinglist_, null);
        //        dtViewConfig.DataSource = source;
        //        dtViewConfig.Refresh();

        //    });
        //}



        public bool validaCampos()
        {
            if (!string.IsNullOrEmpty(txtCaminhoArquivo.Text) && !string.IsNullOrEmpty(txtModelo.Text))
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        //private void dtViewConfig_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) //nunca executou
        //{
        //    if (index > 0)
        //    {
        //        source.RemoveAt(index);
        //        index = 0;
        //    }
        //}

        private void dtViewConfig_SelectionChanged(object sender, EventArgs e)
        {

            index = dtViewConfig.SelectedRows[0].Index;
        }
        public void LimpaAoMudar(int tipo)
        {
            if (tipo == 0)
            {
                dtViewConfig.Invoke((MethodInvoker)delegate
               {
                   bindinglist.Clear();
                   //source = new BindingSource(bindinglist, null);
                   carregaGrid(bindinglist);
                   range_final.Clear();
                   range_inicial.Clear();
                   id_range.Clear();
                   colunas.Clear();
                   //dtViewConfig.DataSource = source;

               });
            }
            else if (tipo == 1)
            {
                dtViewConfig.Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        bindinglist.Clear();
                        if (source.Count > 0)
                        {
                            source.Clear();
                        }

                    }
                    catch { }
                });
            }
        }
        public DialogResult AvisaFechamento()
        {
            DialogResult result = MessageBox.Show("AVISO: Todas as Janelas do MS Excel serão fechadas, salve seu trabalho e clique em OK para continuar!", "AVISO DE FECHAMENTO", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            return result;
        }

        private void carregaGrid(BindingList<csCells> src)
        {
            dtViewConfig.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //DEFINE TIPO DE SELE^ÇÃO
            dtViewConfig.DataSource = src;
            dtViewConfig.Refresh();
            if (this.dtViewConfig.Rows.Count > 0)
            {
                //this.dtViewConfig.Rows[this.dtViewConfig.RowCount -1].Selected = true;
                //this.dtViewConfig.FirstDisplayedScrollingRowIndex = (this.dtViewConfig.Rows.Count - 1);
                //this.dtViewConfig.Rows[this.dtViewConfig.Rows.Count - 1].Selected = true;
                //dtViewConfig.CurrentCell = dtViewConfig.Rows[dtViewConfig.RowCount - 1].Cells[0];
                dtViewConfig.FirstDisplayedScrollingRowIndex = dtViewConfig.RowCount - 1;//Mantem o foco no ultimo elemento cadastrado
            }
        }
        public List<string> carregaWs(Excel.Workbook wb)
        {
            List<string> ws_lista = new List<string>();
            try
            {

                if (wb != null)
                {
                    foreach (Worksheet worksheet in wb.Worksheets)
                    {
                        ws_lista.Add(worksheet.Name);
                    }
                }
            }
            catch { }
            return ws_lista;
        }

        private void dtViewConfig_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            csCells cells_edited = new csCells();
            List<csCells> cells = new List<csCells>();
            List<int> col_aux;
            foreach (DataGridViewCell cell in dtViewConfig.SelectedRows[0].Cells)
            {
                if (cell.Value != null)
                {
                    if (cell.ColumnIndex == 0)
                    {
                        cells_edited.Parametro = cell.Value.ToString();
                    }
                    if (cell.ColumnIndex == 1)
                    {
                        //cells_edited.Coordenda = cell.Value.ToString();
                        //range_inicial.Add(cell.Value.ToString());
                        //range_final.Add(cell.Value.ToString());
                    }
                    if (cell.ColumnIndex == 2)
                    {

                        //col_aux = new List<int>();
                        ////cells_edited.Indice = cell.Value.ToString();
                        //string[] words = cell.Value.ToString().ToUpper().Split('X');
                        //col_aux.Add(Convert.ToInt32(words[0]));
                        //colunas.Add(col_aux);

                    }
                    //if (cell.ColumnIndex == 3)
                    //{
                    //    cells_edited.Intervalo = (bool)cell.Value;
                    //}
                }
                if (!string.IsNullOrEmpty(cells_edited.Parametro) && !string.IsNullOrEmpty(cells_edited.Coordenda) /*&& !string.IsNullOrEmpty(cells_edited.Indice) && cells_edited.Intervalo != null*/)
                {

                    //dtViewConfig.NotifyCurrentCellDirty(true);
                    //cells.Add(cells_edited);
                    bindinglist.Add(cells_edited);
                    bindinglist.RemoveAt(bindinglist.Count - 1);
                    carregaGrid(bindinglist);
                    dtViewConfig.Refresh();
                    id_range.Add(count_id);
                    count_id++;


                    //foreach (DataGridViewCell cell_ in dtViewConfig.SelectedRows[0].Cells)
                    //{
                    //    if (cell_.Value != null)
                    //    {
                    //        cell_.Value = null;
                    //    }
                    //    //dtViewConfig.Rows.RemoveAt(dtViewConfig.SelectedRows[0].Index);
                    //}
                    //                    int contador = 0;
                    //for (int i = 0; i < dtViewConfig.Rows.Count; i++)
                    //{
                    //    for (int c = 0; c < dtViewConfig.Rows[i].Cells.Count; c++)
                    //    {
                    //        if (dtViewConfig.Rows[i].Cells[c].Value == null)
                    //        {
                    //            if (contador == 2)
                    //            {
                    //                dtViewConfig.Rows.RemoveAt(i);
                    //                //if (string.IsNullOrEmpty(bindinglist[i].Parametro) && string.IsNullOrEmpty(bindinglist[i].Coordenda) && string.IsNullOrEmpty(bindinglist[i].Indice))
                    //                //    bindinglist.RemoveAt(i);
                    //            }
                    //            contador++;
                    //        }
                    //    }
                    //}
                    //dtViewConfig.Refresh();
                }
            }


        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                frmImportaDados frmImport = new frmImportaDados();
                frmImport.TopLevel = false;
                frmImport.MdiParent = this.MdiParent;
                frmImport.arquivo = caminho_arquivo;
                frmImport.modelo = txtModelo.Text;
                for (int i = 0; i < bindinglist.Count; i++)
                {
                    frmImport.parametros.Add(bindinglist[i].Parametro);
                    frmImport.coordenadas.Add(bindinglist[i].Coordenda);
                    //frmImport.indices.Add(bindinglist[i].Indice);
                    //frmImport.tipo.Add(bindinglist[i].Intervalo);//se é ou não parte de um intervalo
                }
                frmImport.colunas = colunas;
                frmImport.range_final = range_final;
                frmImport.range_inicial = range_inicial;
                frmImport.id_range = id_range;
                //frmImport.Workbook = xlWb;
                frmImport.MODELO = txtModelo.Text;
                frmImport.IT = txtIT.Text;
                frmImport.sheetname = xlWs.Name;
                frmImport.anteriorArquivo = txtCaminhoArquivo.Text;
                frmImport.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Preencha todos os campos e selecione os Parametros!");
            }
        }
        public void carregaValores(Excel.Range Target, List<string> Adress, List<string> ListaParametros, List<int> colum_, List<int> row_, List<string> indice)
        {
            string test_ = "";
            //for (int y = 0; y <= range.Areas.Count; y++)//Iidex das Areas(Celulas?)
            //{
            try
            {
                foreach (Excel.Range areas in Target.Areas)//OK, ele ignora as colunas com dados Ocultos
                {
                    for (int j = 0; j < areas.Cells.Count; j++)
                    {
                        if (!areas.Cells[j + 1].EntireColumn.Hidden && !areas.Cells[j + 1].EntireRow.Hidden)
                        {
                            try
                            {

                                if (!string.IsNullOrEmpty(Convert.ToString(areas.Cells[j + 1].Address))/*!areas.Cells[j + 1].MergeCells*/)
                                {
                                    try
                                    {
                                        Adress.Add(areas.Cells[j + 1].Address.ToString());//Continuar Daqui
                                    }
                                    catch { }
                                    try
                                    {
                                        ListaParametros.Add(areas.Cells[j + 1].Value.ToString());
                                    }
                                    catch { }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }
            //for (int j = 1; j <= range.Areas[1].Cells.Count; j++)
            //{
            //    if (!range.EntireRow.Hidden)
            //    {
            //        Adress.Add(range.Areas[1].Cells[j].Address.ToString());//LIstas e Arrays no Excel começam com 1 e não com 0
            //    }
            //    //Target.get_Address(Missing.Value, Missing.Value,Excel.XlReferenceStyle.xlA1, Missing.Value, Missing.Value));
            //}
            //}
            foreach (Excel.Range curCol in Target.Columns)
            {
                //if (curCol.Value2 != null)
                //{
                //As far as each column only has one row, each column can be associated with a cell
                if (!curCol.EntireColumn.Hidden)
                {
                    colum_.Add(curCol.Column);
                }
                //}
            }
            foreach (Excel.Range curRow in Target.Rows)
            {
                //if (curRow.Value2 != null)
                //{
                //As far as each column only has one row, each column can be associated with a cell
                if (!curRow.EntireRow.Hidden)
                {
                    row_.Add(curRow.Row);
                }
                //}
            }
            for (int i = 0; i < row_.Count; i++)
            {
                for (int j = 0; j < colum_.Count; j++)
                {
                    indice.Add(row_[i] + " x " + colum_[j]);
                }
            }
        }
    }
}

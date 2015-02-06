using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Negocios
{
    public class csIntervalos
    {
        /// <summary>
        /// Contorla Onde Começa e Onde Termina os Ranges de Intervalos Selecionados
        /// </summary>
        private List<int> id_range = new List<int>();
        private List<List<int>> colunas = new List<List<int>>();
        private List<string> range_inicial = new List<string>();
        private List<string> range_final = new List<string>();


        public List<int> Id_Range
        {
            get { return id_range; }
            set { id_range = value; }
        }
        public List<List<int>> Colunas_Intervalo
        {
            get { return colunas; }
            set { colunas = value; }
        }

        public List<string> Range_Inicial
        {
            get { return range_inicial; }
            set { range_inicial = value; }
        }

        public List<string> Range_Final
        {
            get { return range_final; }
            set { range_final = value; }
        }

        //public void ID_RANGE(int id)
        //{
        //   Id_Range.Add(id);
        //}

        //public List<List<int>> COLUNAS(List<int> col)
        //{
        //    Colunas_Intervalo.Add(col);
        //    return Colunas_Intervalo;
        //}

        //public void RANGE_INICIAL(string ir)
        //{
        //    Range_Inicial.Add(ir);
        //}
        //public void RANGE_FINAL(string fr)
        //{
        //    Range_Final.Add(fr);
        //}
    }
}

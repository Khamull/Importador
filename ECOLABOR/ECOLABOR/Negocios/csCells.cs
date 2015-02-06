using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Negocios
{
    class csCells
    {
        private string _Parametro;
        private string _Coordenda;
        //private bool _Vertical;
        //private bool _Horizontal;
        //private bool _IntervaloOuCelula;//Recebe 0 se for intervalo e 1 se for celula unica

        public string Parametro 
         {
             get { return _Parametro; }
             set { _Parametro = value; }
         }

         public string Coordenda
         {
             get { return _Coordenda; }
             set { _Coordenda = value; }
         }

         //public bool Vertical
         //{
         //    get { return _Vertical; }
         //    set { _Vertical = value; }
         //}
         //public bool Horizontal
         //{
         //    get { return _Horizontal; }
         //    set { _Horizontal = value; }
         //}
   }
}

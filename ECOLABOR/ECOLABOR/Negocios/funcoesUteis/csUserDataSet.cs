using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ECOLABOR.Negocios.funcoesUteis
{
    class csUserDataSet
    {
        /// <summary>
        /// Mantem DATASET do Usuário Logado
        /// </summary>
        /// Usabilidade:
        ///  - USER_.get()
        ///  - USER_.set()
        ///  </summary>
        private static DataSet USER;
        public static  DataSet USER_
        {
            get { return USER; }
            set { USER = value; }
        }


    }
}

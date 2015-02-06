using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Dados
{
    class csCriaTabelas
    {
        csDados insert = new csDados();
        /// <summary>
        /// Recebe Parametros para criação da tabela e suas respectivas colunas
        /// </summary>
        /// <param name="Parametros"></param>
        //public string CriaColunas(List<string> Parametros)
        //{
        //    string campos = "";
        //    for (int i = 0; i< Parametros.Count; i++)
        //    {
        //        campos += Parametros[i] + " VARCHAR (255), ";
        //    }
        //    return campos;
        //}
        public string ComandoSQL(string Parametros, string NomeModelo, int ID_MODELO)
        {
            string ComandoSQL = "";
            ComandoSQL = @"CREATE TABLE " + NomeModelo.Replace(" ", "_") + @" 
            (   
	            ID_MODELO		INT	   NOT NULL, " 
                + Parametros +
                @"DATA_CRIACAO	DATETIME NOT NULL,
                TIME_LAST_EVENT	TIMESTAMP NOT NULL
                CONSTRAINT fk_MODELO" + (ID_MODELO + 1).ToString() + @" FOREIGN KEY (ID_MODELO)
	            REFERENCES MODELOS(ID_MODELO)
            )"; 
            return ComandoSQL;
        }
        public bool insert_(string ComandoSql)
        {
            bool teste = false;
            insert.ExecutarComando(ComandoSql);


            return teste;
        }

        /*
	        ID_PARAMETROS	INT		      NOT NULL PRIMARY KEY,
	        ID_MODELO		INT			  NOT NULL,
	        PARAMETRO		VARCHAR (100) NOT NULL,
	        COORDENADAS		VARCHAR (100) NOT NULL,--COORDENADA@COORDENADA@COORDENADA
	        COLUNAS			VARCHAR (1000)NOT NULL,--INDICE#COLUNA@COLUNA@COLUNA
	        RANGE_INICIAL	VARCHAR (1000)NOT NULL,--RANGE1@Range2@Range3@Range4
	        RANGE_FINAL		VARCHAR	(1000)NOT NULL--RANGE1@Range2@Range3@Range4
	        CONSTRAINT fk_MODELO FOREIGN KEY (ID_MODELO)
	        REFERENCES MODELOS(ID_MODELO)
         */
    }
}

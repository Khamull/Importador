using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Negocios.funcoesUteis
{
    class csStrings
    {
        public string criaString(int index, List<string> parametros)
        {
            string retorno = "";
            int countList = parametros.Count;

            if (index == 1)
            {

               retorno = (@"SELECT * FROM LOGIN_USUARIO WHERE LOGIN_USUARIO= '" + parametros[0].ToString() + "' AND SENHA= '" + parametros[1].ToString() + "' AND ATIVO = 1");
            }
            if (index == 2)//Colunas da Abela Nova
            {
                for (int i = 0; i < countList; i++)
                {

                    retorno += parametros[i] + " VARCHAR (255), ";
                }
            }
            if (index == 3)
            {
                string ID_MODELO="";
	            string NOME_MODELO="";		
	            string NOME_WORKSHEET="";
	            string IT="";			
	            string ID_USUARIO="";
                string DATA_CRIACAO = "";

                for (int i = 0; i < parametros.Count; i++)
                {
                    if (i == 0)//ID MODELO
                    {
                        ID_MODELO = parametros[i];
                    }
                    if (i == 1)//NOME_MODELO
                    {
                        NOME_MODELO = parametros[i];
                    }
                    if(i == 2)//NOME_WORKSHEET
                    {
                        NOME_WORKSHEET = parametros[i];
                    }
                    if (i == 3)//IT
                    {
                        IT = parametros[i];
                    }
                    if (i == 4)//ID USUÀRIO LOGADO
                    {
                        ID_USUARIO = parametros[i];
                    }
                }
                DATA_CRIACAO = System.DateTime.Today.ToLongDateString().ToString();

                retorno = @"INSERT INTO MODELOS 
                            (ID_MODELO,	NOME_MODELO, NOME_WORKSHEET, IT, ID_USUARIO, DATA_CRIACAO)
                            VALUES
                            ('" + ID_MODELO + "', '" + NOME_MODELO + "', '" + NOME_WORKSHEET + "', '" + IT + "','" + ID_USUARIO + "', GETDATE())"; 
                            

            }
            if (index == 4)
            {
                string ID_PARAMETROS = "";
                string ID_MODELO = "";
                string PARAMETRO = "";
                string COLUNAS = "";
                string RANGE_INICIAL = "";
                string RANGE_FINAL = "";
                string COORDENADAS_FINAL = "";
                for(int i = 0; i<countList; i++)
                {
                    if (i == 0)
                    {
                        ID_PARAMETROS = parametros[i];
                    }
                    if (i == 1)
                    {
                        ID_MODELO = parametros[i];
                    }
                    if (i == 2)
                    {
                        PARAMETRO = parametros[i];
                    }
                    if (i == 3)
                    {
                        COLUNAS = parametros[i];
                    }
                    if (i == 4)
                    {
                        RANGE_INICIAL = parametros[i];
                    }
                    if (i == 5)
                    {
                        RANGE_FINAL = parametros[i];
                    }
                    if (i == 6)
                    {
                        COORDENADAS_FINAL = parametros[i];
                    }
                }
                retorno = @"INSERT INTO PARAMETROS
                       ( ID_PARAMETROS 
                       , ID_MODELO 
                       , PARAMETRO 
                       , COLUNAS 
                       , RANGE_INICIAL 
                       , RANGE_FINAL
                       , COORDENADAS_FINAL
                        )
                        VALUES
                       (" + ID_PARAMETROS + ",'" + ID_MODELO + "','" +
                         PARAMETRO + "','" + COLUNAS + "','" +
                         RANGE_INICIAL + "','" +
                         RANGE_FINAL + "','" +
                         COORDENADAS_FINAL + "')";
            }
            if (index == 5)
            {
                for (int i = 0; i < countList; i++)
                {
                    retorno += parametros[i] + ",";
                }
            }
            if (index == 6)
            {
                for (int i = 0; i < countList; i++)
                {
                    retorno += "'"+parametros[i] + "', ";
                }

            }
            return retorno;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLABOR.Negocios.funcoesUteis
{
    static class csRetronaNomeColuna
    {
        public static string RetornaColuna(int IndiceColuna)
        {
            int iAlpha = 0;
            int iRemainder = 0;
            string Convertida = "";
            iAlpha = Convert.ToInt32(IndiceColuna/27);
            iRemainder = IndiceColuna - (iAlpha * 26);
            if (iAlpha > 0)
            {
                Convertida = Convert.ToString(Convert.ToChar(iAlpha + 64));
            }
            if (iRemainder > 0)
            {
                Convertida += Convert.ToString(Convert.ToChar(iRemainder + 64));
            }
            return Convertida;
        }
    }

//    Por exemplo: O número de colunas é 30.
//O número da coluna é dividido por 27:30 / 27 = 1.1111, arredondado para baixo pela função Int para "1". 
//i = 1
//Próximo número de coluna - (i * 26) = 30-(1 * 26) = 30 26 = 4. 
//j = 4
//Converter os valores em caracteres alfabéticos separadamente, 
//i = 1 = "A"
//j = 4 = "D"
//Combinados juntos, eles formam o designador de coluna "AD".
//A função VBA seguinte é apenas uma maneira de converter valores numéricos de coluna em seus caracteres alfabéticos equivalentes:
//Function ConvertToLetter(iCol As Integer) As String
//   Dim iAlpha As Integer
//   Dim iRemainder As Integer
//   iAlpha = Int(iCol / 27)
//   iRemainder = iCol - (iAlpha * 26)
//   If iAlpha > 0 Then
//      ConvertToLetter = Chr(iAlpha + 64)
//   End If
//   If iRemainder > 0 Then
//      ConvertToLetter = ConvertToLetter & Chr(iRemainder + 64)
//   End If
//End Function
}

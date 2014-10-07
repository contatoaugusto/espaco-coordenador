using System;
using System.Security.Cryptography;
using System.Text;

namespace EC.Common
{

	public abstract class Cryptography
	{

        private static string Hex(string Value)
		{
			uint uiDecimal;
			try
			{
				uiDecimal = checked((uint)System.Convert.ToUInt32(Value));
				return String.Format("{0:x2}", uiDecimal);
			}
			catch (OverflowException oExp) 
			{
				throw new ApplicationException(oExp.Message);
			}			
		}


        private static uint UndoHex(string value)
		{
			uint uiHex;
			try
			{
				uiHex = System.Convert.ToUInt32(value, 16);
				return uiHex;
			}
			catch (OverflowException oExp) 
			{
				throw new ApplicationException(oExp.Message);
			}
		}


        public static string Decryption(string text, string key)
		{
			try
			{
				uint iTextoAsc;
				string sTextoTratado = string.Empty;
				uint iTmpTextoAsc;
				int iTextoPos;
				int iChavePos = 0;
				int iTamChave;
				uint iCompoeCod;

				iTamChave = key.Length;

				iCompoeCod = UndoHex(Microsoft.VisualBasic.Strings.Left(text, 2));

				for (iTextoPos = 3; iTextoPos <= text.Length; iTextoPos+= 2)
				{
					iTextoAsc = UndoHex(Microsoft.VisualBasic.Strings.Mid(text,iTextoPos, 2));
					if (iChavePos < iTamChave)
					{
						iChavePos++;
					}
					else
					{
						iChavePos = 1;
					}
					iTmpTextoAsc = iTextoAsc ^ (uint)Microsoft.VisualBasic.Strings.Asc(Microsoft.VisualBasic.Strings.Mid(key,iChavePos, 1));
					if (iTmpTextoAsc <= iCompoeCod )
					{
						iTmpTextoAsc = 255 + iTmpTextoAsc - iCompoeCod;
					}
					else
					{
						iTmpTextoAsc -= iCompoeCod;
					}
					sTextoTratado += Microsoft.VisualBasic.Strings.Chr((int)iTmpTextoAsc);
				
					iCompoeCod = iTextoAsc;

				}
				return sTextoTratado;
			}
			catch
			{
				return "";
			}
		}


		public static string Encryption(string text, string key)
		{
			try
			{
                string sTemp;
				int iMultiplication;
				uint iCompoeCod ;
				string sTextoTratado;
				Random oRandom = new Random(DateTime.Now.Millisecond);
				int iTextoPos;
				uint iTextoAsc;
				int iChavePos = 0;
				int iTamChave = 0;
				string sChar;
			
				iTamChave = key.Length;
				iMultiplication = ((DateTime.Now.Year * 5) + ((int)DateTime.Now.DayOfWeek * DateTime.Now.Hour) + DateTime.Now.Minute + DateTime.Now.Second);
				while (iMultiplication < 10000) { iMultiplication += DateTime.Now.Year; }
				double a = oRandom.NextDouble();
				iCompoeCod = (uint)((a * iMultiplication % 255) + 1);

				while (iCompoeCod < 16) { iCompoeCod += (uint)DateTime.Now.Second; }
			
				//sTextoTratado = Microsoft.VisualBasic.Conversion.Hex(iCompoeCod);
				sTextoTratado = Hex(iCompoeCod.ToString());

				for (iTextoPos = 0; iTextoPos <= text.Length-1; iTextoPos++)
				{
					sChar = text.Substring(iTextoPos,1);
					iTextoAsc = (uint)((Microsoft.VisualBasic.Strings.Asc(sChar) + iCompoeCod) % 255);
				
					if (iChavePos < iTamChave)
					{
						iChavePos = iChavePos + 1;
					}
					else
					{
						iChavePos = 1;
					}
					iTextoAsc = (uint)(iTextoAsc ^ Microsoft.VisualBasic.Strings.Asc(Microsoft.VisualBasic.Strings.Mid(key,iChavePos, 1)));

					sTemp = Hex(iTextoAsc.ToString());
				
					if (sTemp.Length == 1)
					{
						sTemp = "0" + sTemp;
					}
					sTextoTratado = sTextoTratado + sTemp;
					iCompoeCod = iTextoAsc;
				}
				return sTextoTratado;
			}
			catch
			{
				return "";
			}
		}

		/// <summary>
		/// Calcula o Hash (MD5) para uma string de entrada Text. E retorna o HASH em Hexadecimal.
		/// </summary>
		/// <param name="Text">String de entrada o qual deseja calcular o Hash</param>
		/// <returns></returns>
        public static string MD5Hash(string Text)
		{
			string result = "";
			if(Text != "") 
			{
				MD5 oMD5 = new MD5CryptoServiceProvider();
				byte[] Hash = oMD5.ComputeHash(Encoding.UTF8.GetBytes(Text));
				oMD5.Clear();

				//retorna o valor do hash em hexa
				foreach (char c in Hash) 
				{
					result += Convert.ToString(c,16);
				}
			}

			return result;

		}

	}
}

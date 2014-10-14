using System;
using System.Collections;
using Microsoft.VisualBasic;
using System.Web;

namespace EC.Common
{

	public class Safe
	{

		public static DateTime ValidDateTime(object value)
		{
			try
			{
				DateTime Date;
				Date = Convert.ToDateTime(value);
				if (Date < Convert.ToDateTime("01/01/1900"))
				{
					return Convert.ToDateTime("01/01/1900");
				}
				else
				{
					return Convert.ToDateTime(value, new System.Globalization.CultureInfo("pt-BR"));
				}
			}
			catch
			{
				return Convert.ToDateTime("01/01/1900");
			}
		
		}

		public static string ValidIndicator(string value)
		{
			try
			{
				value = ValidString(value, 1);
				if( value == "S" || value == "N" )
					return value;
				else
					return "N";
			}
			catch
			{
				return "N";
			}
		}

		public static string ValidString(string value, int Length)
		{
			try
			{
				return EscapeSqlInjection(value);
				
			}
			catch
			{
				return "";
			}
		}

		public static string ValidString(string value)
		{
			try
			{
				return EscapeSqlInjection(value);
			}
			catch
			{
				return "";
			}
		}

		private static string EscapeSqlInjection(string value)
		{
			try
			{
				value = Convert.ToString(value);
				value = value.Replace(" sp_",""); 
				value = value.Replace("insert ",""); 
				value = value.Replace("update ",""); 
				value = value.Replace("delete ",""); 
				value = value.Replace("drop ",""); 
				value = value.Replace("table ",""); 
				value = value.Replace("--",""); 
				value = value.Replace("create ",""); 
				value = value.Replace("exec ",""); 
				value = value.Replace("execute ","");
				value = value.Replace("alter ","");
				//value = value.Replace("'"," "); 
				return value;
			}
			catch
			{
				return "";
			}
		}

		public static int ValidYear(int value)
		{
			if(value < 1900)
			{
				return 1900;
			}
			else if (value > 2050)
			{
				return 2050;
			}
			return value;
		}

		public static int ValidInt(object value)
		{
			try
			{
				return Convert.ToInt32(value);
			}
			catch
			{
				return 0;
			}
		}

		public static double ValidDouble(object value)
		{
			try
			{
				return Convert.ToDouble(value);
			}
			catch
			{
				return 0;
			}
		}
  
		public static decimal ValidDecimal(object value)
		{
			try
			{
				return Convert.ToDecimal(value);
			}
			catch
			{
				return 0;
			}
		}

		public static long ValidLong(object value)
		{
			try
			{
				return Convert.ToInt64(value);
			}
			catch
			{
				return 0;
			}
		}

		public static char ValidChar(object value)
		{
			try
			{
				return Convert.ToChar(value);
			}
			catch
			{
				return new char();
			}
		}

	}
}

using System;
using System.Data;
using System.Collections;

namespace EC.Common
{
	/// <summary>
	/// Summary description for Params.
	/// </summary>
	[Serializable()]public class Parameters: DictionaryBase
	{
        public static bool Exists
        {
            get { return EC.Common.Session.Params.Count > 0; }
        }
        public const string NameParameterSession = "Parameters";

        public Parameters()
		{
			
		}

		public Parameters(string Key, string Value)
		{
            Add(Key, Value);			
		}
		/// <summary>
		/// Adiciona um parâmetro
		/// </summary>
		/// <param name="Key">Chave identificadora do parâmetro</param>
		/// <param name="Value">Valor do parametro</param>
		public void Add(string Key, string value)
		{
			ValidItem(Key, value);
		}

		public void Add(string Key, object value)
		{
			ValidItem(Key, value);
		}

		public void Add(object Key, object value)
		{
			ValidItem(Key, value);
		}

		private void ValidItem(object Key, object value)
		{
			if (Dictionary[Key] == null)
			{
				Dictionary.Add(Key, value);
			}
			else
			{
				Dictionary[Key] = value;
			}
		}

		public bool IsExists(object Key)
		{
			IEnumerator aKeys = Dictionary.Keys.GetEnumerator();

			while(aKeys.MoveNext())
			{
				if (aKeys.Current.ToString() == Key.ToString()){return true;}
			}
			return false;
		}

		/// <summary>
		/// Verifica se existe contem um determinado objeto.
		/// </summary>
		/// <param name="Key"></param>
		/// <returns></returns>
		public static bool Contains(object Key)
		{
			IEnumerator aKeys = EC.Common.Session.Params.Dictionary.Keys.GetEnumerator();

			while(aKeys.MoveNext())
			{
				if (aKeys.Current.ToString() == Key.ToString()){return true;}
			}
			return false;
		}

		/// <summary>
		/// Remove um parâmetro
		/// </summary>
		/// <param name="Key">Chave identificadora do parâmetro</param>
		public void Remove(string Key)
		{
			Dictionary.Remove(Key);
		}
		/// <summary>
		/// Retorna o valor do parâmetro de chave Key
		/// </summary>
		public object this[object Key]
		{
			get
			{
				try
				{
					return Dictionary[Key];
				}
				catch
				{
					return null;
				}
			}
			set 
			{
				Dictionary[Key] = value;
			}
		
		}

		public string[] KeyArray ()
		{
			IEnumerator oEnum = Dictionary.Keys.GetEnumerator();
			string[] temp = new string[Dictionary.Keys.Count];
			int idx = 0;

			while(oEnum.MoveNext())
			{
				temp[idx] = Convert.ToString(oEnum.Current);
				idx++;
			}
			return temp;
		}
		public string[] ValueArray ()
		{
			IEnumerator e = Dictionary.Values.GetEnumerator();
			string[] temp = new string[Dictionary.Values.Count];
			int idx = 0;

			while(e.MoveNext())
			{
				temp[idx] = Convert.ToString(e.Current);
				idx++;
			}
			return temp;
		}

		/// <summary>
		/// Retorna um DataTable com as colunas Key e Value
		/// </summary>
		/// <returns>DataTable</returns>
		public DataTable ToDataTable() 
		{
			try 
			{
				DataTable oDtt = new DataTable();
				oDtt.Columns.Add("Key");
				oDtt.Columns.Add("Value");

				DataRowCollection oRC = oDtt.Rows;
				object[] values = new object[2];

				IEnumerator oEnum = Dictionary.Keys.GetEnumerator();

				while(oEnum.MoveNext())
				{
					DataRow oRow;

					values[0] = oEnum.Current;
					values[1] = Dictionary[oEnum.Current];

					oRow = oRC.Add(values);
				}
				return oDtt;
			}
			catch 
			{
				return null;
			}
		}
        public void CopyTo(ref EC.Common.Parameters oParameters) 
		{
			IEnumerator oEnum = Dictionary.Keys.GetEnumerator();

			while(oEnum.MoveNext())
				oParameters.Add(oEnum.Current, Dictionary[oEnum.Current]);
		}

		public SortedList ToSortedList()
		{
			IEnumerator oEnum = Dictionary.Keys.GetEnumerator();
			SortedList oSorted = new SortedList();

			while(oEnum.MoveNext())
				oSorted.Add(oEnum.Current, Dictionary[oEnum.Current]);

			return oSorted;
		}
	}
}

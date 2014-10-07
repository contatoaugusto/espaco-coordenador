using System;
using System.Web;
using System.Collections;

namespace EC.Common
{
	[Serializable()]public struct RequestPage
	{
		public string Key;
		public int Position;
		public Parameters Params;

		public RequestPage(string key, Parameters parameters, int position)
		{
			Key = key;
            Params = parameters;
			Position = position;
		}
	}

	[Serializable()]public class RequestPageCollection: System.Collections.CollectionBase
	{
		public RequestPageCollection()
		{}

		public RequestPage this[int index]
		{
			get {return (RequestPage)List[index];}
			set {List[index] = value;}
		}

		public void Add(string key, Parameters value)
		{
			RequestPage o;
			int iTotal = List.Count -1;
			int iTotalStore = 10;
			string hour = DateTime.Now.Hour.ToString();
			string minute =  DateTime.Now.Minute.ToString();
			string second = DateTime.Now.Second.ToString();
			string positionRequestPage = (hour.Length == 1 ? "0" + hour : hour) + (minute.Length == 1 ? "0" + minute : minute) + (second.Length == 1 ? "0" + second : second);

			for(int i = 0; i <= iTotal; i++)
			{
				o = (RequestPage)List[i];
				if(o.Key == key)
				{
					List.RemoveAt(i);
					break;
				}
			}

            for (int i = iTotal; i < iTotalStore; )
            {
                List.Add(new RequestPage(key, value, Convert.ToInt32(positionRequestPage)));
                return;
            }

			for(int j = 0; j < iTotal; j++)
			{
				if( (j+1) >= List.Count ) break;

				List[j] = List[j+1];

			}
			List[List.Count - 1] = new RequestPage(key, value, Convert.ToInt32(positionRequestPage));
		}
		public RequestPage GetLastRequestPage() 
		{
			return (RequestPage)List[List.Count-1];
		}

		public RequestPage GetPreviousRequestPage() 
		{
			return (RequestPage)List[List.Count-2];
		}

		public bool Contains(string key)
		{
			IEnumerator i = List.GetEnumerator();

			while(i.MoveNext())
			{
				RequestPage obj = (RequestPage)i.Current;
				if(obj.Key.ToString() == key.ToString())
					return true;
			}
			return false;
		}

		public int GetIndex(string key)
		{
			IEnumerator i = List.GetEnumerator();
			int index = -1;
			while(i.MoveNext())
			{
				RequestPage obj = (RequestPage)i.Current;
				if(obj.Key.ToString() == key.ToString())
					break;
				index++;
			}
			return index;
		}

		public bool Remove(string key)
		{
			try
			{
				IEnumerator i = List.GetEnumerator();

				while(i.MoveNext())
				{
					RequestPage obj = (RequestPage)i.Current;
					if(obj.Key.ToString() == key.ToString())
					{
						List.Remove(obj);
						return true;
					}
				}
				return false;
			}
			catch
			{
				return false;
			}
		}
	}
}

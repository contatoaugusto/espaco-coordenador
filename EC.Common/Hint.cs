using System;
using System.Xml;

namespace EC.Common
{
	/// <summary>
	/// Summary description for Hint.
	/// </summary>
	public struct Hint
	{
		public string Id;

		public Hint(string Id)
		{
			this.Id = Id;
		}

		public string GetMessage()
		{
			try
			{
				if(Id.Trim().Length == 0) return string.Empty;

				XmlDocument oXmlErr = new XmlDocument();
				XmlElement oElem;

				string sPath = Library.GetPathFileHint() + Library.GetNameFileHint();
				
				if(!System.IO.File.Exists(sPath))
					throw new Exception("O arquivo \"hint.xml\" não foi encontrado no caminho: " + sPath + " .");

				oXmlErr.Load(sPath);

				oElem = oXmlErr.GetElementById(Id);
				if (oElem == null)
					return "Messagem não cadastrada.";
				else
					return oElem.InnerText; 			
			}
			catch(Exception e)
			{
				throw new ApplicationException(e.Message);
			}
		}
	}
}

using System;
using System.Xml;
using System.Configuration;
using Microsoft.VisualBasic;

namespace EC.Common
{

	public class Configuration
	{ 
		private string _Key = string.Empty;

		public struct Connection 
		{
			public string Name;
			public string DataBase;
			public string User;
			public string PassWord;
			public string Provider;
			public string PersistSecurityInfo;
		}

		public struct Debug 
		{
			public string Name;
			public string Type;
			public bool Email;
		}

		public struct Maintenance 
		{
			public string Initial;
			public string Final;
			public bool In;
		}

		public struct Config
		{
			public Connection Server;
			public Debug File;
			public Maintenance Maintenance;
		}


		public Config LoadConfiguration()
		{
			XmlDocument oXmlDoc = new XmlDocument();

			string NameFileConfigXml = Library.GetNameFileConfigXml();
			string NameFileConfigKey = Library.GetNameFileConfigKey();
			string PathFileConfigXml = Library.GetPathFileConfigXml();
			string PathFileConfigKey = Library.GetPathFileConfigKey();

			XmlNodeList oNodLstConfiguration;
			XmlNode oNodServer;
			XmlNode oNodDebug;
			XmlAttributeCollection coXmlAtbColServer;
			XmlAttributeCollection coXmlAtbColDebug;

			oXmlDoc.Load(PathFileConfigXml + NameFileConfigXml);
			oNodLstConfiguration = oXmlDoc.GetElementsByTagName("configuration");

			oNodServer= oNodLstConfiguration.Item(0).ChildNodes.Item(0).ChildNodes.Item(0);
			oNodDebug = oNodLstConfiguration.Item(0).ChildNodes.Item(1).ChildNodes.Item(0);

			coXmlAtbColServer = oNodServer.Attributes;
			coXmlAtbColDebug = oNodDebug.Attributes;

			Connection oCnn = new Connection();
			Debug oDbg = new Debug();
			Config oCfg = new Config();

			foreach (XmlNode oNod in coXmlAtbColServer)
			{
				switch (oNod.Name)
				{					
					case "name":
						oCnn.Name = oNod.Value.ToString();
						break;
					case "database":
						oCnn.DataBase = oNod.Value.ToString();
						break;
					case "user":
						oCnn.User = oNod.Value.ToString();
						break;
					case "password":
						oCnn.PassWord = oNod.Value.ToString();
						break;
					case "provider":
						oCnn.Provider = oNod.Value.ToString();
						break;
					case "persistsecurityinfo":
						oCnn.PersistSecurityInfo = oNod.Value.ToString();
						break;
					default:
						break;
				}
			}

			foreach (XmlNode oNod in coXmlAtbColDebug)
			{
				switch (oNod.Name)
				{					
					case "name":
						oDbg.Name = oNod.Value.ToString();
						break;
					case "type":
						oDbg.Type = oNod.Value.ToString();
						break;
					case "email":
						oDbg.Email = Library.ToBoolean(oNod.Value.ToString());
						break;
					default:
						break;
				}
			}

			ReadFileKey(PathFileConfigKey, NameFileConfigKey);

			oCfg.File = oDbg;
			oCfg.Server = DecodeVariables(oCnn);
			return oCfg;

		}


		public Config LoadConfigurationDebug()
		{
			XmlDocument oXmlDoc = new XmlDocument();

			string NameFileConfigXml = ConfigurationManager.AppSettings["NameFileConfigXml"].ToString();
			string PathFileConfigXml = ConfigurationManager.AppSettings["PathFileConfigXml"].ToString();

			XmlNodeList oNodLstConfiguration;
			XmlNode oNodDebug;
			XmlAttributeCollection coXmlAtbColDebug;

			oXmlDoc.Load(PathFileConfigXml + NameFileConfigXml);
			oNodLstConfiguration = oXmlDoc.GetElementsByTagName("configuration");

			oNodDebug = oNodLstConfiguration.Item(0).ChildNodes.Item(1).ChildNodes.Item(0);

			coXmlAtbColDebug = oNodDebug.Attributes;

			Config oCfg = new Config();
			Debug oDbg = new Debug();

			foreach (XmlNode oNod in coXmlAtbColDebug)
			{
				switch (oNod.Name)
				{					
					case "name":
						oDbg.Name = oNod.Value.ToString();
						break;
					case "type":
						oDbg.Type = oNod.Value.ToString();
						break;
					case "email":
						oDbg.Email = Library.ToBoolean(oNod.Value.ToString());
						break;
					default:
						break;
				}
			}

			oCfg.File = oDbg;
			return oCfg;
		}

		public Maintenance LoadConfigurationMaintenance()
		{
			try
			{
			XmlDocument oXmlDoc = new XmlDocument();

			string NameFileConfigXml = ConfigurationManager.AppSettings["NameFileConfigXml"].ToString();
			string PathFileConfigXml = ConfigurationManager.AppSettings["PathFileConfigXml"].ToString();

			XmlNodeList oNodLstConfiguration;
			XmlNode oNodMaintenance;
			XmlAttributeCollection coXmlAtbColMaintenance;

			oXmlDoc.Load(PathFileConfigXml + NameFileConfigXml);
			oNodLstConfiguration = oXmlDoc.GetElementsByTagName("configuration");

			oNodMaintenance = oNodLstConfiguration.Item(0).ChildNodes.Item(2);

			coXmlAtbColMaintenance = oNodMaintenance.Attributes;

			Maintenance obj = new Maintenance();

			foreach (XmlNode oNod in coXmlAtbColMaintenance)
			{
				switch (oNod.Name)
				{					
					case "initial":
						obj.Initial = oNod.Value.ToString();
						break;
					case "final":
						obj.Final = oNod.Value.ToString();
						break;
					case "in":
						obj.In = Library.ToBoolean(oNod.Value.ToString());
						break;
					default:
						break;
				}
			}

			return obj;
			}
			catch//--(Exception e)
			{
				//-- new SGIException(e);
				Maintenance main = new Maintenance();
				main.In = false;
				return main;
			}

		}

		private void ReadFileKey(string PathFileKey, string NameFileKey)
		{
			string key;
			try
			{
				XmlDocument xml = new XmlDocument();
				xml.Load(PathFileKey + NameFileKey);
				XmlNodeList nodeList = xml.GetElementsByTagName("key");
				key = nodeList[0].Attributes.Item(0).Value;
				
			}
			catch(Exception x)
			{
				throw new ApplicationException("Error reading file \"config.key.xml\".: " + x.Message);
			}

			if (!ValidKey(key))
			{
				throw new ApplicationException("File \"config.key.xml\" invalid.");
			}

			_Key = FormatKey(key);

		}

		private Connection DecodeVariables(Connection oCnn)
		{
            oCnn.Name = Cryptography.Decryption(oCnn.Name, _Key);
            oCnn.DataBase = Cryptography.Decryption(oCnn.DataBase, _Key);
			//oCnn.DataBase = "SGIHML";
            oCnn.User = Cryptography.Decryption(oCnn.User, _Key);
            oCnn.PassWord = Cryptography.Decryption(oCnn.PassWord, _Key);
            oCnn.Provider = Cryptography.Decryption(oCnn.Provider, _Key);
            oCnn.PersistSecurityInfo = Cryptography.Decryption(oCnn.PersistSecurityInfo, _Key);

			return oCnn;
		}

		private bool ValidKey(string value)
		{
			string sSign;
		    
			sSign = value.Substring(4, 1) + value.Substring(9, 1) + value.Substring(14, 1);
		    
			if ( sSign.ToLower() == "sgi")
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		private string FormatKey(string value)
		{
			string sAux;
			int iTam;

			iTam = Convert.ToInt32(value.Substring(5, 1) + value.Substring(8, 1));

			sAux = Strings.Mid(value,16, 5) + Strings.Mid(value,26, 5) + Strings.Mid(value,36, 5) 
				+ Strings.Mid(value,46, 5) + Strings.Mid(value,56, 5) + Strings.Mid(value,66, 5);

			sAux = Strings.StrReverse(sAux);

			return sAux.Substring(0,iTam);

		}

	}
}

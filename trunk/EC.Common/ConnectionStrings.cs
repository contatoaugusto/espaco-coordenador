using System;
using System.Configuration;

namespace EC.Common
{
    public sealed class ConnectionStrings
    {
        public static string Get(string key)
        {
            return ReadConnectionStrings(key);
        }

        private static string ReadConnectionStrings(string key)
        {
            object connectionString = ConfigurationManager.ConnectionStrings[key];

            if (connectionString == null)
                throw new Exception(string.Format("The key \"{0}\" not found in web.config", key));
            else
                return connectionString.ToString();
        }
    }
}

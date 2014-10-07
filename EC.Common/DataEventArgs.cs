using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;

namespace EC.Common
{
    [Serializable]
    [ComVisible(true)]
    public delegate void DataEventHandler(object sender, DataEventArgs e);

    public class DataEventArgs : System.EventArgs
    {
        public DataTable DataSource
        {
            get;
            private set;
        }

        public DataEventArgs(DataTable dataSource)
        {
            DataSource = dataSource;
        }
    }
}

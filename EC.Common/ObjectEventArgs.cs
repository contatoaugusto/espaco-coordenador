using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;

namespace EC.Common
{
    //--
    [Serializable]
    [ComVisible(true)]
    public delegate void ObjectEventHandler(object sender, ObjectEventArgs e);

    public class ObjectEventArgs : System.EventArgs
    {
        public object Object
        {
            get;
            private set;
        }

        public ObjectEventArgs(object obj)
        {
            Object = obj;
        }
    }
}

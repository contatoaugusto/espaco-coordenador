using System;
using System.Runtime.InteropServices;

namespace EC.Common
{
    [Serializable]
    [ComVisible(true)]
    public delegate void ReferenceEventHandler(ref object sender, EventArgs e);
}

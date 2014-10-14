
using System.Collections.Generic;

namespace EC.UI.WebControls
{
    /// <summary>
    /// Descrição resumida para ILinkInfo
    /// </summary>
    public interface ILinkInfo
    {
        string Url { get; set; }
        string Title { get; set; }
        string Subtitle { get; set; }
        string Summary { get; set; }
        List<LinkInfoImage> Images { get; set; }
    }
}
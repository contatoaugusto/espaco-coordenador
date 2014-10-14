
namespace EC.UI.WebControls
{
    /// <summary>
    /// Descrição resumida para LinkInfoImage
    /// </summary>
    public class LinkInfoImage
    {
        public string Src { get; set; }
        public string Alt { get; set; }

        public LinkInfoImage(string src, string alt)
        {
            this.Src = src;
            this.Alt = alt;
        }
    }
}
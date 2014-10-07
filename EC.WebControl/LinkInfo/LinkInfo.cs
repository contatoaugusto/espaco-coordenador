using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.UI;
using EC.Common;
using mshtml;
using System.Text.RegularExpressions;

namespace EC.UI.WebControls
{
    /// <summary>
    /// Descrição resumida para LinkInfo
    /// </summary>
    public class LinkInfo : System.Web.UI.Control, ILinkInfo
    {
        private string _buttonAttachImageUrl = "";
        private string _buttonPublishImageUrl = "";
        private string _imageLoadingUrl = "";
        private string _width = "100%";

        public LinkInfo()
        {

        }

        #region Properties
        
        public string Url
        {
            get { return Library.ToString(ViewState["0"]); }
            set { ViewState["0"] = value; }
        }

        public string Title
        {
            get { return Library.ToString(ViewState["1"]); }
            set { ViewState["1"] = value; }
        }

        public string Subtitle
        {
            get { return Library.ToString(ViewState["2"]); }
            set { ViewState["2"] = value; }
        }

        public string Summary
        {
            get { return Library.ToString(ViewState["3"]); }
            set { ViewState["3"] = value; }
        }

        public string Comment
        {
            get { return Library.ToString(ViewState["4"]); }
            set { ViewState["4"] = value; }
        }  

        public string ButtonAttachImageUrl
        {
            get { return _buttonAttachImageUrl; }
            set { _buttonAttachImageUrl = value; }
        }
        public string ImageLoadingUrl
        {
            get { return _imageLoadingUrl; }
            set { _imageLoadingUrl = value; }
        }

        public string ButtonPublishImageUrl
        {
            get { return _buttonPublishImageUrl; }
            set { _buttonPublishImageUrl = value; }
        }

        public List<LinkInfoImage> Images
        {
            get
            {
                if (ViewState["4"] == null)
                    ViewState["4"] = new List<LinkInfoImage>();

                return (List<LinkInfoImage>)ViewState["4"];
            }
            set { ViewState["4"] = value; }
        }

        #endregion

        public void Bind(string url)
        {
            ILinkInfo linkInfo = LinkInfo.Get(url);

            this.Url = linkInfo.Url;
            this.Title = linkInfo.Title;
            this.Summary = linkInfo.Summary;
            this.Images = linkInfo.Images;
        }

        private bool RemoteFileExists(string url)
        {
            bool result = false;
            using (WebClient client = new WebClient())
            {
                try
                {
                    Stream stream = client.OpenRead(url);
                    if (stream != null)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        public static ILinkInfo Get(string url) {

            if (url.IndexOf("http://") == -1 & url.IndexOf("https://") == -1 & url.IndexOf("ftp://") == -1) 
                url = string.Format("http://{0}", url);

            ILinkInfo linkInfo = new LinkInfo();

            linkInfo.Url = url;

            try
            {
                var parseHtml = HtmlParser(url);

                linkInfo.Title = parseHtml.Title;

                foreach (var meta in parseHtml.Metas)
                {
                    if (meta.Name.ToLower() == "description")
                        linkInfo.Summary = meta.Content;
                }
            }
            catch {

                return linkInfo;
            }


            return linkInfo;
        }

        private static ParseHtml HtmlParser(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream stream = CopyAndClose(response.GetResponseStream());

            response.Close();

            StreamReader reader = new StreamReader(stream);
            string html = reader.ReadToEnd();

            if (html.Replace(" ", "").ToLower().IndexOf("charset=iso-8859-1") > -1)
            {
                stream.Position = 0;
                reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("iso-8859-1"));
                html = reader.ReadToEnd();
            }

            reader.Close();
            stream.Close();     

            string htmlParse = html.ToLower().Trim().Replace(" ", "");
       
            ParseHtml parseHtml = new ParseHtml();
                
            Match matchTitle = Regex.Match(html, "<title>([^<]*)</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            parseHtml.Title = ClearText(matchTitle.Groups[1].Value);

            if (htmlParse.IndexOf("<metaname=\"keywords\"") > -1)
            {
                Match keywordMatch = Regex.Match(html, "<meta\\sname=\"keywords\"\\scontent=\"([^<]*)\"\\s?/>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                parseHtml.Metas.Add(new ParseHtml.Meta("keyword", keywordMatch.Groups[1].Value));
            }
            if (htmlParse.IndexOf("<metaname=\"description\"") > -1)
            {
                Match descriptionMatch = Regex.Match(html, "<meta\\sname=\"description\"\\scontent=\"([^<]*)\"\\s?/>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                parseHtml.Metas.Add(new ParseHtml.Meta("description", ClearText(descriptionMatch.Groups[1].Value)));

            }
            if (htmlParse.IndexOf("<metahttp-equiv=\"content-type\"") > -1)
            {
                Match contentTypeMatch = Regex.Match(html, "<meta\\shttp-equiv=\"content-type\"\\scontent=\"([^<]*)\"\\s?/>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                parseHtml.Metas.Add(new ParseHtml.Meta("content-type", contentTypeMatch.Groups[1].Value));
            }

            return parseHtml;
        }

        private static Stream CopyAndClose(Stream inputStream)
        {
            const int readSize = 256;
            byte[] buffer = new byte[readSize];
            MemoryStream ms = new MemoryStream();
            int count = inputStream.Read(buffer, 0, readSize);
            while (count > 0)
            {
                ms.Write(buffer, 0, count);
                count = inputStream.Read(buffer, 0, readSize);
            }
            ms.Position = 0;
            inputStream.Close();
            return ms;
        }

        private class ParseHtml
        {
            private string _title = "";

            public string Title {
                get { return _title; }
                set { _title = value; }
            }

            public List<Meta> Metas { get; set; }

            public ParseHtml() {

                Metas = new List<Meta>();

            }

            public struct Meta
            {
                public string Name;
                public string Content;

                public Meta(string name, string content)
                {
                    Name = name;
                    Content = content;
                }
            }
        }

        public static string ClearText(string text)
        {
            return text.Replace("'", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
        }

        private readonly string INPUTNAME = "input";

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //Page.ClientScript.RegisterClientScriptBlock(GetType(), "LinkInfo_" + ClientID, GetClientScript(), true);
        }
 
        protected override void Render(HtmlTextWriter writer)
        {

            writer.Write(string.Format("<div id=\"{0}\" class=\"linkinfo\">", ClientID));

            #region Ficar para versão 2.0
            
            //writer.Write("<div class=\"linkinfo-image\">");
            //writer.Write("<div class=\"linkinfo-image-loader\">");
            //writer.Write("</div>");
            //writer.Write("<div class=\"linkinfo-image-thumbs\">");

            //foreach (string src in Images)
            //{
            //    writer.Write("<img src=\"\" alt="" />");
            //}

            //writer.Write("</div>");

            //writer.Write("</div>");
            #endregion

            #region Input

            writer.Write(string.Format("<center><img id=\"{0}_loading\" src=\"{1}\" class=\"dn\" alt=\"Carregando informações...\" /></center>", ClientID, ImageLoadingUrl));

            //Title
            writer.Write("<div class=\"linkinfo-action\">");
            writer.Write("<div class=\"linkinfo-input-action-input\">");
            writer.Write(string.Format("<input id=\"{0}_{1}\" type=\"text\" title=\"Http://\" />", ClientID, INPUTNAME));
            writer.Write("</div>");
            writer.Write("<div class=\"linkinfo-input-action-button\">");
            writer.Write(string.Format("<img src=\"{0}\" class=\"cp\" alt=\"Publicar Link\" onclick=\"Feed.getLinkInfo();return false;\" />", ButtonAttachImageUrl));
            writer.Write("</div>");
            writer.Write("</div>");

            #endregion

            #region Content

            writer.Write("<div class=\"linkinfo-content dn\">");
            
            //Title
            writer.Write("<div class=\"linkinfo-content-title\">");
            writer.Write(string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", Url, Title));
            writer.Write("</div>");

            //Subtitle
            writer.Write("<div class=\"linkinfo-content-subtitle\">");
            writer.Write(string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", Url, Subtitle));
            writer.Write("</div>");

            //Summary
            writer.Write("<div class=\"linkinfo-content-summary\">");
            writer.Write(Summary);
            writer.Write("</div>");

            writer.Write("<div class=\"linkinfo-content-separator\"></div>");

            //Action
            writer.Write("<div class=\"linkinfo-content-action\">");
            writer.Write("<textarea id=\"linkinfo_input_comment\" cols=\"0\" rows=\"1\">");
            writer.Write(Comment);
            writer.Write("</textarea>");
            writer.Write("<br /><label>Utilize no máximo 420 caracteres</label>");
            writer.Write("<br />");
            writer.Write("<div>");
            writer.Write(string.Format("<img src=\"{0}\" alt=\"\" class=\"cp\" onclick=\"Feed.publishLinkInfo();\" />", ButtonPublishImageUrl));
            writer.Write("</div>");
            writer.Write("</div>");
            
            #endregion

            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("<div class=\"cb\"></div>");
        }

        private string GetClientScript()
        {
            #region Leia me
            /*
                Esta função é usada somente para Debug

                Antes de atualizar o script LinkInfo, user o Minimizer Javascript http://dean.edwards.name/packer/

                function getLinkInfo() {

                    var url = $('#CLIENTID_input').val();

                    $.ajax({
                        type: 'GET',
                        url: 'linkinfo.ashx',
                        data: 'url=' + url,
                        dataType: "text/json",
                        success: function (data) {

                            var json = eval(data);

                            if (json != null) {

                                var linkInfo = json[0];
                                var linkInfoImages = linkInfo.images;

                                var content = $('#CLIENTID_content');

                                content.find('.linkinfo-content-title').html(linkInfo.title);
                                content.find('.linkinfo-content-subtitle').html(linkInfo.subtitle);
                                content.find('.linkinfo-content-summary').html(linkInfo.summary);
                                content.show();
                            }
                        }

                    });
                }

            */
            #endregion

            return "function getLinkInfo(){var url=$('#" + ClientID + "_input').val();$.ajax({type:'GET',url:'linkinfo.ashx',data:'url='+url,dataType:\"text/json\",success:function(data){var json=eval(data);if(json!=null){var linkInfo=json[0];var linkInfoImages=linkInfo.images;var content=$('#" + ClientID + "_content');content.find('.linkinfo-content-title').html(linkInfo.title);content.find('.linkinfo-content-subtitle').html(linkInfo.subtitle);content.find('.linkinfo-content-summary').html(linkInfo.summary);content.show()}}})}";
        }
    }

    
}


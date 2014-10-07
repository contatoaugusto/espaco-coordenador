using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using EC.Common;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:GridViewPagination Runat=\"server\"></{0}:GridViewPagination>")]
    [Serializable()]
    [Designer(typeof(ControlDesigner))]
    public class GridViewPagination : System.Web.UI.Control, IPostBackEventHandler
    {
        //public event EventHandler PreviousClick;
        //public event EventHandler NextClick;
        //public event EventHandler SelectedPageClick;
        public event EventHandler DataBindEvent;

        private Panel _items;
        private string _result = string.Empty;
        bool _previousEnable = true;
        bool _nextEnable = true;

        public GridViewPagination()
        {
            _items = new Panel();
        }

        #region Properties
        private int PageCount
        {
            get { return Library.ToInteger(ViewState["PC"]); }
            set { ViewState["PC"] = value; }
        }

        public int PageSize
        {
            get { return Library.ToInteger(ViewState["PS"]); }
            set { ViewState["PS"] = value; }
        }

        private int TotalRecords
        {
            get { return Library.ToInteger(ViewState["TR"]); }
            set { ViewState["TR"] = value; }
        }

        public int PageIndex
        {
            get
            {
                object o = ViewState["PI"];
                if (o == null)
                    o = ViewState["PI"] = 1;
                return Library.ToInteger(o);
            }
            set { ViewState["PI"] = value; }
        }

        private int Selected
        {
            get
            {
                if (Library.ToInteger(ViewState["S"]) == 0)
                    ViewState["S"] = 1;
                return Library.ToInteger(ViewState["S"]);
            }
            set { ViewState["S"] = value; }
        }
        #endregion Properties

        #region IPostBackEventHandler Members

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument.Equals("Previous"))
            {
                //if (PreviousClick != null)
                //    PreviousClick.Invoke(this, EventArgs.Empty);

                if (PageIndex > 1)
                {
                    Selected = PageIndex - 1;
                    PageIndex = Selected;
                    if (DataBindEvent != null)
                        DataBindEvent.Invoke(null, null);
                }
            }
            else if (eventArgument.Equals("Next"))
            {
                //if (NextClick != null)
                //    NextClick.Invoke(this, EventArgs.Empty);

                if (PageIndex < PageCount)
                {
                    Selected = PageIndex + 1;
                    PageIndex = Selected;
                    if (DataBindEvent != null)
                        DataBindEvent.Invoke(null, null);
                }
            }
            else if (eventArgument.IndexOf("SelectedPage") > -1)
            {
                //if (SelectedPageClick != null)
                //    SelectedPageClick.Invoke(this, EventArgs.Empty);

                string[] prs = eventArgument.Split('$');

                Selected = Library.ToInteger(prs[1]);

                if (Selected == 0)
                    throw new Exception("Página inválida");

                PageIndex = Selected;
                if (DataBindEvent != null)
                    DataBindEvent.Invoke(null, null);

                Fill(null, PageSize);
            }
            else if (eventArgument.Equals("DataBind"))
            {
                if (DataBindEvent != null)
                    DataBindEvent.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            Fill(null, PageSize);
            
            base.OnLoad(e);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {

            writer.Write("<table width=\"0%\">");
            writer.Write("    <tr>");
            writer.Write("        <td width=\"0px\">");

            if(_previousEnable)
                writer.Write("            <a href=\"javascript:void(0);\" onclick=\"" + Page.ClientScript.GetPostBackEventReference(this, "Previous") + "\" >Anterior</a>&nbsp;");
            else
                writer.Write("            Anterior&nbsp;");
            writer.Write("        </td>");
            writer.Write("        <td width=\"0px\">");

            _items.RenderControl(writer);

            writer.Write("        </td>");
            writer.Write("        <td>");

            if(_nextEnable)
                writer.Write("            &nbsp;<a href=\"javascript:void(0);\" onclick=\"" + Page.ClientScript.GetPostBackEventReference(this, "Next") + "\">Próximo</a>&nbsp;&nbsp;[" + _result + "]");
            else
                writer.Write("            &nbsp;Próximo&nbsp;&nbsp;[" + _result + "]");
            writer.Write("        </td>");
            writer.Write("    </tr>");
            writer.Write("</table>");

            base.Render(writer);
        }

        public void Fill(DataTable dataSource, int pageSize)
        {
            Visible = true;

            if (dataSource != null)
            {
                if (dataSource.Rows.Count > 0)
                    TotalRecords = dataSource.Rows[0]["NumRegister"].ToInt32();
                else
                    TotalRecords = 0;
            }

            if (TotalRecords == 0)
            {
                Visible = false;
                return;
            }

                PageSize = pageSize;

                PageCount = Math.Ceiling(TotalRecords.ToDouble() / PageSize).ToInt32();

                int startno, endno;
                int selectedpgeno = -1;
                if (Selected == 1)
                {
                    startno = 1;
                    endno = startno + 9;

                    if (endno > PageCount)
                        endno = PageCount;
                }
                else
                {
                    selectedpgeno = Selected;

                    if (selectedpgeno > 1)
                        startno = selectedpgeno - 1;
                    else
                        startno = 1;

                    endno = startno + 9;
                }

                if (endno > PageCount)
                {
                    startno = PageCount - 10;
                    if (startno < 1)
                        startno = 1;
                    endno = PageCount;
                }

                _items.Controls.Clear();

                //LinkButton lkb;
                Literal lkb;
                Literal lit;

                for (int i = startno; i <= endno; i++)
                {
                    //lkb = new LinkButton();
                    //lkb.ID = i.ToString();
                    //lkb.Text = lkb.CommandArgument = i.ToString();
                    //lkb.CausesValidation = false;
                    //lkb.Click += new EventHandler(link_Click);

                    lkb = new Literal();

                    if (i != Selected)
                        lkb.Text = "<a href=\"javascript:void(0);\" onclick=\"" + Page.ClientScript.GetPostBackEventReference(this, "SelectedPage$" + i.ToString()) + "\">" + i.ToString() + "</a>";
                    else
                        lkb.Text = "<b>" + i.ToString() + "</b>";

                    _items.Controls.Add(lkb);

                    if (i < endno)
                    {
                        lit = new Literal();
                        lit.Text = "&nbsp;";
                        _items.Controls.Add(lit);
                    }
                }

                _previousEnable = !(PageIndex == 1);
                _nextEnable = !(PageIndex == (PageCount));

                int ini = (Selected * PageSize) - PageSize;

                if (ini == 0)
                    ini = 1;

                _result = string.Format("Resultados <b>{0}</b> - <b>{1}</b> de <b>{2}</b>", ini, (ini + (PageSize - 1) > TotalRecords ? TotalRecords : ini + (PageSize - 1)), TotalRecords);
            }
    }
}

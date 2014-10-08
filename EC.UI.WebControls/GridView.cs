using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

[assembly: TagPrefix("EC.UI.WebControls", "sgi")]

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:GridView runat=\"server\"></{0}:GridView>")]
    public class GridView : System.Web.UI.WebControls.GridView
    {
        protected GridViewRow _footerRow2;
        protected GridViewRow _headerRow2;
        private bool _groupFooter = false;
        private bool _allowCustomPaging = false;

        public GridView()
        {
            Initialize();
        }

        #region Custom Pagination Properties
        [Browsable(true), Category("NewDynamic")]
        [Description("Set the virtual item count for this grid")]
        public new int VirtualItemCount
        {
            get
            {
                if (ViewState["pgv_vitemcount"] == null)
                    ViewState["pgv_vitemcount"] = -1;
                return Convert.ToInt32(ViewState["pgv_vitemcount"]);
            }
            set
            {
                ViewState["pgv_vitemcount"] = value;
            }
        }

        [Browsable(true), Category("NewDynamic")]
        [Description("Get the order by string to use for this grid when sorting event is triggered")]
        public string OrderBy
        {
            get
            {
                if (ViewState["pgv_orderby"] == null)
                    ViewState["pgv_orderby"] = string.Empty;
                return ViewState["pgv_orderby"].ToString();
            }
            protected set
            {
                ViewState["pgv_orderby"] = value;
            }
        }

        private int CurrentPageIndex
        {
            get
            {
                if (ViewState["pgv_pageindex"] == null)
                    ViewState["pgv_pageindex"] = 0;
                return Convert.ToInt32(ViewState["pgv_pageindex"]);
            }
            set
            {
                ViewState["pgv_pageindex"] = value;
            }
        }
        private bool CustomPaging
        {
            get
            {
                return (VirtualItemCount != -1);
            }
        }
        #endregion Custom Pagination Properties

        #region Pagination Properties
        [Category("Behaviour")]
        [Bindable(BindableSupport.No)]
        public bool AllowCustomPaging
        {
            get { return _allowCustomPaging; }
            set { _allowCustomPaging = value; }
        }
        public bool GroupFooter
        {
            get { return _groupFooter; }
            set { _groupFooter = value; }
        }

        [Category("Behaviour")]
        [Themeable(true)]
        [Bindable(BindableSupport.No)]
        public bool ShowWhenEmpty
        {
            get
            {
                if (ViewState["ShowWhenEmpty"] == null)
                    ViewState["ShowWhenEmpty"] = false;

                return (bool)ViewState["ShowWhenEmpty"];
            }
            set
            {
                ViewState["ShowWhenEmpty"] = value;
            }
        }

        public override GridViewRow FooterRow
        {
            get
            {
                GridViewRow f = base.FooterRow;
                if (f != null)
                    return f;
                else
                    return _footerRow2;
            }
        }

        public override GridViewRow HeaderRow
        {
            get
            {
                GridViewRow h = base.HeaderRow;
                if (h != null)
                    return h;
                else
                    return this._headerRow2;
            }
        }
        #endregion Properties

        private void Initialize()
        {
            CssClass = "gridview";
            AlternatingRowStyle.CssClass = "gridviewrowalternating";
            RowStyle.CssClass = "gridviewrow";
            HeaderStyle.CssClass = "gridviewrowheader";
            BackColor = Color.FromArgb(191, 191, 191);
            BorderColor = Color.FromArgb(191, 191, 191);
            BorderStyle = BorderStyle.Solid;
            PagerStyle.CssClass = "gridviewpaging";
            CellSpacing = 0;
            AutoGenerateColumns = false;
            PageSize = 50;
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].CssClass = "gridviewcolumnheader";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "this.className='gridviewmouseoverrow'");
                if (e.Row.RowState == DataControlRowState.Normal)
                {
                    e.Row.CssClass = "gridviewrow";
                    e.Row.Attributes.Add("onmouseout", "this.className='gridviewrow'");
                }
                else
                {
                    e.Row.CssClass = "gridviewrowalternating";
                    e.Row.Attributes.Add("onmouseout", "this.className='gridviewrowalternating'");
                }

                for (int i = 0; i < e.Row.Cells.Count; i++)
                    if (e.Row.Cells[i].Controls.Count > 0)
                    {
                        bool find = false;
                        foreach (System.Web.UI.Control c in e.Row.Cells[i].Controls)
                        {
                            if (c is System.Web.UI.WebControls.Button || c is LinkButton || c is Button)
                                find = true;

                            if (find)
                                e.Row.Cells[i].CssClass = "gridviewcolumnaction";
                            else
                            {
                                if (e.Row.Cells[i].CssClass.Trim().Length == 0)
                                    e.Row.Cells[i].CssClass = "gridviewcolumnrow";
                            }
                        }
                    }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].CssClass = "gridviewcolumnfooter";
            }

            base.OnRowDataBound(e);
        }

        protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
        {
            int numRows = base.CreateChildControls(dataSource, dataBinding);

            //no data rows created, create empty table if enabled
            if (numRows == 0 && ShowWhenEmpty)
            {
                //create table
                Table table = new Table();
                table.ID = ID;
                table.CssClass = "gridview";

                //convert the exisiting columns into an array and initialize
                DataControlField[] fields = new DataControlField[Columns.Count];
                Columns.CopyTo(fields, 0);

                if (ShowHeader)
                {
                    //create a new header row
                    GridViewRow headerRow = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);

                    InitializeRow(headerRow, fields);
                    table.Rows.Add(headerRow);
                }

                //create the empty row
                GridViewRow emptyRow = new GridViewRow(-1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                emptyRow.CssClass = "gridviewrowempty";

                TableCell cell = new TableCell();
                cell.ColumnSpan = Columns.Count;
                cell.CssClass = "gridviewcolumnempty";
                cell.Width = Unit.Percentage(100);
                if (!String.IsNullOrEmpty(EmptyDataText))
                    cell.Controls.Add(new LiteralControl(EmptyDataText));

                if (EmptyDataTemplate != null)
                    EmptyDataTemplate.InstantiateIn(cell);

                emptyRow.Cells.Add(cell);
                table.Rows.Add(emptyRow);

                if (ShowFooter)
                {
                    //create footer row
                    GridViewRow footerRow = base.CreateRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);

                    InitializeRow(footerRow, fields);
                    table.Rows.Add(footerRow);
                }

                Controls.Clear();
                Controls.Add(table);
            }

            return numRows;
        }

        protected override void OnSorting(GridViewSortEventArgs e)
        {
            if (AllowCustomPaging)
            {
                SortDirection direction = SortDirection.Ascending;
                if (ViewState[e.SortExpression] != null && (SortDirection)ViewState[e.SortExpression] == SortDirection.Ascending)
                {
                    direction = SortDirection.Descending;
                }
                ViewState[e.SortExpression] = direction;
                OrderBy = string.Format("{0} {1}", e.SortExpression, (direction == SortDirection.Descending ? "DESC" : ""));
            }
            base.OnSorting(e);
        }


        #region Overriding the parent methods
        protected override void OnInit(EventArgs e)
        {
            if (AllowCustomPaging)
                AllowPaging = true;

            base.OnInit(e);
        }

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                if (value != null)
                {
                    this.ValidateDataSource(value);
                }

                base.DataSource = value;

                if (AllowCustomPaging)
                {
                    CurrentPageIndex = PageIndex;

                    if (!(DataSource is System.Data.DataTable))
                        throw new Exception("O DataSource tem que ser do tipo DataTable");

                    if ((DataSource as DataTable).Columns.Count > 0)
                    {
                        bool exists = false;
                        foreach (DataColumn column in (DataSource as DataTable).Columns)
                            if (column.ColumnName.Trim().ToLower() == "numregister")
                            {
                                exists = true;
                                break;
                            }

                        if (!exists)
                            throw new Exception("Não foi encontrado a coluna \"NumRegister\" na tabela");

                        if ((DataSource as DataTable).Rows.Count > 0)
                            VirtualItemCount = Convert.ToInt32((DataSource as DataTable).Rows[0]["NumRegister"]);
                    }
                }

                this.OnDataPropertyChanged();
            }
        }

        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            if (CustomPaging)
            {
                pagedDataSource.AllowCustomPaging = true;
                pagedDataSource.VirtualCount = VirtualItemCount;
                pagedDataSource.CurrentPageIndex = CurrentPageIndex;
            }
            base.InitializePager(row, columnSpan, pagedDataSource);
        }
        #endregion
    }
}

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design.WebControls;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;

namespace EC.UI.WebControls
{
	/// <summary>
	/// 
	/// </summary>
	[DefaultProperty("DataSource"),Designer(typeof(RepeaterDesigner))]
	public class PagingRepeater : Repeater, IPostBackEventHandler
	{
		public PagingRepeater()
		{
			Debug.WriteLine(".ctor()");
		}

		public event PagingRepeaterPageChangedEventHandler PageIndexChanged;
		private PagingRepeaterPagerStyle pagerStyle;

		#region View State

		protected override void LoadViewState(object savedState) 
		{
			Debug.WriteLine("LoadViewState()");
			if (savedState != null) 
			{
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
				{
					if ( this.pagerStyle == null ) this.pagerStyle = new PagingRepeaterPagerStyle(this);
					((IStateManager)this.pagerStyle).LoadViewState(myState[1]);
				}
			}
		}
		
		protected override object SaveViewState() 
		{
			Debug.WriteLine("SaveViewState()");
			object baseState = base.SaveViewState();
			object pagingRepeaterPagerStyleState = (this.pagerStyle != null) ? ((IStateManager)this.pagerStyle).SaveViewState() : null;

			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = pagingRepeaterPagerStyleState;

			return myState;
		}

		protected override void TrackViewState() 
		{
			Debug.WriteLine("TrackViewState()");
			base.TrackViewState();

			if (this.pagerStyle != null)
				((IStateManager)this.pagerStyle).TrackViewState();
		}
		#endregion

		public override void DataBind()
		{
			Debug.WriteLine("DataBind()");
			// TODO: this should be fixed.
			this.CalculateSize(this.RealDataSource);
			base.DataSource = this.GetData(this.RealDataSource);
			base.DataBind();
			this.TrackViewState();
		}

		
		private void CalculateSize(object dataSource)
		{
			Debug.WriteLine("CalculateSize()");
			if ( dataSource == null )
			{
				this.PageCount = 0;
				return;
			}
			else if ( dataSource as IEnumerable == null )
			{
				this.PageCount = 0;
				return;
			}
			
			int size = 0;

			IEnumerator en = ((IEnumerable)dataSource).GetEnumerator();
				
			while ( en.MoveNext() )
			{
				size++;
			}
			this.PageCount = size/this.PageSize;
		}

		private ArrayList GetData(object dataSource)
		{
			Debug.WriteLine("GetData()");
			ArrayList currentPageData = new ArrayList();

			if ( dataSource as IEnumerable != null )
			{
				IEnumerator en = ((IEnumerable)dataSource).GetEnumerator();

				int start = this.PageSize * this.CurrentPageIndex;
				int count = 0;

				while ( en.MoveNext() )
				{
					if ( start <= count && (start + this.PageSize) > count )
					{
						currentPageData.Add(en.Current);
					}
					count++;
				}
			}
			else if ( dataSource != null )
			{
				throw new Exception("Unsupported data type");
			}
			return currentPageData;
		}

		#region Render Methods
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			Debug.WriteLine("Render()");

			RenderHeader(writer);
			base.Render(writer);
			RenderFooter(writer);
		}
		
		private void RenderHeader(System.Web.UI.HtmlTextWriter writer)
		{
			Debug.WriteLine("RenderHeader()");
			if ( ! this.AllowPaging ) return;
			if ( this.PagerStyle.Position == PagerPosition.Bottom ) return;

			if ( this.PagerStyle.Mode == PagerMode.NextPrev )
				RenderNextPrev(writer);
		}
				
		private void RenderFooter(System.Web.UI.HtmlTextWriter writer)
		{
			Debug.WriteLine("RenderFooter()");
			if ( ! this.AllowPaging ) return;
			if ( this.PagerStyle.Position == PagerPosition.Top ) return;

			if ( this.PagerStyle.Mode == PagerMode.NextPrev )
				RenderNextPrev(writer);
		}

		private void RenderNextPrev(System.Web.UI.HtmlTextWriter writer)
		{
			Debug.WriteLine("RenderNextPrev()");
			Table table = new Table();
			TableRow tr = new TableRow();
			TableCell tc = new TableCell();

			tr.Cells.Add(tc);
			table.Rows.Add(tr);

			table.Width = this.PagerStyle.Width;
			table.Height = this.PagerStyle.Height;
			tc.Wrap = this.PagerStyle.Wrap;

			HyperLink prev = new HyperLink();
			prev.Text = this.PagerStyle.PrevPageText;
			if (this.CurrentPageIndex > 0) prev.NavigateUrl = CreateHREF(this.CurrentPageIndex - 1);

			Literal literal = new Literal();
			literal.Text = "&nbsp;";

			HyperLink next = new HyperLink();
			next.Text = this.PagerStyle.NextPageText;
			if (this.CurrentPageIndex < (this.PageCount - 1)) next.NavigateUrl = CreateHREF(this.CurrentPageIndex + 1);

			tc.Controls.Add(prev);
			tc.Controls.Add(literal);
			tc.Controls.Add(next);
				
			this.PagerStyle.AddAttributesToRender(writer);
			table.RenderControl(writer);
		}

		private string CreateHREF(int pageIndex)
		{
			Debug.WriteLine("CreateHREF()");
			return Page.ClientScript.GetPostBackClientHyperlink(this, pageIndex.ToString());
		}
		#endregion

		#region Properties
		[
		Category("Style"),
		Description("The style to be applied to the paging controls."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty),
		]
		public virtual PagingRepeaterPagerStyle PagerStyle
		{
			get
			{
				if (this.pagerStyle == null) 
				{
					this.pagerStyle = new PagingRepeaterPagerStyle(this);
					if (this.IsTrackingViewState)
						((IStateManager)this.pagerStyle).TrackViewState();
				}
				return this.pagerStyle;
			}
		}

		
		[
		Bindable(true),
		Category("Data"),
		DefaultValue(null),
		Description("The data source used to build up the control."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public override object DataSource
		{
			get
			{
				Debug.WriteLine("Get_DataSource()");
				return this.GetData(this.RealDataSource); 
			}
			set
			{
				Debug.WriteLine("Set_DataSource()");
				this.CurrentPageIndex = 0;
				this.RealDataSource = value;
			}
		}

		private object RealDataSource
		{
			get { return ViewState["RealDataSource"]; }
			set { ViewState["RealDataSource"] = value; }
		}
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(true),
		Description("")
		]
		public virtual bool AllowPaging
		{
			get{ return ViewState["AllowPaging"] == null ? true :  (bool)ViewState["AllowPaging"];}
			set{ ViewState["AllowPaging"] = value;}
		}		
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(0),
		Description("")
		]
		public virtual int CurrentPageIndex
		{
			get{ return ViewState["CurrentPageIndex"] == null ? 0 : (int)ViewState["CurrentPageIndex"];}
			set{ ViewState["CurrentPageIndex"] = value;}
		}		
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(true),
		Description("")
		]
		public virtual int PageCount
		{
			get{ return ViewState["PageCount"] == null ? 0 : (int)ViewState["PageCount"];}
			set{ ViewState["PageCount"] = value;}
		}		
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(10),
		Description("")
		]
		public virtual int PageSize
		{
			get{ return ViewState["PageSize"] == null ? 10 : (int)ViewState["PageSize"];}
			set{ ViewState["PageSize"] = value;}
		}
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(true),
		Description("")
		]
		public virtual bool ShowFooter
		{
			get{ return ViewState["ShowFooter"] == null ? true : (bool)ViewState["ShowFooter"];}
			set{ ViewState["ShowFooter"] = value;}
		}
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(true),
		Description("")
		]
		public virtual bool ShowHeader
		{
			get{ return ViewState["ShowHeader"] == null ? true : (bool)ViewState["ShowHeader"];}
			set{ ViewState["ShowHeader"] = value;}
		}
		#endregion

		#region Event Handling
		public void RaisePostBackEvent(string eventArgument)
		{        
			Debug.WriteLine("RaisePostBackEvent()");
			this.OnPageIndexChanged(new PagingRepeaterPageChangedEventArgs(this, int.Parse(eventArgument)));
		}

		protected virtual void OnPageIndexChanged(PagingRepeaterPageChangedEventArgs e)
		{
			Debug.WriteLine("OnPageIndexChanged()");
			this.CurrentPageIndex = e.NewPageIndex;
			if ( this.PageIndexChanged != null )
				this.PageIndexChanged(this,e);
		}
		#endregion
	}

	public delegate void PagingRepeaterPageChangedEventHandler(object source,PagingRepeaterPageChangedEventArgs e);

	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class PagingRepeaterPageChangedEventArgs : EventArgs
	{
		private object commandSource;
		private int newPageIndex;

		public PagingRepeaterPageChangedEventArgs(object commandSource,int newPageIndex)
		{
			this.commandSource = commandSource;
			this.newPageIndex = newPageIndex;
		}

		public object CommandSource { get { return this.commandSource; } }
		public int NewPageIndex { get { return this.newPageIndex; } }
	}


	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class PagingRepeaterPagerStyle : TableItemStyle
	{
		private PagingRepeater owner;

		public PagingRepeaterPagerStyle(PagingRepeater owner)
		{
			this.owner = owner;
		}

		[DefaultValue(PagerMode.NextPrev)]
		public PagerMode Mode
		{
			get{ return ViewState["Mode"] == null ? PagerMode.NextPrev : (PagerMode)ViewState["Mode"];}
			set{ ViewState["Mode"] = value;}
		}
		[DefaultValue("Next")]
		public string NextPageText
		{
			get{ return ViewState["NextPageText"] == null ? "Next" : (string)ViewState["NextPageText"];}
			set{ ViewState["NextPageText"] = value;}
		}
		[DefaultValue("Prev")]
		public string PrevPageText
		{
			get{ return ViewState["PrevPageText"] == null ? "Prev" : (string)ViewState["PrevPageText"];}
			set{ ViewState["PrevPageText"] = value;}
		}
		[DefaultValue(0)]
		public int PageButtonCount
		{
			get{ return ViewState["PageButtonCount"] == null ? 0 : (int)ViewState["PageButtonCount"];}
			set{ ViewState["PageButtonCount"] = value;}
		}
		[DefaultValue(PagerPosition.TopAndBottom)]
		public PagerPosition Position
		{
			get{ return ViewState["Position"] == null ? PagerPosition.TopAndBottom : (PagerPosition)ViewState["Position"];}
			set{ ViewState["Position"] = value;}
		}
		[DefaultValue(true)]
		public bool Visible
		{
			get{ return ViewState["Visible"] == null ? true : (bool)ViewState["Visible"];}
			set{ ViewState["Visible"] = value;}
		}

		public override void Reset()
		{
			//this.ViewState.Clear();
			base.Reset();
		}
//			
//		internal new void TrackViewState() 
//		{
//			base.TrackViewState();
//		}
	}
}

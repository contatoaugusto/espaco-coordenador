using System;

namespace EC.Common
{
	public interface IPage
	{
		void BindPage();
		void BindParams();
		void BindDropDownList();
		void BindListBox();
		bool BindPagination();

//		#region Interface IPage
//		public void BindPage()
//		{
//			throw new NotImplementedException();
//		}
//
//		public void BindParams()
//		{
//			throw new NotImplementedException();
//		}
//
//		public void BindDropDownList()
//		{
//			throw new NotImplementedException();
//		}
//
//		public void BindListBox()
//		{
//			throw new NotImplementedException();
//		}
//
//		public bool BindPagination()
//		{
//			return true;
//		}
//		#endregion
	}

	public interface IReportPage
	{
		void BindParams();
		void BindReport();
		
		//		#region Interface IReportPage
		//		public void BindParams()
		//		{
		//			throw new NotImplementedException();
		//		}
		//
		//		public void BindReport()
		//		{
		//			throw new NotImplementedException();
		//		}
		//
		//		#endregion
	}



}

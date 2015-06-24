using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using DXWebFormsEF.Data;

namespace DXWebFormsEF.Sample
{
	 public partial class Default : System.Web.UI.Page
	 {		  
		  protected void Page_Load(object sender, EventArgs e)
		  {

		  }

		  protected void EntityServerModeDataSource1_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
		  {
				e.KeyExpression = "ArtistId";
		  }

		  protected void EntityServerModeDataSource1_Inserting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceEditEventArgs e)
		  {
				var item = new Artist() { Name = (string)e.Values["Name"] };
				using (ChinookModel context = new ChinookModel())
				{
					 context.Artist.Add(item);
					 context.SaveChanges();
				}
				e.Handled = true;
		  }

		  protected void EntityServerModeDataSource1_Updating(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceEditEventArgs e)
		  {
				int id = (int)e.Keys[ASPxGridView1.KeyFieldName];
				using (ChinookModel context = new ChinookModel())
				{
					 var item = context.Artist.Find(id);
					 if (item != null)
					 {
						  item.Name = (string)e.Values["Name"];
						  context.SaveChanges();
					 }
				}
				e.Handled = true;
		  }

		  protected void EntityServerModeDataSource1_Deleting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceEditEventArgs e)
		  {
				int id = (int)e.Keys[ASPxGridView1.KeyFieldName];

				using (ChinookModel context = new ChinookModel())
				{
					 var item = context.Artist.Find(id);
					 if (item != null)
					 {
						  context.Artist.Remove(item);
						  context.SaveChanges();
					 }
				}
				e.Handled = true;
		  }

		  protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
		  {
				ASPxGridView gv = sender as ASPxGridView;
				if ((gv != null) && (gv.DataSourceID == EntityServerModeDataSource1.ID))
					 gv.CancelEdit();
		  }

		  protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
		  {
				ASPxGridView gv = sender as ASPxGridView;
				if ((gv != null) && (gv.DataSourceID == EntityServerModeDataSource1.ID))
					 gv.CancelEdit();
		  }
	 }
}
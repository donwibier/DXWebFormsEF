using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXWebFormsEF.Data
{
	 [DataObject(true)]
	 public class ArtistDatasource
	 {
		  private readonly ChinookModel context = new ChinookModel();

		  [DataObjectMethod(DataObjectMethodType.Select, false)]
		  public IEnumerable<Artist> Select()
		  {

				var result = from n in context.Artist
								 select n;
				return result;
		  }

		  [DataObjectMethod(DataObjectMethodType.Insert, false)]
		  public void Insert(Artist newItem)
		  {
				// Validate your input here !!!!
				//if (!IsValidNew(newItem))
				//		  throw new Exception("Invalid data to insert");
				//insert your item in the dataStore
				context.Artist.Add(newItem);
				context.SaveChanges();
		  }

		  [DataObjectMethod(DataObjectMethodType.Update, false)]
		  public void Update(Artist updatedItem)
		  {
				// Validate your input here !!!!
				//if (!IsValidUpdate(updatedItem))
				//		  throw new Exception("Invalid data to update");
				// update your item in the dataStore
				var item = context.Artist.Find(updatedItem.ArtistId);
				if (item != null)
				{
					 item.Name = updatedItem.Name;
					 context.SaveChanges();
				}
		  }

		  [DataObjectMethod(DataObjectMethodType.Delete, false)]
		  public void Delete(Artist deletedItem)
		  {
				// Validate your input here !!!!
				//if (!IsValidDelete(updatedItem))
				//		  throw new Exception("Invalid data to delete");
				// delete your item in the dataStore
				var item = context.Artist.Find(deletedItem.ArtistId);
				if (item != null)
				{
					 context.Artist.Remove(item);
					 context.SaveChanges();
				}
		  }
	 }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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

		  #region advanced ObjectDatasource methods
		  [DataObjectMethod(DataObjectMethodType.Select, false)]
		  public IEnumerable<Artist> SelectPaged(string searchText, string sortExpression, int startRowIndex, int maximumRows)
		  {
				if (String.IsNullOrEmpty(sortExpression))
					 sortExpression = "ArtistId ASC";

				string[] sort = sortExpression.Split(' ');
				string sortKey = sort[0];
				bool sortAsc = sort[1] == "ASC";

				IQueryable<Artist> query = context.Artist;

				var param = Expression.Parameter(typeof(Artist), "a");
				Expression body = Expression.Property(param, sortKey);
				if (body.Type == typeof(int))
				{
					 var s = Expression.Lambda<Func<Artist, int>>(body, param);
					 query = sortAsc ? query.OrderBy(s) : query.OrderByDescending(s);
				}
				else if (body.Type == typeof(DateTime))
				{
					 var s = Expression.Lambda<Func<Artist, DateTime>>(body, param);
					 query = sortAsc ? query.OrderBy(s) : query.OrderByDescending(s);
				}
				else
				{
					 var s = Expression.Lambda<Func<Artist, object>>(body, param);
					 query = sortAsc ? query.OrderBy(s) : query.OrderByDescending(s);
				}

				if (!String.IsNullOrEmpty(searchText))
					 query = query.Where(a => a.Name.Contains(searchText ?? ""));

				context.Configuration.LazyLoadingEnabled = false;

				var result = query
					 .Skip(startRowIndex)
					 .Take(maximumRows).ToList();

				return result;
		  }

		  public int SelectPagedCount(string searchText, string sortExpression, int startRowIndex, int maximumRows)
		  {
				IQueryable<Artist> result = context.Artist;

				if (!String.IsNullOrEmpty(searchText))
					 result = result.Where(a => a.Name.Contains(searchText));

				return result.Count();
		  }
		  #endregion
	 }

}

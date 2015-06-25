using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DX.Data;
using DX.Data.Linq;

namespace DXWebFormsEF.Data
{
	 //ArtistDatasource : EntityFrameworkObjectDataSource<ChinookModel, Artist>

	 //ATTENTION: Please reference the package DXEFLinqExtensions from NuGet!
	 [DataObject(true)]
	 public class ArtistDatasource : DX.Data.EntityFrameworkObjectDataSource<ChinookModel, Artist>
	 {
		  [DataObjectMethod(DataObjectMethodType.Select, false)]
		  public IEnumerable<Artist> Select()
		  {
				var result = from n in DBContext.Artist
								 select n;
				return result;
		  }

		  public int SelectCount()
		  {
				var result = DBContext.Artist;
				return result.Count();
		  }

		  #region Custom Selection methods
		  [DataObjectMethod(DataObjectMethodType.Select, false)]
		  public IEnumerable<Artist> SelectPaged(string searchText, string sortExpression, int startRowIndex, int maximumRows)
		  {
				DBContext.Configuration.LazyLoadingEnabled = false;

				IQueryable<Artist> query = DBContext.Artist.OrderBy(sortExpression, "ArtistId");
				if (!String.IsNullOrEmpty(searchText))
					 query = query.Where(a => a.Name.Contains(searchText ?? ""));

				return query
					 .Skip(startRowIndex)
					 .Take(maximumRows).ToList();
		  }



		  public int SelectPagedCount(string searchText, string sortExpression, int startRowIndex, int maximumRows)
		  {
				IQueryable<Artist> result = DBContext.Artist;

				if (!String.IsNullOrEmpty(searchText))
					 result = result.Where(a => a.Name.Contains(searchText));

				return result.Count();
		  }
		  #endregion

		  #region CRUD Operations

		  [DataObjectMethod(DataObjectMethodType.Insert, false)]
		  public override void Insert(Artist item)
		  {
				// Validate your input here			
				if (!ValidateInsert(item))
					 throw new Exception("Validation failed on insert");
				//insert your item in the dataStore
				DBContext.Artist.Add(item);
				DBContext.SaveChanges();
		  }

		  [DataObjectMethod(DataObjectMethodType.Update, false)]
		  public override void Update(Artist item)
		  {
				// Validate your input here
				if (!ValidateUpdate(item))
					 throw new Exception("Validation failed on update");
				// update your item in the dataStore
				var m = DBContext.Artist.Find(item.ArtistId);
				if (m != null)
				{
					 //TODO: Update properties
					 m.Name = item.Name;
					 DBContext.SaveChanges();
				}
		  }

		  [DataObjectMethod(DataObjectMethodType.Delete, false)]
		  public override void Delete(Artist item)
		  {
				// Validate your input here 
				if (!ValidateDelete(item))
					 throw new Exception("Validation failed on delete");
				// delete your item in the dataStore
				var m = DBContext.Artist.Find(item.ArtistId);
				if (m != null)
				{
					 DBContext.Artist.Remove(m);
					 DBContext.SaveChanges();
				}
		  }

		  #endregion

		  #region CRUD Validation

		  public override bool ValidateInsert(Artist item) { return true; }

		  public override bool ValidateUpdate(Artist item) { return true; }

		  public override bool ValidateDelete(Artist item) { return true; }

		  #endregion
	 }
		
}

using Autofac;
using Library.Domain.Features.Catalog;
using Library.Infrastructure;
using System.Web;

namespace Library.Web.Areas.Admin.Models
{
	public class BookListModel
	{
		private ILifetimeScope _scope;
		private IBookManagementService _bookManagementService;
		public BookSearch SearchItem { get; set; }

		public BookListModel()
		{
		}

		public BookListModel(IBookManagementService bookManagementService)
		{
			_bookManagementService = bookManagementService;
		}

		public void Resolve(ILifetimeScope scope)
		{
			_scope = scope;
			_bookManagementService = _scope.Resolve<IBookManagementService>();
		}

		public async Task<object> GetPagedBooksAsync(DataTablesAjaxRequestUtility dataTablesUtility)
		{
			var data = await _bookManagementService.GetPagedBooksAsync(
				dataTablesUtility.PageIndex,
				dataTablesUtility.PageSize,
				SearchItem.Title,
				SearchItem.BookPriceFrom,
				SearchItem.BookPriceTo,
				dataTablesUtility.GetSortText(new string[] { "Title", "Price" }));

			return new
			{
				recordsTotal = data.total,
				recordsFiltered = data.totalDisplay,
				data = (from record in data.records
						select new string[]
						{
								HttpUtility.HtmlEncode(record.Title),
								record.Price.ToString(),
								record.Id.ToString()
						}
					).ToArray()
			};
		}

		internal async Task DeleteBookAsync(Guid id)
		{
			await _bookManagementService.DeleteBookAsync(id);
		}
	}
}

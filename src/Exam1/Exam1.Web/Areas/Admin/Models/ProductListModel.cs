using Autofac;
using Exam1.Domain.Features.Inventory;
using Exam1.Application.Features.Inventory;
using Exam1.Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Web;
using Exam1.Web.Areas.Admin.Models;

namespace Exam1.Web.Areas.Admin.Models
{
    public class ProductListModel
    {
		private ILifetimeScope _scope;
		private IProductManagementService _productManagementService;
		public ProductSearch SearchItem { get; set; }

		public ProductListModel()
        {
        }

		public ProductListModel(IProductManagementService productManagementService)
		{
            _productManagementService = productManagementService;
		}

		public void Resolve(ILifetimeScope scope)
		{
			_scope = scope;
            _productManagementService = _scope.Resolve<IProductManagementService>();
		}

		public async Task<object> GetPagedProductsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
		{
			var data = await _productManagementService.GetPagedProductsAsync(
				dataTablesUtility.PageIndex,
				dataTablesUtility.PageSize,
				SearchItem.Name,
				SearchItem.ProductPriceFrom,
				SearchItem.ProductPriceTo,
				dataTablesUtility.GetSortText(new string[] { "Name", "Price", "Weight" }));

			return new
			{
				recordsTotal = data.total,
				recordsFiltered = data.totalDisplay,
				data = (from record in data.records
						select new string[]
						{
								HttpUtility.HtmlEncode(record.Name),
								HttpUtility.HtmlEncode(record.Price).ToString(),
								record.Weight.ToString(),
								record.Id.ToString()
						}
					).ToArray()
			};
		}

		internal async Task DeleteProductAsync(Guid id)
        {
            await _productManagementService.DeleteProductAsync(id);
        }
    }
}

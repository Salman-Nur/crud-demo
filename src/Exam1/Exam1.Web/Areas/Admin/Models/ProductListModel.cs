using Autofac;
using Exam1.Application.Features.Inventory.Services;
using Exam1.Infrastructure;
using System.Web;

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
        internal void Resolve(ILifetimeScope scope)
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
                dataTablesUtility.GetSortText(new string[] { "Name", "Description", "Weight", "Price" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.Name),
                            HttpUtility.HtmlEncode(record.Description),
                            record.Weight.ToString(),
                            record.Price.ToString(),
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

using Autofac;
using Exam1.Domain.Entities;
using Exam1.Domain.Features.Inventory;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Exam1.Web.Areas.Admin.Models
{
    public class ProductUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public uint Price { get; set; }
        public double Weight { get; set; }

        private IProductManagementService _productManagementService;

        public ProductUpdateModel()
        {

        }

        public ProductUpdateModel(IProductManagementService productManagementService)
        {
            _productManagementService = productManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _productManagementService = scope.Resolve<IProductManagementService>();
        }

        internal async Task LoadAsync(Guid id)
        {
            Product product = await _productManagementService.GetProductAsync(id);
            if (product != null)
            {
                Id = product.Id;
                Name = product.Name;
                Price = product.Price;
                Weight = product.Weight;
            }
        }

        internal async Task UpdateProductAsync()
        {
            if (!string.IsNullOrWhiteSpace(Name)
                && Price >= 0)
            {
                await _productManagementService.UpdateProductAsync(Id, Name, Price, Weight);
            }
        }
    }
}

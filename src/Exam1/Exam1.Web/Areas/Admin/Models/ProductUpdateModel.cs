using Autofac;
using Exam1.Application.Features.Inventory.Services;
using Exam1.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Web.Areas.Admin.Models
{
    public class ProductUpdateModel
    {
        private IProductManagementService _productManagementService;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public uint Price { get; set; }
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
            if (product is not null)
            {
                Id = product.Id;
                Name = product.Name;
                Description = product.Description;
                Weight = product.Weight;
                Price = product.Price;
            }
        } 
        internal async Task UpdateProductAsync()
        {
            await _productManagementService.UpdateProductAsync(Id, Name, Description, Weight, Price);
        }
    }
}

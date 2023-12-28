using Exam1.Domain.Entities;
using Exam1.Domain.Features.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features.Inventory
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public ProductManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateProductAsync(string name, uint price, double weight)
        {
            Product product = new Product
            {
                Name = name,
                Price = price,
                Weight = weight
            };
            _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _unitOfWork.ProductRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Product> records, int total, int totalDisplay)> 
            GetPagedProductsAsync(int pageIndex, int pageSize, string searchName, uint searchPriceFrom, uint searchPriceTo, string sortBy)
        {
            return await _unitOfWork.ProductRepository.GetTableDataAsync(searchName, searchPriceFrom, 
                searchPriceTo, sortBy, pageIndex, pageSize);
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task UpdateProductAsync(Guid id, string name, uint price, double weight)
        {
            var product = await GetProductAsync(id);
            if(product != null)
            {
                product.Name = name;
                product.Price = price;
                product.Weight = weight;
            }
            await _unitOfWork.SaveAsync();
        }
    }
}

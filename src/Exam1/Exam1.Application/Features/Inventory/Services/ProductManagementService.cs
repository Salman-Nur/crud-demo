using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features.Inventory.Services
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public ProductManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateProductAsync(string name, string description, double weight, uint price)
        {
            bool isNameDuplicate = await _unitOfWork.ProductRepository.DuplicateNameAsync(name);
            if (isNameDuplicate)
            {
                throw new Exception();
            }
            Product product = new Product
            { 
                Name = name, 
                Description = description,
                Weight = weight,
                Price = price 
            };
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _unitOfWork.ProductRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Product> records, int total, int totalDisplay)> 
            GetPagedProductsAsync(int pageIndex, int pageSize, string searchName, 
            uint searchPriceFrom, uint searchPriceTo, string sortBy)
        {
            return await _unitOfWork.ProductRepository.GetTableDataAsync(searchName, searchPriceFrom,
                searchPriceTo, sortBy, pageIndex, pageSize);
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task UpdateProductAsync(Guid id, string name, string description, double weight, uint price)
        {
            bool isNameDuplicate = await _unitOfWork.ProductRepository.DuplicateNameAsync(name, id);
            if (isNameDuplicate)
            {
                throw new Exception();
            }
            var product = await GetProductAsync(id);
            if(product is not null)
            {
                product.Name = name;
                product.Description = description;
                product.Weight = weight;
                product.Price = price;
            }
            await _unitOfWork.SaveAsync();
        }
    }
}

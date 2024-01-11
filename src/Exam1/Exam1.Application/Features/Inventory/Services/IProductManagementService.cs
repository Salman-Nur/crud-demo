using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features.Inventory.Services
{
    public interface IProductManagementService
    {
        Task CreateProductAsync(string name, string description, double weight, uint price);
        Task DeleteProductAsync(Guid id);
        Task<Product> GetProductAsync(Guid id);
        Task UpdateProductAsync(Guid id, string name, string description, double weight, uint price);
        Task<(IList<Product> records, int total, int totalDisplay)>
            GetPagedProductsAsync(int pageIndex, int pageSize, string searchName, uint searchPriceFrom, 
            uint searchPriceTo, string sortBy);
    }
}

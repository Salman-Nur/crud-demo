using Library.Domain;
using Library.Domain.Entities;
using Library.Domain.Features.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Catalog
{
    internal class BookManagementService : IBookManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public BookManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateBookAsync(string title, uint price)
        {
            Book book = new Book
            {
                Title = title,
                Price = price
            };

            _unitOfWork.BookRepository.Add(book);
            await _unitOfWork.SaveAsync();
        }

        public Task DeleteBookAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBookAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<(IList<Book> records, int total, int totalDisplay)> 
            GetPagedBooksAsync(int pageIndex, int pageSize, string searchTitle, uint searchFeesFrom, uint searchFeesTo, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookAsync(Guid id, string title, uint fees)
        {
            throw new NotImplementedException();
        }
    }
}

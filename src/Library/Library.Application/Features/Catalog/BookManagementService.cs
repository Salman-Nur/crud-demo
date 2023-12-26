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

        public async Task DeleteBookAsync(Guid id)
        {
            await _unitOfWork.BookRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
			return await _unitOfWork.BookRepository.GetByIdAsync(id);
		}

        public async Task<(IList<Book> records, int total, int totalDisplay)> 
            GetPagedBooksAsync(int pageIndex, int pageSize, string searchTitle, uint searchPriceFrom, uint searchPriceTo, string sortBy)
        {
            return await _unitOfWork.BookRepository.GetTableDataAsync(searchTitle,
                searchPriceFrom, searchPriceTo, sortBy, pageIndex, pageSize);
        }

        public async Task UpdateBookAsync(Guid id, string title, uint price)
        {
            var book = await GetBookAsync(id);
            if (book is not null)
            {
                book.Title = title;
                book.Price = price;
            }
            await _unitOfWork.SaveAsync();
        }
    }
}

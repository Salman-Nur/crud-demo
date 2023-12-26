using Library.Domain.Entities;
using Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book, Guid>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

		public async Task<(IList<Book> records, int total, int totalDisplay)> 
			GetTableDataAsync(string searchTitle, uint searchPriceFrom, uint searchPriceTo, string orderBy, int pageIndex, int pageSize)
		{
            Expression<Func<Book, bool>> expression = null;

            if (!string.IsNullOrWhiteSpace(searchTitle))
                expression = x => x.Title.Contains(searchTitle) &&
                (x.Price >= searchPriceFrom && x.Price <= searchPriceTo);

            return await GetDynamicAsync(expression,
                orderBy, null, pageIndex, pageSize, true);
        }
	}
}

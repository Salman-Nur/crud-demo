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

		public Task<(IList<Book> records, int total, int totalDisplay)> 
			GetTableDataAsync(string searchTitle, uint searchFeesFrom, uint searchFeesTo, string orderBy, int pageIndex, int pageSize)
		{
			throw new NotImplementedException();
		}
	}
}

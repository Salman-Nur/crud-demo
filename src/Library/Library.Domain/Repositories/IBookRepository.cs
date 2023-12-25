using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book, Guid>
    {

        Task<(IList<Book> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchTitle, uint searchFeesFrom,
                uint searchFeesTo, string orderBy, int pageIndex, int pageSize);

    }
}

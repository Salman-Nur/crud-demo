using Library.Domain.Entities;

namespace Library.Domain.Features.Catalog
{
    public interface IBookManagementService
    {
        Task CreateBookAsync(string title, uint price);
        Task DeleteBookAsync(Guid id);
        Task<Book> GetBookAsync(Guid id);

		Task<(IList<Book> records, int total, int totalDisplay)>
			GetPagedBooksAsync(int pageIndex, int pageSize, string searchTitle,
			uint searchFeesFrom, uint searchFeesTo, string sortBy);
		Task UpdateBookAsync(Guid id, string title, uint fees);
    }
}
using Autofac;
using Library.Domain.Entities;
using Library.Domain.Features.Catalog;

namespace Library.Web.Areas.Admin.Models
{
    public class BookUpdateModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public uint Price { get; set; }

        private IBookManagementService _bookService;

        public BookUpdateModel()
        {

        }

        public BookUpdateModel(IBookManagementService bookService)
        {
            _bookService = bookService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _bookService = scope.Resolve<IBookManagementService>();
        }

        internal async Task LoadAsync(Guid id)
        {
            Book book = await _bookService.GetBookAsync(id);
            if (book != null)
            {
                Id = book.Id;
                Title = book.Title;
                Price = book.Price;
            }
        }

        internal async Task UpdateBookAsync()
        {
            if (!string.IsNullOrWhiteSpace(Title)
                && Price >= 0)
            {
                await _bookService.UpdateBookAsync(Id, Title, Price);
            }
        }
    }
}

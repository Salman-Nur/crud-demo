using Autofac;
using Library.Domain.Features.Catalog;

namespace Library.Web.Areas.Admin.Models
{
    public class BookCreateModel
    {
        private ILifetimeScope _scope;
        private IBookManagementService _bookManagementService;
        public string Title { get; set; }
        public uint Price { get; set; }

        public BookCreateModel() { }

        public BookCreateModel(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _bookManagementService = _scope.Resolve<IBookManagementService>();
        }

        internal async Task CreateBookAsync()
        {
            await _bookManagementService.CreateBookAsync(Title, Price);
        }
    }
}

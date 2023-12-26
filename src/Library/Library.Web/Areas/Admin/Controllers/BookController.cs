using Autofac;
using Library.Infrastructure;
using Library.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<BookController> _logger;

        public BookController(ILifetimeScope scope,
            ILogger<BookController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = _scope.Resolve<BookCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                await model.CreateBookAsync();
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Book created successfully",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

		[HttpPost]
		public async Task<JsonResult> GetBooks(BookListModel model)
		{
			var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
			model.Resolve(_scope);
			var data = await model.GetPagedBooksAsync(dataTablesModel);
			return Json(data);
		}

        public async Task<IActionResult> Update(Guid id)
        {
            var model = _scope.Resolve<BookUpdateModel>();
            await model.LoadAsync(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BookUpdateModel model)
        {
            model.Resolve(_scope);

            if (ModelState.IsValid)
            {
                await model.UpdateBookAsync();
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Book edited successfully",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<BookListModel>();

            if (ModelState.IsValid)
            {
                await model.DeleteBookAsync(id); TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Book deleted successfully",
                    Type = ResponseTypes.Success
                });
            }
            return RedirectToAction("Index");
        }
    }
}

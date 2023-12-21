using Autofac;
using University.Infrastructure;
using University.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace University.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILifetimeScope scope, ILogger<StudentController> logger)
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
            var model = _scope.Resolve<StudentCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.CreateStudentAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create student");
                }
            }

            return View(model);
        }
        public async Task<JsonResult> GetStudents()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<StudentListModel>();

            var data = await model.GetPagedStudentsAsync(dataTablesModel);
            return Json(data);
        }
    }
}

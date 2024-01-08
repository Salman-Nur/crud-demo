using Autofac;
using Exam1.Infrastructure;
using Exam1.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILifetimeScope scope, ILogger<StudentController> logger)
        {
            _logger = logger;
            _scope = scope;
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
            if(ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.CreateStudentAsync();
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Student created successfully",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server error");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Name is duplicate",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetStudents(StudentListModel model)
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(_scope);
            var data = await model.GetPagedStudentsAsync(dataTablesModel);
            return Json(data);
        }
        public async Task<IActionResult> Update(Guid id) 
        { 
            var model = _scope.Resolve<StudentUpdateModel>();
            await model.LoadAsync(id);
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StudentUpdateModel model)
        {
            model.Resolve(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateStudentAsync();
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Student Updated successfully",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Server error");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Name is duplicate",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<StudentListModel>();

            if (ModelState.IsValid)
            {
                await model.DeleteStudentAsync(id);
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Student deleted successfully",
                    Type = ResponseTypes.Success
                });
            }

            return RedirectToAction("Index");
        }
    }

}

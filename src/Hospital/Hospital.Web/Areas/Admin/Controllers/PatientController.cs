using Autofac;
using Hospital.Infrastructure;
using Hospital.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<PatientController> _logger;
        public PatientController(ILifetimeScope scope, ILogger<PatientController> logger)
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
            var model = _scope.Resolve<PatientCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                await model.CreateBookAsync();
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Patient created successfully.",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}

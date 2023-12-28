﻿using Autofac;
using Exam1.Application;
using Exam1.Domain.Features.Inventory;
using Exam1.Infrastructure;
using Exam1.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILifetimeScope scope,
            ILogger<ProductController> logger)
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
            var model = _scope.Resolve<ProductCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                await model.CreateProductAsync();
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Product created successfully",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetProducts(ProductListModel model)
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(_scope);
            var data = await model.GetPagedProductsAsync(dataTablesModel);
            return Json(data);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var model = _scope.Resolve<ProductUpdateModel>();
            await model.LoadAsync(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductUpdateModel model)
        {
            model.Resolve(_scope);

            if (ModelState.IsValid)
            {
                await model.UpdateProductAsync();
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Product edited successfully",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<ProductListModel>();

            if (ModelState.IsValid)
            {
                await model.DeleteProductAsync(id); TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Course deleted successfully",
                    Type = ResponseTypes.Success
                });
            }
            return RedirectToAction("Index");
        }
    }
}

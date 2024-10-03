using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Book.Data;
using Book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Book.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly BookDbContext _context;

        public CategoryController(ILogger<CategoryController> logger,BookDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories;
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.Name == Convert.ToString(category.DisplayOrder)){
                ModelState.AddModelError("Name", "DisplayOrder and Name should not be same");
            }
            if(ModelState.IsValid){
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = $"Category {category.Name} Added Success";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            if(id == null || id == 0){
                return NotFound();
            }
            Category category = _context.Categories.Find(id);
            if(category == null){
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category){
            if(category.Name == Convert.ToString(category.DisplayOrder)){
                ModelState.AddModelError("Name","Display Order and Name should not be same");
            }

            if(ModelState.IsValid){
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["success"] = $"Category {category.Name} Updated Success";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            if(id == null || id == 0){
                return NotFound();
            }
            Category category = _context.Categories.Find(id);
            if(category == null){
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category){
            _context.Categories.Remove(category);
            _context.SaveChanges();

            TempData["success"] = $"Category {category.Name} Delete Success";
            return RedirectToAction("Index");
        }
    }
}
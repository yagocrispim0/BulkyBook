using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
               _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match tne name.");
            }
            // Adding a server side validation with an empty key. It doesn't bind with the name. 

            /*if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is not a valid value.");
            }*/
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                // You can add another parameter to define a different controller.
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            //Category obj = _db.Categories.FirstOrDefault(c => c.Id == id);
            
            Category objFromCategory = _db.Categories.Find(id);
            
            if (objFromCategory == null)
            {
                return NotFound();
            }
            return View(objFromCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the name.");
            }
            // Adding a server side validation with an empty key. It doesn't bind with the name. 

            /*if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is not a valid value.");
            }*/
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                // You can add another parameter to define a different controller.
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category obj = _db.Categories.FirstOrDefault(c => c.Id == id);

            Category objFromCategory = _db.Categories.Find(id);

            if (objFromCategory == null)
            {
                return NotFound();
            }
            return View(objFromCategory);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {        
            Category obj = _db.Categories.Find(id);
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}


using BookStore.DataAccess.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
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
			List<Category> CategoryList = _db.Categories.ToList();
			return View(CategoryList);
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

				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
			}
			if (obj.Name != null && obj.Name.ToLower() == "test")
			{

				ModelState.AddModelError("", "Test is an invalid value");
			}
			if (ModelState.IsValid)
			{

				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["sucess"] = "Category Created Sucessfully";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Edit(int? id)
		{

			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{

			//if (obj.Name == obj.DisplayOrder.ToString())
			//{

			//	ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
			//}
			//if (obj.Name != null && obj.Name.ToLower() == "test")
			//{

			//	ModelState.AddModelError("", "Test is an invalid value");
			//}
			if (ModelState.IsValid)
			{

				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["sucess"] = "Category Updated Sucessfully";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{

			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost]
		[ActionName("Delete")]

		public IActionResult DeletePOST(int? id)
		{
			Category? obj = _db.Categories.Find(id);
			if (ModelState.IsValid)
			{

				_db.Categories.Remove(obj);
				_db.SaveChanges();
				TempData["sucess"] = "Category Deleted Sucessfully";
				return RedirectToAction("Index");
			}
			return View();
		}

	}
}

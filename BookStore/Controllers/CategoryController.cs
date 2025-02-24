﻿using BookStore.Data;
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

			if (obj.Name==obj.DisplayOrder.ToString()) {

				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
			}
			if (  obj.Name!=null && obj.Name.ToLower() == "test")
			{

				ModelState.AddModelError("", "Test is an invalid value");
			}
			if (ModelState.IsValid)
			{

				_db.Categories.Add(obj);
				_db.SaveChanges();
			return RedirectToAction("Index");
			}
			return View();
		}

	}
}

using Bookify.Web.Core.Models;
//using Booktopia.Web.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Bookify.Web.Controllers
{
    public class CategoriesController : Controller
    {
        // _context =>     represents our database
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // return all categories in database to view Index
        public IActionResult Index()
        {
            //TODO: use viewModel
            var categories = _context.Categories
                //.Select(c => new CategoryViewModel
                //{
                //    Id = c.Id,
                //    Name = c.Name,
                //    IsDeleted = c.IsDeleted,
                //    CreatedOn = c.CreatedOn,
                //    LastUpdatedOn = c.LastUpdatedOn
                //})
                .AsNoTracking().
                ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form", model);

            var category = new Category { Name = model.Name };
            _context.Add(category);
            _context.SaveChanges();

            TempData["Message"] = "Saved successfully";


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);

            if (category is null)
                return NotFound();

            var viewModel = new CategoryFormViewModel
            {
                Id = id,
                Name = category.Name
            };

            return View("Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form", model);

            var category = _context.Categories.Find(model.Id);

            if (category is null)
                return NotFound();

            category.Name = model.Name;
            category.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();

            TempData["Message"] = "Saved successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
                return NotFound();
            category.IsDeleted = !category.IsDeleted;
            category.LastUpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return Ok(category.LastUpdatedOn.ToString());
        }
        public IActionResult AllowItem(CategoryFormViewModel model)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Name == model.Name);
            var isAllowed = category is null || category.Id.Equals(model.Id);
            return Json(isAllowed);
           
        }
    }
}
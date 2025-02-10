using aspnet_ecommerce01.data.Contexts;
using aspnet_ecommerce01.models;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_ecommerce01.Controllers
{
    public class CategoryController : Controller
    {
        // Dependency injection of the database context ApplicationDbContext
        private readonly ApplicationDbContext _db;

        // Constructor that receives the database context
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This function will display the list of categories
        /// </summary>
        public IActionResult Index()
        {
            // Get the list of categories from the database
            var categoryList = _db.Categories.ToList();

            // Return the list of categories to the view
            return View(categoryList);
        }

        // Display the view to create a new category
        public IActionResult Create()
        {
            return View();
        }

        // POST method to create a new category
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // Check if the category name is the same as the DisplayOrder
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // Add an error to the ModelState if the condition is true
                ModelState.AddModelError("Name", "The DisplayOrder cannot be exactly match the Name");
            }

            // Commented: Check if the category name is "test"
            //if (obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}

            // Check if the ModelState is valid
            if (ModelState.IsValid)
            {
                // Add the new category to the database
                _db.Categories.Add(obj);

                // Save the changes to the database
                _db.SaveChanges();

                // Store a success message in TempData to be displayed after the redirect
                TempData["success"] = "Category created successfully";

                // Redirect to the Index action
                return RedirectToAction("Index");
            }

            // Return the view with the category object if there are errors
            return View(obj);
        }

        // Display the view to edit an existing category
        public IActionResult Edit(int? id)
        {
            // Check if the id is null or zero
            if (id == null || id == 0)
            {
                // Return a 404 Not Found response if the id is invalid
                return NotFound();
            }

            // Find the category in the database using the provided id
            Category? category = _db.Categories.Find(id);

            // Check if the category was not found
            if (category == null)
            {
                // Return a 404 Not Found response if the category does not exist
                return NotFound();
            }

            // Return the view with the category object to be edited
            return View(category);
        }

        // POST method to update a category
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            // Check if the ModelState is valid
            if (ModelState.IsValid)
            {
                // Update the category in the database
                _db.Categories.Update(obj);
                // Save the changes to the database
                _db.SaveChanges();
                // Store a success message in TempData to be displayed after the redirect
                TempData["success"] = "Category updated successfully";
                // Redirect to the Index action
                return RedirectToAction("Index");
            }

            // Return the view with the category object if there are errors
            return View(obj);
        }

        // Display the view to delete an existing category
        public IActionResult Delete(int? id)
        {
            // Check if the id is null or zero
            if (id == null || id == 0)
            {
                // Return a 404 Not Found response if the id is invalid
                return NotFound();
            }

            // Find the category in the database using the provided id
            Category? category = _db.Categories.Find(id);

            // Check if the category was not found
            if (category == null)
            {
                // Return a 404 Not Found response if the category does not exist
                return NotFound();
            }

            // Return the view with the category object to be deleted
            return View(category);
        }

        // POST method to delete a category
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            // Find the category in the database using the provided id
            Category? category = _db.Categories.Find(id);

            // Check if the category was not found
            if (category == null)
            {
                // Return a 404 Not Found response if the category does not exist
                return NotFound();
            }

            // Remove the category from the database
            _db.Categories.Remove(category);
            // Save the changes to the database
            _db.SaveChanges();
            // Store a success message in TempData to be displayed after the redirect
            TempData["success"] = "Category deleted successfully";
            // Redirect to the Index action
            return RedirectToAction("Index");
        }
    }
}
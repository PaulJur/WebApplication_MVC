using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()//when newly created, the index does not exist in the Views folder
        {
            List<Category> objCategoryList = _db.Categories.ToList();//convert it to a list and assign it. It runs all the SQL code in the database and returns the data in the Index view
                                                                     //to retrieve SELECT * FROM Categories and assigns it to the object etc.
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]//Im guessing this returns the input from the user side to the back-end.
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())//if it's the same as display order, then modelstate model error is called
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if(_db.Categories.Any(c => c.DisplayOrder == obj.DisplayOrder))//This checks in the categories database if the same display number is already taken
            {
                ModelState.AddModelError("displayorder", "Cannot have an existing Display Number");
            }
            if(_db.Categories.Any(n => n.Name == obj.Name))
            {
                ModelState.AddModelError("name", "Cannot have an existing Category Name"); //This checks in the categories database if the same Name is already taken
            }
            if (ModelState.IsValid)//if everything from Category.cs is validated like maxlenght and display order, then return valid and execute
            {
                _db.Categories.Add(obj);//This will add the category object(made by the user input) to the category table. This keeps track of the changes
                _db.SaveChanges();//this makes the changes in the database table
                TempData["success"] = "Category created succesfully"; //"success" is the key name
                return RedirectToAction("Index");//This will refresh the database table and show the new stuff
            }
            return View(obj);//stays on the create category page
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);//will work off the primary key off the model


            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.CategoryId==id);           //Both of these do the same thing but need to be used if sometimes need calculations or filtering
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.CategoryId==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);//If any value from Category has a value in that ID, it will automatically populate it. Really cool !!!!!!!!!!!!!!!!!!!!!!!!!!!!! Important here
        }

        [HttpPost]//this returns the input from the user side to the back-end.
        public IActionResult Edit(Category obj)
        {
           
            if (ModelState.IsValid)//if everything from Category.cs is validated like maxlenght and display order, then return valid and execute
            {
                _db.Categories.Update(obj);//This will Update the category object(made by the user input) to the category table. This keeps track of the changes
                _db.SaveChanges();//this makes the changes in the database table
                TempData["success"] = "Category updated succesfully";
                return RedirectToAction("Index");//This will refresh the database table and show the new stuff
            }
            return View(obj);//stays on the create category page
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);//will work off the primary key off the model

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);//If any value from Category has a value in that ID, it will automatically populate it. Really cool !!!!!!!!!!!!!!!!!!!!!!!!!!!!! Important here
        }

        [HttpPost, ActionName("Delete")]//this returns the input from the user side to the back-end and the actionName is "Delete"
        //send data to a server to create/update a resource

        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);//Removes object based on the ID found 
            _db.SaveChanges();//this makes the changes in the database table
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");//This will refresh the database table and show the new stuff
        }
    }
}

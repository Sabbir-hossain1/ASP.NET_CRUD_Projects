using EcommerceProject.Data;
using EcommerceProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
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
      //Retrive Category information from Category table
      //var objectCategoryList = _db.Categories.ToList();
      IEnumerable<Category> objCategoryList = _db.Categories;
      return View(objCategoryList);
    }
    //Get Action Method
    public IActionResult Create()
    {
      return View();
    }
    //Post Action Method
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
      if (obj.Name == obj.DisplayOrder.ToString())
      {
        ModelState.AddModelError("CustomError", "Name and Date can not be same type"); // Show validation error on above
                                                                                       //ModelState.AddModelError("name", "Name and Date can not be same type"); // Show validation error below Name field
      }

      if (ModelState.IsValid)
      {
        _db.Categories.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(obj);
    }

    //Get Action Method
    public IActionResult Edit(int? id)
    {
      if (id == null || id == 0)
      {
        return NotFound();
      }
      //------------- These process using C# to retrive data based on Id-----------
      var categoryFromDb = _db.Categories.Find(id);
      //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
      //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id == id);

      //----------Retrive data using Entity FramworkCore--------
      if (categoryFromDb == null)
      {
        return NotFound(categoryFromDb);
      }
      return View(categoryFromDb);
    }

    //Post Action Method
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
      if (obj.Name == obj.DisplayOrder.ToString())
      {
        ModelState.AddModelError("CustomError", "Name and Date can not be same type"); // Show validation error on above
                                                                                       //ModelState.AddModelError("name", "Name and Date can not be same type"); // Show validation error below Name field
      }

      if (ModelState.IsValid)
      {
        _db.Categories.Update(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(obj);
    }


    //Get Action Method
    public IActionResult Delete(int? id)
    {
      if (id == null || id == 0)
      {
        return NotFound();
      }
      //------------- These process using C# to retrive data based on Id-----------
      var categoryFromDb = _db.Categories.Find(id);
      //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
      //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id == id);

      //----------Retrive data using Entity FramworkCore--------
      if (categoryFromDb == null)
      {
        return NotFound(categoryFromDb);
      }
      return View(categoryFromDb);
    }

    //Post Action Method
    //[HttpPost]
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {

      var obj = _db.Categories.Find(id);
      if(obj == null)
      {
        return NotFound();
      }

      _db.Categories.Remove(obj);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}


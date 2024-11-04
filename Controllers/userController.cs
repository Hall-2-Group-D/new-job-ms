using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.Dbent;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers
{
  public class userController : Controller
  {
    private readonly RecruiterDbcontext context;




    public userController(RecruiterDbcontext context)
    {
      this.context = context;
    }

    [HttpGet]
    public IActionResult userhell()
    {
      var user = context.tbl_users.ToList();
      List<userViewNodel> userlis = new List<userViewNodel>();

      if (user != null)
      {
        foreach(var usr in user)
        {
          var userViewNodel = new userViewNodel()
          {
            id = usr.id,
            fullname = usr.fullname,
            username  = usr.username,
            email = usr.email,
            pass = usr.pass,
            reg_date = usr.reg_date
          };
          userlis.Add(userViewNodel);
        }
        return View(userlis);
      }
      return View(userlis);
    }

    [HttpGet]
    public IActionResult createUser()
    {
      return View();
    }

    [HttpPost]
    public IActionResult createUser(userViewNodel userViewNod)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var uss = new user()
          {
            id = userViewNod.id,
            fullname = userViewNod.fullname,
            username = userViewNod.username,
            email = userViewNod.email,
            pass = userViewNod.pass,
            reg_date = userViewNod.reg_date


          };

          context.tbl_users.Add(uss);
          context.SaveChanges();
          TempData["successMessage"] = " User Created Successfully!";
          return RedirectToAction("userhell");

        }
        else
        {
          TempData["ErrorMessage"] = "Model Data Is Not Valid.";
          return View();
        }
      }
      catch (Exception ex)
      {

        TempData["ErrorMessage"] = ex.Message;
        return View();
      }
      
    }

   
  }
}

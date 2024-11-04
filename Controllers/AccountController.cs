using AspnetCoreMvcFull.adminn;
using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static AspnetCoreMvcFull.Models.Dbent.Recruiterpr;

namespace AspnetCoreMvcFull.Controllers
{
  
  public class AccountController : Controller
  {
    private readonly RecruiterDbcontext _context;

    public AccountController(RecruiterDbcontext RecruiterDbcontext)
    {
      _context = RecruiterDbcontext;
        
    }
    public IActionResult RegistrationAdmin()
    {
      return View();
    }

    

    [HttpPost]
    public IActionResult RegistrationAdmin(RegistrationViewModel model)
    {
      if(ModelState.IsValid)
      {
        AdminAccount account = new AdminAccount();
        account.fullname = model.fullname;
        account.username = model.username;
        account.email = model.email;
        account.pass = model.pass;

        try
        {
          _context.tbl_admin_user.Add(account);
          _context.SaveChanges();

          ModelState.Clear();
          ViewBag.Message = $"{account.fullname} Registered SuccessFully.";

        }
        catch (DbUpdateException ex)
        {


          ModelState.AddModelError("", "Please Enter Unique Email Or Paaword");
          return View(model);

        }
        return View();
      }
      return View(model);
    }

    public IActionResult Account()
    {
      return View();
    }

    [HttpPost]

    public IActionResult Account(LoginAdminViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = _context.tbl_admin_user.Where(x => x.username == model.username && x.pass == model.pass).FirstOrDefault();
        if (user != null)
        {
          

         var claims = new List<Claim>
         {
            new Claim(ClaimTypes.Name, user.email),
            new Claim("Name", user.fullname),
            new Claim(ClaimTypes.Role, "User"),
         };

          var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
          HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimsIdentity));

          return RedirectToAction("Index", "Dashboards");
        }
        else
        {
          ModelState.AddModelError("", "Username Or Password Is Not Correct");
        }
      }
      return View();
    }


    public IActionResult LogOut()
    {
      HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return RedirectToAction("Account");
    }


    [HttpGet]
    public IActionResult Admindabacid()
    {
      var user = _context.tbl_admin_user.ToList();
      List<RegistrationViewModel> userlis = new List<RegistrationViewModel>();

      if (user != null)
      {
        foreach (var usr in user)
        {
          var AdminRegis = new RegistrationViewModel()
          {
            fullname = usr.fullname,
            username = usr.username,
            email = usr.email,
          };
          userlis.Add(AdminRegis);
        }
        return View(userlis);
      }
      return View(userlis);
    }




  }
}

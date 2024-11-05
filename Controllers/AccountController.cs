using AspnetCoreMvcFull.adminn;
using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.Dbent;
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
      if (!ModelState.IsValid)
      {
        TempData["ErrorMessage"] = "Model data is not valid. Please review the entries.";
        return View(model); // Return the view with existing model data to show form validation errors
      }

      try
      {
        // Check if the username already exists
        bool usernameExists = _context.tbl_admin_user.Any(u => u.username == model.username);
        if (usernameExists)
        {
          TempData["ErrorMessage"] = "The username already exists. Please choose a different username.";
          return View(model); // Return the same view to allow user correction
        }

        // Proceed to create new account
        var account = new AdminAccount
        {
          fullname = model.fullname,
          username = model.username,
          email = model.email,
          pass = model.pass,
        };

        _context.tbl_admin_user.Add(account);
        _context.SaveChanges();
        TempData["successMessage"] = "User created successfully!";
        return View(); // Redirect to a confirmation or a success page to avoid resubmission
      }
      catch (Exception ex)
      {
        TempData["ErrorMessage"] = $"Error: {ex.Message}";
        return View(model); // Return the view with error messages
      }
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

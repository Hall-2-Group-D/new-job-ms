using AspnetCoreMvcFull.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers
{
  public class logController : Controller
  {
    private readonly RecruiterDbcontext context;

    public logController(RecruiterDbcontext context)
    {
      this.context = context;
    }
    public IActionResult loggin()
    {
      var userlg =context.tbl_users.ToList();
      return View(userlg);
    }
  }
}

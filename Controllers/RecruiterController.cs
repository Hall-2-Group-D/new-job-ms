using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.Dbent;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers
{
  public class RecruiterController : Controller
  {
    private readonly RecruiterDbcontext context;

    public RecruiterController(RecruiterDbcontext context)
    {
      this.context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
      var recruiter =context.tbl_Recruiterprs.ToList();
      List<Recruiterviewmodel> recruiterlist = new List<Recruiterviewmodel>();
      if (recruiter != null)
      {

        foreach (var rec in recruiter)
        {
          var recruiterviewmodel = new Recruiterviewmodel()
          {
            id= rec.id,
            company_name= rec.company_name,
            company_website= rec.company_website,
            contact_number= rec.contact_number,
            location= rec.location,
            reg_date= rec.reg_date,
            user_id= rec.user_id,


          };
          recruiterlist.Add(recruiterviewmodel);

        }
        return View(recruiterlist);

      }
      return View(recruiterlist);
    }

    [HttpGet]
    public IActionResult createRecruiter()
    {

      return View();
    }

    [HttpPost]
    public IActionResult createRecruiter(Recruiterviewmodel RecruiterviewmData)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var recrui = new Recruiterpr()
          {
            company_name = RecruiterviewmData.company_name,
            company_website = RecruiterviewmData.company_website,
            contact_number = RecruiterviewmData.contact_number,
            location = RecruiterviewmData.location,
            reg_date = RecruiterviewmData.reg_date,
            user_id = RecruiterviewmData.user_id,


          };

          context.tbl_Recruiterprs.Add(recrui);
          context.SaveChanges();
          TempData["successMessage"] = "Recruiter Created Successfully!";
          return RedirectToAction("Index");

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

    [HttpGet]
    public IActionResult edit(int id)
    {
      try
      {
        var Recruite = context.tbl_Recruiterprs.SingleOrDefault(x => x.id == id);

        if (Recruite != null)
        {
          var Recruiterview = new Recruiterviewmodel()
          {
            id = Recruite.id,
            company_name = Recruite.company_name,
            company_website = Recruite.company_website,
            contact_number = Recruite.contact_number,
            location = Recruite.location,
            reg_date = Recruite.reg_date,
            user_id = Recruite.user_id
          };
          return View(Recruiterview);
        }
        else
        {
          TempData["ErrorMessage"] = $"Recruiter Details Not Available with the id: {id}";
          return RedirectToAction("index");
        }
      }
      catch (Exception ex)
      {

        TempData["ErrorMessage"] = ex.Message;
        return RedirectToAction("index"); 
      }
    }

    [HttpPost]
    public IActionResult edit(Recruiterviewmodel model)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var Recruit = new Recruiterpr()
          {
            id = model.id,
            company_name = model.company_name,
            company_website = model.company_website,
            contact_number = model.contact_number,
            location = model.location,
            reg_date = model.reg_date,
            user_id = model.user_id

          };
          context.tbl_Recruiterprs.Update(Recruit);
          context.SaveChanges();
          TempData["successMessage"] = " Recruiter Updated ";
          return RedirectToAction("index");
        }
        else
        {
          TempData["ErrorMessage"] = " Model Data Is Invalid";
          return View();
        }
      }
      catch (Exception ex)
      {

        TempData["ErrorMessage"] = ex.Message;
        return View();
      }
      
    }


    [HttpGet]
    public IActionResult delete(int id)
    {
      try
      {
        var Recruite = context.tbl_Recruiterprs.SingleOrDefault(x => x.id == id);

        if (Recruite != null)
        {
          var Recruiterview = new Recruiterviewmodel()
          {
            id = Recruite.id,
            company_name = Recruite.company_name,
            company_website = Recruite.company_website,
            contact_number = Recruite.contact_number,
            location = Recruite.location,
            reg_date = Recruite.reg_date,
            user_id = Recruite.user_id
          };
          return View(Recruiterview);
        }
        else
        {
          TempData["ErrorMessage"] = $"Recruiter Details Not Available with the id: {id}";
          return RedirectToAction("index");
        }
      }
      catch (Exception ex)
      {

        TempData["ErrorMessage"] = ex.Message;
        return RedirectToAction("index");
      }
    }

    [HttpPost]

    public IActionResult delete(Recruiterviewmodel modal)
    {
      try
      {
        var Recruit = context.tbl_Recruiterprs.SingleOrDefault(x => x.id == modal.id);
        if (Recruit != null)
        {
          context.tbl_Recruiterprs.Remove(Recruit);
          context.SaveChanges();
          TempData["successMessage"] = " Recruiter Deleted ";
          return RedirectToAction("index");
        }
        else
        {
          TempData["ErrorMessage"] = $"Recruiter Details Not Available with the id: {modal.id}";
          return RedirectToAction("index");
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

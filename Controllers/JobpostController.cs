using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Controllers
{
  public class JobpostController : Controller
  {
    private readonly RecruiterDbcontext jbpostDbContext;

    public JobpostController(RecruiterDbcontext jbpostDbContext)
    {
      this.jbpostDbContext = jbpostDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var joblist = await jbpostDbContext.Tbl_jobpostModels.ToListAsync();
      return View(joblist);
    }

    // Add job post functionality
    [HttpPost]
    public async Task<IActionResult> Add(jobpostviewModel jobpostRequest)
    {
      var jobpostModel = new jobpostModel()
      {
        Id = Guid.NewGuid(),
        JobTitle = jobpostRequest.JobTitle,
        JobDescription = jobpostRequest.JobDescription,
        Requirements = jobpostRequest.Requirements,
        Category = jobpostRequest.Category,
        Location = jobpostRequest.Location,
        SalaryRange = jobpostRequest.SalaryRange,
        Deadline = jobpostRequest.Deadline,
        Status = jobpostRequest.Status,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now
      };

      await jbpostDbContext.Tbl_jobpostModels.AddAsync(jobpostModel);
      await jbpostDbContext.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    // Edit job post functionality
    [HttpGet]
    public async Task<IActionResult> EditJobpost(Guid id)
    {
      var jobpost = await jbpostDbContext.Tbl_jobpostModels.FirstOrDefaultAsync(x => x.Id == id);
      if (jobpost == null)
      {
        TempData["ErrorMessage"] = "Job post not found.";
        return RedirectToAction("Index");
      }

      var model = new updatejobpostModel()
      {
        Id = jobpost.Id,
        JobTitle = jobpost.JobTitle,
        JobDescription = jobpost.JobDescription,
        Requirements = jobpost.Requirements,
        Category = jobpost.Category,
        Location = jobpost.Location,
        SalaryRange = jobpost.SalaryRange,
        Deadline = jobpost.Deadline,
        Status = jobpost.Status,
        CreatedAt = jobpost.CreatedAt,
        UpdatedAt = DateTime.Now
      };

      return View("Editjobpostview", model); // Use your existing view for edit
    }

    [HttpPost]
    public async Task<IActionResult> EditJobpost(updatejobpostModel model)
    {
      var jobpost = await jbpostDbContext.Tbl_jobpostModels.FindAsync(model.Id);
      if (jobpost == null)
      {
        TempData["ErrorMessage"] = "Job post not found.";
        return RedirectToAction("Index");
      }

      jobpost.JobTitle = model.JobTitle;
      jobpost.JobDescription = model.JobDescription;
      jobpost.Requirements = model.Requirements;
      jobpost.Category = model.Category;
      jobpost.Location = model.Location;
      jobpost.SalaryRange = model.SalaryRange;
      jobpost.Deadline = model.Deadline;
      jobpost.Status = model.Status;
      jobpost.UpdatedAt = DateTime.Now;

      jbpostDbContext.Tbl_jobpostModels.Update(jobpost);
      await jbpostDbContext.SaveChangesAsync();

      TempData["SuccessMessage"] = "Job post updated successfully.";
      return RedirectToAction("Index");
    }

    // Delete job post functionality
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
      var jobpost = await jbpostDbContext.Tbl_jobpostModels.FindAsync(id);
      if (jobpost == null)
      {
        TempData["ErrorMessage"] = "Job post not found.";
        return RedirectToAction("Index");
      }

      jbpostDbContext.Tbl_jobpostModels.Remove(jobpost);
      await jbpostDbContext.SaveChangesAsync();

      TempData["SuccessMessage"] = "Job post deleted successfully.";
      return RedirectToAction("Index");
    }

    // Get job post by ID as JSON
    [HttpGet]
    public async Task<IActionResult> GetJobById(Guid id)
    {
      var jobPost = await jbpostDbContext.Tbl_jobpostModels.FindAsync(id);
      if (jobPost == null)
      {
        return NotFound();
      }
      return Json(jobPost);
    }
  }
}

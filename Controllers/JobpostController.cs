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
        CompanyName = jobpostRequest.CompanyName,
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
        CompanyName =jobpost.CompanyName,
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
      jobpost.CompanyName = model.CompanyName;
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
    //hataban please
    [HttpGet]
    public async Task<IActionResult> JobCards()
    {
      var jobs = await jbpostDbContext.Tbl_jobpostModels.ToListAsync();
      return View(jobs);
    }

    [HttpPost]
    public async Task<IActionResult> ApplyForJob(JobApplication application, IFormFile resume)
    {
      if (resume == null || resume.Length == 0)
      {
        return Content("<script>alert('Resume file is required.'); window.location.href = document.referrer;</script>", "text/html");
      }

      var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
      var fileExtension = Path.GetExtension(resume.FileName).ToLower();

      if (!allowedExtensions.Contains(fileExtension))
      {
        return Content("<script>alert('Invalid file type. Only PDF, DOCX, and XLSX files are accepted.'); window.location.href = document.referrer;</script>", "text/html");
      }

      var resumeDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resumes");
      Directory.CreateDirectory(resumeDirectory);

      var newFileName = Guid.NewGuid().ToString() + fileExtension;
      var filePath = Path.Combine(resumeDirectory, newFileName);

      try
      {
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await resume.CopyToAsync(stream);
        }

        application.ResumePath = filePath;

        if (ModelState.IsValid)
        {
          jbpostDbContext.Tbl_JobApplications.Add(application);
          await jbpostDbContext.SaveChangesAsync();
          return Content("<script>alert('Application submitted successfully. We will contact you via email As soon.'); window.location.href = document.referrer;</script>", "text/html");
        }

        var errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
        return Content($"<script>alert('{string.Join("; ", errorMessages)}'); window.location.href = document.referrer;</script>", "text/html");
      }
      catch (Exception ex)
      {
        return Content($"<script>alert('An error occurred while processing your request: {ex.Message}'); window.location.href = document.referrer;</script>", "text/html");
      }
    }


  }
}
    

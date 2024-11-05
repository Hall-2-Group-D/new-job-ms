namespace AspnetCoreMvcFull.Models
{
  public class jobpostModel
  {
    public Guid Id { get; set; } // Unique identifier for each job post

    public string CompanyName { get; set; } // New field for the company name


    public string JobTitle { get; set; } // Matches "Job Title" field

    public string JobDescription { get; set; } // Matches "Job Description" field

    public string Requirements { get; set; } // Matches "Requirements" field

    public string Category { get; set; } // Matches "Category" field

    public string Location { get; set; } // Matches "Location" field

    public string SalaryRange { get; set; } // Matches "Salary Range" field

    public DateTime Deadline { get; set; } // Matches "Application Deadline" field

    public JobStatus Status { get; set; } // Matches "Status" dropdown

    public DateTime CreatedAt { get; set; } = DateTime.Now; // Automatically set to current time

    public DateTime UpdatedAt { get; set; } = DateTime.Now; // Automatically updated when edited
  }

  // Enum for Job Status
  public enum JobStatus
  {
    Active,
    Inactive
  }





  public class JobApplication
  {
    public Guid Id { get; set; } // Unique identifier for each application
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ContactNumber { get; set; }
    public String? ResumePath { get; set; } // Path to stored resume file
    public string? Skills { get; set; }
    public string? Experience { get; set; }
    public string? Education { get; set; }
    public DateTime AppliedOn { get; set; } = DateTime.Now; // Date of application
  }


}


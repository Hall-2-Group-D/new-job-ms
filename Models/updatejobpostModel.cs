namespace AspnetCoreMvcFull.Models
{
  public class updatejobpostModel
  {
    public Guid Id { get; set; } // Unique identifier for each job post

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


}


using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.adminn
{
  [Index(nameof(username), IsUnique =true)]

  public class AdminAccount
  {
    [Key]
    public int id { get; set; }
    [Required(ErrorMessage = "Full Name Is required.")]
    public string fullname { get; set; }
    [Required(ErrorMessage = "User Name Is required.")]
    public string username { get; set; }
    [Required(ErrorMessage = "Email Is required.")]
    public string email { get; set; }
    [Required(ErrorMessage = "Password Is required.")]
    [MaxLength(10 , ErrorMessage = "Max 10 Characters Allowed." )]
    public string pass { get; set; }

  }
}

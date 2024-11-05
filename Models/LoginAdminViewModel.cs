using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class LoginAdminViewModel
  {
    [Required(ErrorMessage = "Username is required.")]
    public string username { get; set; }

    [DataType(DataType.Password)]
    [MaxLength(10, ErrorMessage = "Password cannot exceed 10 characters.")]
    public string pass { get; set; }
  }
}

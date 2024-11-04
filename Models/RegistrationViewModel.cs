using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class RegistrationViewModel
  {
    public int id { get; set; }
    [Required(ErrorMessage = "Full Name Is required.")]
    public string fullname { get; set; }
    [Required(ErrorMessage = "User Name Is required.")]
    public string username { get; set; }
    [Required(ErrorMessage = "Email Is required.")]
    [EmailAddress(ErrorMessage = "Please Enter Your Valid Email")]
    
    public string email { get; set; }
    [Required(ErrorMessage = "Password Is required.")]
    [MaxLength(10, ErrorMessage = "Max 10 Characters Allowed.")]

    [DataType(DataType.Password)]
    public string pass { get; set; }
    
  }
}

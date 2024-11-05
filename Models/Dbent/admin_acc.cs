using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models.Dbent
{
  public class admin_acc
  {
    public int id { get; set; }
    public string fullname { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string pass { get; set; }
  }
}

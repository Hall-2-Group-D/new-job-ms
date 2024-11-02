using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models.Dbent
{
  public class user
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string fullname { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string pass { get; set; }
    public DateTime reg_date { get; set; }
  }
}

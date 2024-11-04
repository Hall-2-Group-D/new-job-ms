using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.Dbent
{
  public class Recruiterpr
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string company_name { get; set; }
    public string company_website { get; set; }
    public int contact_number { get; set; }
    public string location { get; set; }
    public  DateTime reg_date { get; set; }
    public int user_id { get; set; }

    public class context
    {
    }
  }
}

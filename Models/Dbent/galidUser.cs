using Microsoft.AspNetCore.Identity;

namespace AspnetCoreMvcFull.Models.Dbent
{
  public class galidUser : IdentityUser
  {
    public string FuulName { get; set; }
  }
}

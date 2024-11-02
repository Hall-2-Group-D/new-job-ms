using AspnetCoreMvcFull.Models.Dbent;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Data
{
  public class RecruiterDbcontext : DbContext
  {
    public RecruiterDbcontext()
    {
    }

    public RecruiterDbcontext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Recruiterpr> tbl_Recruiterprs { get; set; }
    public DbSet<user> tbl_users { get; set; }
  }
}

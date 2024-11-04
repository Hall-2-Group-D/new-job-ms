using AspnetCoreMvcFull.adminn;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.Dbent;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Data
{
  public class RecruiterDbcontext : DbContext
  {
    public RecruiterDbcontext()
    {
    }

    public RecruiterDbcontext(DbContextOptions<RecruiterDbcontext> options) : base(options)
    {
    }

    public DbSet<Recruiterpr> tbl_Recruiterprs { get; set; }
    public DbSet<user> tbl_users { get; set; }
    
    public DbSet<jobpostModel> Tbl_jobpostModels { get; set; }
    public DbSet<AdminAccount> tbl_admin_user { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}

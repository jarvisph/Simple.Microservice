using Microsoft.EntityFrameworkCore;

namespace Simple.SignalR.Entity.DbContext
{
    public class SignalRDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SignalRDbContext(DbContextOptions<SignalRDbContext> options) : base(options)
        {

        }
        public DbSet<ConnectionClient> Connection { get; set; }
        public DbSet<ApplicationSetting> Application { get; set; }
        public DbSet<PushLog> PushLog { get; set; }
    }
}

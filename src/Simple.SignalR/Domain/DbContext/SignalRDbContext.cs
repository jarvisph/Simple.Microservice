using Microsoft.EntityFrameworkCore;
using Simple.SignalR.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SignalR.Domain.DbContext
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

using Microsoft.EntityFrameworkCore;
using Simple.Translate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Translate.Domain.DBContext
{
    public class TranslateDbContext : DbContext
    {
        public TranslateDbContext(DbContextOptions<TranslateDbContext> options) : base(options)
        {

        }
        public DbSet<TranslateChannel> TranslateChannel { get; set; }
        public DbSet<TranslateContent> TranslateContent { get; set; }
        public DbSet<TranslateWord> TranslateWord { get; set; }
    }
}

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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TranslateWord>(c => c.HasKey(t => new { t.Word, t.Language }));
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TranslateChannel> TranslateChannel { get; set; }
        public DbSet<TranslateContent> TranslateContent { get; set; }
        public DbSet<TranslateWord> TranslateWord { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Oppgave3.Model;

namespace Oppgave3.DAL
{
    public class FAQContext : DbContext
    {
        public FAQContext(DbContextOptions<FAQContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Kategori> kategorier { get; set; }
        public DbSet<FAQ> faqs { get; set; }
        public DbSet<Skjema> skjemaer { get; set; }
    }
}
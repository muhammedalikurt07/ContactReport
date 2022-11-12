using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Contexts
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> _options) : base(_options)
        {

        }

        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {
            base.OnModelCreating(_modelBuilder);
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactInfo> ContactInfos { get; set; }
        public virtual DbSet<ContactInfoContact> ContactInfoContacts { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
    }
}

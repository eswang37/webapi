using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext (DbContextOptions<ContactContext> options)
            : base(options)
        {
        }
       
        public DbSet<webapi.Models.Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("tblContact");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.LastName)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .IsUnicode(false);



            });
        }
    }
}

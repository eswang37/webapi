using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options)
            : base(options)
        {
        }

        public DbSet<webapi.Models.Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostId);

                entity.ToTable("tblPost");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Content)
                   .HasMaxLength(4000)
                   .IsUnicode(false);

               

            });
        }
    }
}

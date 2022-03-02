using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorApp.Api.Models
{
    public partial class EyerollContext : DbContext
    {
        public EyerollContext()
        {
        }

        public EyerollContext(DbContextOptions<EyerollContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Joke> Jokes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Joke>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Joke1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Joke");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

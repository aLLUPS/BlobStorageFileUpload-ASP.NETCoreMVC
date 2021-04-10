using BlobUploadWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlobUploadWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<UserMedium> UserMedia { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserMedium>(entity =>
            {
                entity.HasKey(e => e.MediaId)
                    .HasName("PK__UserMedia");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.MediaId)
                    //.ValueGeneratedNever()
                    .ValueGeneratedOnAdd()
                    .HasColumnName("media_id");

                entity.Property(e => e.MediaUrl)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("media_url");

                entity.Property(e => e.MediaFileName)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("media_file_name");

                entity.Property(e => e.MediaFileType)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("media_file_type");

                entity.Property(e => e.DateTimeUploaded)
                    .HasDefaultValueSql("getdate()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMedia)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserMedia_fk_User");
            });
        }
    }
}

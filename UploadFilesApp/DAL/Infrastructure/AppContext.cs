using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using UploadFilesApp.Models;

namespace UploadFilesApp.Infrastructure
{
    public class AppContext : DbContext
    {
        public virtual DbSet<MaterialModel> Materials { get; set; }
        public virtual DbSet<MaterialVersionModel> MaterialVersions { get; set; }

        public AppContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialVersionModel>().HasRequired<MaterialModel>(s => s.MaterialModel)
                .WithMany(g =>g.MaterialVersions).HasForeignKey<Guid>(s => s.MaterialId);
        }
    }
}
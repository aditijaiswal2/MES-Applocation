
using MES.Shared.Entities;
using MES.Shared.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MES.Shared.Models.Rotors;


namespace MES.Server.Data
{
    public class ProjectdbContext : IdentityDbContext<AppUser, AppRole, int,
      IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
      IdentityRoleClaim<int>, IdentityUserToken<int>>

    //: IdentityDbContext<AppUser, IdentityRole<int>, int>  added by ranjitha
    {
        public ProjectdbContext(DbContextOptions<ProjectdbContext> options)
          : base(options)
        {
        }
        public DbSet<LoginUserDetails> LoginUserDetails { get; set; }
        public DbSet<Logindetail> Logindetails { get; set; }
        public DbSet<MESWorkcenters> MESWorkcenters { get; set; }
        public DbSet<WcList> WorkCenters { get; set; }
        public DbSet<Params> Params { get; set; }
        public DbSet<Receiving> Receivings { get; set; }
        public DbSet<ShipmentImage> ShipmentImage { get; set; }
        public DbSet<IncomingImages> IncomingImages { get; set; }
        public DbSet<MES.Shared.Models.Rotors.Imagedata> Imagedatas { get; set; }

        public DbSet<IncomingInspection> IncomingInspections { get; set; }
       // public DbSet<Imagedata> Imagedatas { get; set; }
        public DbSet<MES.Shared.Models.Image> Images { get; set; }
        public DbSet<RotorSalesData> RotorSalesData { get; set; }
        public DbSet<RotorSalesSavedData> RotorSalesSavedData { get; set; }
        public DbSet<RotorProductionData> RotorProductionData { get; set; }
        public DbSet<RotorProductionSavedData> RotorProductionSavedData { get; set; }
        public DbSet<RotorProcessDimensionsReport> RotorProcessDimensionsReports { get; set; }
        public DbSet<NewRotorData> NewRotorData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<AppUser>()
    .HasMany(ur => ur.UserRoles)
    .WithOne(u => u.User)
    .HasForeignKey(ur => ur.UserId)
    .IsRequired()
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IncomingImages>()
.HasKey(i => i.Id);

            modelBuilder.Entity<MES.Shared.Models.Rotors.Imagedata>()
                .HasKey(i => i.ID);

            modelBuilder.Entity<MES.Shared.Models.Rotors.Imagedata>()
                .HasOne(i => i.IncomingImages)
                .WithMany(its => its.Images)
                .HasForeignKey(i => i.IncomingImageId)
                .IsRequired();


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Imagedata>()
                .HasOne(i => i.IncomingImages)
                .WithMany(ii => ii.Images)
                .HasForeignKey(i => i.IncomingImageId);
        



            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUserRole>(eb =>
            {
                // Transfer relationship from RoleId1 to RoleId, remove RoleId1
                var roleId = (Microsoft.EntityFrameworkCore.Metadata.Internal.Property)eb.Metadata.FindProperty("RoleId");
                var roleIdFk = eb.Metadata.FindForeignKeys(roleId).Single();
                eb.Metadata.RemoveForeignKey(roleIdFk);

                var roleId1 = eb.Metadata.FindProperty("RoleId1");
                var roleId1Fk = eb.Metadata.FindForeignKeys(roleId1).Single();

                var applicationRolePk = modelBuilder.Entity<AppRole>().Metadata.FindPrimaryKey();
                roleId1Fk.SetProperties(
                    new List<Microsoft.EntityFrameworkCore.Metadata.Internal.Property> { roleId }, applicationRolePk);

                eb.Metadata.RemoveProperty(roleId1);

                // Transfer relationship from UserId1 to UserId, remove UserId1
                var userId = (Microsoft.EntityFrameworkCore.Metadata.Internal.Property)eb.Metadata.FindProperty("UserId");
                var userIdFk = eb.Metadata.FindForeignKeys(userId).Single();
                eb.Metadata.RemoveForeignKey(userIdFk);

                var userId1 = eb.Metadata.FindProperty("UserId1");
                var userId1Fk = eb.Metadata.FindForeignKeys(userId1).Single();

                var applicationUserPk = modelBuilder.Entity<AppUser>().Metadata.FindPrimaryKey();
                userId1Fk.SetProperties(
                    new List<Microsoft.EntityFrameworkCore.Metadata.Internal.Property> { userId }, applicationUserPk);

                eb.Metadata.RemoveProperty(userId1);
            });

            modelBuilder.Entity<AppRole>().HasData(
                 new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ListItems = "None" },
                 new AppRole { Id = 2, Name = "User", NormalizedName = "USER", ListItems = "None" }
            );

           

            
        }
    }
}


using MES.Shared.Entities;
using MES.Shared.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MES.Shared.Models.Rotors;
using iTextSharp.text.rtf.graphic;


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
        //public DbSet<LoginUserDetails> LoginUserDetails { get; set; }
        //public DbSet<Logindetail> Logindetails { get; set; }      
        //public DbSet<WcList> WorkCenters { get; set; }
        //public DbSet<Params> Params { get; set; }

        public DbSet<MESWorkcenters> MESWorkcenters { get; set; }
        public DbSet<Receiving> Receivings { get; set; }
        public DbSet<ShipmentImage> ShipmentImage { get; set; }
        public DbSet<IncomingImages> IncomingImages { get; set; }
        public DbSet<IncomingInspectionFeedRolls> IncomingInspectionFeedRollsdata { get;set; }
        public DbSet<MES.Shared.Models.Rotors.Imagedata> Imagedatas { get; set; }
        public DbSet<FinalInspection> FinalInspections { get; set; }
        public DbSet<MES.Shared.Models.Rotors.FinalImagedata> FinalImagedatas { get; set; }
        
        public DbSet<IncomingInspection> IncomingInspections { get; set; }
        public DbSet<RotorIncominInspectionSavedData> rotorIncominInspectionSavedDatas { get; set; }
        // public DbSet<Imagedata> Imagedatas { get; set; }
        public DbSet<MES.Shared.Models.Image> Images { get; set; }
        public DbSet<RotorSalesData> RotorSalesData { get; set; }
        public DbSet<Other> Others { get; set; }
        public DbSet<Materials> Material { get; set; }
        public DbSet<SaddlepartNumber> saddlepartNumbers { get; set; }
        public DbSet<NewBoxRequiredNumber> NewBoxRequiredNumbers { get; set; }
        public DbSet<RotorSalesSavedData> RotorSalesSavedData { get; set; }
        public DbSet<RotorsFinalInspection> RotorsFinalInspections { get; set; }
        public DbSet<FinalInspectionSaveData> finalInspectionSaveDatas { get; set; }
        public DbSet<RotorProductionData> RotorProductionData { get; set; }
        public DbSet<RotorProductionSavedData> RotorProductionSavedData { get; set; }
        //public DbSet<RotorProcessDimensionsReport> RotorProcessDimensionsReports { get; set; }
        public DbSet<NewRotorData> NewRotorData { get; set; }
        public DbSet<RotorGrindingData> RotorGrindingData { get; set; }
        public DbSet<RotorGrindingSavedData> RotorGrindingSavedData { get; set; }
        public DbSet<RotorDamageGrindingDataFromGrinding>RotorDamageGrindingDataFromGrinding { get; set; }
        public DbSet<RotorDamageGrindingSaveData> RotorDamageGrindingSaveData { get; set; }
        public DbSet<RotorDamageGrindingSubmitedData> RotorDamageGrindingSubmitedData { get; set; }
        public DbSet<SalesAttachedFile> SalesAttachedFile { get; set; }
        public DbSet<MES.Shared.Models.Rotors.Filedata> Filedata { get; set; }
        public DbSet<RotorGrindingSecondaryWorkCentersData> RotorGrindingSecondaryWorkCentersData { get; set; }
        public DbSet<RotorsStyle> rotorsStyles { get; set; }
        public DbSet<Typesdetails> types { get; set; }
        public DbSet<RotorSalesClearance> RotorSalesClearance { get; set; }
        public DbSet<RotorShipping> RotorShipping { get; set; }
        public DbSet<SaveEnterdRotorGrindingDetails> SaveEnterdRotorGrindingDetails { get; set; }
        public DbSet<MESDelayReason> MESDelayReason { get; set; }

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


            modelBuilder.Entity<FinalInspection>()
 .HasKey(i => i.Id);

            modelBuilder.Entity<FinalInspectionSaveData>()
        .HasKey(f => f.Id);
            modelBuilder.Entity<MES.Shared.Models.Rotors.FinalImagedata>()
                .HasKey(i => i.ID);

            modelBuilder.Entity<MES.Shared.Models.Rotors.FinalImagedata>()
                         .HasOne(i => i.FinalInspection)
                         .WithMany(its => its.Images)
                         .HasForeignKey(i => i.FinalInspectionId)
                         .IsRequired();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Imagedata>()
                .HasOne(i => i.IncomingImages)
                .WithMany(ii => ii.Images)
                .HasForeignKey(i => i.IncomingImageId);


            modelBuilder.Entity<SalesAttachedFile>()
            .HasKey(i => i.Id);

            modelBuilder.Entity<MES.Shared.Models.Rotors.Filedata>()
                .HasKey(i => i.ID);

            modelBuilder.Entity<MES.Shared.Models.Rotors.Filedata>()
                .HasOne(i => i.SalesAttachedFile)
                .WithMany(its => its.File)
                .HasForeignKey(i => i.SalesAttachedFileId)
                .IsRequired();

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

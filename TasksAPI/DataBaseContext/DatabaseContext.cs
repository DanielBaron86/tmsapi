using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml;
using TasksAPI.Entities;
using TasksAPI.Models;
using static TasksAPI.Entities.UserTypes;

namespace TasksAPI.DataBaseContext
{
    /// <summary>
    /// Main Database Connection Handler 
    /// </summary>
    public class DatabaseConnectContext : DbContext
    {
        /// <summary>
        /// Initiate with base options
        /// </summary>
        /// <param name="options"></param>
        public DatabaseConnectContext(DbContextOptions<DatabaseConnectContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<UserTypes> UserTypes { get; set; }
        public DbSet<LocationTypesInstances> LocationTypesInstances { get; set; }
        public DbSet<LocationTypes> LocationEntity { get; set; }
        public DbSet<GoodsTypes> GoodsTypes { get; set; }
        public DbSet<GoodsTypesInstances> GoodsTypesInstances { get; set; }
        public DbSet<TasksEntities> TasksEntities { get; set; }
        public DbSet<TasksEntitiesProcurements> TasksEntitiesProcurements { get; set; }
        public DbSet<TasksEntitiesTransfer> TasksEntitiesTransfer { get; set; }
        public DbSet<AccountsGoodsEntity> AccountsGoodsEntity { get; set; }
        public DbSet<ItemMovementEntity> ItemMovementEntity { get; set; }
        public DbSet<CashRegisterEntity> CashRegisterEntity { get; set; }
        public DbSet<CashRegisterEntitySessions> CashRegisterEntitySessions { get; set; }
        public DbSet<StoreCartsEntity> StoreCartsEntity { get; set; }
        public DbSet<StoreCartsEntityDetails> StoreCartsEntityDetails { get; set; }
        public DbSet<ReportsEntities> ReportsEntities { get; set; }
        public DbSet<ReportsEntitiesResults> ReportsEntitiesResults { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ut => ut.HasIndex(i => i.Email).IsUnique());
            modelBuilder.Entity<User>(ut => ut.HasIndex(i => i.Username).IsUnique());

            modelBuilder.Entity<GoodsTypes>(ut => ut.HasIndex(i => i.GoodModelId).IsUnique());

            modelBuilder.Entity<LocationTypes>(ut => ut.HasIndex(i => i.LocationType).IsUnique());
            

            modelBuilder.Entity<User>()
                        .HasOne(e => e.UserTypes)
                        .WithMany(e => e.Users)
                        .HasForeignKey(e => e.UserTypeId)
                        .HasPrincipalKey(e => e.UserTypeId);

            modelBuilder.Entity<Accounts>()
                        .HasOne(e => e.UserTypes)
                        .WithMany(e => e.Accounts)
                        .HasForeignKey(e => e.UserTypeId)
                        .HasPrincipalKey(e => e.UserTypeId);

            modelBuilder.Entity<LocationTypesInstances>()
                        .HasOne(e => e.LocationTypes)
                        .WithMany(e => e.LocationTypesInstances)
                        .HasForeignKey(e => e.LocationTypeID)
                        .HasPrincipalKey(e => e.LocationType);

            modelBuilder.Entity<GoodsTypesInstances>()
                        .HasOne(e => e.GoodsTypes)
                        .WithMany(e => e.GoodsTypesInstances)
                        .HasForeignKey(e => e.GoodModelId)
                        .HasPrincipalKey(e => e.GoodModelId);

            modelBuilder.Entity<GoodsTypesInstances>()
                        .Property(p => p.Price)
                        .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<TasksEntitiesProcurements>()
                        .HasOne(e => e.GoodsTypes)
                        .WithMany(e => e.TasksEntitiesProcurements)
                        .HasForeignKey(e => e.GoodTypeID)
                        .HasPrincipalKey(e => e.GoodModelId);

            modelBuilder.Entity<TasksEntitiesTransfer>()
                        .HasOne(e => e.LocationTypesInstances)
                        .WithMany(e => e.TasksEntitiesTransfer)
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasForeignKey(e => e.FromLocation)
                        .HasForeignKey(e => e.ToLocation)
                        .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<CashRegisterEntity>()
                        .Property(p => p.InternalNotes)
                        .HasColumnName("Notes");

            modelBuilder.Entity<CashRegisterEntitySessions>()
                        .Property(p => p.InternalNotes)
                        .HasColumnName("Notes");

            modelBuilder.Entity<StoreCartsEntityDetails>()
                        .Property(p => p.InternalNotes)
                        .HasColumnName("Notes");

            modelBuilder.Entity<StoreCartsEntityDetails>()
                        .Property(p => p.Price)
                        .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<StoreCartsEntity>()
                        .Property(p => p.Total)
                        .HasColumnType("decimal(18,4)");
            modelBuilder.Entity<StoreCartsEntity>()
                        .Property(p => p.Paid)
                        .HasColumnType("decimal(18,4)");
            modelBuilder.Entity<StoreCartsEntity>()
                        .Property(p => p.Remaining)
                        .HasColumnType("decimal(18,4)");



            modelBuilder.Entity<UserTypes>().HasData(
                      new UserTypes() { Id = 1, UserTypeId = (int)EnumTypes.CLIENT, Description = "Client",CreatedDate= DateTime.Parse("Aug 9, 2025") },
                      new UserTypes() { Id = 2, UserTypeId = (int)EnumTypes.CLERK, Description = "Clerk" },
                      new UserTypes() { Id = 3, UserTypeId = (int)EnumTypes.SUPERVISOR, Description = "Supervisor" }
                  );

            modelBuilder.Entity<LocationTypes>().HasData(
                new LocationTypes() { Id = 1, LocationType = (int)LocationTypesList.WAREHOUES, Description = "Warehouse" },
                new LocationTypes() { Id = 2, LocationType = (int)LocationTypesList.STORE, Description = "STORE" },
                new LocationTypes() { Id = 3, LocationType = (int)LocationTypesList.CLIENT, Description = "CLIENT" },
                new LocationTypes() { Id = 4, LocationType = (int)LocationTypesList.SUPPLIER, Description = "SUPPLIER" }
                );



            modelBuilder.Entity<LocationTypesInstances>().HasData(
                new LocationTypesInstances() { Id = 1, LocationTypeID = 1, Adress = "Iasi", Description = "MAIN Warehouse" },
                new LocationTypesInstances() { Id = 2, LocationTypeID = 2, Adress = "Iasi", Description = "Iasi Mall" },
                new LocationTypesInstances() { Id = 3, LocationTypeID = 2, Adress = "Suceava", Description = "Suceava Mall" },
                new LocationTypesInstances() { Id = 4, LocationTypeID = 3, Adress = "Client", Description = "Goods Assigned to clients" },
                new LocationTypesInstances() { Id = 5, LocationTypeID = 1, Adress = "Iasi", Description = "Returned Items" },
                new LocationTypesInstances() { Id = 6, LocationTypeID = 4, Adress = "Iasi", Description = "Item Supplier" }

                );

            modelBuilder.Entity<GoodsTypes>().HasData(
                new GoodsTypes() { Id = 1, GoodModelId = 1, Name = "A53", Description = "Samsung Smartphone" },
                new GoodsTypes() { Id = 2, GoodModelId = 2, Name = "ZFLIP", Description = "Samsung Smartphone" },
                new GoodsTypes() { Id = 3, GoodModelId = 3, Name = "M14", Description = "Samsung Smartphone" },
                new GoodsTypes() { Id = 4, GoodModelId = 4, Name = "S21", Description = "Samsung Smartphone" },
                new GoodsTypes() { Id = 5, GoodModelId = 5, Name = "Apple 15", Description = "Apple Smartphone" }
                );


        }



        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {

            var entries = ChangeTracker
                          .Entries()
                          .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }



            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }



    }
}

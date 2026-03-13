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

        public DbSet<UserTypes> UserTypes { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<LocationTypesEntity> LocationEntity { get; set; }
        public DbSet<LocationTypesInstances> LocationTypesInstances { get; set; }
        public DbSet<GoodModelBaseTypeEntity> GoodModelBaseType { get; set; }
        public DbSet<GoodsTypesEntity> GoodsTypes { get; set; }
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
        public DbSet<RefreshTokenEntity> RefreshTokenEntity { get; set; }
        public virtual DbSet<v_GoodsTypes> v_GoodsTypes { get; set; }
        public virtual DbSet<v_GoodsTypesInstances> v_GoodsTypesInstances { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<v_GoodsTypes>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("v_GoodsTypes");
                    eb.Property(e => e.Id).HasColumnName("id");
                    eb.Property(e => e.GoodBaseId).HasColumnName("GoodBaseId");
                    eb.Property(e => e.InventoryKey).HasColumnName("InventoryKey");
                    eb.Property(e => e.Name).HasColumnName("name");
                    eb.Property(e => e.Description).HasColumnName("description");
                    eb.Property(e => e.Type).HasColumnName("type");
                    eb.Property(e => e.Manufacturer).HasColumnName("manufacturer");
                });

            modelBuilder
                .Entity<v_GoodsTypesInstances>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("v_GoodsTypesInstances");
                    eb.Property(e => e.Id).HasColumnName("id");
                    eb.Property(e => e.GoodModelId).HasColumnName("GoodModelId");
                    eb.Property(e => e.GoodBaseId).HasColumnName("GoodBaseId");
                    eb.Property(e => e.Price).HasColumnName("Price");
                    eb.Property(e => e.LocationId).HasColumnName("LocationId");
                    eb.Property(e => e.LocationName).HasColumnName("LocationName");
                    eb.Property(e => e.SerialNumber).HasColumnName("SerialNumber");
                    eb.Property(e => e.Name).HasColumnName("Name");
                    eb.Property(e => e.Type).HasColumnName("Type");
                    eb.Property(e => e.Manufacturer).HasColumnName("Manufacturer");
                    eb.Property(e => e.Status).HasColumnName("Status");
                    eb.Property(e => e.Quantity).HasColumnName("Quantity");
                });

            modelBuilder.Entity<UserEntity>(ut => ut.HasIndex(i => i.Email).IsUnique());
            modelBuilder.Entity<UserEntity>(ut => ut.HasIndex(i => i.Username).IsUnique());

            modelBuilder.Entity<GoodsTypesEntity>(ut => ut.HasIndex(i => i.GoodBaseId));

            modelBuilder.Entity<GoodsTypesEntity>()
                .HasOne(g => g.GoodModelBaseTypeEntity)
                .WithMany(g => g.GoodsTypesList)
                .HasForeignKey(g => g.GoodBaseId)
                .HasPrincipalKey(g => g.Id);


            modelBuilder.Entity<LocationTypesEntity>(ut => ut.HasIndex(i => i.LocationType).IsUnique());


            modelBuilder.Entity<UserEntity>()
                        .HasOne(e => e.UserTypes)
                        .WithMany(e => e.Users)
                        .HasForeignKey(e => e.UserTypeId)
                        .HasPrincipalKey(e => e.UserTypeId);

            modelBuilder.Entity<RefreshTokenEntity>()
                .HasOne(e => e.UserEntity)
                .WithOne(e => e.RefreshTokenEntity)
                .HasForeignKey<RefreshTokenEntity>(e => e.userId);



            modelBuilder.Entity<Accounts>()
                        .HasOne(e => e.UserTypes)
                        .WithMany(e => e.Accounts)
                        .HasForeignKey(e => e.UserTypeId)
                        .HasPrincipalKey(e => e.UserTypeId);

            modelBuilder.Entity<LocationTypesInstances>()
                        .HasOne(e => e.LocationTypesEntity)
                        .WithMany(e => e.LocationTypesInstances)
                        .HasForeignKey(e => e.LocationTypeID)
                        .HasPrincipalKey(e => e.LocationType);

            modelBuilder.Entity<GoodsTypesInstances>()
                .HasOne(e => e.GoodsTypes)
                .WithMany(e => e.GoodsTypesInstances)
                .HasForeignKey(e => e.GoodModelId);

            modelBuilder.Entity<GoodsTypesInstances>()
                        .Property(p => p.Price)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TasksEntitiesProcurements>()
                .HasOne(e => e.GoodsTypes)
                .WithMany(e => e.TasksEntitiesProcurements)
                .HasForeignKey(e => e.GoodTypeID);

            modelBuilder.Entity<TasksEntitiesProcurements>()
                .HasOne(e => e.LocationTypesInstances)
                .WithMany(e => e.TasksEntitiesProcurements)
                .HasForeignKey(e => e.Location);


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
            modelBuilder.Entity<StoreCartsEntityDetails>()
                .HasOne(p => p.GoodsTypesInstance)
                .WithMany(p => p.StoreCartsEntityDetails)
                .HasForeignKey(p => p.GoodId);


            // modelBuilder.Entity<StoreCartsEntityDetails>()
            //     .HasMany(p => p.GoodsTypesInstances)
            //     .WithMany(p => p.StoreCartsEntityDetails)
            //     .UsingEntity(j => j.ToTable("StoreCartDetails_GoodsInstances"));

            modelBuilder.Entity<StoreCartsEntity>()
                        .Property(p => p.Total)
                        .HasColumnType("decimal(18,4)");
            modelBuilder.Entity<StoreCartsEntity>()
                        .Property(p => p.Paid)
                        .HasColumnType("decimal(18,4)");
            modelBuilder.Entity<StoreCartsEntity>()
                        .Property(p => p.Remaining)
                        .HasColumnType("decimal(18,4)");
            modelBuilder.Entity<StoreCartsEntity>()
                .HasOne(e => e.LocationTypesInstances)
                .WithMany(e => e.StoreCartsEntity)
                .HasForeignKey(e => e.storeLocation);
            modelBuilder.Entity<StoreCartsEntity>()
                .HasOne(e => e.Accounts)
                .WithMany(e => e.StoreCartsEntity)
                .HasForeignKey(e => e.clientId);
            modelBuilder.Entity<StoreCartsEntity>()
                .HasOne(e => e.UserEntity)
                .WithMany(e => e.StoreCartsEntity)
                .HasForeignKey(e => e.clientId);



            modelBuilder.Entity<UserTypes>().HasData(
                      new UserTypes() { Id = 1, UserTypeId = (int)EnumTypes.CLIENT, Description = "Client" },
                      new UserTypes() { Id = 2, UserTypeId = (int)EnumTypes.CLERK, Description = "Clerk" },
                      new UserTypes() { Id = 3, UserTypeId = (int)EnumTypes.SUPERVISOR, Description = "Supervisor" }
                  );

            modelBuilder.Entity<LocationTypesEntity>().HasData(
                new LocationTypesEntity() { Id = 1, LocationType = (int)LocationTypesList.WAREHOUSE, Description = "Warehouse" },
                new LocationTypesEntity() { Id = 2, LocationType = (int)LocationTypesList.STORE, Description = "STORE" },
                new LocationTypesEntity() { Id = 3, LocationType = (int)LocationTypesList.CLIENT, Description = "CLIENT" },
                new LocationTypesEntity() { Id = 4, LocationType = (int)LocationTypesList.SUPPLIER, Description = "SUPPLIER" }
                );



            modelBuilder.Entity<LocationTypesInstances>().HasData(
                new LocationTypesInstances() { Id = 1, LocationTypeID = 1, Address = "Iasi", Description = "MAIN Warehouse" },
                new LocationTypesInstances() { Id = 2, LocationTypeID = 2, Address = "Iasi", Description = "Iasi Mall" },
                new LocationTypesInstances() { Id = 3, LocationTypeID = 2, Address = "Suceava", Description = "Suceava Mall" },
                new LocationTypesInstances() { Id = 4, LocationTypeID = 3, Address = "Client", Description = "Goods Assigned to clients" },
                new LocationTypesInstances() { Id = 5, LocationTypeID = 1, Address = "Iasi", Description = "Returned Items" },
                new LocationTypesInstances() { Id = 6, LocationTypeID = 4, Address = "Iasi", Description = "Item Supplier" }

                );
        }



        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            var entries = ChangeTracker
                          .Entries()
                          .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
                }
            }



            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }



    }
}

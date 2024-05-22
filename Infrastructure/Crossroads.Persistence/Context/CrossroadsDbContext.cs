using Crossroads.Domain.Entities.Bases;
using Crossroads.Domain.Entities.DbSets;
using Crossroads.Domain.EntityConfigurations.Interfaces;
using Crossroads.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Persistence.Context
{
    public class CrossroadsDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public const string ConnectionString = "Data Source=DESKTOP-F4M3HC0\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Initial Catalog=Crossroads; Application Intent=ReadWrite;Multi Subnet Failover=False";

        public CrossroadsDbContext(DbContextOptions<CrossroadsDbContext> options) : base(options)
        {
            
        }

        #region DbSets
        public virtual RefreshToken ReferenceTokens { get; set; }
        public virtual AppUser AppUsers { get; set; } 
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        private void SetBaseProperties()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            var userId = "NotFound-User";

            foreach (var entry in entries)
            {
                SetIfAdded(entry, userId);
                SetIfModified(entry, userId);
                SetIfDeleted(entry, userId);
            }
        }

        private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State is not EntityState.Deleted)
            {
                return;
            }

            if (entry.Entity is not AuditableEntity entity)
            {
                return;
            }

            entry.State = EntityState.Modified;

            entity.Status = Status.Deleted;
            entity.DeletedDate = DateTime.Now;
            entity.DeletedBy = userId;
        }

        private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State is EntityState.Modified)
            {
                if (entry.Entity.Status == Status.Passive)
                    entry.Entity.Status = Status.Passive;
                if (entry.Entity.Status == Status.Active)
                    entry.Entity.Status = Status.Active;
            }
            entry.Entity.ModifiedBy = userId;
            entry.Entity.ModifiedDate = DateTime.Now;
        }

        private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State is not EntityState.Added)
            {
                return;
            }
            entry.Entity.Status = Status.Active;
            entry.Entity.CreatedBy = userId;
            entry.Entity.CreatedDate = DateTime.Now;
        }

        public override int SaveChanges()
        {
            SetBaseProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseProperties();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

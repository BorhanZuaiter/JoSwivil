using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class VODContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService _currentUserService;

        public VODContext(DbContextOptions<VODContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var userId = _currentUserService.UserId;
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
            AddedEntities.ForEach(E =>
            {
                var prop = E.Metadata.FindProperty("CreatedOn");
                if (prop != null)
                {
                    E.Property("CreatedOn").CurrentValue = DateTime.Now;
                }
                var createdByProp = E.Metadata.FindProperty("CreatedBy");
                if (createdByProp != null)
                {
                    if (E.Property("CreatedBy").CurrentValue == null)
                        E.Property("CreatedBy").CurrentValue = userId;
                }
                var IsDeletedByProp = E.Metadata.FindProperty("IsDeleted");
                if (IsDeletedByProp != null)
                {
                    E.Property("IsDeleted").CurrentValue = false;
                }

            });
            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
            EditedEntities.ForEach(E =>
            {
                var prop = E.Metadata.FindProperty("ModifiedOn");
                if (prop != null)
                {
                    E.Property("ModifiedOn").CurrentValue = DateTime.Now;
                }
                var modifiedByProp = E.Metadata.FindProperty("ModifiedBy");
                if (modifiedByProp != null)
                {
                    E.Property("ModifiedBy").CurrentValue = userId;
                }
            });
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;

            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
            AddedEntities.ForEach(E =>
            {
                var prop = E.Metadata.FindProperty("CreatedOn");
                if (prop != null)
                {
                    E.Property("CreatedOn").CurrentValue = DateTime.Now;
                }
                var createdByProp = E.Metadata.FindProperty("CreatedBy");
                if (createdByProp != null)
                {
                    if (E.Property("CreatedBy").CurrentValue == null)
                        E.Property("CreatedBy").CurrentValue = userId;
                }
                var IsDeletedByProp = E.Metadata.FindProperty("IsDeleted");
                if (IsDeletedByProp != null)
                {
                    E.Property("IsDeleted").CurrentValue = false;
                }
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
            EditedEntities.ForEach(E =>
            {
                var prop = E.Metadata.FindProperty("ModifiedOn");
                if (prop != null)
                {
                    E.Property("ModifiedOn").CurrentValue = DateTime.Now;
                }
                var modifiedByProp = E.Metadata.FindProperty("ModifiedBy");
                if (modifiedByProp != null)
                {
                    E.Property("ModifiedBy").CurrentValue = userId;
                }
            });

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
    }
}

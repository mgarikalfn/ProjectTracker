using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProjectTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProjectTracker.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string,
     IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>,
     IdentityRoleClaim<string>, IdentityUserToken<string>>

    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }

        // Main Entities
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignments { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<ProjectUpdate> ProjectUpdates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Identity tables
            ConfigureIdentityTables(builder);

            // Configure application entities
            ConfigureEntities(builder);

            // Configure relationships
            ConfigureRelationships(builder);
        }

        private void ConfigureIdentityTables(ModelBuilder builder)
        {
            // Rename the ASP.NET Identity tables
            builder.Entity<AppUser>(b =>
            {
                b.ToTable("Users");
                b.HasMany(u => u.UserRoles)
                 .WithOne(ur => ur.User)
                 .HasForeignKey(ur => ur.UserId)
                 .IsRequired();
            });

            builder.Entity<AppRole>(b =>
            {
                b.ToTable("Roles");
                b.HasMany(r => r.UserRoles)
                 .WithOne(ur => ur.Role)
                 .HasForeignKey(ur => ur.RoleId)
                 .IsRequired();
            });

            builder.Entity<AppUserRole>(b =>
            {
                b.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("UserTokens");
            });
        }

        private void ConfigureEntities(ModelBuilder builder)
        {
            // Project configuration
            builder.Entity<Project>(b =>
            {
                b.Property(p => p.Name).IsRequired().HasMaxLength(100);
                b.Property(p => p.Description).HasMaxLength(500);
                b.Property(p => p.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20);
                b.Property(p => p.Priority)
                    .HasConversion<string>()
                    .HasMaxLength(10);
                b.Property(p => p.StartDate).HasColumnType("date");
                b.Property(p => p.Deadline).HasColumnType("date");
                b.Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
            });

            // Task configuration
            builder.Entity<ProjectTask>(b =>
            {
                b.Property(t => t.Title).IsRequired().HasMaxLength(100);
                b.Property(t => t.Description).HasMaxLength(1000);
                b.Property(t => t.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20);
                b.Property(t => t.Type)
                    .HasConversion<string>()
                    .HasMaxLength(20);
                b.Property(t => t.EstimatedHours).HasColumnType("decimal(5,2)");
                b.Property(t => t.ActualHours).HasColumnType("decimal(5,2)");
                b.Property(t => t.DueDate).HasColumnType("date");
                b.Property(t => t.CreatedAt).HasDefaultValueSql("getutcdate()");
                b.Property(t => t.ExternalTaskId).HasMaxLength(50);
                b.Property(t => t.ExternalSystem).HasMaxLength(20);
            });

            // Team configuration
            builder.Entity<Team>(b =>
            {
                b.Property(t => t.Name).IsRequired().HasMaxLength(100);
                b.Property(t => t.Description).HasMaxLength(500);
                b.Property(t => t.CreatedAt).HasDefaultValueSql("getutcdate()");
            });

            // Task Comment configuration
            builder.Entity<TaskComment>(b =>
            {
                b.Property(c => c.Content).IsRequired().HasMaxLength(1000);
                b.Property(c => c.CreatedAt).HasDefaultValueSql("getutcdate()");
            });

            // Project Update configuration
            builder.Entity<ProjectUpdate>(b =>
            {
                b.Property(u => u.UpdateText).IsRequired().HasMaxLength(1000);
                b.Property(u => u.CreatedAt).HasDefaultValueSql("getutcdate()");
                b.Property(u => u.Type)
                    .HasConversion<string>()
                    .HasMaxLength(20);
            });
        }


        private void ConfigureRelationships(ModelBuilder builder)
        {
            // Team Member (User to Team many-to-many)
            builder.Entity<TeamMember>(b =>
            {
                b.HasKey(tm => new { tm.UserId, tm.TeamId });

                b.HasOne(tm => tm.User)
                    .WithMany(u => u.Teams)
                    .HasForeignKey(tm => tm.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(tm => tm.Team)
                    .WithMany(t => t.Members)
                    .HasForeignKey(tm => tm.TeamId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Project Assignment (Team to Project many-to-many)
            builder.Entity<ProjectAssignment>(b =>
            {
                b.HasKey(pa => new { pa.TeamId, pa.ProjectId });

                b.HasOne(pa => pa.Team)
                    .WithMany(t => t.ProjectAssignments)
                    .HasForeignKey(pa => pa.TeamId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(pa => pa.Project)
                    .WithMany(p => p.TeamAssignments)
                    .HasForeignKey(pa => pa.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Project to Tasks (one-to-many)
            builder.Entity<Project>(b =>
            {
                b.HasMany(p => p.Tasks)
                    .WithOne(t => t.Project)
                    .HasForeignKey(t => t.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Task to Comments (one-to-many)
            builder.Entity<ProjectTask>(b =>
            {
                b.HasMany(t => t.Comments)
                    .WithOne(c => c.Task)
                    .HasForeignKey(c => c.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Task to Assignee (many-to-one)
            builder.Entity<ProjectTask>(b =>
            {
                b.HasOne(t => t.Assignee)
                    .WithMany(u => u.AssignedTasks)
                    .HasForeignKey(t => t.AssigneeId)
                    .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete
            });

            // Project to Updates (one-to-many)
            builder.Entity<Project>(b =>
            {
                b.HasMany(p => p.Updates)
                    .WithOne(u => u.Project)
                    .HasForeignKey(u => u.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Team to Team Lead (one-to-one)
            builder.Entity<Team>(b =>
            {
                b.HasOne(t => t.TeamLead)
                    .WithMany()
                    .HasForeignKey(t => t.TeamLeadId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProjectTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Domain.Identity;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProjectTracker.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string,
     IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>,
     IdentityRoleClaim<string>, IdentityUserToken<string>>

    {
        public AppDbContext()
          : base(new DbContextOptionsBuilder<AppDbContext>()
              .UseSqlServer("Data Source=DESKTOP-D1VN9TT\\SQLEXPRESS;Initial Catalog=ProjectTrackerDb;Integrated Security=True;Encrypt=False;Trust Server Certificate=True")
              .Options)
        {
        }

        // Existing constructor (for dependency injection)
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
            // Configure AppUser
            builder.Entity<AppUser>(b =>
            {
                b.ToTable("Users");
                b.Property(u => u.Id).HasMaxLength(450); // Set max length for UserId

                // Configure UserRoles relationship with restricted delete behavior
                b.HasMany(u => u.UserRoles)
                    .WithOne(ur => ur.User)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade

                // Configure other relationships
                b.HasMany(u => u.AssignedTasks)
                    .WithOne(t => t.Assignee)
                    .HasForeignKey(t => t.AssigneeId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasMany(u => u.Teams)
                    .WithOne(tm => tm.User)
                    .HasForeignKey(tm => tm.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure AppRole
            builder.Entity<AppRole>(b =>
            {
                b.ToTable("Roles");
                b.Property(r => r.Id).HasMaxLength(450); // Set max length for RoleId

                b.HasMany(r => r.UserRoles)
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure AppUserRole (junction table)
            builder.Entity<AppUserRole>(b =>
            {
                b.ToTable("UserRoles");

                // Set max lengths for composite key columns
                b.Property(ur => ur.UserId).HasMaxLength(450);
                b.Property(ur => ur.RoleId).HasMaxLength(450);

                // Define composite primary key
                b.HasKey(ur => new { ur.UserId, ur.RoleId });

                // Configure relationships with appropriate delete behaviors
                b.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevents multiple cascade paths

                b.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure other Identity tables with proper string lengths
            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("UserClaims");
                b.Property(uc => uc.UserId).HasMaxLength(450);
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("UserLogins");
                b.Property(l => l.ProviderKey).HasMaxLength(450);
                b.Property(l => l.LoginProvider).HasMaxLength(450);
            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("RoleClaims");
                b.Property(rc => rc.RoleId).HasMaxLength(450);
            });

            builder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("UserTokens");
                b.Property(t => t.UserId).HasMaxLength(450);
                b.Property(t => t.LoginProvider).HasMaxLength(450);
                b.Property(t => t.Name).HasMaxLength(450);
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
                b.ToTable("ProjectTasks");
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
                    .WithMany(p => p.Assignments)
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
                    .HasForeignKey(t => t.AssigneeId)  // Must be string to match AppUser.Id
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Team to Team Lead - Fix foreign key type
            builder.Entity<Team>(b =>
            {
                b.HasOne(t => t.TeamLead)
                    .WithMany()
                    .HasForeignKey(t => t.TeamLeadId)  // Must be string to match AppUser.Id
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Project to Updates (one-to-many)
            builder.Entity<Project>(b =>
            {
                b.HasMany(p => p.Updates)
                    .WithOne(u => u.Project)
                    .HasForeignKey(u => u.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

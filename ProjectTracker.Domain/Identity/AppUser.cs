using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectTracker.Domain.Entities;

namespace ProjectTracker.Domain.Identity
{
    public class AppUser:IdentityUser
    {
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastActive { get; set; }
        //public string ? ProfilePictureUrl { get; set; }

        public ICollection<TeamMember> Teams { get; set; } = new List<TeamMember>();
        public ICollection<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();
        public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    }
}

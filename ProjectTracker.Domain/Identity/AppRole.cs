using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjectTracker.Domain.Identity
{
    public class AppRole:IdentityRole
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    }
}

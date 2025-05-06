using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Domain.Identity;
using static ProjectTracker.Domain.Enum.Enums;

namespace ProjectTracker.Domain.Entities
{
    public class TeamMember:BaseEntity
    {
        public string UserId { get; set; }  // Matches IdentityUser's string Id
        public virtual AppUser User { get; set; }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public MemberRole Role { get; private set; } // Enum (Developer/QA/PM/etc.)
        public DateTime JoinDate { get; private set; }

        // Relationships
        public Guid? TeamId { get; private set; }
        public virtual Team? Team { get; private set; }

        public decimal WeeklyCapacityHours { get; private set; } = 40;
        public decimal CurrentWorkloadHours { get; private set; }
    }
}

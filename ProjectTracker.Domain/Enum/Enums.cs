using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Enum
{
    public class Enums
    {
        public enum ProjectStatus { Planning, Active, OnHold, Completed, Cancelled }
        public enum ProjectPriority { Critical, High, Medium, Low }
        public enum TaskStatus { Backlog, Todo, InProgress, CodeReview, Testing, Done }
        public enum TaskType { Feature, Bug, Improvement, Maintenance }
        public enum MemberRole { Developer, QA, ProjectManager, Designer }

        public enum UpdateType
        {
            StatusChange,
            PriorityChange,
            DeadlineChange,
            GeneralUpdate
        }
    }
}

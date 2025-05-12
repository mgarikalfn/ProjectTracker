using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Application.Dtos.Project
{
    public class AssignDeveloperToProjectDto
    {
        public string DeveloperId { get; set; }
        public decimal? AllocatedHoursPerWeek { get; set; }

    }
}

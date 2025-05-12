using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace ProjectTracker.Application.Features.Project.Command
{
    public class AssignProjectToDeveloperCommand :IRequest<Result>
    {
        public string ProjectId { get; set; }
        public string DeveloperId { get; set; }
        public decimal? AllocatedHoursPerWeek { get; set; }
        public string AssignedBy { get; set; }
    }
}

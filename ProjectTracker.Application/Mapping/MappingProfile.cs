using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectTracker.Application.Dtos.Project;
using ProjectTracker.Application.Features.Project.Command;
using ProjectTracker.Application.Features.Project.Query;
using ProjectTracker.Domain.Entities;

namespace ProjectTracker.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<CreateProjectDto, Project>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Domain.Enum.Enums.ProjectStatus.Active))
                .ForMember(dest => dest.ExternalSystem, opt => opt.MapFrom(src => "ProjectTracker"))
                .ForMember(dest => dest.CompletedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ExternalProjectId, opt => opt.Ignore());

            CreateMap<AssignProjectToDeveloperCommand, ProjectAssignment>()
                .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.AllocatedHoursPerWeek, opt => opt.MapFrom(src => src.AllocatedHoursPerWeek));

            CreateMap<ProjectFilterDto, GetProjectsQuery>();
            CreateMap<AssignDeveloperToProjectDto, AssignProjectToDeveloperCommand>()
               .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
               .ForMember(dest => dest.AssignedBy, opt => opt.Ignore());

        }
    }
}

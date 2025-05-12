//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AutoMapper;
//using FluentResults;
//using MediatR;
//using ProjectTracker.Domain.Entities;
//using ProjectTracker.Domain.Interface;


//namespace ProjectTracker.Application.Features.Project.Command
//{
//    public class AssignProjectToDeveloperCommandHandler : IRequestHandler<AssignProjectToDeveloperCommand, Result>
//    {
//        private readonly IProjectRepository _projectRepository;
//        private readonly IRepository<ProjectAssignment> _assignmentRepository;
//        private readonly IRepository<TeamMember> _developerRepository;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        public AssignProjectToDeveloperCommandHandler(IProjectRepository projectRepository, IRepository<ProjectAssignment> assignmentRepository, 
//            IRepository<TeamMember> developerRepository, IMapper mapper, IUnitOfWork unitOfWork)
//        {
//            _projectRepository = projectRepository;
//            _assignmentRepository = assignmentRepository;
//            _developerRepository = developerRepository;
//            _mapper = mapper;
//            _unitOfWork = unitOfWork;
//        }
//        public async Task<Result> Handle(AssignProjectToDeveloperCommand request, CancellationToken cancellationToken)
//        {
//            var projectExists = await _projectRepository.ExistsAsync(request.ProjectId);

//            if (!projectExists)
//            {
//                return Result.Fail("project can't be fould");
//            }

//            var developer = _developerRepository.GetByIdAsync(request.DeveloperId);

//            if (developer == null)
//            {
//                return Result.Fail("developer can't be found");
//            }

//            var existingAssignment = await _assignmentRepository.FindAsync(a =>
//               a.ProjectId == request.ProjectId &&
//               a.DeveloperId == request.DeveloperId);

//            if (existingAssignment != null)
//            {
//                return Result.Fail("Developer is already assigned to this project");
//            }
//            var AssignedProject = new ProjectAssignment
//            {
//                ProjectId = request.ProjectId,
//                DeveloperId = request.DeveloperId,
//                AllocatedHoursPerWeek = (decimal)request.AllocatedHoursPerWeek,
//                AssignedDate = DateTime.UtcNow
//            };

//            try
//            {
//                await _assignmentRepository.AddAsync(AssignedProject);
//                await _unitOfWork.SaveEntitiesAsync(cancellationToken);
               
//                return Result.Ok();
//            }
//            catch (Exception ex)
//            {
//                return Result.Fail($"Failed to assign developer: {ex.Message}");
//            }
//        }
//    }
//}
 
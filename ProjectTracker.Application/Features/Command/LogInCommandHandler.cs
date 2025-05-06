using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectTracker.Application.Dtos.Account;
using ProjectTracker.Domain.Identity;
using ProjectTracker.Domain.Interface;

namespace ProjectTracker.Application.Features.Command
{
    public class LogInCommandHandler : IRequestHandler<LogInCommand, Result<AuthResponseDto>>
    {
           private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public LogInCommandHandler(UserManager<AppUser> userManager,
            ITokenService tokenService,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
     


        public async Task<Result<AuthResponseDto>> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LogInDto.Email);

            if (user == null)
                return Result.Fail<AuthResponseDto>("Unauthorized");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.LogInDto.Password, false);

            if (!result.Succeeded)
                return Result.Fail<AuthResponseDto>("Unauthorized");

            return Result.Ok(new AuthResponseDto
            {
                Token = await _tokenService.GenerateToken(user),
                Email = user.Email,
                DisplayName = user.DisplayName,
                Roles = await _userManager.GetRolesAsync(user)
            });
        }
    }
}
        

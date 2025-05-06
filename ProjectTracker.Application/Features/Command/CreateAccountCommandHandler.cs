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
    public class CreateAccountCommandHandler:IRequestHandler<CreateAccountCommand, Result<AuthResponseDto>>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        public CreateAccountCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthResponseDto>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.registerDto.Email) != null)
                return Result.Fail<AuthResponseDto>("Email already exists");

            var user = new AppUser
            {
                DisplayName = request.registerDto.DisplayName,
                Email = request.registerDto.Email,
                UserName = request.registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, request.registerDto.Password);

            if (!result.Succeeded)
                return Result.Fail<AuthResponseDto>("User creation failed");

            await _userManager.AddToRoleAsync(user, "Manager");

            return Result.Ok(new AuthResponseDto
            {
                Token = await _tokenService.GenerateToken(user),
                Email = user.Email,
                DisplayName = user.DisplayName
            });
        }
    }
}
    
    


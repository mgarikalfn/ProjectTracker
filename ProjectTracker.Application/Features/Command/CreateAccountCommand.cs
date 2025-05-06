using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProjectTracker.Application.Dtos.Account;

namespace ProjectTracker.Application.Features.Command
{
    public class CreateAccountCommand :IRequest<Result<AuthResponseDto>>
    {
        public RegisterDto registerDto { get; set; }
    }
}

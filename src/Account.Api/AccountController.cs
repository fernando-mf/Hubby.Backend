﻿using System.Threading.Tasks;
using Account.Application.CommandService.Register;
using Account.Application.QueryService.Login;
using Buddy.Infrastructure;
using Core.Api;
using Core.Api.Exceptions;
using Core.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api
{
    [Route("api/[controller]")]
    public class AccountController: BaseController
    {
        public AccountController(IMediator mediator, IExceptionHandler exceptionHandler, IUnitOfWork unitOfWork): base(mediator, exceptionHandler, unitOfWork)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterAccountCommand command)
        {
            return await Publish(command);
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            return await SendRequest<LoginQuery, LoginQueryResponse>(query);
        }
    }
}

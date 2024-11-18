using MediatR;
using Microsoft.AspNetCore.Mvc;
using PasswordVaultAPI.Application.CQRS.Commands.User.CreateUser;
using PasswordVaultAPI.Application.CQRS.Commands.User.ResetPassword;
using PasswordVaultAPI.Application.CQRS.Queries.User.ForgotPassword;
using PasswordVaultAPI.Application.CQRS.Queries.User.LoginUser;


namespace PasswordVaultAPI.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller
	{

		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}




		[HttpPost("Register")]
		public async Task<CreateUserCommandResponse> Register(CreateUserCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}



		[HttpPost("Login")]
		public async Task<LoginUserQueryResponse> Login(LoginUserQueryRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}



		[HttpPost("ForgotPassword")]
		public async Task<ForgotPasswordQueryResponse> ForgotPassword(ForgotPasswordQueryRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}



		[HttpPost("ResetPassword")]
		public async Task<ResetPasswordCommandResponse> ResetPassword(ResetPasswordCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}



    }
}

using MediatR;
using PasswordVaultAPI.Application.DTOs;
using PasswordVaultAPI.Application.Services;
using PasswordVaultAPI.Application.Utilities.Excepitons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Commands.User.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
	{

		private readonly IAuthService _service;

		public CreateUserCommandHandler(IAuthService service)
		{
			_service = service;
		}



		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{

			var user = await _service.RegisterUserAsync(request.RegisterDto);

			if (user == null) throw new Exception("Failed registration");

			return new CreateUserCommandResponse
			{
				User = user,
			};

		}

	}
}

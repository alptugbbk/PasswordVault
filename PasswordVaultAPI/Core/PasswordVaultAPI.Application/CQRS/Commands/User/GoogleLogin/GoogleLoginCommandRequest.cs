using MediatR;
using PasswordVaultAPI.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Commands.User.GoogleLogin
{
	public class GoogleLoginCommandRequest : IRequest<GoogleLoginCommandResponse>
	{
		public GoogleLoginRequestDTO GoogleLoginRequestDTO { get; set; }
	}
}

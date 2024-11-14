using MediatR;
using PasswordVaultAPI.Application.DTOs.Auth;

namespace PasswordVaultAPI.Application.CQRS.Commands.User.ResetPassword
{
	public class ResetPasswordCommandRequest : IRequest<ResetPasswordCommandResponse>
	{
		public ResetPasswordRequestDTO ResetPasswordRequestDTO { get; set; }
	}
}

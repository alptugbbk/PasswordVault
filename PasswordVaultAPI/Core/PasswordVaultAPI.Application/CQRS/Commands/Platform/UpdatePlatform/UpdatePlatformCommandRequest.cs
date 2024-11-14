using MediatR;
using PasswordVaultAPI.Application.DTOs.Platform;


namespace PasswordVaultAPI.Application.CQRS.Commands.Platform.UpdatePlatform
{
	public class UpdatePlatformCommandRequest : IRequest<UpdatePlatformCommandResponse>
	{
		public UpdatePlatformRequestDTO UpdatePlatformRequestDTO { get; set; }
	}
}

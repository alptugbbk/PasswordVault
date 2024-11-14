using MediatR;
using PasswordVaultAPI.Application.DTOs.Platform;


namespace PasswordVaultAPI.Application.CQRS.Commands.Platform.CreatePlatform
{
    public class CreatePlatformCommandRequest : IRequest<CreatePlatformCommandResponse>
	{
		public CreatePlatformRequestDTO PlatformRequestDto { get; set; }
	}
}

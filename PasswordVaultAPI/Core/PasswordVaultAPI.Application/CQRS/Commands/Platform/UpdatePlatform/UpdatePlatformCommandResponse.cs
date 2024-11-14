using PasswordVaultAPI.Application.DTOs.Base;

namespace PasswordVaultAPI.Application.CQRS.Commands.Platform.UpdatePlatform
{
    public class UpdatePlatformCommandResponse
	{
		public PlatformDTO PlatformResponseDto { get; set; }
	}
}

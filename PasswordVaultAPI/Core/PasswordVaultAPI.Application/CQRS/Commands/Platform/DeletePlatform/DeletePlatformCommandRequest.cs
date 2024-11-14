using MediatR;
using PasswordVaultAPI.Application.Utilities.Outcome;


namespace PasswordVaultAPI.Application.CQRS.Commands.Platform.DeletePlatform
{
	public class DeletePlatformCommandRequest : IRequest<DeletePlatformCommandResponse>
	{
		public string Id { get; set; }
	}
}

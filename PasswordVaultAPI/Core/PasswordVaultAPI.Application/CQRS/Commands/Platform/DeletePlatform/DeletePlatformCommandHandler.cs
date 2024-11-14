using MediatR;
using Microsoft.Extensions.Logging;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Application.Utilities.Outcome;

namespace PasswordVaultAPI.Application.CQRS.Commands.Platform.DeletePlatform
{
	public class DeletePlatformCommandHandler : IRequestHandler<DeletePlatformCommandRequest, DeletePlatformCommandResponse>
	{

		
		private readonly IPlatformRepository _platformRepository;
		private readonly IUserRepository _userRepository;
		private readonly ILogger<DeletePlatformCommandHandler> _logger;



		public DeletePlatformCommandHandler(IPlatformRepository platformRepository, ILogger<DeletePlatformCommandHandler> logger, IUserRepository userRepository)
		{
			_platformRepository = platformRepository;
			_logger = logger;
			_userRepository = userRepository;
		}


		public  async Task<DeletePlatformCommandResponse> Handle(DeletePlatformCommandRequest request, CancellationToken cancellationToken)
		{

			var platform = _platformRepository.GetAll(false).Where(x => x.Id.ToString() == request.Id).FirstOrDefault();
			if (platform == null)
			{
				return new DeletePlatformCommandResponse { IsSuccess = false };
			}
			else
			{

				_platformRepository.Remove(platform);

				await _platformRepository.SaveAsync();

				var user = await _userRepository.GetByIdAsync(platform.UserId.ToString());

				_logger.LogInformation("Platfrom Deleted {UserName}", user.UserName);

				return new DeletePlatformCommandResponse
				{
					IsSuccess = true
				};
			}

			
		}
	}
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PasswordVaultAPI.Application.DTOs.Base;
using PasswordVaultAPI.Application.DTOs.Platform;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Application.Services;
using PasswordVaultAPI.Application.Utilities.Cryptography;

namespace PasswordVaultAPI.Application.CQRS.Commands.Platform.UpdatePlatform
{
	public class UpdatePlatformCommandHandler : IRequestHandler<UpdatePlatformCommandRequest, UpdatePlatformCommandResponse>
	{

		private readonly IPlatformRepository _platformRepository;
		private readonly IUserRepository _userRepository;
		private readonly IAESEncryption _aesEncryption;
		private readonly ILogger<UpdatePlatformCommandHandler> _logger;

		public UpdatePlatformCommandHandler(IPlatformRepository platformRepository, IAESEncryption aesEncryption, IUserRepository userRepository, ILogger<UpdatePlatformCommandHandler> logger)
		{
			_platformRepository = platformRepository;
			_aesEncryption = aesEncryption;
			_logger = logger;
			_userRepository = userRepository;
		}


		public async Task<UpdatePlatformCommandResponse> Handle(UpdatePlatformCommandRequest request, CancellationToken cancellationToken)
		{
			var platform = await _platformRepository.GetByIdAsync(request.UpdatePlatformRequestDTO.Id);

			if (platform == null)
			{
				return null;
			}

			var passwordHash = _aesEncryption.EncryptPassword(request.UpdatePlatformRequestDTO.Password);

			platform.Name = request.UpdatePlatformRequestDTO.Name;

			platform.UserName = request.UpdatePlatformRequestDTO.UserName;

			platform.Password = passwordHash;

			await _platformRepository.UpdateAsync(platform);

			await _platformRepository.SaveAsync();

			var user = await _userRepository.GetWhere(x => x.Id == platform.UserId).Select(x => x.UserName).FirstOrDefaultAsync();

			_logger.LogInformation("Platform updated by user: {UserName}", user);

			var platfromDto = new PlatformDTO
			{
				Id = platform.Id.ToString(),
			};

			return new UpdatePlatformCommandResponse
			{
				PlatformResponseDto = platfromDto,
			};
		}
	}
}

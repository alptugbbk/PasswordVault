using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PasswordVaultAPI.Application.DTOs.Base;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Application.Utilities.Cryptography;
using PasswordVaultAPI.Application.Utilities.Excepitons;


namespace PasswordVaultAPI.Application.CQRS.Commands.Platform.CreatePlatform
{
	public class CreatePlatformCommandHandler : IRequestHandler<CreatePlatformCommandRequest, CreatePlatformCommandResponse>
	{

		private readonly IPlatformRepository _platformRepository;
		private readonly IAESEncryption _aesEncryption;
		private readonly IUserRepository _userRepository;
		private readonly ILogger<CreatePlatformCommandHandler> _logger;

		public CreatePlatformCommandHandler(IPlatformRepository platformRepository, IAESEncryption aesEncryption, ILogger<CreatePlatformCommandHandler> logger, IUserRepository userRepository)
		{
			_platformRepository = platformRepository;
			_aesEncryption = aesEncryption;
			_logger = logger;
			_userRepository = userRepository;
		}


		public async Task<CreatePlatformCommandResponse> Handle(CreatePlatformCommandRequest request, CancellationToken cancellationToken)
		{
			if (request.PlatformRequestDto == null) throw new PlatformCreationException();



			var passwordHash = _aesEncryption.EncryptPassword(request.PlatformRequestDto.Password);

			var newPlatform = new Domain.Entities.Platform
			{
				Name = request.PlatformRequestDto.Name,
				UserName = request.PlatformRequestDto.UserName,
				Password = passwordHash,
				UserId = Guid.Parse(request.PlatformRequestDto.UserId),

			};

			await _platformRepository.AddAsync(newPlatform);

			await _platformRepository.SaveAsync();

			var user = await _userRepository.GetWhere(x => x.Id.ToString() == request.PlatformRequestDto.UserId).Select(x => x.UserName).FirstOrDefaultAsync();

			_logger.LogInformation("Platfrom added {UserName}", user);

			var platfromDto = new PlatformDTO
			{
				Id = newPlatform.Id.ToString(),

			};


			return new CreatePlatformCommandResponse
			{
				PlatformResponseDto = platfromDto
			};


		}
	}
}

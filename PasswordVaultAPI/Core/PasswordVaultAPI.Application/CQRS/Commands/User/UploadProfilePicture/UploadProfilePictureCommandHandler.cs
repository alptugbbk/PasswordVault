using MediatR;
using Microsoft.Extensions.Logging;
using PasswordVaultAPI.Application.DTOs.Base;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Commands.User.UploadProfilePicture
{
	public class UploadProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommandRequest, UploadProfilePictureCommandResponse>
	{

		private readonly IUserRepository _userRepository;
		private readonly ILogger<UploadProfilePictureCommandHandler> _logger;



		public UploadProfilePictureCommandHandler(IUserRepository userRepository, ILogger<UploadProfilePictureCommandHandler> logger)
		{
			_userRepository = userRepository;
			_logger = logger;
		}



		public async Task<UploadProfilePictureCommandResponse> Handle(UploadProfilePictureCommandRequest request, CancellationToken cancellationToken)
		{

			if (request == null)
			{

				return new UploadProfilePictureCommandResponse
				{
					IsSuccess = false,
				};


			}

			var user = await _userRepository.GetByIdAsync(request.UserId);

			if (user == null)
			{
				_logger.LogError("User not found: {UserId}", request.UserId);
				return new UploadProfilePictureCommandResponse
				{
					IsSuccess = false
				};
			}

			user.ProfilePicture = request.ProfilePicturePath;

			await _userRepository.UpdateAsync(user);

			await _userRepository.SaveAsync();
			


			return new UploadProfilePictureCommandResponse
			{
				IsSuccess = true,
				ProfilePicturePath = user.ProfilePicture
				
			};

		}

		

	}
}

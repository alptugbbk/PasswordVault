using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Application.Utilities.Cryptography;

namespace PasswordVaultAPI.Application.CQRS.Commands.User.ResetPassword
{
	public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
	{
		private readonly IUserRepository _userRepository;
		private readonly ISHAEncryption _shaEncryption;
		private readonly ILogger<ResetPasswordCommandHandler> _logger;

		public ResetPasswordCommandHandler(IUserRepository userRepository, ISHAEncryption shaEncryption, ILogger<ResetPasswordCommandHandler> logger)
		{
			_userRepository = userRepository;
			_shaEncryption = shaEncryption;
			_logger = logger;
		}

		public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				return new ResetPasswordCommandResponse
				{
					IsSuccess = false,
					Message = "The request cannot be null."
				};
			}

			var user = await _userRepository.GetByIdAsync(request.ResetPasswordRequestDTO.UserId);

			if (user == null)
			{
				_logger.LogError("Reset Password Failed");
				return new ResetPasswordCommandResponse
				{
					IsSuccess = false,
					Message = "Reset Password Failed"
				};
			}

			_logger.LogInformation("{UserName} successfully reset password", user.UserName);

			user.Password = _shaEncryption.Encrypt(request.ResetPasswordRequestDTO.NewPassword, user.UserName);

			await _userRepository.UpdateAsync(user);
			await _userRepository.SaveAsync();

			return new ResetPasswordCommandResponse
			{
				IsSuccess = true,
				Message = "password change successful."
			};


		}



	}
}

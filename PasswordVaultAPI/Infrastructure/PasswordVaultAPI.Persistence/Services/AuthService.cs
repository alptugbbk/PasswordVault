using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PasswordVaultAPI.Application.DTOs.Auth;
using PasswordVaultAPI.Application.DTOs.Base;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Application.Services;
using PasswordVaultAPI.Application.Utilities.Cryptography;
using PasswordVaultAPI.Application.Utilities.Excepitons;
using PasswordVaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Persistence.Services
{
    public class AuthService : IAuthService
	{

		private readonly IUserRepository _userRepository;
		private readonly ISHAEncryption _shaEncryption;
		private readonly ILogger<AuthService> _logger;



		public AuthService(IUserRepository userRepository, ISHAEncryption shaEncryption, ILogger<AuthService> logger)
		{
			_userRepository = userRepository;
			_shaEncryption = shaEncryption;
			_logger = logger;
		}



		public async Task<UserDTO> RegisterUserAsync(RegisterDTO registerDto)
		{
			if (registerDto == null)
			{
				_logger.LogError("Registration failed: details cannot be null {UserName}", registerDto.UserName);
				return null;
			}

			var existingUser = await _userRepository.GetSingleAsync(x => x.UserName == registerDto.UserName || x.Email == registerDto.Email);

			if (existingUser != null)
			{
				_logger.LogError("Registration failed:  with Username username or Email already exists {UserName}", registerDto.UserName);
                return null;

            }

            var newUser = new User
			{
				
				FirstName = registerDto.FirstName,
				LastName = registerDto.LastName,
				UserName = registerDto.UserName,
				Email = registerDto.Email,
				Password = _shaEncryption.Encrypt(registerDto.Password, registerDto.UserName)
			};

			await _userRepository.AddAsync(newUser);

			await _userRepository.SaveAsync();

			_logger.LogInformation("Successfully registered {UserName}", registerDto.UserName);

			return new UserDTO
			{
				Id = newUser.Id.ToString(),
			};
		}



        public async Task<UserDTO> LoginUserAsync(LoginDTO loginDto)
        {
            if (loginDto == null)
			{
				_logger.LogError("Login failed: Detail cannot be null {UserName}", loginDto.UserName);
                return null;
            }

            var user = await _userRepository.GetSingleAsync(x => x.UserName == loginDto.UserName);
   
            if (user == null)
			{
				_logger.LogError("Login failed: {UserName} not found with provided username or password", loginDto.UserName);
                return null;
            }

			var verifyPassword = _shaEncryption.Encrypt(loginDto.Password, loginDto.UserName);

			if(verifyPassword != user.Password)
			{
				_logger.LogError("Login failed: {UserName} not found with provided Username or Password", loginDto.UserName);
                return null;
            }

			_logger.LogInformation("Successfully logged in. {UserName}", user.UserName);

			return new UserDTO
			{
				Id = user.Id.ToString(),
				UserName = user.UserName
			};

        }


			
    }
}

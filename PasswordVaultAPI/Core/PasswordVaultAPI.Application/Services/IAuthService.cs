using PasswordVaultAPI.Application.DTOs.Auth;
using PasswordVaultAPI.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.Services
{
    public interface IAuthService
	{
		Task<UserDTO> RegisterUserAsync(RegisterDTO registerDto);

		Task<UserDTO> LoginUserAsync(LoginDTO loginDto);

	}
}

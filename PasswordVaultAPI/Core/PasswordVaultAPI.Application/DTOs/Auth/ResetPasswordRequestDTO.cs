﻿

namespace PasswordVaultAPI.Application.DTOs.Auth
{
    public class ResetPasswordRequestDTO
    {
		public string UserId { get; set; }
		public string NewPassword { get; set; }
	}
}
using System.ComponentModel.DataAnnotations;

namespace PasswordVaultUI.DTOs.AuthDTO
{
	public class ResetPasswordDTO
	{
		public string UserId { get; set; }

		[Required]
		public string NewPassword { get; set; }

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Commands.User.ResetPassword
{
	public class ResetPasswordCommandResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
}

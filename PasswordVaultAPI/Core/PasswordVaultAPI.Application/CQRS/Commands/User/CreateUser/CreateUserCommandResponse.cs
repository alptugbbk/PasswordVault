using PasswordVaultAPI.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Commands.User.CreateUser
{
    public class CreateUserCommandResponse
	{
		public UserDTO User { get; set; }
	}
}

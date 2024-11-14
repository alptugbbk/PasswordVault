using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Commands.Platform.DeletePlatform
{
	public class DeletePlatformCommandResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
}

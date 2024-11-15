using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Commands.User.UploadProfilePicture
{
	public class UploadProfilePictureCommandResponse
	{
		public bool IsSuccess { get; set; }
		public string ProfilePicturePath { get; set; }
	}
}

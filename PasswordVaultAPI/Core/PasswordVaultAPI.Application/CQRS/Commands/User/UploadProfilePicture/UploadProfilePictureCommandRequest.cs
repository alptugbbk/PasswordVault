using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Commands.User.UploadProfilePicture
{
	public class UploadProfilePictureCommandRequest : IRequest<UploadProfilePictureCommandResponse>
	{
		public string UserId { get; set; }
		public string ProfilePicturePath { get; set; }
	}
}

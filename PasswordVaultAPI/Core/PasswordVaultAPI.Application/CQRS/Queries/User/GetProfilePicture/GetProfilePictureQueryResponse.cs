using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Queries.User.GetProfilePicture
{
	public class GetProfilePictureQueryResponse
	{
		public string ProfilePicturePath { get; set; }
	}
}

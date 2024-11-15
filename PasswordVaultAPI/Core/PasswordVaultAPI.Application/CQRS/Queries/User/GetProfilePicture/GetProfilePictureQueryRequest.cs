using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Queries.User.GetProfilePicture
{
	public class GetProfilePictureQueryRequest : IRequest<GetProfilePictureQueryResponse>
	{
		public string UserId { get; set; }
	}
}

using MediatR;
using PasswordVaultAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Queries.Platform.GetAllPlatformsByUserId
{
	public class GetPlatformsQueryRequest : IRequest<GetPlatformsQueryResponse>
	{
		public string UserId { get; set; }
	}
}

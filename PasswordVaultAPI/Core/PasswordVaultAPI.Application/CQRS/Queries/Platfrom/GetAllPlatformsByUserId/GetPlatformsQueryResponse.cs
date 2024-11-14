using PasswordVaultAPI.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Queries.Platform.GetAllPlatformsByUserId
{
    public class GetPlatformsQueryResponse
	{
		public IEnumerable<PlatformDTO> PlatformsResponseDto { get; set; }
	}
}

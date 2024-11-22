using MediatR;
using PasswordVaultAPI.Application.DTOs.Base;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Application.Services;
using PasswordVaultAPI.Application.Utilities.Cryptography;
using PasswordVaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Queries.Platform.GetAllPlatformsByUserId
{
    public class GetPlatformsQueryHandler : IRequestHandler<GetPlatformsQueryRequest, GetPlatformsQueryResponse>
	{
		private readonly IPlatformRepository _platformRepository;
		private readonly IAESEncryption _aesEncryption;

		public GetPlatformsQueryHandler(IPlatformRepository platformRepository, IAESEncryption aesEncryption)
		{
			_platformRepository = platformRepository;
			_aesEncryption = aesEncryption;
		}

		public async Task<GetPlatformsQueryResponse> Handle(GetPlatformsQueryRequest request, CancellationToken cancellationToken)
		{

			if (string.IsNullOrWhiteSpace(request.UserId))
			{
				return null;
			}


			var allPlatforms = _platformRepository.GetAll();

			var platforms = allPlatforms.Where(x => x.UserId.ToString() == request.UserId).ToList();

			var result = platforms.Select(x => new PlatformDTO
			{
				Id = x.Id.ToString(),
				Name = x.Name,
				UserName = x.UserName,
				Password = _aesEncryption.DecryptPassword(x.Password),
			}).ToList();

			return new GetPlatformsQueryResponse
			{
				PlatformsResponseDto = result ?? new List<PlatformDTO>()
			};

		}


	}
}

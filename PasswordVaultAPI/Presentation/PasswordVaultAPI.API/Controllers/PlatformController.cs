using MediatR;
using Microsoft.AspNetCore.Mvc;
using PasswordVaultAPI.Application.CQRS.Commands.Platform.CreatePlatform;
using PasswordVaultAPI.Application.CQRS.Commands.Platform.DeletePlatform;
using PasswordVaultAPI.Application.CQRS.Commands.Platform.UpdatePlatform;
using PasswordVaultAPI.Application.CQRS.Commands.User.UploadProfilePicture;
using PasswordVaultAPI.Application.CQRS.Queries.Platform.GetAllPlatformsByUserId;
using PasswordVaultAPI.Application.CQRS.Queries.User.GetProfilePicture;
using PasswordVaultAPI.Application.Utilities.Outcome;

namespace PasswordVaultAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlatformController : Controller
	{

		private readonly IMediator _mediator;

		public PlatformController(IMediator mediator)
		{
			_mediator = mediator;
		}



		[HttpPost("CreatePlatform")]
		public async Task<CreatePlatformCommandResponse> CreatePlatform([FromBody] CreatePlatformCommandRequest request)
		{
			var platform = await _mediator.Send(request);
			return platform;
		}



		[HttpPost("UpdatePlatform")]
		public async Task<UpdatePlatformCommandResponse> UpdatePlatform([FromBody] UpdatePlatformCommandRequest request)
		{
			var platform = await _mediator.Send(request);
			return platform;
		}



		[HttpPost("DeletePlatform")]
		public async Task<DeletePlatformCommandResponse> DeletePlatform([FromBody] DeletePlatformCommandRequest request)
		{
			var platform = await _mediator.Send(request);
			return platform;
		}



		[HttpPost("GetPlatform")]
		public async Task<GetPlatformsQueryResponse> GetPlatform([FromBody] GetPlatformsQueryRequest request)
		{
			var platform = await _mediator.Send(request);
			return platform;
		}



		[HttpPost("UploadProfilePicture")]
		public async Task<UploadProfilePictureCommandResponse> UploadProfilePicture(UploadProfilePictureCommandRequest request)
		{
			var profile = await _mediator.Send(request);
			return profile;
		}



        [HttpPost("GetProfilePicture")]
        public async Task<GetProfilePictureQueryResponse> GetProfilePicture(GetProfilePictureQueryRequest request)
        {
            var profile = await _mediator.Send(request);
            return profile;
        }




    }
}

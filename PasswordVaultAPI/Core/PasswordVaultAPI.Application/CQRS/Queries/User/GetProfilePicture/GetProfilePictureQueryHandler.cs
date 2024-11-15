using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PasswordVaultAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Queries.User.GetProfilePicture
{
    public class GetProfilePictureQueryHandler : IRequestHandler<GetProfilePictureQueryRequest, GetProfilePictureQueryResponse>
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetProfilePictureQueryHandler> _logger;



        public GetProfilePictureQueryHandler(IUserRepository userRepository, ILogger<GetProfilePictureQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }



        public async Task<GetProfilePictureQueryResponse> Handle(GetProfilePictureQueryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var allUsers = _userRepository.GetAll();

            var user = await allUsers.Where(x => x.Id.ToString() == request.UserId).FirstOrDefaultAsync();

            var profilePicturePath = user.ProfilePicture;

            return new GetProfilePictureQueryResponse
            {
                ProfilePicturePath = profilePicturePath
            };

        }



    }
}

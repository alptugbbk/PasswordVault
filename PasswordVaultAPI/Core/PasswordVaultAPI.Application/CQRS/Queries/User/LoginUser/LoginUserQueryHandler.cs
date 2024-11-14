using MediatR;
using Microsoft.AspNetCore.Http;
using PasswordVaultAPI.Application.DTOs;
using PasswordVaultAPI.Application.Services;
using PasswordVaultAPI.Application.Utilities.Excepitons;

namespace PasswordVaultAPI.Application.CQRS.Queries.User.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQueryRequest, LoginUserQueryResponse>
    {

        private readonly IAuthService _userService;



        public LoginUserQueryHandler(IAuthService userService)
        {
            _userService = userService;

        }

        public async Task<LoginUserQueryResponse> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {

            var user = await _userService.LoginUserAsync(request.LoginDto);

            if (user == null) throw new Exception("Failed login");

            return new LoginUserQueryResponse
            {
                User = user,
            };


        }


    }
}

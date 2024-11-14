using MediatR;
using PasswordVaultAPI.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Queries.User.LoginUser
{
    public class LoginUserQueryRequest : IRequest<LoginUserQueryResponse>
    {
        public LoginDTO LoginDto { get; set; }
    }
}

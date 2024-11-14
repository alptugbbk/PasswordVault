using PasswordVaultAPI.Application.DTOs.Auth;
using PasswordVaultAPI.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Queries.User.LoginUser
{
    public class LoginUserQueryResponse
    {
        public UserDTO User { get; set; }
    }

}

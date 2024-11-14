using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.CQRS.Queries.User.ForgotPassword
{
    public class ForgotPasswordQueryRequest : IRequest<ForgotPasswordQueryResponse>
    {
        public string Email { get; set; }
    }
}

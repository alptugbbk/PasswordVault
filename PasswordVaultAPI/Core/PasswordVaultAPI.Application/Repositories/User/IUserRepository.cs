using PasswordVaultAPI.Domain.Entities;
using PasswordVaultAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.Repositories
{
	public interface IUserRepository :IGenericRepository<User>
	{
	}
}

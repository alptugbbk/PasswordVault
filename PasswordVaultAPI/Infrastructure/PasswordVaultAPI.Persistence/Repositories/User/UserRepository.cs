using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Domain.Entities;
using PasswordVaultAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Persistence.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(MyContext context) : base(context)
		{
		}
	}
}

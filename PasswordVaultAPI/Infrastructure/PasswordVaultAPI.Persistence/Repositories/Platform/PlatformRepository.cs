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
	public class PlatformRepository : GenericRepository<Platform>, IPlatformRepository
	{
		public PlatformRepository(MyContext context) : base(context)
		{
		}
	}
}

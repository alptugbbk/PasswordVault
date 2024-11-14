using PasswordVaultAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Domain.Entities
{
	public class Platform : BaseEntity
	{
		public string Name { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }


		// relational
		public User User { get; set; }
		public Guid UserId { get; set; }

	}
}

using Microsoft.EntityFrameworkCore;
using PasswordVaultAPI.Domain.Entities;
using PasswordVaultAPI.Domain.Entities.Common;
using PasswordVaultAPI.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Persistence.Context
{
	public class MyContext : DbContext
	{
		public MyContext(DbContextOptions<MyContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Platform> Platforms { get; set; }



		// configuration
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new PlatformConfiguration());
		}


		// interceptor
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var datas = ChangeTracker.Entries<BaseEntity>();
			foreach (var data in datas)
			{
				_ = data.State switch
				{
					EntityState.Added => data.Entity.CreatedDate == DateTime.UtcNow,
					EntityState.Modified => data.Entity.UpdatedDate == DateTime.UtcNow,
					EntityState.Deleted => true
				};
			}



			return await base.SaveChangesAsync(cancellationToken);
		}

	}
}

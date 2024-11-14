using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordVaultAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Persistence.Configurations
{
	public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
	{
		public void Configure(EntityTypeBuilder<Platform> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasOne(x => x.User).WithMany(x => x.Platforms).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
	
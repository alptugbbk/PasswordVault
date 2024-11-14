using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PasswordVaultAPI.Application.Utilities.Cryptography;
using PasswordVaultAPI.Persistence.Context;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Persistence.Repositories;
using PasswordVaultAPI.Persistence.Configurations;
using PasswordVaultAPI.Application.Services;
using PasswordVaultAPI.Persistence.Services;
using PasswordVaultAPI.Persistence.Services.Utilities.Cryptography;



namespace PasswordVaultAPI.Persistence
{
    public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services)
		{
			services.AddDbContext<MyContext>(options => options.UseSqlServer(DbConfiguration.ConnectionString));
			services.AddScoped<IPlatformRepository, PlatformRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ISHAEncryption, SHAEncryption>();
			services.AddScoped<IAESEncryption, AESEncryption>();
			services.AddScoped<IAuthService,AuthService>();
		
		}
	}
}

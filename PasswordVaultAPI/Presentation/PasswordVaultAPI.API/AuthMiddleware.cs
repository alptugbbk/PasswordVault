using Microsoft.Extensions.Logging;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Application.Utilities.Excepitons;

public class AuthMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<AuthMiddleware> _logger;

	public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context, IServiceScopeFactory serviceScopeFactory)
	{

		try
		{

			if (context.Request.Path.StartsWithSegments("/api/auth"))
			{
				await _next(context);
				return;
			}

			using (var scope = serviceScopeFactory.CreateScope())
			{

				var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

				var userId = context.Request.Headers["UserId"].ToString();

				var user = userRepository.GetAll(false).Where(x => x.Id.ToString() == userId).FirstOrDefault();

				if (user == null)
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					await context.Response.WriteAsync("User is not authorized.");
					return;
				}

				_logger.LogInformation("{UserName} check", user.UserName);

				await _next(context);

			}

		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An unexpected error occurred during the request.");
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");

		}

	}


}

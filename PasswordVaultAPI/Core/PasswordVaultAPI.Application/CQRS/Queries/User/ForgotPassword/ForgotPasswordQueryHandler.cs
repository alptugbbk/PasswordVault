using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Application.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;


namespace PasswordVaultAPI.Application.CQRS.Queries.User.ForgotPassword
{
	public class ForgotPasswordEmailQueryHandler : IRequestHandler<ForgotPasswordQueryRequest, ForgotPasswordQueryResponse>
	{
		private readonly IUserRepository _userRepository;
		private readonly ILogger<ForgotPasswordEmailQueryHandler> _logger;




		public ForgotPasswordEmailQueryHandler(IUserRepository userRepository, ILogger<ForgotPasswordEmailQueryHandler> logger)
		{
			_userRepository = userRepository;
			_logger = logger;
		}


		public async Task<ForgotPasswordQueryResponse> Handle(ForgotPasswordQueryRequest request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(request.Email))
			{
				return new ForgotPasswordQueryResponse
				{
					IsSuccess = false,
					Message = "The email address cannot be empty."
				};
			}

			var userMail = await _userRepository.GetSingleAsync(x => x.Email == request.Email);

			if (userMail == null)
			{
				return new ForgotPasswordQueryResponse
				{
					IsSuccess = false,
					Message = "User not found."
				};
			}

			var userId = userMail.Id;

			var resetPasswordLink = $"https://localhost:7076/Auth/ResetPassword?userId={userId}";

			var from = "";
			var to = request.Email;
			var subject = "PV Forgot Password";
			var body = $"To reset your password, click the following link: {resetPasswordLink}";

			var message = new MailMessage(from, to)
			{
				Subject = subject,
				Body = body,
				IsBodyHtml = false
			};

			using (var client = new SmtpClient("smtp.gmail.com", 587))
			{
				client.EnableSsl = true;
				client.Credentials = new System.Net.NetworkCredential("", "");
				await client.SendMailAsync(message);
			}

			return new ForgotPasswordQueryResponse
			{
				IsSuccess = true,
				Message = "A password reset link has been sent to your email."
			};

		}

	}
}


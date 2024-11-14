using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordVaultUI.DTOs.AuthDTO;
using PasswordVaultUI.DTOs.UserDTO.UserDTO;
using PasswordVaultUI.Models;
using System.Security.Claims;
using System.Text;

namespace PasswordVaultUI.Controllers
{
    public class AuthController : Controller
	{

		private readonly HttpClient _httpClient;

		public AuthController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}



		public IActionResult Register()
		{
			return View();
		}


		
		public IActionResult Login()
		{
			return View();
		}



		public IActionResult ResetPassword(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return RedirectToAction("ForgotPassword", "Auth");
			}

			return View(new ForgotPasswordViewModel { UserId = userId });
		}



		public IActionResult ForgotPassword()
		{
			return View();
		}





		[HttpPost]
		public async Task<IActionResult> Register([FromBody]RegisterViewModel registerViewModel)
		{

			var registerRequestDto = new RegisterRequestDTO()
			{
				FirstName = registerViewModel.FirstName,
				LastName = registerViewModel.LastName,
				UserName = registerViewModel.UserName,
				Email = registerViewModel.Email,
				Password = registerViewModel.Password
			};

			var request = new
			{
				RegisterDto = registerRequestDto,
			};

			var convert = JsonConvert.SerializeObject(request);

			var content = new StringContent(convert, Encoding.UTF8,"application/json");

			var response = await _httpClient.PostAsync("https://localhost:7173/api/Auth/Register", content);

			if (response.IsSuccessStatusCode)
			{
				return Json( new {success = true });
			}
			else
			{
				return Json( new { success = false});
			}

		}



		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
		{

			var loginDto = new LoginRequestDTO()
			{
				UserName = loginViewModel.UserName,
				Password = loginViewModel.Password
			};

			var request = new 
			{
				LoginDto = loginDto
			};

			var convert = JsonConvert.SerializeObject(request);

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("https://localhost:7173/api/Auth/Login", content);

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();

				var user = JsonConvert.DeserializeObject<LoginResponseDTO>(result);

				HttpContext.Session.SetString("UserId", user.User.Id.ToString());
				HttpContext.Session.SetString("UserName", user.User.UserName);

				if (loginViewModel.RememberMe)
				{
					var cookie = new CookieOptions
					{
						Expires = DateTimeOffset.UtcNow.AddDays(10),
						HttpOnly = true,
						Secure = true
					};
					Response.Cookies.Append("UserId", user.User.Id.ToString(), cookie);
				}

				return Json(new { success = true });
			}
			else
			{
                return Json(new { success = false });
            }

		}



		[HttpPost]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel forgotPasswordViewModel)
		{
			var userId = HttpContext.Session.GetString("UserId");

			var request = new ForgotPasswordDTO()
			{
				Email = forgotPasswordViewModel.Email,
			};

			var convert = JsonConvert.SerializeObject(request);

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("https://localhost:7173/api/auth/forgotpassword", content);

			if (response.IsSuccessStatusCode)
			{;

				return Json( new { success = true } );
			}
			else
			{
				return Json(new { success = false });
			}

        }


		
		[HttpPost]
		public async Task<IActionResult> ResetPassword([FromBody] ForgotPasswordViewModel forgotPasswordViewModel)
		{

			var forgotPasswordDto = new ResetPasswordDTO()
			{	
				UserId = forgotPasswordViewModel.UserId,
				NewPassword = forgotPasswordViewModel.NewPassword
			};

			var request = new
			{
				ResetPasswordRequestDTO = forgotPasswordDto
			};

			var convert = JsonConvert.SerializeObject(request);

			var content = new StringContent(convert, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("https://localhost:7173/api/auth/resetpassword", content);

			if (response.IsSuccessStatusCode)
			{
				return Json(new { success = true });
			}
			else
			{
				return Json(new { success = false });
			}
		}




	}
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordVaultUI.DTOs.AuthDTO;
using PasswordVaultUI.DTOs.PlatformDTO;
using PasswordVaultUI.Models;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace PasswordVaultUI.Controllers
{
	[SessionCheckFilter]
	public class PlatformController : Controller
    {

        private readonly HttpClient _httpClient;



        public PlatformController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        
        public IActionResult PIndex(PlatformViewModel platformViewModel)
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName ?? "Kullanıcı Adı";

            var model = new PlatformViewModel
            {
				LoginUserName = userName
            };

            return View(model);
        }


        
        public async Task<IActionResult> CreatePlatform([FromBody] PlatformViewModel dto)
        {
            var id = HttpContext.Session.GetString("UserId");

            var RequestPlatformDto = new CreatePlatformRequestDTO()
            {
                
                Name = dto.Name,
                UserName = dto.UserName,
                Password = dto.Password,
                UserId = id,
            };

            var model = new
            {
                PlatformRequestDto = RequestPlatformDto
            };

            var convert = JsonConvert.SerializeObject(model);

            var content = new StringContent(convert, Encoding.UTF8, "application/json");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7173/api/platform/CreatePlatform")
            {
                Content = content
            };

            httpRequestMessage.Headers.Add("UserId", HttpContext.Session.GetString("UserId"));

            var response = await _httpClient.SendAsync(httpRequestMessage);

			return Json(model);
		}



        public async Task<IActionResult> GetPlatform()
        {

            var userId = HttpContext.Session.GetString("UserId");

            var getAllRequestDto = new GetAllPlatformRequestDTO
            {
                UserId = userId
            };

            var convert = JsonConvert.SerializeObject(getAllRequestDto);

            var contet = new StringContent(convert, Encoding.UTF8, "application/json");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7173/api/platform/GetPlatform")
            {
                Content = contet
            };

            httpRequestMessage.Headers.Add("UserId", userId);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            var result = await response.Content.ReadAsStringAsync();

            var platform = JsonConvert.DeserializeObject<PlatformsResponseDTO>(result);

            return Json(platform.PlatformsResponseDto);
        }



		public async Task<IActionResult> UpdatePlatform([FromBody] PlatformViewModel platformViewModel)
		{
			var userId = HttpContext.Session.GetString("UserId");

			var updatePlatformRequestDto = new UpdatePlatformRequestDTO()
			{
				Id = platformViewModel.Id,
				Name = platformViewModel.Name,
				UserName = platformViewModel.UserName,
				Password = platformViewModel.Password,
			};

			var updatePlatformCommandRequest = new
			{
				UpdatePlatformRequestDTO = updatePlatformRequestDto
			};

			var content = new StringContent(JsonConvert.SerializeObject(updatePlatformCommandRequest), Encoding.UTF8, "application/json");

			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7173/api/Platform/UpdatePlatform")
			{
				Content = content
			};

			httpRequestMessage.Headers.Add("UserId", userId);

			var response = await _httpClient.SendAsync(httpRequestMessage);

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				var platform = JsonConvert.DeserializeObject<GetAllPlatfromResponseDTO>(result);
				return Json(platform);
			}
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				return StatusCode((int)response.StatusCode, $"Failed to update platform: {errorContent}");
			}
		}

            

		public async Task<IActionResult> DeletePlatform([FromBody] PlatformViewModel platformViewModel)
        {

            var userId = HttpContext.Session.GetString("UserId");

            var deletePlatformRequestDto = new DeletePlatformRequestDTO()
            {
                Id = platformViewModel.Id,
            };

            var content = new StringContent(JsonConvert.SerializeObject(deletePlatformRequestDto), Encoding.UTF8, "application/json");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7173/api/Platform/DeletePlatform")
            {
                Content = content
            };

            httpRequestMessage.Headers.Add("UserId", userId);

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Platfrom delete is success" });
            }
            else
            {
				var errorContent = await response.Content.ReadAsStringAsync();
				return StatusCode((int)response.StatusCode, $"Failed to Delete platform: {errorContent}");
			}


		}



        public IActionResult Logout()
        {
           HttpContext.Session.Remove("UserId");

			return Json(new { redirectUrl = Url.Action("Login", "Auth") });
		}



    }
}
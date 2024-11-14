namespace PasswordVaultUI.DTOs.PlatformDTO
{
	public class PlatformsResponseDTO
	{
		public List<GetAllPlatfromResponseDTO> PlatformsResponseDto { get; set; }
	}

	public class GetAllPlatfromResponseDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}

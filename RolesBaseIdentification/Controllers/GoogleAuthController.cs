using Google.Authenticator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RolesBaseIdentification.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GoogleAuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IConfiguration _configuration;

		public GoogleAuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		[HttpGet("EnableGoogleAuthenticator")]
		public async Task<bool> EnableGoogleAuthenticator(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);

			string secretName = "secretName";
			string secretKey = "secretKey";

			var authenticator = new TwoFactorAuthenticator();
			return true;
		}

	}
}

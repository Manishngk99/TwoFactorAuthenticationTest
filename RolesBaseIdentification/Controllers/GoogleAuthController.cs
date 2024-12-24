using Google.Authenticator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RolesBaseIdentification.Model.DTOs.Response;
using System.Text;

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

		[HttpGet("SetGoogleAuthenticator")]
		public async Task<bool> SetGoogleAuthenticator(string userName)
		{
			try
			{
				var user = await _userManager.FindByNameAsync(userName);

				string secretName = "nirajName";
				string secretKey = "nirajKey";

				var accountSecretKey = $"{secretKey}-{user.Email}";
				var result = await _userManager.SetAuthenticationTokenAsync(user, "Google", secretName, accountSecretKey);
				
				return result != null ? true : false;
			}
			catch (Exception)
			{

				throw;
			}

		}

		[HttpGet("EnableGoogleAuthenticator")]
		public async Task<EnableGoogleAuthResponse> EnableGoogleAuthenticator(string userName)
		{
			try
			{
				var user = await _userManager.FindByNameAsync(userName);

				string secretName = "nirajName";
				string secretKey = "nirajKey";

				var authenticator = new TwoFactorAuthenticator();
				var setupCode = authenticator.GenerateSetupCode("Niraj setup", user.Email, Encoding.ASCII.GetBytes(secretKey));
				string qrCodeUrl = setupCode.QrCodeSetupImageUrl;

				EnableGoogleAuthResponse enableGoogleAuthResponse = new EnableGoogleAuthResponse() { QRCode = qrCodeUrl, EntryKey = setupCode.ManualEntryKey };
				return enableGoogleAuthResponse;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpGet("VerifyGoogleAuthenticator")]
		public async Task<bool> VerifyGoogleAuthenticator(string userName, string code)
		{
			try
			{
				var user = await _userManager.FindByNameAsync(userName);
				string secretName = "nirajName";
				string secretKey = "nirajKey";
				var authenticator = new TwoFactorAuthenticator();
				var pinCode = authenticator.GetCurrentPIN(secretKey);
				if (pinCode == code)
				{
					var result = authenticator
					.ValidateTwoFactorPIN(secretKey, code);

					return true;

				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		[HttpGet("DisableGoogleAuthenticator")]
		public async Task<bool> DisableGoogleAuthenticator(string userName)
		{
			try
			{
				var user = await _userManager.FindByNameAsync(userName);
				string secretName = "nirajName";
				// Remove the secret key associated with the user's account
				await _userManager.RemoveAuthenticationTokenAsync(user, "Google", secretName);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

	}
}

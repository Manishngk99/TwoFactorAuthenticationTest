namespace RolesBaseIdentification.Model.DTOs.Request
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int OtpType { get; set; }    
        public int Otp { get; set; }

    }
}

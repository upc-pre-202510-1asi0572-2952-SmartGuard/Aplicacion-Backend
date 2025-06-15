namespace UPC.SmartLock.BE.Auth.Request
{
    public class AuthRequest : IAuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

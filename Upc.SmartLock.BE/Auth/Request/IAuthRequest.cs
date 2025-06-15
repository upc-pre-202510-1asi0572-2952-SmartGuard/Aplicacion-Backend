namespace UPC.SmartLock.BE.Auth.Request
{
    public interface IAuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

namespace SelfieAwookie.API.UI.Applications.DTOs
{
    public class AuthenticateUserDto
    {
        #region Properties
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        #endregion
    }
}

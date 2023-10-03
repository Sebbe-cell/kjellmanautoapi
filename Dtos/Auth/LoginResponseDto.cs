namespace kjellmanautoapi.Dtos.Auth
{
    public class LoginResponseDto
    {
        public UserLoginDto? UserData { get; set; }
        public string Token { get; set; } = "";
    }
}
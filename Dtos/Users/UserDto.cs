namespace kjellmanautoapi.Dtos.UserDto
{
    public class UserLoginDto
    {
        public  string UserName { get; set; } = "";
        public string PasswordHash { get; set; } = "";
    }
}
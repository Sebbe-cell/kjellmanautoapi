namespace kjellmanautoapi.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<UserLoginDto>> Register(UserLoginDto newUser);

        Task<ServiceResponse<UserLoginDto>> Login(string username, string password);

        public string CreateToken(UserLoginDto user);
    }
}
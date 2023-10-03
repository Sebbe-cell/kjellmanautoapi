using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace kjellmanautoapi.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(IMapper mapper, DataContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<UserLoginDto>> Register(UserLoginDto newUser)
        {
            var serviceResponse = new ServiceResponse<UserLoginDto>();
            try
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.PasswordHash);

                // Update the UserDto object with the hashed password
                newUser.PasswordHash = passwordHash;

                // Map the UserDto to Users entity
                var user = _mapper.Map<Users>(newUser);

                // Add the new user to the context
                _context.Users.Add(user);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Map the created user back to UserDto for the response
                serviceResponse.Data = _mapper.Map<UserLoginDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UserLoginDto>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<UserLoginDto>();
            try
            {
                // Find the user by their username in the database
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

                // Check if the user exists
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found.";
                    return serviceResponse;
                }

                // Verify the password using BCrypt
                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Incorrect password.";
                    return serviceResponse;
                }

                // User is successfully authenticated
                serviceResponse.Data = _mapper.Map<UserLoginDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public string CreateToken(UserLoginDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value ?? ""
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwtHandler = new JwtSecurityTokenHandler();

            // Write the token to a string
            var jwt = jwtHandler.WriteToken(token);

            return jwt;
        }

    }
}
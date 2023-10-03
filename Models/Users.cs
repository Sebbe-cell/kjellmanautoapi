namespace kjellmanautoapi.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "";
        public string PasswordHash { get; set; } = "";
    }
}
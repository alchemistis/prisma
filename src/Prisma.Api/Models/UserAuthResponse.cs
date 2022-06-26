namespace Prisma.Api.Models
{
    public class UserAuthResponse
    {
        public string? Username { get; set; }
        public string? Token { get; set; }

        public UserAuthResponse(string name, string token)
        {
            Username = name;
            Token = token;
        }
    }
}

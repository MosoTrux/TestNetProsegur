namespace TestNetProsegur.Application.Dtos.Auth
{
    public class LoginResultDto
    {
        public string? Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
        public string? Token { get; set; }
    }
}

namespace Core.Application.WebApi
{
    public class JwtConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int Expires { get; set; }
    }
}

namespace Blog;

public static class Configuration
{
    //TOKEN - JWT - Json Web Token
    public static string JwtKey = "51f07a62458d4572b40cecef67083e90=";
    public static string apiKeyName = "api_key";
    public static string apiKey = "curso_api_IlTevUM/z0ey3NwCV/unWg==";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

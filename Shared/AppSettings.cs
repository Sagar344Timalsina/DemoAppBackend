namespace DemoAppBE.Shared
{
    public class AppSettings:IAppSettings
    {
        public int CacheDuration { get; set; }
        public int JWTTimeOut { get; set; }
        public string JWTSecret { get; set; }
        public string JWTIssuer { get; set; }
        public string JWTAudience { get; set; }
        public string ConnectionString { get; set; }
        public string Path { get; set; }
        public string Drive { get; set; }
    }
}

namespace DemoAppBE.Shared
{
    public interface IAppSettings
    {
        string ConnectionString { get; }
        int CacheDuration { get; }
        public int JWTTimeOut { get; }
        public string JWTSecret { get; }
        public string JWTIssuer { get; }
        public string JWTAudience { get; }
        public string Drive { get; }
        public string Path { get; }
    }
}

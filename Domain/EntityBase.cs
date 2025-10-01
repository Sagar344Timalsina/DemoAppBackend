namespace DemoAppBE.Domain
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTimeOffset Created { get; private set; } = DateTimeOffset.Now;
        public DateTimeOffset LastModified { get; private set; } = DateTimeOffset.Now;
        public void UpdateLastModified()
        {
            LastModified = DateTimeOffset.Now;
        }
        public EntityBase()
        {
            LastModified = DateTimeOffset.Now;
        }
    }
}

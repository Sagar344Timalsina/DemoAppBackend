namespace DemoAppBE.Domain
{
    public class Role:EntityBase
    {
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}

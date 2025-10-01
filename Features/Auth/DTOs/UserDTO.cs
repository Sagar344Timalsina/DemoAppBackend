namespace DemoAppBE.Features.Auth.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<UserRoleDTO> UserRoles { get; set; }
    }
}

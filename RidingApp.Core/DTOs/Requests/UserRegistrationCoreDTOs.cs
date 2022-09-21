namespace RidingApp.Core.DTOs.Requests
{
    public class UserRegistrationCoreDTOs
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int RoleCheck { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}

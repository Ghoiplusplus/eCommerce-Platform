using System.Text.Json.Serialization;

namespace UserService.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

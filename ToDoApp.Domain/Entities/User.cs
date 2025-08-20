using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string ImgUrl { get; set; }
    }
}

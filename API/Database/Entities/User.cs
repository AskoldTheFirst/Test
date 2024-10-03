using Microsoft.AspNetCore.Identity;

namespace API.Database.Entities
{
    public class User : IdentityUser
    {
        public string About { get; set; }

        public string Contacts { get; set; }
    }
}
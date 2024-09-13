using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace API.Database.Entities
{
    // [Index(nameof(Login), IsUnique = true)]
    // [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser
    {
        // public int Id { get; set; }

        // [StringLength(24), Required]
        // public string Login { get; set; }

        // [StringLength(320), Required]
        // public string Email { get; set; }

        // [StringLength(64), Required]
        // public string Passsword { get; set; }

        // public bool IsActive { get; set; }

        // public DateTime? Registered { get; set; }

        // public DateTime? LastLogin { get; set; }

        // public ICollection<Test> Tests { get; set; }
    }
}
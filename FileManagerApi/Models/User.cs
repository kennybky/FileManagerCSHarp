using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagerApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string Name { get =>  FirstName + " " + LastName; }

       
        public ICollection<UserRole> Roles { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }
    }

    public enum Role
    {
        User = 0, Developer = 1, Manager = 2, Admin = 3
    }

    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; }



        public Role Role { get; set; }
    }
}

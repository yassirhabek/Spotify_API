using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Interface.DTO;

namespace Logic.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        
        [Required(ErrorMessage = "Required")]
        public string Username { get; set; }
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        public string VerPass { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public User(UserDTO userDTO)
        {
            UserID = userDTO.UserID;
            Username = userDTO.Username;
            Email = userDTO.Email;
            Password = userDTO.Password;
            VerPass = userDTO.VerPass;
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User(string username, string email, string password, string verpass)
        {
            Username = username;
            Password = password;
            Email = email;
            VerPass = verpass;
        }
        public UserDTO UserToDTO()
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserID = UserID;
            userDTO.Username = Username;
            userDTO.Password = Password;
            userDTO.VerPass = VerPass;
            return userDTO;
        }
    }
}

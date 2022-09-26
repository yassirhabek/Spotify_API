using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.DTO
{
    public class UserDTO
    {
        [Key]
        public int UserID { get; set; }
        public string? Username;
        public string? Email;
        public string? Password;
        public string? VerPass;
        public DateTime CreatedDate;
        public DateTime? LastLoginDate;
    }
}

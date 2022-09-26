using Logic.Model;
using Interface.Interface;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Interface.DTO;
using Logic.Model;

namespace Logic.Containers
{
    public class UserContainer
    {
        private IUserContainer _iUserContainer;

        public UserContainer(IUserContainer iUserContainer)
        {
            _iUserContainer = iUserContainer;
        }

        public bool Login(User user, out List<string> errors)
        {
            errors = new List<string>();

            // validate if values are filled
            if (string.IsNullOrEmpty(user.Email))
            {
                errors.Add("email is required");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                errors.Add("password is required");
            }

            if (errors.Count == 0) // if all required fields are properly filled
            {
                string hashedPw = ComputeSha256Hash(user.Password + user.Email);
                UserDTO userDTO = new UserDTO() { Email = user.Email, Password = hashedPw };
                if (!_iUserContainer.Login(userDTO))
                {
                    errors.Add("wrong email/password");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            errors.Add("unexpected error");
            return false;
        }

        public int Register(User user, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(user.Email))
            {
                errors.Add("email is required");
            }
            if (string.IsNullOrEmpty(user.Username))
            {
                errors.Add("username is required");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                errors.Add("password is required");
            }
            if (string.IsNullOrEmpty(user.VerPass))
            {
                errors.Add("password confirmation is required");
            }

            if (errors.Count == 0)
            {
                string emailRegEx = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                    + "@"
                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
                if (!Regex.IsMatch(user.Email, emailRegEx))
                {
                    errors.Add("email must be valid");
                }
                if (_iUserContainer.checkIfEmailExists(user.Email))
                {
                    errors.Add("account already linked to this email");
                }
                if (user.Password.Length < 7)
                {
                    errors.Add("password must be at least 8 characters");
                }
                if (!Regex.IsMatch(user.Password, @"[a-z]"))
                {
                    errors.Add("password must contain at least 1 lowercase letter");
                }
                if (!Regex.IsMatch(user.Password, @"[A-Z]"))
                {
                    errors.Add("password must contain at least 1 uppercase letter");
                }
                if (!Regex.IsMatch(user.Password, @"(?=.*\d)|(?=.*[!-\/:-@[-`{-~])")) // digit | symbol
                {
                    errors.Add("password must contain at least 1 symbol/digit");
                }
                if (user.Password != user.VerPass)
                {
                    errors.Add("two passwords must match");
                }
            }

            if (errors.Count == 0) // if any errors occurred
            {
                string passwordHash = ComputeSha256Hash(user.Password + user.Email);
                UserDTO userDTO = new UserDTO()
                {
                    Email = user.Email,
                    Username = user.Username,
                    Password = passwordHash,
                };
                return _iUserContainer.Register(userDTO);
            }
            else
                return 0;
        }
        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public User GetSingleUser(User user)
        {
            UserDTO userDTO = new UserDTO()
            {
                Email = user.Email
            };
            UserDTO completedUserDTO = _iUserContainer.GetSingleUser(userDTO);

            User completedUser = new User(completedUserDTO);
            return completedUser;
        }
    }
}
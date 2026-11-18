using System.ComponentModel.DataAnnotations;


namespace Project.ViewModel
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("password")]
        public string Confirmpassword { get; set; }
        public string address { get; set; }
    }
}

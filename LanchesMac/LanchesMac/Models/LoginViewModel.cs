using Microsoft.Build.Framework;
using Xunit.Sdk;

namespace LanchesMac.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}

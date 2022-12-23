using LanchesMac.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, 
                                SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) //Verifica se não é válido
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByNameAsync(loginVM.UserName); //Procura o usuário

            if (user != null)
            {   //Procura a senha
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, false);

                if(result.Succeeded) //Se o usuário e senha estiverem corretos
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl)) //Se não tiver Url para voltar
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Falha ao realizar login!!"); //Se não der certo o login, retorna um erro
            return View(loginVM); //Retorna o erro pra view
        }
    }
}

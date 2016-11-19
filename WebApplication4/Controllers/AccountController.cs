using System.Web.Mvc;
using System.Web.Security;
using WebApplication4.Services;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    public class AccountController : Controller
    {
        private AccountService _accountService;

        public AccountController()
        {
            _accountService = new AccountService();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (_accountService.Login(model.Login, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Имя пользователя и пароль были введены неверно. Либо ваш пользователь не зарегистрирован.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
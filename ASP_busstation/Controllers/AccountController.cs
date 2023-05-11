
using ASP_busstation.Models1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ASP_busstation.ViewModel;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP_busstation.Controllers
{
    //[Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        /// <summary>
        /// Осуществляет регистрацию пользователя.
        /// </summary>
        /// <param name="model">Содержит данные из окна регистрации.</param>
        /// <returns>Код ответа.</returns>
        [HttpPost]
        [Route("api/account/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new() { Email = model.Email, UserName = model.Email };
                // Добавление нового пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Установка роли User
                    await _userManager.AddToRoleAsync(user, "user");
                    // Установка куки
                    await _signInManager.SignInAsync(user, false);
                    return Ok(new { message = "Добавлен новый пользователь: " + user.UserName });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    var errorMsg = new
                    {
                        message = "Пользователь не добавлен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Created("", errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return Created("", errorMsg);
            }
        }

        /// <summary>
        /// Осуществляет вход в систему.
        /// </summary>
        /// <param name="model">Содержит данные из окна регистрации.</param>
        /// <returns>Код ответа.</returns>
        [HttpPost]
        [Route("api/account/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    //User usr = await GetCurrentUserAsync();
                    //IList<string> roles = await _userManager.GetRolesAsync(usr);
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    IList<string>? roles = await _userManager.GetRolesAsync(user);
                    string? userRole = roles.FirstOrDefault();
                    return Ok(new { message = "Выполнен вход", userName = model.Email, userRole });
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    var errorMsg = new
                    {
                        message = "Вход не выполнен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Created("", errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return Created("", errorMsg);
            }
        }

        /// <summary>
        /// Осуществляет выход из системы.
        /// </summary>
        /// <returns>Код ответа.</returns>
        [HttpPost]
        [Route("api/account/logoff")]
        public async Task<IActionResult> Logoff()
        {
            User usr = await GetCurrentUserAsync();
            if (usr == null)
            {
                return Unauthorized(new { message = "Сначала выполните вход" });
            }
            // Удаление куки
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Выполнен выход", userName = usr.UserName });
        }

        /// <summary>
        /// Проверяет, авторизирован ли пользователь
        /// </summary>
        /// <returns>Информация о пользователе, если пользователь авторизирован, иначе - сообщение о необходимости авторизации</returns>
        [HttpGet]
        [Route("api/account/isauthenticated")]
        public async Task<IActionResult> IsAuthenticated()
        {
            User usr = await GetCurrentUserAsync();
            if (usr == null)
            {
                return Unauthorized(new { message = "Вы Гость. Пожалуйста, выполните вход" });
            }
            IList<string> roles = await _userManager.GetRolesAsync(usr);
            string? userRole = roles.FirstOrDefault();
            //var roles = await _userManager.GetRolesAsync(usr);
            //if (roles.Count() > 0) { }
            return Ok(new { message = "Сессия активна", userName = usr.UserName });
        }

        /// <summary>
        /// Returns the user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in
        /// the principal or null.
        /// </summary>
        /// <returns>The user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in
        /// the principal or null</returns>
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
        


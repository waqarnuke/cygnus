
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Identity;
using Web.ViewModel;
using Utility.Common;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace Web.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;


        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult Login(string returnUrl)
        {
            returnUrl??=Url.Content("~/");
            LoginVM loignVM = new ()
            {
                RedirectUrl = returnUrl
            };
           
            return View(loignVM);
        }
        public IActionResult Register(string returnUrl=null)
        {
            returnUrl??=Url.Content("~/");
            if(!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait(); 
            }
            if(!_roleManager.RoleExistsAsync(SD.Role_Company).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).Wait();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).Wait(); 
            }
            RegisterVM registerVM = new ()
            {
                RoleList = _roleManager.Roles.Select(x => new SelectListItem{
                    Text = x.Name,
                    Value = x.Name
                }),
                CompanyList = _unitOfWork.Company.GetAll().Select(x => new SelectListItem{
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                RedirectUrl = returnUrl
            };
            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM,string returnUrl=null)
        {
            returnUrl??=Url.Content("~/");
            AppUser user = new()
            {
                Email = registerVM.Email,
                UserName = registerVM.Email,
                DisplayName = registerVM.Name,
                NormalizedEmail = registerVM.Email.ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = registerVM.PhoneNumber
            } ;
            
            if(registerVM.Role == SD.Role_Company)
            {
                user.CompanyId = registerVM.CompanyRole;
            }
            
            var result  = await _userManager.CreateAsync(user,registerVM.Password);
            
            if(result.Succeeded)
            {
                if(!string.IsNullOrEmpty(registerVM.Role))
                {
                    await _userManager.AddToRoleAsync(user, registerVM.Role);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user,SD.Role_Customer);
                }

                // var userId = await _userManager.GetUserIdAsync(user);
                //     var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //     code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //     var callbackUrl = Url.Page(
                //         "/Account/ConfirmEmail",
                //         pageHandler: null,
                //         values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                //         protocol: Request.Scheme);
                // // send email
                // await _emailSender.SendEmailAsync(registerVM.Email, "Confirm your email",
                //         $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if(User.IsInRole(SD.Role_Admin))
                {
                    TempData["success"] = "New User Created Successfully";
                }
                else
                {
                    await _signInManager.SignInAsync(user,isPersistent:false);
                }
                
                if(string.IsNullOrEmpty(registerVM.RedirectUrl))
                {
                    return RedirectToAction("Index", "Home",new { area = "Customer" });
                }
                else
                {
                    return LocalRedirect(registerVM.RedirectUrl);
                }
                
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            registerVM.RoleList = _roleManager.Roles.Select(x => new SelectListItem{
                    Text = x.Name,
                    Value = x.Name
                });

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(ModelState.IsValid)
            {
                var result =  await _signInManager
                    .PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure:false);

                if(result.Succeeded)
                {
                    if(string.IsNullOrEmpty(loginVM.RedirectUrl))
                    {
                        return RedirectToAction("Index", "Home",new { area = "Customer" });
                    }
                    else
                    {
                        return LocalRedirect(loginVM.RedirectUrl);
                    }
                    
                } 
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }   
            }

            return View(loginVM);
        }

        public  async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home",new { area = "Customer" });
        }
      
        public  IActionResult AccessDenied()
        {
            return View();
        }
    }

}
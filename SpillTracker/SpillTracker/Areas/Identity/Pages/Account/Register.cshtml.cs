using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SpillTracker.Data;
using SpillTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SpillTracker.Utilities;
using Microsoft.Extensions.Configuration;

namespace SpillTracker.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly SpillTrackerDbContext _stDbContext;
        // add 
        private readonly ApplicationDbContext AspNetContext;
        private readonly IConfiguration _config;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            SpillTrackerDbContext spillTrackerDbContext,
            IConfiguration config)
            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _stDbContext = spillTrackerDbContext;
            // add 
            _roleManager = roleManager;
            _config = config;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }            
            
            // added username to the InputModel

            [Required]            
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            //

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string RoleOption { get; set; }

            [Required]
            public string Token { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


            // check captcha here
            string secret = _config["reCaptchaSecretKey"];
            GoogleReCAPTCHAResponse captchaResult = GoogleReCAPTCHAService.VerifyToken(Input.Token, secret);

            if (captchaResult.Success == false || captchaResult.Score <= 0.5)
            {
                ModelState.AddModelError(string.Empty, "We've detected that you might be a robot. Please Try again later.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                 
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //Creates one of our users
                    Stuser a = new Stuser
                    {
                        AspnetIdentityId = user.Id, // grabs the aspnet id of the new user
                        FirstName = Input.FirstName,
                        LastName = Input.LastName
                    };
                    _stDbContext.Add(a);
                    await _stDbContext.SaveChangesAsync();

                    // switch case that helps tie the roles to the database
                    
                    string roleName = "";
                    //_logger.LogInformation("Role Chosen: " + Input.RoleOption);
                    switch (Input.RoleOption)
                    {
                        case "1":
                            roleName = "Employee";
                            break;                       
                        case "2":
                            roleName = "FacilityManager";
                            break;
                    }

                    bool employeeRoleExists = await _roleManager.RoleExistsAsync(roleName);
                    if (!employeeRoleExists)
                    {
                        _logger.LogInformation("Adding " + roleName);
                        await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }

                    if(!await _userManager.IsInRoleAsync(user, roleName))
                    {
                        _logger.LogInformation("Adding user to the " + roleName +" role");
                        var userResult = await _userManager.AddToRoleAsync(user, roleName);
                    }                    

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

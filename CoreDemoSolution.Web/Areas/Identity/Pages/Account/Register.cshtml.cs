using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CoreDemoSolution.Data.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CoreDemoSolution.Repository.Utility;
using CoreDemoSolution.Repository.Models;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.IO;

namespace CoreDemoSolution.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly AppCommon _appCommon;
        private readonly EmailService _emailService;
        private readonly IHttpContextAccessor _httpContext;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IHttpContextAccessor httpContext,
            IOptions<EmailSettings> emailSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _appCommon = new AppCommon();
            _httpContext = httpContext;
            _emailService = new EmailService(emailSettings);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new User { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        #region Helper Methods
        private async Task<string> PrepareEmailTemplateAsync(string email, string code, string fullName, string url)
        {
            var templateStr = "";

            try
            {
                var emailTemplatefile = _appCommon.EmailTemplateFilePath;

                using (var reader = new StreamReader(emailTemplatefile))
                    templateStr = await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
            }

            var content = "Thanks for signing up with Pyarana! <br>" +
                "You can verify your email account by clicking on following button.<br><br>";
            var buttonName = "Confirm Email";

            templateStr =
               templateStr
               .Replace("{UserName}", fullName)
               .Replace("{Mail Content}", content)
               .Replace("{ConfirmButton}", url)
                .Replace("{ConfirmButtonText}", buttonName);
            return templateStr;
        }

        #endregion
    }
}

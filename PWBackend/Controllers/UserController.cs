using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtpNet;
using PWBackend.Models.Entities;
using PWBackend.Models.Helpers;
using PWBackend.Service;
using QRCoder;

namespace PWBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(PWDbContext context, IConfiguration configuration) : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserLogin details)
        {
            var userService = new UserService(context, configuration);
            var user = userService.GetUserByBaseLogin(details.Username, details.Password);

            // If the password is valid, check if the account needs to be reset
            if (user != null)
            {
                if (user.RequiresReset)
                {
                    return Unauthorized(new { code = "resetNeeded" });
                }
                else if (user.HasOTP) // Check if user has two-factor auth
                {
                    if (details.OTPCode is null or "")
                    {
                        return Unauthorized(new { code = "requiresOTP" });
                    }
                    else
                    {
                        var secretKey = Base32Encoding.ToBytes(user.OTPSecret);
                        var totp = new Totp(secretKey);
                        if (!totp.VerifyTotp(details.OTPCode, out _))
                        {
                            return Unauthorized(new { code = "invalidOTP" });
                        }
                    }
                }

                var token = userService.GenerateJSONWebToken(user);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized(new { code = "passwordInvalid" });
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(PasswordReset details)
        {
            var userService = new UserService(context, configuration);
            var user = userService.GetUserByBaseLogin(details.Username, details.OldPassword);

            if (user != null)
            {
                if (!user.RequiresReset)
                {
                    return Unauthorized(new { code = "resetForbidden" });
                }
            
                user.Password = BCrypt.Net.BCrypt.HashPassword(details.NewPassword);
                user.RequiresReset = false;
            
                context.SaveChanges();

                var token = userService.GenerateJSONWebToken(user);

                return Ok(new { hasOTP = user.HasOTP, token });
            }
            else
            {
                return Unauthorized(new { code = "passwordInvalid" });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GenerateOTP")]
        public IActionResult GenerateOTP()
        {
            var userService = new UserService(context, configuration);
            var user = userService.FindUserByClaims(HttpContext.User);

            if (user != null)
            {
                if (user.HasOTP)
                {
                    return BadRequest(new { code = "otpExists" });
                }

                var secretKey = KeyGeneration.GenerateRandomKey(20);
                var base32Secret = Base32Encoding.ToString(secretKey);

                const string issuer = "PWBackend";
                var totpUri = $"otpauth://totp/{issuer}:{user.Username}?secret={base32Secret}&issuer={issuer}";

                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(totpUri, QRCodeGenerator.ECCLevel.Q);
                    var qrCode = new Base64QRCode(qrCodeData);

                    user.OTPSecret = base32Secret;
                    context.SaveChanges();

                    return Ok(new { qrCode = qrCode.GetGraphic(20) });
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Authorize]
        [Route("EnableOTP")]
        public IActionResult EnableOTP(string code)
        {
            var userService = new UserService(context, configuration);
            var user = userService.FindUserByClaims(HttpContext.User);

            if (user != null)
            {
                if (user.HasOTP)
                {
                    return BadRequest(new { code = "otpExists" });
                }

                if (user.OTPSecret is null or "")
                {
                    return BadRequest(new { code = "otpNotGenerated" });
                }

                var secretKey = Base32Encoding.ToBytes(user.OTPSecret);
                var totp = new Totp(secretKey);
                if (totp.VerifyTotp(code, out _))
                {
                    user.HasOTP = true;
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return Unauthorized(new { code = "invalidOTP" });
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        [Route("CheckAuth")]
        public IActionResult CheckAuth()
        {
            return Ok();
        }
    }
}

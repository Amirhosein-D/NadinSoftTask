
using Microsoft.AspNetCore.Mvc;
using SampleTask.Application.Contracts.Identity;
using SampleTask.Application.Models.Identity;
using SampleTask.Application.Tools.Captcha;

namespace SampleTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaTools _captchaTools;

        public CaptchaController(ICaptchaTools captchaTools)
        {
            _captchaTools = captchaTools;
        }


        // Simple Captcha code
        [HttpGet("GenerateCaptchaSP")]
        public IActionResult GenerateCaptchaSP()
        {

            string captchaCode = _captchaTools.GenerateRandomCode_SimplePhrase();

            byte[] imageBytes = _captchaTools.GenerateCaptchaImage_SimplePhrase(captchaCode);

            HttpContext.Session.SetString("CaptchaCodeSP", captchaCode);

            return Ok(File(imageBytes, "image/jpeg"));

        }




        //Marh Capcha Code
        [HttpGet("GenerateCaptchaMP")]
        public IActionResult GenerateCaptchaMP()
        {

            string captchaCode = _captchaTools.GenerateRandomCode_MathematicalPhrase();

            byte[] imageBytes = _captchaTools.GenerateCaptchaImage_MathematicalPhrase(captchaCode);

            string result = _captchaTools.CalculateMathExpression(captchaCode).ToString();

            HttpContext.Session.SetString("CaptchaCodeMP", result);

            return Ok(File(imageBytes, "image/jpeg"));
        }





    }
}

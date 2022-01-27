using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using System.IO;

namespace AchievementAPI.Controllers
{
    [Route("[controller]/api")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly ILogger<AchievementController> _logger;

        public AchievementController(ILogger<AchievementController> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name = "GetAchievement")]
        public IActionResult Get([FromBody] AchievementGetBody requestBody)
        {
            if (requestBody.valid)
            {
                _logger.LogInformation(Enum.GetName(typeof(AchievementType), requestBody.achievementType));
                AchievementRequest request = AchievementRequest.fromGetBody(requestBody);
                ImageGenerator generator = new ImageGenerator();
                string imagePath = generator.GenerateImage(request);
                Byte[] b = System.IO.File.ReadAllBytes(imagePath);
                IActionResult response = File(b, "image/png");
                generator.DeleteImage(request.UUID.ToString());
                return response;
            }
            else
            {
                return BadRequest("Request body is invalid");
            }
        }
    }
}

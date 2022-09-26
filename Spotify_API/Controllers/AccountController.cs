using Microsoft.AspNetCore.Mvc;

namespace Spotify_API.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Account()
        {
            return Ok();
        }
        
        
    }
}

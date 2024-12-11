using DBLibrary;
using Microsoft.AspNetCore.Mvc;

namespace VtoroeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private Ne_navredyEntities _context = new Ne_navredyEntities();

        [HttpGet(Name = "GetUserWithId")]
        public Models.ResponseUser Get(int ID)
        {
            Models.ResponseUser response = new();
            response.user = _context.User.FirstOrDefault(u => u.id == ID);
            return response;
        }
    }
}

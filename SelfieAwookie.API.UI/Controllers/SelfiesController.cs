using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;

namespace SelfieAwookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfiesController : ControllerBase
    {
        #region Fiels
        private readonly SelfiesContext? _context = null;
        #endregion
        #region Constructor
        public SelfiesController(SelfiesContext context)
        {
            _context = context;
        }
        #endregion
        #region Public methods
        //[HttpGet]
        //public IEnumerable<Selfie> Get()
        //{
        //    return Enumerable.Range(1, 10).Select(i => new Selfie { Id = i });
        //}

        [HttpGet]
        public IActionResult Get()
        {
            var model = _context.Selfies.Include(i => i.Wookie).Select(item => new {Title = item.Title, WookieId = item.Wookie.Id, NbSelfiesFromWookie = item.Wookie.Selfies.Count}).ToList();

            return this.Ok(model);
        }
        #endregion

    }
}

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAwookie.API.UI.Applications.Commands;
using SelfieAwookie.API.UI.Applications.DTOs;
using SelfieAwookie.API.UI.Applications.Queries;
using SelfieAwookie.API.UI.ExtensionsMethods;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;

namespace SelfieAwookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
   // [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class SelfiesController : ControllerBase
    {
        #region Fiels
        private readonly ISelfieRepository? _repository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;
        private readonly IMediator _mediator = null;
        #endregion
        #region Constructor
        public SelfiesController(ISelfieRepository repository, IWebHostEnvironment webHostEnvironment, IMediator mediator)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
            _mediator = mediator;
        }
        #endregion
        #region Public methods
        
        [HttpGet]
        //[DisableCors()]
        //[EnableCors(SecurityMethods.DEFAULT_POLICY_3)]
        public IActionResult Get([FromQuery] int wookieId = 0)
        {
            var param = this.Request.Query["wookieId"];

            var model = _mediator.Send(new SelectAllSefliesQuery() { WookieId = wookieId });

            return this.Ok(model);
        }

       
        [Route("photos")]
        [HttpPost]
        public async Task<IActionResult> AddPicture(IFormFile picture)
        {
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, @"images\\selfies");

            if(!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            filePath = Path.Combine(filePath, picture.FileName);

            using var stream = new FileStream(filePath, FileMode.OpenOrCreate);
            await picture.CopyToAsync(stream);

            var itemFile = _repository?.AddOnePicture(filePath);

            try
            {
                _repository?.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            return this.Ok(itemFile);
        }

        [HttpPost]
        public async Task<IActionResult> AddOne(SelfieDto selfieDto)
        {
            IActionResult result = this.BadRequest();

           var item = await  _mediator.Send(new AddSelfieCommand() { Item = selfieDto });

            if(item != null)
            {
                result = this.Ok(item);
            }

            return result;
        }
        #endregion

    }
}

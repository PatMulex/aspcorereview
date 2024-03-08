﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAwookie.API.UI.Applications.DTOs;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;

namespace SelfieAwookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfiesController : ControllerBase
    {
        #region Fiels
        private readonly ISelfieRepository? _repository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;
        #endregion
        #region Constructor
        public SelfiesController(ISelfieRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Public methods
        //[HttpGet]
        //public IEnumerable<Selfie> Get()
        //{
        //    return Enumerable.Range(1, 10).Select(i => new Selfie { Id = i });
        //}

        [HttpGet]
        public IActionResult Get([FromQuery] int wookieId = 0)
        {
            var param = this.Request.Query["wookieId"];

            var selfiesList = _repository.GetAll(wookieId);
            var model = selfiesList.Select(item => new SelfieResumeDto { Title = item.Title, WookieId = item.Wookie.Id, NbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0) }).ToList();

            return this.Ok(model);
        }

        //[Route("photos")]
        //[HttpPost]
        //public async Task<IActionResult> AddPicture() 
        //{
        //    using var stream = new StreamReader(this.Request.Body);

        //    var content = await stream.ReadToEndAsync();

        //    return this.Ok();
        //}

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

            return this.Ok();
        }

        [HttpPost]
        public IActionResult AddOne(SelfieDto selfieDto)
        {
            IActionResult result = this.BadRequest();

            Selfie addSelfie = _repository.AddOne(new Selfie()
            {
                ImagePath = selfieDto.ImagePath,
                Title = selfieDto.Title
            });

            _repository.UnitOfWork.SaveChanges();

            if (addSelfie != null)
            {
                selfieDto.Id = addSelfie.Id;
                result = this.Ok(selfieDto);
            }

            return result;
        }
        #endregion

    }
}
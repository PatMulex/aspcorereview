using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfieAwookie.API.UI.Applications.DTOs;
using SelfieAwookie.API.UI.Controllers;
using SelfieAWookies.Core.Selfies.Domain;
using SelfiesAWookies.Core.Framework;

namespace TestWebApi
{
    public class SelfieControllerUnitTest
    {
        #region Public methods
        [Fact]
        public void ShouldReturnListSelfies()
        {
            //ARRANGE

            var expectedList = new List<Selfie>()
            {
                new(){Wookie = new Wookie() },
                new(){Wookie = new Wookie() }
            };
            var reposirotyMock = new Mock<ISelfieRepository>();

            reposirotyMock.Setup(item => item.GetAll(It.IsAny<int>())).Returns(expectedList);
            var controller = new SelfiesController(reposirotyMock.Object);


            //ACT
            var result = controller.Get();

                //ASSERT

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
                
            OkObjectResult? okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);

            Assert.IsType<List<SelfieResumeDto>>(okResult.Value);

            List<SelfieResumeDto>? list = okResult.Value as List<SelfieResumeDto>;
            Assert.True((list?.Count).GetValueOrDefault(0) == 2);

        }

        [Fact]
        public void ShouldAddOneSelfie()
        {
            //ARRANGE
            SelfieDto selfie = new SelfieDto();
            var reposirotyMock = new Mock<ISelfieRepository>();
            var unit = new Mock<IUnitOfWork>();
            reposirotyMock.Setup(item => item.UnitOfWork).Returns(new Mock<IUnitOfWork>().Object);
            reposirotyMock.Setup(item => item.AddOne(It.IsAny<Selfie>())).Returns(new Selfie() { Id = 4 });

            //ACT
            var controller = new SelfiesController(reposirotyMock.Object);
            var result = controller.AddOne(selfie);

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var addedSelfie = (result as OkObjectResult).Value as SelfieDto;
            Assert.NotNull(addedSelfie);
            Assert.True(addedSelfie.Id > 0);
        }
        #endregion

    }
}
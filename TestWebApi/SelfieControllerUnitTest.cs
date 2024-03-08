using Microsoft.AspNetCore.Mvc;
using SelfieAwookie.API.UI.Controllers;

namespace TestWebApi
{
    public class SelfieControllerUnitTest
    {
        #region
            [Fact]
            public void ShouldReturnListSelfies()
            {
                //ARRANGE
                var controller = new SelfiesController(null);


                //ACT
                var result = controller.Get();

                    //ASSERT

                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);
                
                OkObjectResult? okResult = result as OkObjectResult;
                Assert.NotNull(okResult.Value);

            }
        #endregion

    }
}
using System.Threading.Tasks;
using kamrj.Models.TokenAuth;
using kamrj.Web.Controllers;
using Shouldly;
using Xunit;

namespace kamrj.Web.Tests.Controllers
{
    public class HomeController_Tests: kamrjWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
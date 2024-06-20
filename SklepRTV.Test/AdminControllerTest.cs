using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SklepRTV.MVC.Controllers;
using System.Threading.Tasks;
using Xunit;

    public class AdminControllerTest
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            var controller = new AdminController();
            var result = controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }
    }


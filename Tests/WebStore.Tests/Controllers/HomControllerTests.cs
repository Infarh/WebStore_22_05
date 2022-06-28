using Microsoft.AspNetCore.Mvc;
using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers;

[TestClass]
public class HomControllerTests
{
    [TestMethod]
    public void Contacts_returns_with_View()
    {
        // AAA = A-A-A = Arrange - Act - Assert

        #region Arrange
        
        var controller = new HomeController(null!);

        #endregion

        #region Act
        
        var result = controller.Contacts();

        #endregion

        #region Assert

        var view_result = Assert.IsType<ViewResult>(result);

        //Assert.Equal(nameof(HomeController.Contacts), view_result.ViewName); // неправильное утверждение
        Assert.Null(view_result.ViewName);

        #endregion
    }
}

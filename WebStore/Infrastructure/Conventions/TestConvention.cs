using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WebStore.Controllers;

namespace WebStore.Infrastructure.Conventions;

public class TestConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerName == "Home")
        {
            //controller.Actions.Add(new ActionModel(typeof(HomeController).GetMethod("TestMethod"), Array.Empty<object>()));
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace PLManagementSystem.UI.Helpers
{
    public static class RazorHelper
    {
        public static async Task<string> RenderViewToStringAsync<TModel>(this Controller controller, string viewNamePath, TModel model)
        {
            if (string.IsNullOrEmpty(viewNamePath))
                viewNamePath = controller.ControllerContext.ActionDescriptor.ActionName;

            controller.ViewData.Model = model;

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                    ViewEngineResult viewResult = null;

                    if (viewNamePath.EndsWith(".cshtml"))
                        viewResult = viewEngine.GetView(viewNamePath, viewNamePath, false);
                    else
                        viewResult = viewEngine.FindView(controller.ControllerContext, viewNamePath, false);

                    if (!viewResult.Success)
                        return $"A view with the name '{viewNamePath}' could not be found";

                    ViewContext viewContext = new ViewContext(
                        controller.ControllerContext,
                        viewResult.View,
                        controller.ViewData,
                        controller.TempData,
                        writer,
                        new HtmlHelperOptions()
                    );

                    await viewResult.View.RenderAsync(viewContext);

                    return writer.GetStringBuilder().ToString();
                }
                catch (Exception exc)
                {
                    return $"Failed - {exc.Message}";
                }
            }
        }
        public static async Task<string> RenderViewToStringAsync(this Controller controller, string viewNamePath)
        {
            if (string.IsNullOrEmpty(viewNamePath))
                viewNamePath = controller.ControllerContext.ActionDescriptor.ActionName;

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                    ViewEngineResult viewResult = null;

                    if (viewNamePath.EndsWith(".cshtml"))
                        viewResult = viewEngine.GetView(viewNamePath, viewNamePath, false);
                    else
                        viewResult = viewEngine.FindView(controller.ControllerContext, viewNamePath, false);

                    if (!viewResult.Success)
                        return $"A view with the name '{viewNamePath}' could not be found";

                    ViewContext viewContext = new ViewContext(
                        controller.ControllerContext,
                        viewResult.View,
                        controller.ViewData,
                        controller.TempData,
                        writer,
                        new HtmlHelperOptions()
                    );

                    await viewResult.View.RenderAsync(viewContext);

                    return writer.GetStringBuilder().ToString();
                }
                catch (Exception exc)
                {
                    return $"Failed - {exc.Message}";
                }
            }
        }
        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}

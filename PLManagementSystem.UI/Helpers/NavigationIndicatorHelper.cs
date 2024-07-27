using PLManagementSystem.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace PLManagementSystem.UI.Helpers
{

    public static class NavigationIndicatorHelper
    {
        static Dictionary<string, List<string>> moduleControllers = new Dictionary<string, List<string>>
        {
            
            
        };
        public static string MakeActiveClass(this IUrlHelper urlHelper, string controller = "", string action = "", string area = "")
        {
            try
            {
                string result = "active";
                string controllerName = urlHelper.ActionContext.RouteData.Values["controller"] == null ? "" : urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                string methodName = urlHelper.ActionContext.RouteData.Values["action"] == null ? "" : urlHelper.ActionContext.RouteData.Values["action"].ToString();
                string areaName = urlHelper.ActionContext.RouteData.Values["area"] == null ? "" : urlHelper.ActionContext.RouteData.Values["area"].ToString();

                if (string.IsNullOrEmpty(controllerName))
                {
                    return null;
                }

                if (string.IsNullOrEmpty(controller) && string.IsNullOrEmpty(area))
                {
                    return null;
                }
                else if (string.IsNullOrEmpty(controller) == false && string.IsNullOrEmpty(area))
                {
                    if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                    {
                        if (action == "" || methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
                        {
                            return result;
                        }

                    }
                }
                else if (string.IsNullOrEmpty(controller) && string.IsNullOrEmpty(area) == false)
                {
                    if (areaName.Equals(area, StringComparison.OrdinalIgnoreCase))
                    {

                        return result;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string MakeActiveClassForModule(this IUrlHelper urlHelper, string module = "")
        {
            try
            {
                string result = "active menu-open";
                string controllerName = urlHelper.ActionContext.RouteData.Values["controller"] == null ? "" : urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                string methodName = urlHelper.ActionContext.RouteData.Values["action"] == null ? "" : urlHelper.ActionContext.RouteData.Values["action"].ToString();
                if (string.IsNullOrEmpty(controllerName))
                {
                    return null;
                }


                return moduleControllers.ContainsKey(module) && moduleControllers[module].Contains($"{controllerName}.{methodName}") ? result : null;


            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string MakeActiveMenuForModule(this IUrlHelper urlHelper, string module = "", string action = "")
        {
            string activeResult = "";
            string nonActiveResult = "display: none;";
            try
            {
                string controllerName = urlHelper.ActionContext.RouteData.Values["controller"] == null ? "" : urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                string methodName = urlHelper.ActionContext.RouteData.Values["action"] == null ? "" : urlHelper.ActionContext.RouteData.Values["action"].ToString();

                if (string.IsNullOrEmpty(controllerName))
                {
                    return null;
                }
                return moduleControllers.ContainsKey(module) && moduleControllers[module].Contains($"{controllerName}.{methodName}") ? activeResult : nonActiveResult;


            }
            catch (Exception)
            {
                return nonActiveResult;
            }
        }
    }
}
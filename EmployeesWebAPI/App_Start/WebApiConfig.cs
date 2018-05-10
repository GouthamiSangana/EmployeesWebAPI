using System.Web.Http;
using System.Web.OData.Builder;
using EmployeesWebAPI.Models;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using System.Net.Http.Formatting;
using System.Linq;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace EmployeesWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            GlobalConfiguration.Configuration.EnableDependencyInjection();

            //Attribute Routing
            config.MapHttpAttributeRoutes();

            //Map OData Route
            config.MapODataServiceRoute(
                routeName: "OData",
                routePrefix: "odata",
                model: GetEDMModel()
            );

            // Default route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static IEdmModel GetEDMModel()
        {
            // OData Route
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            var employeeEntitySet = builder.EntitySet<Employee>("Employees");
            employeeEntitySet.EntityType.HasKey(entity => entity.EmpId);
            return builder.GetEdmModel();
        }
    }
}

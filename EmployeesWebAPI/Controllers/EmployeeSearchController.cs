using EmployeesWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;

namespace EmployeesWebAPI.Controllers
{
    public class EmployeeSearchController : ODataController
    {

        EmployeeDBContext empDbContext = null;

        public EmployeeSearchController()
        {
            empDbContext = new EmployeeDBContext();
        }

        [ODataRoute("Employees")]
        public List<Employee> Get(ODataQueryOptions searchOptions)
        {
            List<Employee> employeesLst = empDbContext.GetSearchedEmployees(searchOptions);
            return employeesLst;
        }
    }
}

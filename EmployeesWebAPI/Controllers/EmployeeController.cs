using System.Collections.Generic;
using System.Web.Http;
using EmployeesWebAPI.Models;
using System;
using System.Web.Http.Cors;
using System.Web.OData;
using System.Web.OData.Query;

namespace EmployeesWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeDBContext empDbContext = null;

        public EmployeeController()
        {
            empDbContext = new EmployeeDBContext();
        }

        /// <summary>
        /// This method will return all the Employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Employee/GetEmployees")]
        public IHttpActionResult GetEmployees()
        {
            List<Employee> employeesLst = new List<Employee>();
            employeesLst = empDbContext.GetAllEmployees();
            return Ok<List<Employee>>(employeesLst);
        }

        [Route("api/Employee/AddEmployee")]
        [HttpPost]
        public IHttpActionResult AddEmployee(Employee emp)
        {
            empDbContext.AddEmployee(emp);
            return Ok("Employee added succesfully");
        }

        [Route("api/Employee/UpdateEmployee")]
        [HttpPost]
        public IHttpActionResult UpdateEmployee(Employee emp)
        {
            empDbContext.UpdateEmployee(emp);
            return Ok("Employee updated auccesfully");
        }

        [Route("api/Employee/DeleteEmployee/{empId}")]
        [HttpGet]
        public IHttpActionResult DeleteEmployee(int empId)
        {
            empDbContext.DeleteEmployee(empId);
            return Ok("Employee deleted succesfully");
        }

        [Route("api/Employee/GetStates")]
        [HttpGet]
        public IHttpActionResult GetStates()
        {
            List<State> states = new List<State>();
            states = empDbContext.GetAllStates();
            return Ok<List<State>>(states);
        }

        [Route("api/Employee/GetCities")]
        [HttpGet]
        public IHttpActionResult GetCities()
        {
            List<City> cities = new List<City>();
            cities = empDbContext.GetAllCities();
            return Ok<List<City>>(cities);
        }

        [Route("api/Employee/GetEmployee/{empId}")]
        [HttpGet]
        public IHttpActionResult GetEmployee(int empId)
        {
            return Ok<Employee>(empDbContext.GetEmployee(empId));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Web.OData.Query;

namespace EmployeesWebAPI.Models
{
    public class EmployeeDBContext
    {
        /// <summary>
        /// This method will return the Employees List
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public List<Employee> GetAllEmployees()
        {
            using (PersonalEntities entities = new PersonalEntities())
            {

               // entities.getEmployees();

                return entities.Employees.ToList();
            }
        }

        /// <summary>
        /// Returns a list of States
        /// </summary>
        /// <returns></returns>
        public List<State> GetAllStates()
        {
            using (PersonalEntities entities = new PersonalEntities())
            {
                return entities.States.ToList();
            }
        }

        /// <summary>
        /// Returns list of Cities
        /// </summary>
        /// <returns></returns>
        public List<City> GetAllCities()
        {
            using (PersonalEntities entities = new PersonalEntities())
            {
                return entities.Cities.ToList();
            }
        }

        /// <summary>
        /// Gets the particular Employee
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public Employee GetEmployee(int empId)
        {
            using (PersonalEntities entities = new PersonalEntities())
            {
                return entities.Employees.Single(emp => emp.EmpId == empId);
            }
        }

        /// <summary>
        /// Add a new employee
        /// </summary>
        /// <param name="empObj"></param>
        public void AddEmployee(Employee empObj)
        {
            using (PersonalEntities entities = new PersonalEntities())
            {
                Employee emp = new Employee
                {
                    Name = empObj.Name,
                    Phone = empObj.Phone,
                    AddressLine1 = empObj.AddressLine1,
                    AddressLine2 = empObj.AddressLine2,
                    CityCode = empObj.CityCode,
                    StateCode = empObj.StateCode,
                    PinCode = empObj.PinCode
                };
                entities.Employees.Add(emp);
                entities.SaveChanges();
            }
        }

        /// <summary>
        /// Update existing employee
        /// </summary>
        /// <param name="empObj"></param>
        public void UpdateEmployee(Employee empObj)
        {
            using (PersonalEntities entities = new PersonalEntities())
            {
                Employee emp = entities.Employees.Single(employee => employee.EmpId == empObj.EmpId);
                emp.Name = empObj.Name;
                emp.Phone = empObj.Phone;
                emp.AddressLine1 = empObj.AddressLine1;
                emp.AddressLine2 = empObj.AddressLine2;
                emp.CityCode = empObj.CityCode;
                emp.StateCode = empObj.StateCode;
                emp.PinCode = empObj.PinCode;
                entities.SaveChanges();
            }
        }

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEmployee(int id)
        {
            using (PersonalEntities entities = new PersonalEntities())
            {
                Employee emp = entities.Employees.Single(col => col.EmpId == id);
                entities.Employees.Remove(emp);
                entities.SaveChanges();
            }
        }

        /// <summary>
        /// Get the list of searched employees
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public List<Employee> GetSearchedEmployees(ODataQueryOptions searchOptions)
        {
            using (PersonalEntities entities = new PersonalEntities())
            {
                IEnumerable<Employee> results = searchOptions.ApplyTo(entities.Employees.AsQueryable()) as IEnumerable<Employee>;
                return results.ToList();
            }
        }
    }
}
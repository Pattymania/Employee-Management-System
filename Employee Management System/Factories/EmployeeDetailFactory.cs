using Employee_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Management_System.Factories
{
    public class EmployeeDetailFactory
    {

        private EmployeeManagementSystemEntities db = new EmployeeManagementSystemEntities();
        public EmployeeInfoModel PrepareEmployeeModelFromEmployee(EmployeeInfo employee)
        {
            EmployeeInfoModel employeeInfo = new EmployeeInfoModel();

            employeeInfo.EmployeeID = employee.EmployeeID;
            employeeInfo.FirstName = employee.FirstName;
            employeeInfo.LastName = employee.LastName;
            employeeInfo.PhoneNumber = employee.PhoneNumber;
            employeeInfo.Department = employee.Department;
            employeeInfo.DOB = employee.DOB;
            employeeInfo.Email = employee.Email;
            employeeInfo.Password = employee.Password;

            return employeeInfo;
        }

        public EmployeeInfo PrepareEmployeeFromEmployeeModel(EmployeeInfoModel employeeInfo)
        {
            EmployeeInfo employee = new EmployeeInfo();

            employee.EmployeeID = employeeInfo.EmployeeID;
            employee.FirstName = employeeInfo.FirstName;
            employee.LastName = employeeInfo.LastName;
            employee.PhoneNumber = employeeInfo.PhoneNumber;
            employee.Department = employeeInfo.Department;
            employee.DOB = employeeInfo.DOB;
            employee.Email = employeeInfo.Email;
            employee.Password = employeeInfo.Password;

            employee.CreatedDate = DateTime.Now;

            return employee;
        }

        public void UpdateEmployeeFromEmployeeModel(EmployeeInfoModel employeeInfo)
        {
            try
            {
                var employee = db.EmployeeInfoes.Find(employeeInfo.EmployeeID);

                employee.EmployeeID = employeeInfo.EmployeeID;
                employee.FirstName = employeeInfo.FirstName;
                employee.LastName = employeeInfo.LastName;
                employee.PhoneNumber = employeeInfo.PhoneNumber;
                employee.Department = employeeInfo.Department;
                employee.DOB = employeeInfo.DOB;
                employee.Email = employeeInfo.Email;
                employee.Password = employeeInfo.Password;

                employee.UpdatedDate = DateTime.Now;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

        }
    }
}
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBFirst_React.Server.Controllers;
using DBFirst_React.Server.Models;

namespace MSTestTests
{
    [TestClass]
    public sealed class EmployeeControllerTests
    {
        private EmployeeDbContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new EmployeeDbContext(options);
        }

        private void SeedEmployees(EmployeeDbContext context)
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Joseph", Salary = 55500.00m },
                new Employee { Id = 2, Name = "John", Salary = 62000.00m },
                new Employee { Id = 3, Name = "David", Salary = 48000.00m },
                new Employee { Id = 4, Name = "Michael", Salary = 75000.00m },
                new Employee { Id = 5, Name = "Robert", Salary = 68000.00m },
                new Employee { Id = 6, Name = "James", Salary = 52000.00m },
                new Employee { Id = 7, Name = "Daniel", Salary = 81000.00m },
                new Employee { Id = 8, Name = "William", Salary = 59000.00m },
                new Employee { Id = 9, Name = "Thomas", Salary = 72000.00m },
                new Employee { Id = 10, Name = "Chris", Salary = 45000.00m },
                new Employee { Id = 11, Name = "king", Salary = 42424.00m }
            };

            context.Employees.AddRange(employees);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetEmployees_ReturnsAll()
        {
            using var context = CreateInMemoryContext("MGetEmployees_All");
            SeedEmployees(context);
            await context.SaveChangesAsync();

            var controller = new EmployeeController(context);

            var result = await controller.GetEmployees();
            var ok = result as OkObjectResult;
            Assert.IsNotNull(ok);
            var list = ok.Value as List<Employee>;
            Assert.IsNotNull(list);
            Assert.AreEqual(11, list.Count);
            Assert.IsTrue(list.Any(e => e.Name == "king" && e.Salary == 42424.00m));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CreateEmployee_AddsEmployee()
        {
            using var context = CreateInMemoryContext("MCreateEmployee_AddsEmployee");
            var controller = new EmployeeController(context);

            var newEmp = new Employee { Name = "NewM", Salary = 50000.00m };
            var result = await controller.CreateEmployee(newEmp);

            var ok = result as OkObjectResult;
            Assert.IsNotNull(ok);
            var created = ok.Value as Employee;
            Assert.IsNotNull(created);
            Assert.AreEqual("NewM", created.Name);

            Assert.AreEqual(1, context.Employees.Count());
        }
    }
}

using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DBFirst_React.Server.Controllers;
using DBFirst_React.Server.Models;

namespace xUnitTests
{
    public class EmployeeControllerTests
    {
        private EmployeeDbContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            var context = new EmployeeDbContext(options);
            // ensure a clean, isolated in-memory database for each test run
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
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

        [Fact]
        public async System.Threading.Tasks.Task GetEmployees_ReturnsAll()
        {
            using var context = CreateInMemoryContext("GetEmployees_All_XUnit");
            SeedEmployees(context);
            await context.SaveChangesAsync();

            var controller = new EmployeeController(context);

            var result = await controller.GetEmployees();
            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<List<Employee>>(ok.Value);
            Assert.Equal(11, list.Count);
            Assert.Contains(list, e => e.Name == "king" && e.Salary == 42424.00m);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateEmployee_AddsEmployee()
        {
            using var context = CreateInMemoryContext("CreateEmployee_XUnit");
            var controller = new EmployeeController(context);

            var newEmp = new Employee { Name = "NewGuy", Salary = 12345.67m };
            var result = await controller.CreateEmployee(newEmp);

            var ok = Assert.IsType<OkObjectResult>(result);
            var created = Assert.IsType<Employee>(ok.Value);
            Assert.Equal("NewGuy", created.Name);

            Assert.Equal(1, context.Employees.Count());
        }
    }
}

using System;
using System.Collections.Generic;

namespace DBFirst_React.Server.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }
}

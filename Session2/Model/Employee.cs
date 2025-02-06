using System;
using System.Collections.Generic;

namespace Session2.Model;

public partial class Employee : IComparable<Employee>
{
    

    public int IdEmployee { get; set; }

    public string Surname { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? SecondName { get; set; }

    public string? Position { get; set; }

    public string PhoneWork { get; set; } = null!;

    public string? Phone { get; set; }

    public string Cabinet { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int IdDepartment { get; set; }

    public int? IdHelper { get; set; }

    public string? Other { get; set; }

    public DateOnly? BirthDay { get; set; }

    public int? IdBoss { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Calendar_> CalendarIdAlternateNavigations { get; set; } = new List<Calendar_>();

    public virtual ICollection<Calendar_> CalendarIdEmployeeNavigations { get; set; } = new List<Calendar_>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual Department IdDepartmentNavigation { get; set; } = null!;


    public int CompareTo(Employee? other)
    {
        if (other is Employee)
        {
            var emp = other as Employee;
            int comp = this.Surname.CompareTo(emp.Surname);
            if (comp != 0)
            {
                return comp;
            }
            else
            {
                comp = this.FirstName.CompareTo(emp.FirstName);
                if (comp != 0)
                {
                    return comp;
                }
                else if (this.SecondName != null && emp.SecondName != null)
                {
                    return this.SecondName!.CompareTo(emp.SecondName);
                }
                else return 0;
            }
        }
        else
        {
            throw new ArgumentException("Некорректное значение параметра");
        }
    }
}

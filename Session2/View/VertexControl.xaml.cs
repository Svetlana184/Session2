using Session2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Session2.View
{
    /// <summary>
    /// Логика взаимодействия для VertexControl.xaml
    /// </summary>
    public partial class VertexControl : UserControl
    {
        public int Department { get; set; }
        public string? NameDepartment { get; set; }
        public int? ParentDepartment { get; set; }
        public int Level { get; set; }
        public MainWindow ParentWindow { get; set; }
        private RoadOfRussiaContext db;
        public VertexControl(int department, string? name, int? parentDepartment,MainWindow parentWindow)
        {
            InitializeComponent();
            Department = department;
            NameDepartment = name;
            ParentDepartment = parentDepartment;
            Content = name;
            Height = 50;
            Width = 200;
            VerticalContentAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            ParentWindow = parentWindow;
            db = new RoadOfRussiaContext();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = from emp in db.Employees
                       where emp.IdDepartment == Department
                       select new
                       {
                           DepAndPosition=db.Departments.FirstOrDefault(p=>p.IdDepartment==Department)!.DepartmentName+" - "+
                           db.Employees.FirstOrDefault(p=>p.IdDepartment==Department)!.Position
                       };
            ParentWindow.EmployerList.ItemsSource = list;

        }
    }
}

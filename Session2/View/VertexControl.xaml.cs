using Session2.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        public bool IsActive { get; set; }


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

        private void UserControl_Dep(object sender, MouseButtonEventArgs e)
        {
            ParentWindow.Graph.v.Background= new SolidColorBrush(Color.FromRgb(120, 178, 75));
            foreach (var g in ParentWindow.Graph.vertices)
            {
                g.Background = new SolidColorBrush(Color.FromRgb(120, 178, 75));
                g.IsActive = false;
            }
            this.Background=new SolidColorBrush(Colors.Green);
            this.IsActive = true;
            ParentWindow.EmployerList.ItemsSource=null;


            List<Department> depList = new List<Department>();

            depList.Add(db.Departments.FirstOrDefault(p => p.IdDepartment == Department)!);

            for (int j = Level+1; j < ParentWindow.Graph.MaxLevel; j++)
            {
                List<Department> childList = new List<Department>();
                foreach (Department v in depList)
                {
                    childList.AddRange(db.Departments.Where(p => p.IdDepartmentParent == v.IdDepartment));
                }
                depList.AddRange(childList);

            }

            List<Employee> listMain = new List<Employee>();

            foreach(Department d in depList)
            {
                listMain.AddRange(db.Employees.Where(p => p.IdDepartment == d.IdDepartment && (p.IsFired == null || ((DateTime)p.IsFired).AddDays(30)>DateTime.Now)));
            }
            listMain.Sort();

            var list = (from empl in listMain
                        select new
                        {
                            DepAndPosition = db.Departments.FirstOrDefault(p => p.IdDepartment == empl.IdDepartment)!.DepartmentName + " - " + empl.Position,
                            Fio = empl.Surname + " " + empl.FirstName + " " + empl.SecondName,
                            Contacts = empl.PhoneWork + " " + empl.Email,
                            Cabinet = empl.Cabinet,
                            Id = empl.IdEmployee,
                        }).ToList();

            ParentWindow.EmployerList.Items.Clear();
            ParentWindow.EmployerList.ItemsSource = list;
        }
    }
}

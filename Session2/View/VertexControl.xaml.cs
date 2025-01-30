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

        private void UserControl_MouseDown_1(object sender, MouseButtonEventArgs e)
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



            ArrayList all = new ArrayList();
            //1 - список первичных наследников
            var depList = (from dep in db.Departments
                           where dep.IdDepartmentParent == Department
                           select new
                           {
                               IdDep = dep.IdDepartment
                           }).ToList();

            List<Employee> listMain = db.Employees.Where(p => p.IdDepartment == Department).ToList();

            for (int i = 0; i < depList.Count; i++)
            {
                List<Employee> ListTemp = db.Employees.Where(p => p.IdDepartment == depList[i].IdDep).ToList();
                listMain.AddRange(ListTemp);
            }
            listMain.Sort();

            var list = (from empl in listMain
                        select new
                        {
                            DepAndPosition = db.Departments.FirstOrDefault(p => p.IdDepartment == empl.IdDepartment)!.DepartmentName + " - " + empl.Position,
                            Fio = empl.Surname + " " + empl.FirstName + " " + empl.SecondName,
                            Contacts = empl.PhoneWork + " " + empl.Email,
                            Cabinet = empl.Cabinet
                        }).ToList();

            all.AddRange(list);

            ParentWindow.EmployerList.ItemsSource = all;


        }
    }
}

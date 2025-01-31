using Microsoft.Identity.Client;
using Session2.Model;
using Session2.View;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Session2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Graph Graph { get; set; }
        private RoadOfRussiaContext db;
        private VertexControl SelectedVertex { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            db=new RoadOfRussiaContext();
            VertexControl vertexRoot = new(987, "Дороги России", 0, this);
            vertexRoot.Level = 1;
            Graph = new Graph(vertexRoot);
            List<Department> departmentList = db.Departments.Where(p => p.IdDepartment != 987).ToList();
            foreach (Department department in departmentList)
            {
                VertexControl v = new(department.IdDepartment, department.DepartmentName, department.IdDepartmentParent,this);
                Graph.AddVertex(v);
            }
            Graph.DrawGraph(MainCanvas);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedVertex = FindSelected();
            PersonWindow personWindow = new PersonWindow(new Employee(),SelectedVertex);
            if (personWindow.ShowDialog() == true)
            {
                Employee employee = new()
                {
                    Surname = personWindow.Surname,
                    FirstName = personWindow.Firstname,
                    SecondName = personWindow.Secondname,
                    Position = personWindow.Position,
                    PhoneWork = personWindow.PhoneWork,
                    Phone = personWindow.Phone,
                    Cabinet = personWindow.Cabinet,
                    Email = personWindow.Email,
                    Other = personWindow.Other,
                    IdDepartment = SelectedVertex.Department,
                    IdBoss = personWindow.BossId,
                    IdHelper = personWindow.HelperId,
                    BirthDay = personWindow.BirthDay
                };

                db.Employees.Add(employee);
                db.SaveChanges();
                   
            }
        }
        private VertexControl FindSelected()
        {
            foreach (VertexControl v in Graph.vertices)
            {
                if(v.IsActive==true) return v;
            }
            return null!;
        }

        private void EmployerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedVertex = FindSelected();
            var emp = EmployerList.SelectedItem.GetType();
            int id= (int)emp.GetProperty("Id")!.GetValue(EmployerList.SelectedItem, null)!;
            Employee employee = db.Employees.FirstOrDefault(p => p.IdEmployee == id)!;
            PersonWindow personWindow = new(employee, SelectedVertex);
            personWindow.Show();
            
        }
    }
}
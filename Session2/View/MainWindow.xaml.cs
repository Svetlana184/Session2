using Microsoft.Identity.Client;
using Session2.Model;
using Session2.View;
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
            VertexControl vertexRoot = new VertexControl(987, "Дороги России", 0,this);
            vertexRoot.Level = 1;
            Graph = new Graph(vertexRoot);
            List<Department> departmentList = db.Departments.Where(p => p.IdDepartment != 987).ToList();
            foreach (Department department in departmentList)
            {
                VertexControl v = new VertexControl(department.IdDepartment, department.DepartmentName, department.IdDepartmentParent,this);
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
                Employee employee = new Employee();
                employee.Surname=personWindow.Surname;
                employee.FirstName = personWindow.Firstname;
                employee.SecondName = personWindow.Secondname;
                employee.Position = personWindow.Position;
                employee.PhoneWork = personWindow.PhoneWork;
                employee.Phone = personWindow.Phone;
                employee.Cabinet = personWindow.Cabinet;
                employee.Email = personWindow.Email;
                //employee.IdDepartment = db.Departments.FirstOrDefault(p => p.DepartmentName == personWindow.Department)!.IdDepartment;
                employee.Other = personWindow.Other;
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
    }
}
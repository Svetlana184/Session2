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
    /// 
    
    public partial class MainWindow : Window
    {
        public Graph Graph { get; set; }
        private RoadOfRussiaContext db;
        private VertexControl SelectedVertex { get; set; }

       

        public MainWindow()
        {
            InitializeComponent();
            db=new RoadOfRussiaContext();
            FormUpdate();
        }
        private void FormUpdate()
        {
            VertexControl vertexRoot = new(987, "Дороги России", 0, this);
            vertexRoot.Level = 1;
            Graph = new Graph(vertexRoot);

            List<Department> departmentList = db.Departments.Where(p => p.IdDepartment != 987).ToList();
            foreach (Department department in departmentList)
            {
                VertexControl v = new(department.IdDepartment, department.DepartmentName, department.IdDepartmentParent, this);
                Graph.AddVertex(v);
            }
            Graph.DrawGraph(MainCanvas);


            if (SelectedVertex != null)
            {
                EmployerList.ItemsSource = null;


                List<Department> depList = new List<Department>();

                depList.Add(db.Departments.FirstOrDefault(p => p.IdDepartment == SelectedVertex.Department)!);

                for (int j = SelectedVertex.Level + 1; j < Graph.MaxLevel; j++)
                {
                    List<Department> childList = new List<Department>();
                    foreach (Department v in depList)
                    {
                        childList.AddRange(db.Departments.Where(p => p.IdDepartmentParent == v.IdDepartment));
                    }
                    depList.AddRange(childList);

                }

                List<Employee> listMain = new List<Employee>();

                foreach (Department d in depList)
                {
                    listMain.AddRange(db.Employees.Where(p => p.IdDepartment == d.IdDepartment && (p.IsFired == null || ((DateTime)p.IsFired).AddDays(30) > DateTime.Now)));
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
                                IsFired = empl.IsFired
                            }).ToList();

                
                EmployerList.ItemsSource = list;

            }
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
                FormUpdate();
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
            
            if (EmployerList.SelectedIndex != -1)
            {
                SelectedVertex = FindSelected();


                var emp = EmployerList.SelectedItem.GetType();
                int id = (int)emp.GetProperty("Id")!.GetValue(EmployerList.SelectedItem, null)!;
                Employee employee = db.Employees.FirstOrDefault(p => p.IdEmployee == id)!;
                
                PersonWindow personWindow = new(employee, SelectedVertex);
                if (personWindow.ShowDialog() == true)
                {
                    employee.Surname = personWindow.Surname;
                    employee.FirstName = personWindow.Firstname;
                    employee.SecondName = personWindow.Secondname;
                    employee.Position = personWindow.Position;
                    employee.PhoneWork = personWindow.PhoneWork;
                    employee.Phone = personWindow.Phone;
                    employee.Cabinet = personWindow.Cabinet;
                    employee.Email = personWindow.Email;
                    employee.Other = personWindow.Other;
                    employee.IdDepartment = SelectedVertex.Department;
                    employee.IdBoss = personWindow.BossId;
                    employee.IdHelper = personWindow.HelperId;
                    employee.BirthDay = personWindow.BirthDay;
                    db.Employees.Update(employee);
                    db.SaveChanges();
                    FormUpdate();
                }

            }
        }
    }
}
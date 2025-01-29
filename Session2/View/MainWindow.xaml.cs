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
    }
}
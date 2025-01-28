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
        private Graph graph;
        private RoadOfRussiaContext db;
        public MainWindow()
        {
            InitializeComponent();
            db=new RoadOfRussiaContext();
            VertexControl vertexRoot = new VertexControl(987, "Дороги России", 0);
            vertexRoot.Level = 1;
            graph = new Graph(vertexRoot);
            //VertexControl v1 = new VertexControl(2, "Отдел 1", 987);
            //graph.AddVertex(v1);
            //VertexControl v2 = new VertexControl(3, "Отдел 2", 2);
            //graph.AddVertex(v2);
            List<Department> departmentList = db.Departments.Where(p => p.IdDepartment != 987).ToList();
            foreach (Department department in departmentList)
            {
                VertexControl v = new VertexControl(department.IdDepartment, department.DepartmentName, department.IdDepartmentParent);
                graph.AddVertex(v);
            }
            graph.DrawGraph(MainCanvas);
            
        }
    }
}
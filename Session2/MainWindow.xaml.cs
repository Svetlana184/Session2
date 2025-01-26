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
        public MainWindow()
        {
            InitializeComponent();
            VertexControl vertexRoot = new VertexControl(1, "Дороги России", 0);
            graph = new Graph(vertexRoot);
            VertexControl v1 = new VertexControl(2, "Отдел 1", 1);
            graph.AddVertex(v1);
            VertexControl v2 = new VertexControl(3, "Отдел 2", 1);
            graph.AddVertex(v2);
            graph.DrawGraph(MainCanvas);
        }
    }
}
using Session2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Session2.Model
{
    struct Paint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class Graph
    {
        public VertexControl v { get; }
        public List<VertexControl> vertices { get; set; }

        public Graph(VertexControl _v)
        {
            this.v = _v;
            vertices = new List<VertexControl>();
        }
        public void AddVertex(VertexControl _v)
        {
            vertices.Add(_v);
        }
        public void DrawGraph(Canvas canvas)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].ParentDepartment==987)
                    vertices[i].Level = 2;
            }
            int maxLevel = 2;
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                for (int j = i+1; j < vertices.Count; j++)
                {
                    if (vertices[i].Department == vertices[j].ParentDepartment)
                    {
                        vertices[j].Level = vertices[i].Level+1;
                        if (vertices[j].Level>maxLevel) maxLevel = vertices[j].Level;

                    }
                }
            }
            Canvas.SetTop(v, 20);
            Canvas.SetLeft(v, 50);
            canvas.Children.Add(v);
            int y = 20;
            for (int i = 2; i <= maxLevel; i++)
            {
                y += 75;int x = 50;
                for (int j = 0; j < vertices.Count; j++)
                {
                    if (vertices[j].Level==i)
                    {
                        Canvas.SetLeft(vertices[j], x);
                        Canvas.SetTop(vertices[j], y);
                        canvas.Children.Add(vertices[j]);
                        x += 250;
                    }
                }
            }
        }
    }
}

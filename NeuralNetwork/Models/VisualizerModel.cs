using NeuralNetwork.Infrastructure.Interfaces;
using NeuralNetwork.Infrastructure.Services;
using NeuralNetwork.Infrastructure.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuralNetwork.Models
{
    public class VisualizerModel
    {
        public static VisualizerModel Instance { get; } = new VisualizerModel();
        
        public const int DEFAULT_POINT_SIZE = 10;

        public IEnumerable<Path> VisualizeMnistData(QueryDataModel model)
        {
            var startPoint = new Point(0, 0);
            var valueArray = (from val in model.InputValues select (byte)val).ToArray();
            int Index = 0;
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    var color = Color.FromRgb(valueArray[Index], valueArray[Index], valueArray[Index]);

                    var rectanglePath = new Path();
                    rectanglePath.Fill = new SolidColorBrush(color);
                    var size = new Size(DEFAULT_POINT_SIZE, DEFAULT_POINT_SIZE);
                    var rectGeometry = new RectangleGeometry(new Rect(startPoint, size));
                    rectanglePath.Data = rectGeometry;

                    yield return rectanglePath;
                    startPoint.X += DEFAULT_POINT_SIZE;
                    Index++;
                }

                startPoint.Y += DEFAULT_POINT_SIZE;
                startPoint.X = 0;
            }
        }
    }
}

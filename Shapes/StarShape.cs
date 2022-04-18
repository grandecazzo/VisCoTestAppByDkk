using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ViscoTestApp.Shapes
{
    class StarShape : BaseShape
    {
        public StarShape(double x, double y, double size, Brush color) : base(x, y, size, color)
        {

        }

        public override Shape GetShape()
        {
            var shape = new Polygon()
            {
                Height = Size,
                Width = Size,
                Fill = Color,
                Style = App.Current.Resources["CoolShape"] as Style,
                StrokeThickness = 2,
                Stretch = Stretch.Fill,
                Points = PointCollection.Parse("144 48, 200 222, 53 114, 235 114, 88 222")
            };
            return shape;
        }


    }
}

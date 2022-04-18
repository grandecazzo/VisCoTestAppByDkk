using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ViscoTestApp.Shapes
{
    class HeartShape : BaseShape
    {
        public HeartShape(double x, double y, double size, Brush color) : base(x, y, size, color)
        {}
        public override Shape GetShape()
        {
            var heart = CreateShape<Path>();

            heart.Data = Geometry.Parse
                ("M 241,200 A 20,20 0 0 0 200,240" +
                "C 210,250 240,270 240,270" +
                "C 240,270 260,260 280,240" +
                "A 20,20 0 0 0 239,200");

            heart.Stretch = Stretch.Fill;

            return heart;
        }
    }
}

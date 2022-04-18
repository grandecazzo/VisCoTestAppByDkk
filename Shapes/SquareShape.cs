using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ViscoTestApp.Shapes
{
    class SquareShape : BaseShape
    {
        public SquareShape(double x, double y, double size, Brush color) : base(x, y, size, color)
        {
        }

        public override Shape GetShape()
        {
            return CreateShape<Rectangle>();
        }
    }
}
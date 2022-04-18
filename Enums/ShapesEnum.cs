using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ViscoTestApp.Enums
{
    public enum ShapesEnum
    {
        [Description("Circle")]
        Circle,
        [Description("Square")]
        Square,
        [Description("Heart")]
        Heart,
        [Description("Star")]
        Star
    }
}

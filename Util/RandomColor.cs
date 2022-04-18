using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ViscoTestApp.Util
{
    static class RandomColorBrush
    {
        public static SolidColorBrush Generate()
        {
            var brushes = App.Current.Resources.MergedDictionaries[4];
            var  color = brushes.Values.OfType<SolidColorBrush>().ElementAt( new Random().Next(0, brushes.Count));

            return color;
        }
    }
}

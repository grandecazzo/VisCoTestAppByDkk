using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using ViscoTestApp.Shapes;

namespace ViscoTestApp
{
    public class Model :INotifyPropertyChanged
    {
        private List<BaseShape> _shapes;

        public List<BaseShape> Shapes
        {
            get
            {
                return _shapes;
            }
        }

        public Model()
        {
            _shapes = new List<BaseShape>();
        }

        public void Add(BaseShape shape)
        {
            shape.PropertyChanged += OnPropertyChanged;
            _shapes.Add(shape);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shapes)));
        }

        public void AddRange(IList<BaseShape> shapes)
        {
            _shapes.AddRange(shapes.Where(x => { x.PropertyChanged += OnPropertyChanged; return true; }));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shapes)));
        }

        public void Remove(BaseShape shape)
        {
            _shapes.Remove(shape);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shapes)));
        }

        public void Clear()
        {
            _shapes.Clear();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shapes)));
        }
           
        private void OnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(s, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

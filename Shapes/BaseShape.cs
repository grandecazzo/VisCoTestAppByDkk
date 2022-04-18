using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ViscoTestApp.Util;
using Newtonsoft.Json;

namespace ViscoTestApp.Shapes
{   
    public abstract class BaseShape : INotifyPropertyChanged
    {
        protected double _coordx;

        protected double _coordy;

        protected double _size;

        protected Brush _color;

        protected string _name;

        public Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        [JsonIgnore]
        public double CenterX
        {
            get
            {
                return _size / 2;
            }
        }

        [JsonIgnore]
        public double CenterY
        {
            get
            {
                return _size / 2;
            }
        }

        public double Size
        {
            get
            {
                return _size;
            }
        }

        public double X
        {
            get
            {
                return _coordx;
            }
            set
            {
                _coordx = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }

        public double Y
        {
            get
            {
                return _coordy;
            }
            set
            {
                _coordy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
            }
        }

        public BaseShape(double x, double y, double size, Brush color)
        {
            _coordx = x;
            _coordy = y;

            _color = color;

            _size = size;
        }
        public void FromShape(Shape shape)
        {
            _coordx = Canvas.GetLeft(shape);
            _coordy = Canvas.GetTop(shape);

            _size = shape.ActualWidth;
            _color = shape.Fill;
        }

        protected virtual T CreateShape<T>() where T : Shape, new()
        {
            var shape = new T()
            {
                Height = Size,
                Width = Size,
                Fill = Color,
                Style = App.Current.Resources["CoolShape"] as Style,
                StrokeThickness = 2
            };

            return shape;
        }

        public abstract Shape GetShape();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

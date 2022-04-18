using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using ViscoTestApp.CustomControls;
using ViscoTestApp.Enums;
using ViscoTestApp.Shapes;
using Newtonsoft.Json;
using Microsoft.Win32;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Windows.Media;
using ViscoTestApp.Util;
using System.IO;

namespace ViscoTestApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public Model Model;

        public MainWindowViewModel()
        {
            Model = new Model();
            Model.PropertyChanged += OnPropertyChanged;
        }

        public void Add(ShapesEnum shapeType, double x, double y, double size = default, Brush color = default)
        {
            BaseShape shape;

            if (size == default)
                size = new Random().Next(25, 75);

            if (color == default)
                color = RandomColorBrush.Generate();
            
            switch (shapeType)
            {
                case ShapesEnum.Circle:
                    shape = new CircleShape(x, y, size, color);
                    break;
                case ShapesEnum.Square:
                    shape = new SquareShape(x, y, size, color);
                    break;
                case ShapesEnum.Heart:
                    shape = new HeartShape(x, y, size, color);
                    break;
                case ShapesEnum.Star:
                    shape = new StarShape(x, y, size, color);
                    break;
                default:
                    shape = null;
                    throw new ArgumentException("", nameof(shapeType));
            }

            int itemsCount = Model.Shapes.Where(x => x.GetType() == shape.GetType()).Count();
            shape.Name = $"{shapeType} {itemsCount}";

            Model.Add(shape);
        }

        public void Save(string path)
        {
            var data = JsonConvert.SerializeObject(Model.Shapes, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });

            File.WriteAllText(path, data);
        }

        public void Load (Stream fileStream)
        {
            using (StreamReader reader = new StreamReader(fileStream))
            {
                var data = JsonConvert.DeserializeObject<List<BaseShape>>(reader.ReadToEnd(), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                });

                Model.Clear();

                Model.AddRange(data);
                
            }
        }

        internal void Remove(BaseShape ShapeData)
        {
            Model.Remove(ShapeData);
        }

        private void OnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(s, e);
        }

        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            errorContext.Handled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

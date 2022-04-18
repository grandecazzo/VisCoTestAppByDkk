using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViscoTestApp.Shapes;

namespace ViscoTestApp.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для ShapeControl.xaml
    /// </summary>
    public partial class ShapeControl : UserControl
    {
        public Shape Shape
        {
            get
            {
                return ShapeContentPanel.Children[0] as Shape;
            }
        }

        public ShapeControl(BaseShape shapeData)
        {
            DataContext = shapeData;
            InitializeComponent();
            var shape = shapeData.GetShape();

            var sizeBinding = new Binding("Size");
            var colorBinding = new Binding("Color");
            var nameBinding = new Binding("Name");
            var leftBinding = new Binding("X");
            var topBinding = new Binding("Y");

            CenterPointLabel.Content = $"{shapeData.CenterX},{shapeData.CenterY}";

            sizeBinding.Source = this.DataContext;
            colorBinding.Source = this.DataContext;
            nameBinding.Source = this.DataContext;
            leftBinding.Source = this.DataContext;
            topBinding.Source = this.DataContext;

            leftBinding.Mode = BindingMode.TwoWay;
            topBinding.Mode = BindingMode.TwoWay;

            shape.SetBinding(Shape.FillProperty, colorBinding);
            shape.SetBinding(WidthProperty, sizeBinding);
            shape.SetBinding(HeightProperty, sizeBinding);

            ShapeNameLabel.SetBinding(ContentProperty, nameBinding);
            NameTextBox.SetBinding(TextBox.TextProperty, nameBinding);

            this.SetBinding(Canvas.LeftProperty, leftBinding);
            this.SetBinding(Canvas.TopProperty, topBinding);

            shape.MouseDown += (s, e) =>
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    EditPopup.IsOpen = !EditPopup.IsOpen;
                }
            };

            ShapeContentPanel.Children.Add(shape);
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            MovedEvent?.Invoke(sender, e);
        }

        public event MouseEventHandler MovedEvent;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var color = ((sender as Button).Content as Ellipse).Fill;

            (DataContext as BaseShape).Color = color;
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ViscoTestApp.CustomControls;
using ViscoTestApp.Enums;
using ViscoTestApp.Shapes;

namespace ViscoTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel _vm;

        public object SaveFildeDialog { get; private set; }

        public MainWindow()
        {

            _vm = new MainWindowViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var shapetype = (ShapesEnum)(sender as MenuItem).Header;
            var x = new Random().Next((int)ObjectsCanvas.ActualWidth - 75);
            var y = new Random().Next((int)ObjectsCanvas.ActualHeight - 75);

            _vm.Add(shapetype, x, y);
        }

        /// <summary>
        /// Item was added or removed
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void OnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            var childs = ObjectsCanvas.Children.OfType<ShapeControl>();
            if (childs.Count() != _vm.Model.Shapes.Count)
            {
                List<BaseShape> matchedChilds = childs.Select(x => x.Shape.DataContext as BaseShape).Intersect(_vm.Model.Shapes).ToList();

                var toAdd = _vm.Model.Shapes.Except(matchedChilds);
                var toRemove = childs.Select(x => x.Shape.DataContext as BaseShape).Except(matchedChilds);

                foreach(var child in toRemove.ToList())
                {
                    var controlToRemove = childs.Where(x => x.DataContext == child).FirstOrDefault();
                    controlToRemove.MovedEvent -= OnMovedEvent;
                    ObjectsCanvas.Children.Remove(controlToRemove);

                    var tabItemToRemove = ObjectListPanel.Children.OfType<ObjectsTabItemControl>()
                        .Where(x => x.DataContext == child).FirstOrDefault();
                    tabItemToRemove.RemoveButtonClickedEvent -= OnRemoveButtonEvent;
                    ObjectListPanel.Children.Remove(tabItemToRemove);

                }

                foreach(var child in toAdd)
                {
                    var shapecontrol = new ShapeControl(child);
                    var shapetab = new ObjectsTabItemControl(child);

                    //shapecontrol.DataContext = child;

                    shapecontrol.MovedEvent += OnMovedEvent;
                    shapetab.RemoveButtonClickedEvent += OnRemoveButtonEvent;

                    ObjectsCanvas.Children.Add(shapecontrol);
                    ObjectListPanel.Children.Add(shapetab);
                }
            }

        }

        private void OnRemoveButtonEvent(object s, EventArgs e)
        {
            _vm.Remove(s as BaseShape);
        }

        private void OnMovedEvent(object s, MouseEventArgs e)
        {
            var shape = s as ShapeControl;
            var point = e.GetPosition(ObjectsCanvas);

            var newLeft = point.X - shape.ActualWidth / 2;
            var newTop = point.Y - shape.Shape.ActualHeight / 2;
            if (e.LeftButton == MouseButtonState.Pressed && EditButton.IsChecked.Value)
            {
                if (newLeft < 0)
                    newLeft = 0;

                if (newTop < 0)
                    newTop = 0;

                Canvas.SetLeft(shape, newLeft);
                Canvas.SetTop(shape, newTop);
                shape.CaptureMouse();

                ObjectsCanvas.InvalidateMeasure();
            }
            else
            {
                shape.ReleaseMouseCapture();
            } 
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog dialog = new SaveFileDialog()
            {
                FileName = "NewTestAppData.json"
            };

            if (dialog.ShowDialog() == true)
                _vm.Save(dialog.FileName);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
            };

            if(dialog.ShowDialog() == true)
            {
                _vm.Load(dialog.OpenFile());
            }
        }
    }
}
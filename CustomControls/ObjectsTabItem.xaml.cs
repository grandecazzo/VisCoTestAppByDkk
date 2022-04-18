using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ObjectsTabItemControl.xaml
    /// </summary>
    public partial class ObjectsTabItemControl : UserControl
    {
        public ObjectsTabItemControl(BaseShape shape)
        {
            this.DataContext = shape;
            
            var nameBinding = new Binding("Name");
            nameBinding.Source = this.DataContext;

            InitializeComponent();

            ObjectNameTextBox.SetBinding(TextBlock.TextProperty, nameBinding);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveButtonClickedEvent?.Invoke(DataContext, EventArgs.Empty);
        }

        public event EventHandler RemoveButtonClickedEvent;
    }
}

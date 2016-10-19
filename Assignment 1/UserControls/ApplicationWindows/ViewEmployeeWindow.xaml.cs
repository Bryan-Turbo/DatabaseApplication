using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Assignment_1.ExternalWindow;
using Assignment_1.UserControls.EntityViews;
using DatabaseTool.Connector;
using UserControl = System.Windows.Controls.UserControl;

namespace Assignment_1.UserControls.ApplicationWindows {
    /// <summary>
    /// Interaction logic for ViewEmployeeWindow.xaml
    /// </summary>
    public partial class ViewEmployeeWindow : UserControl {
        private DispatcherTimer timer;
        public ViewEmployeeWindow() {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += CheckforButtonsToBeEnabled;
            timer.Start();
        }

        private void CheckforButtonsToBeEnabled(object sender, EventArgs e) {
            if (EmployeeViewer.IsSelected()) {
                EditButton.IsEnabled = true;
                RemoveButton.IsEnabled = true;
                timer.Stop();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {
            EmployeeEditor editor = new EmployeeEditor(EmployeeViewer.GetSelectedEmployee());
            editor.Show();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("This feature has not been implemented yet".ToUpper(), "not yet implemented".ToUpper(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
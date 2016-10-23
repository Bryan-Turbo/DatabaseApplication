using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Assignment_1.UserControls.ApplicationWindows;
using Assignment_1.UserControls.ApplicationWindows.WindowViews;
using DatabaseTool.Entities;
using DatabaseTool.Connector;
using DatabaseTool.Query;

namespace Assignment_1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private DispatcherTimer _timer;
        public MainWindow() {
            InitializeComponent();
            _timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(16)};
            _timer.Tick += CheckActiveWindow;
            _timer.Start();
        }

        private void CheckActiveWindow(object sender, EventArgs e) {
            if (ViewEmployeesWindow.Visibility == Visibility.Visible) {
                this.Header.Content = this.ViewEmployeesWindow.Title;
                this.ReturnButton.Visibility = Visibility.Visible;
            }else if (ViewProjectWindow.Visibility == Visibility.Visible) {
                this.Header.Content = this.ViewProjectWindow.Title;
                this.ReturnButton.Visibility = Visibility.Visible;
            } else{
                this.Header.Content = "Select a Window";
                this.ReturnButton.Visibility = Visibility.Hidden;
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e) {
            ViewEmployeesWindow.Visibility = Visibility.Hidden;
            ViewProjectWindow.Visibility = Visibility.Hidden;
            SelectEmployeeWindowButton.Visibility = Visibility.Visible;
            SelectProjectWindowButton.Visibility = Visibility.Visible;
        }

        private void SelectEmployeeWindowButton_Click(object sender, RoutedEventArgs e) {
            ViewEmployeesWindow.Visibility = Visibility.Visible;
            ViewProjectWindow.Visibility = Visibility.Hidden;
            SelectEmployeeWindowButton.Visibility = Visibility.Hidden;
            SelectProjectWindowButton.Visibility = Visibility.Hidden;
        }

        private void SelectProjectWindowButton_Click(object sender, RoutedEventArgs e) {
            ViewEmployeesWindow.Visibility = Visibility.Hidden;
            ViewProjectWindow.Visibility = Visibility.Visible;
            SelectEmployeeWindowButton.Visibility = Visibility.Hidden;
            SelectProjectWindowButton.Visibility = Visibility.Hidden;
        }
    }
}

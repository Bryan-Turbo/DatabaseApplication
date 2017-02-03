using System;
using System.Windows;
using System.Windows.Threading;

namespace DatabaseGUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private DispatcherTimer _timer;
        public MainWindow() {
            InitializeComponent();
            this.Title = "Awesome Database Application";
            this._timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(16)};
            this._timer.Tick += this.CheckActiveWindow;
            this._timer.Start();
        }

        private void CheckActiveWindow(object sender, EventArgs e) {
            if (this.ViewEmployeesWindow.Visibility == Visibility.Visible) {
                this.Header.Content = this.ViewEmployeesWindow.Title;
                this.ReturnButton.Visibility = Visibility.Visible;
            }else if (this.ViewProjectWindow.Visibility == Visibility.Visible) {
                this.Header.Content = this.ViewProjectWindow.Title;
                this.ReturnButton.Visibility = Visibility.Visible;
            } else{
                this.Header.Content = "Select a Window";
                this.ReturnButton.Visibility = Visibility.Hidden;
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e) {
            this.ViewEmployeesWindow.Visibility = Visibility.Hidden;
            this.ViewProjectWindow.Visibility = Visibility.Hidden;
            this.SelectEmployeeWindowButton.Visibility = Visibility.Visible;
            this.SelectProjectWindowButton.Visibility = Visibility.Visible;
            this.AwesomeFace.Visibility = Visibility.Visible;
        }

        private void SelectEmployeeWindowButton_Click(object sender, RoutedEventArgs e) {
            this.ViewEmployeesWindow.Visibility = Visibility.Visible;
            this.ViewProjectWindow.Visibility = Visibility.Hidden;
            this.SelectEmployeeWindowButton.Visibility = Visibility.Hidden;
            this.SelectProjectWindowButton.Visibility = Visibility.Hidden;
            this.AwesomeFace.Visibility = Visibility.Hidden;
        }

        private void SelectProjectWindowButton_Click(object sender, RoutedEventArgs e) {
            this.ViewEmployeesWindow.Visibility = Visibility.Hidden;
            this.ViewProjectWindow.Visibility = Visibility.Visible;
            this.SelectEmployeeWindowButton.Visibility = Visibility.Hidden;
            this.SelectProjectWindowButton.Visibility = Visibility.Hidden;
            this.AwesomeFace.Visibility = Visibility.Hidden;
        }
    }
}

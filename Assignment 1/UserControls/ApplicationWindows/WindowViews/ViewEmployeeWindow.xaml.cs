using System;
using System.Windows;
using System.Windows.Threading;
using Assignment_1.ExternalWindow;
using DatabaseTool.Query;
using UserControl = System.Windows.Controls.UserControl;

namespace Assignment_1.UserControls.ApplicationWindows.WindowViews {
    /// <summary>
    /// Interaction logic for ViewEmployeeWindow.xaml
    /// </summary>
    public partial class ViewEmployeeWindow : UserControl {
        private DispatcherTimer timer;

        public ViewEmployeeWindow() {
            InitializeComponent();


            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(200);
            this.timer.Tick += this.CheckforButtonsToBeEnabled;
            this.timer.Start();
        }

        private void CheckforButtonsToBeEnabled(object sender, EventArgs e) {
            if (!this.EmployeeViewer.IsSelected()) { return; }
            this.EditButton.IsEnabled = true;
            this.RemoveButton.IsEnabled = true;
            this.EmployeeAdress.IsEnabled = true;
            this.EmployeeEducation.IsEnabled = true;
            this.EmployeePsotiion.IsEnabled = true;
            this.timer.Stop();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            EmployeeAdder adder = new EmployeeAdder(this.EmployeeViewer.Connection);
            adder.ShowDialog();
            EmployeeViewer.UpdateViewer();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {
            EmployeeEditor editor = new EmployeeEditor(this.EmployeeViewer.GetSelectedEmployee(), this.EmployeeViewer.Connection);
            editor.ShowDialog();
            EmployeeViewer.UpdateViewer();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this employee from the database?", "CONFIRM DELETION", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) { return; }
            DeleteFromTable.DeleteEmployee(this.EmployeeViewer.Connection, this.EmployeeViewer.GetSelectedEmployee().Bsn);
            this.EmployeeViewer.RemoveEmployee(this.EmployeeViewer.GetSelectedEmployee());
        }

        private void EmployeeAdress_Click(object sender, RoutedEventArgs e) {
            EmployeeAdressWindow window = new EmployeeAdressWindow(this.EmployeeViewer.GetSelectedEmployee(), EmployeeViewer.Connection);
            window.ShowDialog();
        }
    }
}
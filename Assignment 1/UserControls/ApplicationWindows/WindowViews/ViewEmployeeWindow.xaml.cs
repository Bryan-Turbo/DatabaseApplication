using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        private List<Button> _buttonList;

        public ViewEmployeeWindow() {
            InitializeComponent();

            _buttonList = new List<Button> {
                this.EditButton,
                this.RemoveButton,
                this.EmployeeAddress,
                this.EmployeeEducation,
                this.EmployeePosition
            };
            
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(100);
            this.timer.Tick += this.CheckforButtonsToBeEnabled;
            this.timer.Start();
        }

        private void CheckforButtonsToBeEnabled(object sender, EventArgs e) {
            if (!this.EmployeeViewer.IsSelected()) {
                foreach (Button b in _buttonList) b.IsEnabled = false;
                return;
            }
            foreach (Button b in _buttonList) b.IsEnabled = true;
            this.timer.Stop();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            EmployeeAdder adder = new EmployeeAdder(this.EmployeeViewer.Connection);
            adder.ShowDialog();
            EmployeeViewer.UpdateViewer();
            this.timer.Start();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {
            EmployeeEditor editor = new EmployeeEditor(this.EmployeeViewer.GetSelectedEmployee(), this.EmployeeViewer.Connection);
            editor.ShowDialog();
            EmployeeViewer.UpdateViewer();
            this.timer.Start();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this employee from the database?", "CONFIRM DELETION", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) { return; }
            DeleteFromTable.DeleteEmployee(this.EmployeeViewer.Connection, this.EmployeeViewer.GetSelectedEmployee().Bsn);
            this.EmployeeViewer.RemoveEmployee(this.EmployeeViewer.GetSelectedEmployee());
            this.timer.Start();
        }

        private void EmployeeAddress_Click(object sender, RoutedEventArgs e) {
            EmployeeAddressWindow window = new EmployeeAddressWindow(this.EmployeeViewer.GetSelectedEmployee(), EmployeeViewer.Connection);
            window.ShowDialog();
        }

        private void EmployeeEducation_Click(object sender, RoutedEventArgs e) {
            EmployeeDegreeWindow window = new EmployeeDegreeWindow(this.EmployeeViewer.GetSelectedEmployee(), EmployeeViewer.Connection);
            window.ShowDialog();
        }

        private void EmployeePosition_Click(object sender, RoutedEventArgs e) {
            EmployeePositionWindow window = new EmployeePositionWindow(this.EmployeeViewer.GetSelectedEmployee(), EmployeeViewer.Connection);
            window.ShowDialog();
        }
    }
}
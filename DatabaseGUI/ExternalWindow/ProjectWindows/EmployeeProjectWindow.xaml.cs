using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace DatabaseGUI.ExternalWindow.ProjectWindows {
    /// <summary>
    /// Interaction logic for EmployeeProjectWindow.xaml
    /// </summary>
    public partial class EmployeeProjectWindow : Window {
        private List<Employee> _employeeList;
        private List<EmployeePosition> _employeePositionList;
        private List<EmployeeProject> _employeeProjectList;
        private List<Position> _positionList;
        private List<Position> _selectedPositionList;

        private Project _project;

        private DispatcherTimer _timer;

        public EmployeeProjectWindow(Project project) {
            InitializeComponent();

            this._project = project;

            this.GetData();

            this.ViewEmployees();

            this.InitializeTimer();
        }

        private void GetData() {
            this._employeeProjectList = EntityContentSelector.SelectEmployeeProject( ).Where(e => e.ProjectId == this._project.ProjectId).ToList();
            this._employeeList = EntityContentSelector.SelectEmployee( );
            this._employeePositionList = EntityContentSelector.SelectEmployeePosition( );
            this._positionList = EntityContentSelector.SelectPosition( );

            var bsnposList = this._employeeProjectList.Select(e => new {e.Bsn, e.PositionName});
            var feList = this._employeePositionList.OrderBy(e => e.Bsn).Where(e => bsnposList.Contains(new {e.Bsn, e.PositionName})).Select(e => e.Bsn).ToList();
            var fpList = this._employeePositionList.OrderBy(e => e.Bsn).Where(e => bsnposList.Contains(new {e.Bsn, e.PositionName})).Select(e => e.PositionName).ToList();

            List<Employee> eList = new List<Employee>();
            foreach (int b in feList) {
                eList.Add(this._employeeList.Where(e => e.Bsn == b).ToList()[0]);
            }

            List<Position> pList = new List<Position>();
            foreach (string n in fpList) {
                pList.Add(this._positionList.Where(p => p.PositionName == n).ToList()[0]);
            }

            this.EmployeePositionViewer.PopulateList(eList, pList, this._employeeProjectList);
        }

        private void ViewEmployees() {
            foreach (Employee em in this._employeeList) {
                this.EmployeeBox.Items.Add($"{em.Bsn} - {em.Name} {em.Surname}");
            }
        }

        private void InitializeTimer() {
            this._timer = new DispatcherTimer();
            this._timer.Interval = TimeSpan.FromMilliseconds(50);
            this._timer.Tick += this.CheckBoxes;
            this._timer.Start();
        }

        private void CheckBoxes(object sender, EventArgs e) {
            if (this.EmployeeBox.SelectedIndex < 0) {
                this.EmployeePositionBox.IsEnabled = false;
                return;
            }
            this.EmployeePositionBox.IsEnabled = true;
        }

        private void EmployeeBox_DropDownClosed(object sender, EventArgs e) {
            if (this.EmployeeBox.SelectedIndex < 0) {
                return;
            }
            var fpList = this._employeePositionList.Where(em => em.Bsn == this._employeeList[this.EmployeeBox.SelectedIndex].Bsn).Select(em => em.PositionName).ToList();

            this._selectedPositionList = this._positionList.Where(a => fpList.Contains(a.PositionName)).ToList();

            this.EmployeePositionBox.Items.Clear();
            foreach (Position p in this._selectedPositionList) {
                this.EmployeePositionBox.Items.Add(p.PositionName);
            }
        }

        private void AssignEmployee_Click(object sender, RoutedEventArgs e) {
            if (this.EmployeeBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a employee", "NO EMPLOYEE SELECTED", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (this.EmployeePositionBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a position","NO POSITION SELECTED",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            int workingHours;
            if (this.WorkingHours.Text == "" || !int.TryParse(this.WorkingHours.Text, out workingHours)) {
                MessageBox.Show("Please enter a valid amount of hours","INVALID AMOUNT OF HOURS",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            InsertIntoTable.InsertEmployeeProject(this._employeeList[this.EmployeeBox.SelectedIndex].Bsn,
                this._project.ProjectId, this._selectedPositionList[this.EmployeePositionBox.SelectedIndex].PositionName,
                workingHours);
            this.GetData();
        }

        private void Remove_Click(object sender, RoutedEventArgs e) {
            if (this.EmployeePositionViewer.SelectedIndex < 0) {
                MessageBox.Show("Please select an employee","NO EMPLOYEE SELECTED",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            DeleteFromTable.DeleteEmployeeProject(this.EmployeePositionViewer.SelectedEmployee.Bsn, this._project.ProjectId, this.EmployeePositionViewer.SelectedPosition.PositionName);
            this.GetData();
        }
    }
}

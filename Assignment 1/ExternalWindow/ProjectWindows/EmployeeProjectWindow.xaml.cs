using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.ExternalWindow.ProjectWindows {
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

            GetData();

            ViewEmployees();

            InitializeTimer();
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

            EmployeePositionViewer.PopulateList(eList, pList, _employeeProjectList);
        }

        private void ViewEmployees() {
            foreach (Employee em in this._employeeList) {
                EmployeeBox.Items.Add($"{em.Bsn} - {em.Name} {em.Surname}");
            }
        }

        private void InitializeTimer() {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += CheckBoxes;
            _timer.Start();
        }

        private void CheckBoxes(object sender, EventArgs e) {
            if (EmployeeBox.SelectedIndex < 0) {
                EmployeePositionBox.IsEnabled = false;
                return;
            }
            EmployeePositionBox.IsEnabled = true;
        }

        private void EmployeeBox_DropDownClosed(object sender, EventArgs e) {
            if (EmployeeBox.SelectedIndex < 0) {
                return;
            }
            var fpList = this._employeePositionList.Where(em => em.Bsn == _employeeList[EmployeeBox.SelectedIndex].Bsn).Select(em => em.PositionName).ToList();

            _selectedPositionList = this._positionList.Where(a => fpList.Contains(a.PositionName)).ToList();

            EmployeePositionBox.Items.Clear();
            foreach (Position p in _selectedPositionList) {
                EmployeePositionBox.Items.Add(p.PositionName);
            }
        }

        private void AssignEmployee_Click(object sender, RoutedEventArgs e) {
            if (EmployeeBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a employee", "NO EMPLOYEE SELECTED", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EmployeePositionBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a position","NO POSITION SELECTED",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            int workingHours;
            if (WorkingHours.Text == "" || !int.TryParse(WorkingHours.Text, out workingHours)) {
                MessageBox.Show("Please enter a valid amount of hours","INVALID AMOUNT OF HOURS",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            InsertIntoTable.InsertEmployeeProject(this._employeeList[EmployeeBox.SelectedIndex].Bsn,
                this._project.ProjectId, _selectedPositionList[EmployeePositionBox.SelectedIndex].PositionName,
                workingHours);
            GetData();
        }

        private void Remove_Click(object sender, RoutedEventArgs e) {
            if (EmployeePositionViewer.SelectedIndex < 0) {
                MessageBox.Show("Please select an employee","NO EMPLOYEE SELECTED",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            DeleteFromTable.DeleteEmployeeProject(EmployeePositionViewer.SelectedEmployee.Bsn, this._project.ProjectId, EmployeePositionViewer.SelectedPosition.PositionName);
            GetData();
        }
    }
}

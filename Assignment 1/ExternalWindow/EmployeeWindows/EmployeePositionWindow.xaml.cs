using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.ExternalWindow.EmployeeWindows {
    /// <summary>
    /// Interaction logic for EmployeePositionWindow.xaml
    /// </summary>
    public partial class EmployeePositionWindow : Window {
        private DatabaseConnection _connection;
        private Employee _employee;

        private List<Position> _positionList;
        private List<EmployeePosition> _employeePositionList;
        public EmployeePositionWindow(Employee employee, DatabaseConnection connection) {
            InitializeComponent();
            this._connection = connection;
            this._employee = employee;

            this.PositionListViewer.PopulateList(this.GetPositionsOfEmployee());

            foreach (Position p in this._positionList) {
                this.PositionBox.Items.Add(p.PositionName);
            }

            this.Header.Content = $"Employee: {this._employee.Name} {this._employee.Surname}";
        }

        private void GetData() {
            this._positionList = EntityContentSelector.SelectPosition(this._connection);
            this._employeePositionList = EntityContentSelector.SelectEmployeePosition(this._connection);
        }

        private List<Position> GetPositionsOfEmployee() {
            this.GetData();
            var fresult = this._employeePositionList.Where(e => e.Bsn == this._employee.Bsn).Select(n => n.PositionName);

            List<Position> result = this._positionList.Where(p => fresult.Contains(p.PositionName)).ToList();

            return result;
        }

        private void AddPosition_Click(object sender, RoutedEventArgs e) {
            if (this.PositionBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a position","NO POSITION SELECTED",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            InsertIntoTable.InsertEmployeePosition(this._connection, this._employee.Bsn, this._positionList[this.PositionBox.SelectedIndex].PositionName);
            this.PositionListViewer.PopulateList(this.GetPositionsOfEmployee());
        }

        private void RemovePosition_Click(object sender, RoutedEventArgs e) {
            if (this.PositionListViewer.SelectedItem == null) {
                MessageBox.Show("Please select a position", "NO POSITION SELECTED", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DeleteFromTable.DeleteEmployeePosition(this._connection, this._employee.Bsn, this.PositionListViewer.SelectedItem.PositionName);
            this.PositionListViewer.PopulateList(this.GetPositionsOfEmployee());
        }
    }
}

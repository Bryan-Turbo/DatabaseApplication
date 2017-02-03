using System.Collections.Generic;
using System.Windows.Controls;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace DatabaseGUI.UserControls.EntityViews {
    /// <summary>
    /// Interaction logic for EmployeeViewer.xaml
    /// </summary>
    public partial class EmployeeViewer : UserControl {
        private List<Employee> _employeeList;
        public EmployeeViewer() {
            InitializeComponent();
            this._employeeList = EntityContentSelector.SelectEmployee();
            this.PopulateViewer();
        }

       private void PopulateViewer() {
            this.EmployeeList.Items.Clear();
            foreach (Employee employee in this._employeeList) {
                this.EmployeeList.Items.Add(employee);
            }
        }

        public void RemoveEmployee(Employee employee) {
            this.EmployeeList.Items.Remove(employee);
            this._employeeList.Remove(employee);
        }

        public Employee GetSelectedEmployee() {
            return this._employeeList[this.EmployeeList.SelectedIndex];
        }

        public void UpdateViewer() {
            this._employeeList = EntityContentSelector.SelectEmployee();
            this.PopulateViewer();
        }

        public bool IsSelected() {
            return this.EmployeeList.SelectedIndex >= 0;
        }
    }
}

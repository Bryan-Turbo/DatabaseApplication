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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.UserControls.EntityViews {
    /// <summary>
    /// Interaction logic for EmployeeViewer.xaml
    /// </summary>
    public partial class EmployeeViewer : UserControl {
        public  DatabaseConnection Connection;
        private List<Employee> _employeeList;
        public EmployeeViewer() {
            InitializeComponent();
            Connection = new DatabaseConnection("localhost", "assignment1", "root", "");
            _employeeList = EntityContentSelector.SelectEmployee(this.Connection);
            PopulateViewer();
        }

        public void PopulateViewer() {
            EmployeeList.Items.Clear();
            foreach (Employee employee in _employeeList) {
                EmployeeList.Items.Add(employee);
            }
        }

        public void RemoveEmployee(Employee employee) {
            EmployeeList.Items.Remove(employee);
            _employeeList.Remove(employee);
        }

        public Employee GetSelectedEmployee() {
            return _employeeList[EmployeeList.SelectedIndex];
        }

        public void UpdateViewer() {
            _employeeList = EntityContentSelector.SelectEmployee(this.Connection);
            PopulateViewer();
        }

        public bool IsSelected() {
            return EmployeeList.SelectedIndex >= 0;
        }
    }
}

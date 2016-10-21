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
using DatabaseTool.Connector;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.ExternalWindow {
    /// <summary>
    /// Interaction logic for EmployeeDegreeWindow.xaml
    /// </summary>
    public partial class EmployeeDegreeWindow : Window {
        private Employee _employee;
        private DatabaseConnection _connection;

        private List<Degree> _degreeList;
        private List<EmployeeDegree> _employeeDegreeList;
        public EmployeeDegreeWindow(Employee employee, DatabaseConnection connection) {
            InitializeComponent();

            this._employee = employee;
            this._connection = connection;

            this.GetData();
            this.DegreeViewer.PopulateList(GetDegreesOfEmployee(this._employeeDegreeList, this._degreeList));

            foreach (Degree d in this._degreeList) {
                DegreeBox.Items.Add($"{d.Course}, {d.DegreeLevel}");
            }
        }

        private void GetData() {
            _degreeList = EntityContentSelector.SelectDegree(this._connection);
            _employeeDegreeList = EntityContentSelector.SelectEmployeeDegree(this._connection).Where(e => e.Bsn == this._employee.Bsn).ToList();
        }

        private List<Degree> GetDegreesOfEmployee(List<EmployeeDegree> eDList, List<Degree> dList) {
            var fresult = eDList.Select(c => c.Course);

            List<Degree> result = dList.Where(e => fresult.Contains(e.Course)).ToList();

            return result;
        }

        private void AddDegree_Click(object sender, RoutedEventArgs e) {
            if (DegreeBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a degree", "NO DEGREE SELECTED", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            InsertIntoTable.InsertEmployeeDegree(this._connection, this._employee.Bsn, this._degreeList[DegreeBox.SelectedIndex].Course);
            this.GetData();
            this.DegreeViewer.PopulateList(GetDegreesOfEmployee(this._employeeDegreeList, this._degreeList));
        }

        private void RemoveDegree_Click(object sender, RoutedEventArgs e) {
            if (DegreeBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a degree", "NO DEGREE SELECTED", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DeleteFromTable.DeleteEmployeeDegree(this._connection, this._employee.Bsn, this._degreeList[DegreeBox.SelectedIndex].Course);
            this.GetData();
            this.DegreeViewer.PopulateList(GetDegreesOfEmployee(this._employeeDegreeList, this._degreeList));
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace DatabaseGUI.ExternalWindow.EmployeeWindows {
    /// <summary>
    /// Interaction logic for EmployeeDegreeWindow.xaml
    /// </summary>
    public partial class EmployeeDegreeWindow : Window {
        private Employee _employee;

        private List<Degree> _degreeList;
        private List<EmployeeDegree> _employeeDegreeList;

        public EmployeeDegreeWindow(Employee employee) {
            InitializeComponent();

            this._employee = employee;

            this.GetData();
            this.DegreeViewer.PopulateList(this.GetDegreesOfEmployee(this._employeeDegreeList, this._degreeList));

            foreach (Degree d in this._degreeList) {
                this.DegreeBox.Items.Add($"{d.Course}, {d.DegreeLevel}");
            }

            this.Header.Content = $"Employee: {this._employee.Name} {this._employee.Surname}";
        }

        private void GetData() {
            this._degreeList = EntityContentSelector.SelectDegree();
            this._employeeDegreeList = EntityContentSelector.SelectEmployeeDegree().Where(e => e.Bsn == this._employee.Bsn).ToList();
        }

        private List<Degree> GetDegreesOfEmployee(List<EmployeeDegree> eDList, List<Degree> dList) {
            var fresult = eDList.Select(c => c.Course);

            List<Degree> result = dList.Where(e => fresult.Contains(e.Course)).ToList();

            return result;
        }

        private void AddDegree_Click(object sender, RoutedEventArgs e) {
            if (this.DegreeBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a degree", "NO DEGREE SELECTED", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            InsertIntoTable.InsertEmployeeDegree(this._employee.Bsn,
                this._degreeList[this.DegreeBox.SelectedIndex].Course);
            this.GetData();
            this.DegreeViewer.PopulateList(this.GetDegreesOfEmployee(this._employeeDegreeList, this._degreeList));
        }

        private void RemoveDegree_Click(object sender, RoutedEventArgs e) {
            if (this.DegreeBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a degree", "NO DEGREE SELECTED", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            DeleteFromTable.DeleteEmployeeDegree(this._employee.Bsn,
                this._degreeList[this.DegreeBox.SelectedIndex].Course);
            this.GetData();
            this.DegreeViewer.PopulateList(this.GetDegreesOfEmployee(this._employeeDegreeList, this._degreeList));
        }
    }
}
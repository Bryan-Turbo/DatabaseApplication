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
using DatabaseTool.Entities;

namespace Assignment_1.UserControls.EntityViews {
    /// <summary>
    /// Interaction logic for EmployeePositionViewer.xaml
    /// </summary>
    public partial class EmployeePositionViewer : UserControl {
        private List<Employee> _employeeList;
        private List<Position> _positionList;
        public EmployeePositionViewer() {
            InitializeComponent();
        }

        public void PopulateList(List<Employee> employeeList, List<Position> positionList, List<EmployeeProject> employeeProjectList) {
            this._employeeList = employeeList;
            this._positionList = positionList;

            EmployeePositionList.Items.Clear();
            for (int i = 0; i < this._employeeList.Count; i++) {
                var e = this._employeeList[i];
                var p = this._positionList[i];
                EmployeePositionList.Items.Add(new {e.Bsn, e.Name, e.Surname, p.PositionName, employeeProjectList[i].WorkingHours});
            }
        }

        public int SelectedIndex {
            get { return this.EmployeePositionList.SelectedIndex; }
        }

        public Employee SelectedEmployee {
            get { return this._employeeList[this.EmployeePositionList.SelectedIndex]; }
        }

        public Position SelectedPosition {
            get { return this._positionList[this.EmployeePositionList.SelectedIndex]; }
        }
    }
}

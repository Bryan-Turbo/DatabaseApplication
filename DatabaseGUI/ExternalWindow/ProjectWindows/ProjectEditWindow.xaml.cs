using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace DatabaseGUI.ExternalWindow.ProjectWindows {
    /// <summary>
    /// Interaction logic for ProjectEditWindow.xaml
    /// </summary>
    public partial class ProjectEditWindow : Window {
        private Project _project;

        private List<Headquarters> _hqList;
        public ProjectEditWindow(Project project) {
            InitializeComponent();

            this._project = project;
            this._hqList = EntityContentSelector.SelectHeadquarters( );

            this.Header.Content = $"Project: {this._project.ProjectId}";

            foreach (Headquarters hq in this._hqList) {
                this.Headquarters.Items.Add(hq.BuildingName);
            }

            this.SetData();
        }

        private void SetData() {
            this.Budget.Text = this._project.Budget.ToString("0.00");
            this.TotalHours.Text = this._project.TotalHours.ToString("0");
            this.Headquarters.SelectedIndex = this._hqList.IndexOf(this._hqList.Where(b => b.BuildingName == this._project.BuildingName).ToList()[0]);
        }

        private void ProcessChanges(object sender, RoutedEventArgs e) {
            if (this.Budget.Text == "" || this.TotalHours.Text == "") {
                MessageBox.Show("Please make sure all the fields are correctly filled in","ONE OR MORE FIELDS EMPTY",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            float budget;
            if (!float.TryParse(this.Budget.Text, out budget)) {
                MessageBox.Show("Please enter a valid budget", "INVALID BUDGET",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int totalHours;
            if (!int.TryParse(this.TotalHours.Text, out totalHours)) {
                MessageBox.Show("Please enter a valid amount of hours", "INVALID AMOUNT OF HOURS", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            UpdateTable.UpdateProject(this._project.ProjectId, budget, totalHours, this._hqList[this.Headquarters.SelectedIndex].BuildingName);

            this._project.Budget = budget;
            this._project.TotalHours = totalHours;
            this._project.BuildingName = this._hqList[this.Headquarters.SelectedIndex].BuildingName;
        }

        private void ResetFields(object sender, RoutedEventArgs e) {
            this.SetData();
        }
    }
}

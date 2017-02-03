using System.Collections.Generic;
using System.Windows;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace DatabaseGUI.ExternalWindow.ProjectWindows {
    /// <summary>
    /// Interaction logic for ProjectAddWindow.xaml
    /// </summary>
    public partial class ProjectAddWindow : Window {
        private List<Headquarters> _hqList;
        public ProjectAddWindow( ) {
            InitializeComponent();

            this._hqList = EntityContentSelector.SelectHeadquarters( );
            foreach (Headquarters hq in this._hqList) {
                this.HeadquarterBox.Items.Add($"{hq.BuildingName}");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            if (this.HeadquarterBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a Headquarters", "NO HQ SELECTED", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            float budget;
            int totalHours;
            if (!float.TryParse(this.Budget.Text, out budget)) {
                MessageBox.Show("Please enter a valid budget", "INVALID BUDGET", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            float.TryParse(budget.ToString("0.00"), out budget);

            if (!int.TryParse(this.TotalHours.Text, out totalHours)) {
                MessageBox.Show("Please enter a valid amount of hours", "INVALID AMOUNT OF HOURS", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            InsertIntoTable.InsertProject(budget, totalHours, this._hqList[this.HeadquarterBox.SelectedIndex].BuildingName);
        }
    }
}

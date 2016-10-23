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

namespace Assignment_1.ExternalWindow.ProjectWindows {
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
            if (HeadquarterBox.SelectedIndex < 0) {
                MessageBox.Show("Please select a Headquarters", "NO HQ SELECTED", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            float budget;
            int totalHours;
            if (!float.TryParse(Budget.Text, out budget)) {
                MessageBox.Show("Please enter a valid budget", "INVALID BUDGET", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            float.TryParse(budget.ToString("0.00"), out budget);

            if (!int.TryParse(TotalHours.Text, out totalHours)) {
                MessageBox.Show("Please enter a valid amount of hours", "INVALID AMOUNT OF HOURS", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            InsertIntoTable.InsertProject(budget, totalHours, this._hqList[HeadquarterBox.SelectedIndex].BuildingName);
        }
    }
}

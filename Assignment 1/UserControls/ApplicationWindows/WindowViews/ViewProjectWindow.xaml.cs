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
using System.Windows.Threading;
using Assignment_1.ExternalWindow.ProjectWindows;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace Assignment_1.UserControls.ApplicationWindows.WindowViews {
    /// <summary>
    /// Interaction logic for ViewProjectWindow.xaml
    /// </summary>
    public partial class ViewProjectWindow : UserControl {
        private readonly DispatcherTimer _timer;
        private readonly List<Button> _buttonList;
        public string Title { get; }
        public ViewProjectWindow() {
            InitializeComponent();

            this.Title = "Project Viewer";

            _buttonList = new List<Button> {
                this.EditProject,
                this.RemoveProject,
                this.CheckEmployees
            };

            this._timer = new DispatcherTimer();
            this._timer.Interval = TimeSpan.FromMilliseconds(100);
            this._timer.Tick += this.CheckforButtonsToBeEnabled;
            this._timer.Start();
        }

        private void CheckforButtonsToBeEnabled(object sender, EventArgs e) {
            if (this.ProjectViewer.SelectedItem == null) {
                foreach (Button b in _buttonList)
                    b.IsEnabled = false;
                return;
            }
            foreach (Button b in _buttonList)
                b.IsEnabled = true;
            this._timer.Stop();
        }

        private void AddProject_Click(object sender, RoutedEventArgs e) {
            ProjectAddWindow window = new ProjectAddWindow(this.ProjectViewer.Connection);
            window.ShowDialog();
            UpdateProjectViewer();
            this._timer.Start();
        }

        private void RemoveProject_Click(object sender, RoutedEventArgs e) {
            DeleteFromTable.DeleteProject(this.ProjectViewer.Connection, this.ProjectViewer.SelectedItem.ProjectId);
            UpdateProjectViewer();
            this._timer.Start();
        }

        private void EditProject_Click(object sender, RoutedEventArgs e) {
            ProjectEditWindow window = new ProjectEditWindow(this.ProjectViewer.Connection, this.ProjectViewer.SelectedItem);
            window.ShowDialog();
            UpdateProjectViewer();
            this._timer.Start();
        }

        private void UpdateProjectViewer() {
            if (this.ProjectViewer.IsShowingFailingProjects)
                this.ProjectViewer.UpdateFailingViewer();
            else
                this.ProjectViewer.UpdateViewer();
        }

        private void CheckRent_Click(object sender, RoutedEventArgs e) {
            ProjectViewer.UpdateFailingViewer();
        }

        private void CheckAllProject_Click(object sender, RoutedEventArgs e) {
            ProjectViewer.UpdateViewer();
        }

        private void CheckEmployees_Click(object sender, RoutedEventArgs e) {
            EmployeeProjectWindow window = new EmployeeProjectWindow(ProjectViewer.Connection, ProjectViewer.SelectedItem);
            window.ShowDialog();
        }
    }
}

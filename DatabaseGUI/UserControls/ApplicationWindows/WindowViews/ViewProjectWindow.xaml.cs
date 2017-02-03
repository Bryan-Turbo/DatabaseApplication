using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DatabaseGUI.ExternalWindow.ProjectWindows;
using DatabaseTool.Query;

namespace DatabaseGUI.UserControls.ApplicationWindows.WindowViews {
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

            this._buttonList = new List<Button> {
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
                foreach (Button b in this._buttonList)
                    b.IsEnabled = false;
                return;
            }
            foreach (Button b in this._buttonList)
                b.IsEnabled = true;
            this._timer.Stop();
        }

        private void AddProject_Click(object sender, RoutedEventArgs e) {
            ProjectAddWindow window = new ProjectAddWindow();
            window.ShowDialog();
            this.UpdateProjectViewer();
            this._timer.Start();
        }

        private void RemoveProject_Click(object sender, RoutedEventArgs e) {
            DeleteFromTable.DeleteProject(this.ProjectViewer.SelectedItem.ProjectId);
            this.UpdateProjectViewer();
            this._timer.Start();
        }

        private void EditProject_Click(object sender, RoutedEventArgs e) {
            ProjectEditWindow window = new ProjectEditWindow(this.ProjectViewer.SelectedItem);
            window.ShowDialog();
            this.UpdateProjectViewer();
            this._timer.Start();
        }

        private void UpdateProjectViewer() {
            if (this.ProjectViewer.IsShowingFailingProjects)
                this.ProjectViewer.UpdateFailingViewer();
            else
                this.ProjectViewer.UpdateViewer();
        }

        private void CheckRent_Click(object sender, RoutedEventArgs e) {
            this.ProjectViewer.UpdateFailingViewer();
        }

        private void CheckAllProject_Click(object sender, RoutedEventArgs e) {
            this.ProjectViewer.UpdateViewer();
        }

        private void CheckEmployees_Click(object sender, RoutedEventArgs e) {
            EmployeeProjectWindow window = new EmployeeProjectWindow(this.ProjectViewer.SelectedItem);
            window.ShowDialog();
        }
    }
}

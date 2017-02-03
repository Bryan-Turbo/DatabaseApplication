using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using DatabaseTool.Entities;
using DatabaseTool.Query;

namespace DatabaseGUI.UserControls.EntityViews {
    /// <summary>
    /// Interaction logic for ProjectViewer.xaml
    /// </summary>
    public partial class ProjectViewer : UserControl {
        public bool IsShowingFailingProjects;
        private List<Project> _projectList;
        public ProjectViewer() {
            InitializeComponent();

            this.PopulateViewer();
        }

        private void PopulateViewer() {
            this.IsShowingFailingProjects = false;
            this._projectList = EntityContentSelector.SelectProject();
            foreach (Project p in this._projectList) {
                this.ProjectList.Items.Add(p);
            }
        }

        private void PopulateFailingViewer() {
            this.IsShowingFailingProjects = true;
            this._projectList = EntityContentSelector.SelectProject();
            var hqList = EntityContentSelector.SelectHeadquarters();

            foreach (Project p in this._projectList) {
                Headquarters hq = hqList.Where(h => h.BuildingName == p.BuildingName).ToList()[0];
                if (p.Budget * 0.10F < hq.Rent) {
                    this.ProjectList.Items.Add(p);
                }
            }
        }

        public void UpdateViewer() {
            this.ProjectList.Items.Clear();
            this.PopulateViewer();
        }

        public void UpdateFailingViewer() {
            this.ProjectList.Items.Clear();
            this.PopulateFailingViewer();
        }

        public Project SelectedItem {
            get {
                if (this.ProjectList.SelectedIndex < 0) 
                    return null;
                return (Project) this.ProjectList.SelectedItem;
            }
        }
    }
}

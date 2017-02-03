using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using DatabaseTool.Entities;

namespace DatabaseGUI.UserControls.EntityViews {
    /// <summary>
    /// Interaction logic for PositionViewer.xaml
    /// </summary>
    public partial class PositionViewer : UserControl {
        public PositionViewer() {
            InitializeComponent();
        }

        public void PopulateList(List<Position> positionList) {
            this.PositionListViewer.Items.Clear();
            foreach (Position p in positionList) {
                this.PositionListViewer.Items.Add(p);
            }
        }

        public Position SelectedItem {
            get { return (Position) this.PositionListViewer.SelectedItem; }
        }

        public List<Position> Items {
            get {
                var list = this.PositionListViewer.Items;
                return list.Cast<Position>().ToList();
            }
        }
    }
}

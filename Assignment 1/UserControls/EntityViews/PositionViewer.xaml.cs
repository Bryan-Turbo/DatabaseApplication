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
    /// Interaction logic for PositionViewer.xaml
    /// </summary>
    public partial class PositionViewer : UserControl {
        public PositionViewer() {
            InitializeComponent();
        }

        public void PopulateList(List<Position> positionList) {
            PositionListViewer.Items.Clear();
            foreach (Position p in positionList) {
                PositionListViewer.Items.Add(p);
            }
        }

        public Position SelectedItem {
            get { return (Position) PositionListViewer.SelectedItem; }
        }

        public List<Position> Items {
            get {
                var list = PositionListViewer.Items;
                return list.Cast<Position>().ToList();
            }
        }
    }
}

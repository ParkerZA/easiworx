using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finx.App.UserControls
{
    public partial class xListView : UserControl
    {
        public xListView()
        {
            InitializeComponent();
        }

        bool _ReadOnly;
        public bool ReadOnly
        {
            get {return _ReadOnly;}
            set { _ReadOnly = value;
            if (_ReadOnly)
            {
                this.DataGridList.SelectionMode = SourceGrid.GridSelectionMode.Row;
                this.DataGridList.Columns.ColumnsAdded += Columns_ColumnsAdded;
            };
            }
        }

        void Columns_ColumnsAdded(object sender, SourceGrid.IndexRangeEventArgs e)
        {
            if (_ReadOnly)
            {
                foreach (SourceGrid.DataGridColumn dC in (SourceGrid.DataGridColumns)sender)
                {
                    if (dC.DataCell.Editor != null)
                        dC.DataCell.Editor.EnableEdit = false;
                }
            }
        }
    }
}

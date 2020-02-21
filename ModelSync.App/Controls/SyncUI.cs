using ModelSync.App.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinForms.Library;

namespace ModelSync.App.Controls
{
    public partial class SyncUI : UserControl
    {
        public ControlBinder<MergeDefinition> _binder;

        public SyncUI()
        {
            InitializeComponent();
        }

        public MergeDefinition Document
        {
            get { return _binder.Document; }
            set
            {
                _binder = new ControlBinder<MergeDefinition>();                
                _binder.AddItems(cbSourceType, m => m.SourceType, new Dictionary<SourceType, string>()
                {
                    { SourceType.Assembly, "From Assembly" },
                    { SourceType.Connection, "From Connection" }
                });
                _binder.Add(tbSource, m => m.Source);
                _binder.Add(tbDest, m => m.Destination);
                // todo: color
                // todo: ignored objects

                _binder.Document = value;
            }
        }
        
        private void btnGenerateScript_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exc)
            {

                throw;
            }
        }
    }
}

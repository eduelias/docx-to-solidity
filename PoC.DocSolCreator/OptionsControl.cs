using System;
using System.Windows.Forms;
using DocSolTemplateer.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace PoC.DocSolCreator
{
    public partial class OptionsControl : UserControl
    {
        public Microsoft.Office.Tools.Word.Document ActiveDocument;        

        public Action<ISCExpression> InsertControls;
        public Action<IEnumerable<ISContractTemplate>> MakeContract;

        public OptionsControl()
        {
            InitializeComponent();
        }

        private void ComboboxTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ComboboxTemplates.SelectedIndex;
            ISContractTemplate selectedItem = ComboboxTemplates.SelectedItem as ISContractTemplate;
            var expressions = selectedItem.GetDefaultExpression();
            UpperPanel.Controls.Clear();

            foreach (var xp in expressions)
            {
                var bt = new Button()
                {
                    Text = xp.Name                    
                };

                bt.Click += (s, ev) => InsertControls(xp);
                UpperPanel.Controls.Add(bt);
            }

            var bt2 = new Button()
            {
                Text = "Refresh it"
            };

            bt2.Click += (s, ev) =>
            {
                var lst = new List<ISContractTemplate>();         
                foreach (var i in ComboboxTemplates.Items)
                {
                    lst.Add(i as ISContractTemplate);
                }
                MakeContract(lst);
            };

            UpperPanel.Controls.Add(bt2);
        }
    }
}

using System;
using System.Windows.Forms;
using DocSolTemplateer.Infrastructure.Interfaces;


namespace PoC.DocSolCreator
{
    public partial class OptionsControl : UserControl
    {
        public Microsoft.Office.Tools.Word.Document ActiveDocument;        

        public Action<ISCExpression> InsertControls;
        public Action MakeContract;

        public OptionsControl()
        {
            InitializeComponent();
        }

        private void ComboboxTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ComboboxTemplates.SelectedIndex;
            ISContractTemplate selectedItem = ComboboxTemplates.SelectedItem as ISContractTemplate;
            var templates = selectedItem.GetExpressionList();

            foreach (var xp in templates)
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
                MakeContract();
            };

            UpperPanel.Controls.Add(bt2);
        }
    }
}

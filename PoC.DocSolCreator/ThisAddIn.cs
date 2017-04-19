using PoC.DocSolCreator.Templates;
using System;
using System.Linq;
using Microsoft.Office.Interop.Word;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace PoC.DocSolCreator
{
    public partial class ThisAddIn
    {
        private OptionsControl myOptionsControl;
        private Microsoft.Office.Tools.CustomTaskPane myCustomTaskPane;
        private Microsoft.Office.Tools.Word.GroupContentControl protectedGroup;
        private Microsoft.Office.Tools.Word.Document ActiveDocument;
        private IEnumerable<Type> LoadedTemplates;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            ActiveDocument =
                Globals.Factory.GetVstoObject(this.Application.ActiveDocument);

            SetupControl();
            SetupCombobox();
        }

        private void SetupCombobox()
        {
            var type = typeof(ISContractTemplate);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            LoadedTemplates = types;

            myOptionsControl.ComboboxTemplates.Items.Clear();
            foreach (var template in types)
            {
                var Template = Activator.CreateInstance(template) as ISContractTemplate;
                myOptionsControl.ComboboxTemplates.Items.Add(Template);
            }
        }

        private void SetupControl()
        {
            myOptionsControl = new OptionsControl();
            myOptionsControl.ActiveDocument = ActiveDocument;         
            myCustomTaskPane = this.CustomTaskPanes.Add(myOptionsControl, "Generate smart-contract");
            myCustomTaskPane.Visible = true;

            myOptionsControl.InsertControls += InsertControls;
            myOptionsControl.MakeContract += GenerateContract;
        }

        private void InsertControls(ISCExpression expression)
        {
            Selection currentSelection = Application.Selection;
            var currentRange = currentSelection.Range;

            expression.ShouldCheck = true;

            foreach (var ctrl in expression.GetControls().OrderByDescending(o => o.Order))
            {
                switch (ctrl.Kind) {
                    case SCTControlKindEnum.Integer:
                    case SCTControlKindEnum.String:
                        {
                            var tctrl = this.ActiveDocument.ContentControls.Add(WdContentControlType.wdContentControlRichText);
                            tctrl.SetPlaceholderText(null, null, ctrl.DisplayText);
                            tctrl.LockContentControl = true;
                        }
                        break;
                    default:
                        return;
                }
            }
        }

        private void GenerateContract()
        {
            foreach(var template in myOptionsControl
                .ComboboxTemplates
                .Items
                .Cast<ISContractTemplate>()
                .Where(ct => ct.ShouldCheck))
            {

            }
        }

        [Obsolete("Should use InsertControls")]
        private void InputText(Func<string> exp, Func<int, IEnumerable<Tuple<string, Tuple<int, int>>>> rangeMaker)
        {
            Selection currentSelection = Application.Selection;            

            // Store the user's current Overtype selection
            bool userOvertype = Application.Options.Overtype;
            var startingPoint = currentSelection.Range.Start;

            // Make sure Overtype is turned off.
            if (Application.Options.Overtype)
            {
                Application.Options.Overtype = false;
            }

            // Test to see if selection is an insertion point.
            if (currentSelection.Type == WdSelectionType.wdSelectionIP)
            {
                currentSelection.TypeText(exp());
            }
            else
                if (currentSelection.Type == WdSelectionType.wdSelectionNormal)
            {
                // Move to start of selection.
                if (Application.Options.ReplaceSelection)
                {
                    object direction = WdCollapseDirection.wdCollapseStart;
                    currentSelection.Collapse(ref direction);
                }
                currentSelection.TypeText(exp());                
            }
            else
            {
                // Do nothing.
            }            

            var i = 0;
            foreach (var range in rangeMaker(startingPoint))
            {
                var r1 = ActiveDocument.Range(range.Item2.Item1, range.Item2.Item2);
                
                ActiveDocument.Bookmarks.Add(
                    string.Format(range.Item1, i),
                    r1
                );
                r1.Select();
                protectedGroup = ActiveDocument.Controls.AddGroupContentControl(string.Format(range.Item1, i));
                protectedGroup.LockContentControl = true;                
                i++;
            }
            
            // Restore the user's Overtype selection
            Application.Options.Overtype = userOvertype;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {            
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {            
            this.Startup += new System.EventHandler(ThisAddIn_Startup);            
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}

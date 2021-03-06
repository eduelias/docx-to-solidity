﻿using DocSolGenerator;
using DocSolTemplateer.Infrastructure.Enums;
using DocSolTemplateer.Infrastructure.Interfaces;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PoC.DocSolCreator
{
    public partial class ThisAddIn
    {
        private OptionsControl myOptionsControl;
        private Microsoft.Office.Tools.CustomTaskPane myCustomTaskPane;
        private Microsoft.Office.Tools.Word.GroupContentControl protectedGroup;
        private Microsoft.Office.Tools.Word.Document ActiveDocument;
        private IEnumerable<Type> LoadedTemplates;


        private Dictionary<string, Tuple<string, object>> DataStorage = new Dictionary<string, Tuple<string, object>>();

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

            myOptionsControl.ComboboxTemplates.SelectedIndex = 0;
            myOptionsControl.ComboboxTemplates.Refresh();
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

            expression = expression.ContainerTemplate.GetDefaultExpression().First();

            expression.GroupIdentification = Guid.NewGuid();

            (expression.ContainerTemplate.UsedExpressions as List<ISCExpression>).Add(expression);

            foreach (var ctrl in expression.Controls.OrderByDescending(o => o.Order))
            {                
                switch (ctrl.Kind) {
                    case SCTControlKindEnum.Integer:
                    case SCTControlKindEnum.String:
                        {                                                    
                            ActiveDocument.Paragraphs[ActiveDocument.Paragraphs.Count].Range.InsertParagraphAfter();

                            var id = ctrl.InternalName + "_" + expression.GroupIdentification;
                            var range = ActiveDocument.Paragraphs[ActiveDocument.Paragraphs.Count].Range;

                            PlainTextContentControl textControl2 = ActiveDocument.Controls.AddPlainTextContentControl(range,id);
                            textControl2.PlaceholderText = ctrl.DisplayText;
                            textControl2.Deleting += (a, e) =>
                            {
                                (expression.ContainerTemplate.UsedExpressions as List<ISCExpression>)?.Remove(
                                    expression);
                            };

                            ctrl.AddObjectId(expression.GroupIdentification.ToString(), textControl2.ID);
                            ctrl.Grouping = expression.GroupIdentification;                            
                            
                            
                            ActiveDocument.Paragraphs[ActiveDocument.Paragraphs.Count].Range.InsertParagraphAfter();
                        }
                        break;
                    case SCTControlKindEnum.Date:
                        break;
                    default:
                        return;                        
                }                
            }
        }

        private void GenerateContract(IEnumerable<ISContractTemplate> templates)
        {
            myOptionsControl.ContractEditBox.Text = string.Empty;                        
            foreach (var template in templates)
            {
                foreach (var expression in template.UsedExpressions)
                {   
                    List<object> values = new List<object>();
                    foreach (var control in expression.GetControlIds())
                    {
                        var wordcomp = ActiveDocument.Controls.Cast<Microsoft.Office.Tools.Word.ContentControlBase>().Where(i => i.ID == control).First();
                        values.Add(wordcomp.GetType().GetProperties().Where(p => p.Name == "Text").First().GetValue(wordcomp));                        
                    }
                    expression.ControlValues.AddRange(values);                 
                }
            }

            myOptionsControl.ContractEditBox.Text = Generator.GenerateContract("alpha", templates, "");
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

    //public static class WordExtensionMethods
    //{
    //    public static List<ContentControl> GetAllContentControls(this Microsoft.Office.Tools.Word.Document wordDocument)
    //    {
    //        if (null == wordDocument)
    //            throw new ArgumentNullException("wordDocument");

    //        List<Microsoft.Office.Tools.Word.ContentControl> ccList = new List<Microsoft.Office.Tools.Word.ContentControl>();

    //        // The code below search content controls in all
    //        // word document stories see http://word.mvps.org/faqs/customization/ReplaceAnywhere.htm
    //        Range rangeStory;
    //        foreach (Range range in wordDocument.StoryRanges)
    //        {
    //            rangeStory = range;
    //            do
    //            {
    //                try
    //                {
    //                    foreach (ContentControl cc in rangeStory.ContentControls)
    //                    {
    //                        ccList.Add(cc);
    //                    }
    //                    foreach (Shape shapeRange in rangeStory.ShapeRange)
    //                    {
    //                        foreach (ContentControl cc in shapeRange.TextFrame.TextRange.ContentControls)
    //                        {
    //                            ccList.Add(cc);
    //                        }
    //                    }
    //                }
    //                catch (COMException) { }
    //                rangeStory = rangeStory.NextStoryRange;

    //            }
    //            while (rangeStory != null);
    //        }
    //        return ccList;
    //    }
    //}
}
namespace PoC.DocSolCreator
{
    partial class OptionsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.UpperPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ComboboxTemplates = new System.Windows.Forms.ComboBox();
            this.ContractEditBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.UpperPanel);
            this.splitContainer1.Panel1.Controls.Add(this.ComboboxTemplates);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ContractEditBox);
            this.splitContainer1.Panel2MinSize = 75;
            this.splitContainer1.Size = new System.Drawing.Size(800, 800);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 0;
            // 
            // UpperPanel
            // 
            this.UpperPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpperPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.UpperPanel.Location = new System.Drawing.Point(3, 30);
            this.UpperPanel.Name = "UpperPanel";
            this.UpperPanel.Size = new System.Drawing.Size(794, 71);
            this.UpperPanel.TabIndex = 3;
            // 
            // ComboboxTemplates
            // 
            this.ComboboxTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboboxTemplates.FormattingEnabled = true;
            this.ComboboxTemplates.Items.AddRange(new object[] {
            "Shares"});
            this.ComboboxTemplates.Location = new System.Drawing.Point(3, 3);
            this.ComboboxTemplates.Name = "ComboboxTemplates";
            this.ComboboxTemplates.Size = new System.Drawing.Size(794, 21);
            this.ComboboxTemplates.TabIndex = 2;
            this.ComboboxTemplates.SelectedIndexChanged += new System.EventHandler(this.ComboboxTemplates_SelectedIndexChanged);
            // 
            // ContractEditBox
            // 
            this.ContractEditBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContractEditBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ContractEditBox.Location = new System.Drawing.Point(3, 3);
            this.ContractEditBox.Name = "ContractEditBox";
            this.ContractEditBox.Size = new System.Drawing.Size(794, 690);
            this.ContractEditBox.TabIndex = 0;
            this.ContractEditBox.Text = "";
            // 
            // OptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(800, 0);
            this.Name = "OptionsControl";
            this.Size = new System.Drawing.Size(800, 800);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.ComboBox ComboboxTemplates;
        public System.Windows.Forms.FlowLayoutPanel UpperPanel;
        public System.Windows.Forms.RichTextBox ContractEditBox;
    }
}

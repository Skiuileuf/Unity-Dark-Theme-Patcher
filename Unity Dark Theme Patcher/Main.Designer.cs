namespace Unity_Dark_Theme_Patcher
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.unityFolderPath = new System.Windows.Forms.ComboBox();
            this.pickFolderButton = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.restoreButton = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink();
            this.exeCheckBox = new System.Windows.Forms.CheckBox();
            this.pdbCheckBox = new System.Windows.Forms.CheckBox();
            this.applyPatchButton = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink();
            this.logBox = new System.Windows.Forms.ListBox();
            this.unityVersionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Unity installation folder";
            // 
            // unityFolderPath
            // 
            this.unityFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.unityFolderPath.Location = new System.Drawing.Point(15, 25);
            this.unityFolderPath.Name = "unityFolderPath";
            this.unityFolderPath.Size = new System.Drawing.Size(474, 21);
            this.unityFolderPath.TabIndex = 1;
            this.unityFolderPath.TextChanged += new System.EventHandler(this.unityFolderPath_TextChanged);
            // 
            // pickFolderButton
            // 
            this.pickFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pickFolderButton.Location = new System.Drawing.Point(504, 24);
            this.pickFolderButton.Name = "pickFolderButton";
            this.pickFolderButton.Size = new System.Drawing.Size(25, 22);
            this.pickFolderButton.TabIndex = 2;
            this.pickFolderButton.Text = "...";
            this.pickFolderButton.UseVisualStyleBackColor = true;
            this.pickFolderButton.Click += new System.EventHandler(this.pickFolderButton_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.unityVersionLabel);
            this.splitContainer.Panel1.Controls.Add(this.restoreButton);
            this.splitContainer.Panel1.Controls.Add(this.exeCheckBox);
            this.splitContainer.Panel1.Controls.Add(this.pdbCheckBox);
            this.splitContainer.Panel1.Controls.Add(this.applyPatchButton);
            this.splitContainer.Panel1.Controls.Add(this.unityFolderPath);
            this.splitContainer.Panel1.Controls.Add(this.pickFolderButton);
            this.splitContainer.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer.Panel2.Controls.Add(this.logBox);
            this.splitContainer.Size = new System.Drawing.Size(800, 450);
            this.splitContainer.SplitterDistance = 542;
            this.splitContainer.TabIndex = 3;
            // 
            // restoreButton
            // 
            this.restoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.restoreButton.Enabled = false;
            this.restoreButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.restoreButton.Location = new System.Drawing.Point(12, 346);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.NoteText = "";
            this.restoreButton.Size = new System.Drawing.Size(514, 43);
            this.restoreButton.TabIndex = 6;
            this.restoreButton.Text = "Restore";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
            // 
            // exeCheckBox
            // 
            this.exeCheckBox.AutoCheck = false;
            this.exeCheckBox.AutoSize = true;
            this.exeCheckBox.Location = new System.Drawing.Point(15, 52);
            this.exeCheckBox.Name = "exeCheckBox";
            this.exeCheckBox.Size = new System.Drawing.Size(47, 17);
            this.exeCheckBox.TabIndex = 5;
            this.exeCheckBox.Text = "EXE";
            this.exeCheckBox.UseVisualStyleBackColor = true;
            // 
            // pdbCheckBox
            // 
            this.pdbCheckBox.AutoCheck = false;
            this.pdbCheckBox.AutoSize = true;
            this.pdbCheckBox.Location = new System.Drawing.Point(68, 51);
            this.pdbCheckBox.Name = "pdbCheckBox";
            this.pdbCheckBox.Size = new System.Drawing.Size(48, 17);
            this.pdbCheckBox.TabIndex = 4;
            this.pdbCheckBox.Text = "PDB";
            this.pdbCheckBox.UseVisualStyleBackColor = true;
            // 
            // applyPatchButton
            // 
            this.applyPatchButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applyPatchButton.Enabled = false;
            this.applyPatchButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.applyPatchButton.Location = new System.Drawing.Point(12, 395);
            this.applyPatchButton.Name = "applyPatchButton";
            this.applyPatchButton.NoteText = "";
            this.applyPatchButton.Size = new System.Drawing.Size(517, 43);
            this.applyPatchButton.TabIndex = 3;
            this.applyPatchButton.Text = "Apply Patch";
            this.applyPatchButton.UseVisualStyleBackColor = true;
            this.applyPatchButton.Click += new System.EventHandler(this.applyPatchButton_Click);
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.FormattingEnabled = true;
            this.logBox.Location = new System.Drawing.Point(0, 0);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(254, 450);
            this.logBox.TabIndex = 0;
            // 
            // unityVersionLabel
            // 
            this.unityVersionLabel.AutoSize = true;
            this.unityVersionLabel.Location = new System.Drawing.Point(15, 76);
            this.unityVersionLabel.Name = "unityVersionLabel";
            this.unityVersionLabel.Size = new System.Drawing.Size(71, 13);
            this.unityVersionLabel.TabIndex = 7;
            this.unityVersionLabel.Text = "Unity version:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer);
            this.Name = "Main";
            this.Text = "Unity Dark Theme Patcher";
            this.Load += new System.EventHandler(this.Main_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.TextBox unityFolderPath;
        private System.Windows.Forms.ComboBox unityFolderPath;
        private System.Windows.Forms.Button pickFolderButton;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink applyPatchButton;
        private System.Windows.Forms.CheckBox exeCheckBox;
        private System.Windows.Forms.CheckBox pdbCheckBox;
        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink restoreButton;
        public System.Windows.Forms.ListBox logBox;
        private System.Windows.Forms.Label unityVersionLabel;
    }
}


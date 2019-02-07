namespace LiveSplit.TimeoutAutoSplitter.UI
{
    partial class ComponentSettings
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
            this.chkPause = new System.Windows.Forms.CheckBox();
            this.cmbComparison = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSkip = new System.Windows.Forms.RadioButton();
            this.rbSplit = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkPause
            // 
            this.chkPause.AutoSize = true;
            this.chkPause.Location = new System.Drawing.Point(10, 89);
            this.chkPause.Name = "chkPause";
            this.chkPause.Size = new System.Drawing.Size(118, 17);
            this.chkPause.TabIndex = 1;
            this.chkPause.Text = "Pause after splitting";
            this.chkPause.UseVisualStyleBackColor = true;
            // 
            // cmbComparison
            // 
            this.cmbComparison.FormattingEnabled = true;
            this.cmbComparison.Location = new System.Drawing.Point(215, 10);
            this.cmbComparison.Name = "cmbComparison";
            this.cmbComparison.Size = new System.Drawing.Size(234, 21);
            this.cmbComparison.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSkip);
            this.groupBox1.Controls.Add(this.rbSplit);
            this.groupBox1.Location = new System.Drawing.Point(10, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 46);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Split Behavior";
            // 
            // rbSkip
            // 
            this.rbSkip.AutoSize = true;
            this.rbSkip.Location = new System.Drawing.Point(120, 19);
            this.rbSkip.Name = "rbSkip";
            this.rbSkip.Size = new System.Drawing.Size(67, 17);
            this.rbSkip.TabIndex = 1;
            this.rbSkip.TabStop = true;
            this.rbSkip.Text = "Skip split";
            this.rbSkip.UseVisualStyleBackColor = true;
            // 
            // rbSplit
            // 
            this.rbSplit.AutoSize = true;
            this.rbSplit.Location = new System.Drawing.Point(6, 19);
            this.rbSplit.Name = "rbSplit";
            this.rbSplit.Size = new System.Drawing.Size(108, 17);
            this.rbSplit.TabIndex = 0;
            this.rbSplit.TabStop = true;
            this.rbSplit.Text = "Split at exact time";
            this.rbSplit.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Comparison for timeout";
            // 
            // ComponentSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkPause);
            this.Controls.Add(this.cmbComparison);
            this.Controls.Add(this.groupBox1);
            this.Name = "ComponentSettings";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(459, 121);
            this.Load += new System.EventHandler(this.ComponentSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkPause;
        private System.Windows.Forms.ComboBox cmbComparison;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSkip;
        private System.Windows.Forms.RadioButton rbSplit;
        private System.Windows.Forms.Label label1;
    }
}

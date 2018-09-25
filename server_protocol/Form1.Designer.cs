namespace WindowsFormsApp4
{
    partial class Form1
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
            this.MainText = new System.Windows.Forms.TextBox();
            this.StartCon = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainText
            // 
            this.MainText.Location = new System.Drawing.Point(39, 36);
            this.MainText.Multiline = true;
            this.MainText.Name = "MainText";
            this.MainText.ReadOnly = true;
            this.MainText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MainText.Size = new System.Drawing.Size(316, 305);
            this.MainText.TabIndex = 1;
            // 
            // StartCon
            // 
            this.StartCon.Location = new System.Drawing.Point(134, 385);
            this.StartCon.Name = "StartCon";
            this.StartCon.Size = new System.Drawing.Size(93, 54);
            this.StartCon.TabIndex = 3;
            this.StartCon.Text = "Start Connection";
            this.StartCon.UseVisualStyleBackColor = true;
            this.StartCon.Click += new System.EventHandler(this.StartCon_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(361, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(802, 725);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 787);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.StartCon);
            this.Controls.Add(this.MainText);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox MainText;
        private System.Windows.Forms.Button StartCon;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}


namespace LitKit1.Controls
{
    partial class frmToast
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
            this.components = new System.ComponentModel.Container();
            this.title = new System.Windows.Forms.Label();
            this.subtitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(10, 9);
            this.title.MaximumSize = new System.Drawing.Size(300, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(51, 16);
            this.title.TabIndex = 0;
            this.title.Text = "label1";
            // 
            // subtitle
            // 
            this.subtitle.AutoSize = true;
            this.subtitle.Location = new System.Drawing.Point(12, 28);
            this.subtitle.MaximumSize = new System.Drawing.Size(300, 0);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(35, 13);
            this.subtitle.TabIndex = 1;
            this.subtitle.Text = "label2";
            // 
            // frmToast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(259, 52);
            this.Controls.Add(this.subtitle);
            this.Controls.Add(this.title);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmToast";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label subtitle;
        private System.Windows.Forms.Timer timer1;
    }
}
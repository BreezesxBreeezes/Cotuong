namespace Board
{
    partial class FlashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlashScreen));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer_Loading = new System.Windows.Forms.Timer(this.components);
            this.progressBar_Loading = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Board.Properties.Resources.R2HtQQ0;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(56, -12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(590, 399);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer_Loading
            // 
            this.timer_Loading.Interval = 2000;
            this.timer_Loading.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar_Loading
            // 
            this.progressBar_Loading.Location = new System.Drawing.Point(22, 368);
            this.progressBar_Loading.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBar_Loading.Name = "progressBar_Loading";
            this.progressBar_Loading.Size = new System.Drawing.Size(544, 19);
            this.progressBar_Loading.TabIndex = 1;
            // 
            // FlashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 396);
            this.Controls.Add(this.progressBar_Loading);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FlashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cờ Tướng";
            this.Load += new System.EventHandler(this.FlashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer_Loading;
        private System.Windows.Forms.ProgressBar progressBar_Loading;
    }
}
namespace Board
{
    partial class Sound_Options
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
            this.Ok = new System.Windows.Forms.Button();
            this.Sound = new System.Windows.Forms.CheckBox();
            this.BgMusic = new System.Windows.Forms.CheckBox();
            this.Browse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.path = new System.Windows.Forms.MaskedTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(82, 147);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "Lưu";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Sound
            // 
            this.Sound.AutoSize = true;
            this.Sound.BackColor = System.Drawing.Color.Transparent;
            this.Sound.Location = new System.Drawing.Point(14, 48);
            this.Sound.Name = "Sound";
            this.Sound.Size = new System.Drawing.Size(81, 17);
            this.Sound.TabIndex = 2;
            this.Sound.Text = "Tiếng động";
            this.Sound.UseVisualStyleBackColor = false;
            this.Sound.CheckedChanged += new System.EventHandler(this.Sound_CheckedChanged);
            // 
            // BgMusic
            // 
            this.BgMusic.AutoSize = true;
            this.BgMusic.BackColor = System.Drawing.Color.Transparent;
            this.BgMusic.Location = new System.Drawing.Point(14, 71);
            this.BgMusic.Name = "BgMusic";
            this.BgMusic.Size = new System.Drawing.Size(73, 17);
            this.BgMusic.TabIndex = 3;
            this.BgMusic.Text = "Nhạc nền";
            this.BgMusic.UseVisualStyleBackColor = false;
            this.BgMusic.CheckedChanged += new System.EventHandler(this.BgMusic_CheckedChanged);
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(199, 109);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(26, 23);
            this.Browse.TabIndex = 5;
            this.Browse.Text = "...";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(11, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Đường dẫn tới file nhạc nền";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Ok_Click);
            // 
            // path
            // 
            this.path.BackColor = System.Drawing.Color.Peru;
            this.path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.path.Location = new System.Drawing.Point(14, 111);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(185, 20);
            this.path.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Sound_Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = global::Board.Properties.Resources.OptBg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(237, 188);
            this.Controls.Add(this.path);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.BgMusic);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Sound);
            this.Controls.Add(this.Ok);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Sound_Options";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sound_Options";
            this.TransparencyKey = System.Drawing.Color.DimGray;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.CheckBox Sound;
        private System.Windows.Forms.CheckBox BgMusic;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox path;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
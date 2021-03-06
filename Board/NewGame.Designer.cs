namespace Board
{
    partial class NewGame
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
            this.label2 = new System.Windows.Forms.Label();
            this.TenQuanDo = new System.Windows.Forms.TextBox();
            this.TenQuanDen = new System.Windows.Forms.TextBox();
            this.BatDau = new System.Windows.Forms.PictureBox();
            this.Close_NewGame_Form = new System.Windows.Forms.PictureBox();
            this.TinhThoiGian = new System.Windows.Forms.CheckBox();
            this.Chap = new System.Windows.Forms.CheckBox();
            this.ChonThoiGian = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.BatDau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close_NewGame_Form)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(21, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quân Đỏ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(21, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Quân Đen";
            // 
            // TenQuanDo
            // 
            this.TenQuanDo.BackColor = System.Drawing.Color.Gray;
            this.TenQuanDo.Location = new System.Drawing.Point(107, 51);
            this.TenQuanDo.MaxLength = 69;
            this.TenQuanDo.Name = "TenQuanDo";
            this.TenQuanDo.Size = new System.Drawing.Size(171, 20);
            this.TenQuanDo.TabIndex = 2;
            // 
            // TenQuanDen
            // 
            this.TenQuanDen.BackColor = System.Drawing.Color.Gray;
            this.TenQuanDen.Location = new System.Drawing.Point(107, 87);
            this.TenQuanDen.MaxLength = 69;
            this.TenQuanDen.Name = "TenQuanDen";
            this.TenQuanDen.Size = new System.Drawing.Size(171, 20);
            this.TenQuanDen.TabIndex = 3;
            // 
            // BatDau
            // 
            this.BatDau.BackColor = System.Drawing.Color.Silver;
            this.BatDau.Image = global::Board.Properties.Resources.BatDau;
            this.BatDau.Location = new System.Drawing.Point(122, 152);
            this.BatDau.Name = "BatDau";
            this.BatDau.Size = new System.Drawing.Size(76, 30);
            this.BatDau.TabIndex = 4;
            this.BatDau.TabStop = false;
            this.BatDau.Click += new System.EventHandler(this.BatDau_Click);
            this.BatDau.MouseEnter += new System.EventHandler(this.BatDau_MouseEnter);
            this.BatDau.MouseLeave += new System.EventHandler(this.BatDau_MouseLeave);
            // 
            // Close_NewGame_Form
            // 
            this.Close_NewGame_Form.BackColor = System.Drawing.Color.Transparent;
            this.Close_NewGame_Form.Image = global::Board.Properties.Resources.cCancel;
            this.Close_NewGame_Form.Location = new System.Drawing.Point(297, 11);
            this.Close_NewGame_Form.Name = "Close_NewGame_Form";
            this.Close_NewGame_Form.Size = new System.Drawing.Size(25, 25);
            this.Close_NewGame_Form.TabIndex = 5;
            this.Close_NewGame_Form.TabStop = false;
            this.Close_NewGame_Form.Click += new System.EventHandler(this.Close_NewGame_Form_Click);
            this.Close_NewGame_Form.MouseEnter += new System.EventHandler(this.Close_NewGame_Form_MouseEnter);
            this.Close_NewGame_Form.MouseLeave += new System.EventHandler(this.Close_NewGame_Form_MouseLeave);
            // 
            // TinhThoiGian
            // 
            this.TinhThoiGian.AutoSize = true;
            this.TinhThoiGian.BackColor = System.Drawing.Color.Transparent;
            this.TinhThoiGian.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TinhThoiGian.Location = new System.Drawing.Point(122, 119);
            this.TinhThoiGian.Name = "TinhThoiGian";
            this.TinhThoiGian.Size = new System.Drawing.Size(114, 21);
            this.TinhThoiGian.TabIndex = 6;
            this.TinhThoiGian.Text = "Tính thời gian";
            this.TinhThoiGian.UseVisualStyleBackColor = false;
            this.TinhThoiGian.CheckedChanged += new System.EventHandler(this.TinhThoiGian_CheckedChanged);
            // 
            // Chap
            // 
            this.Chap.AutoSize = true;
            this.Chap.BackColor = System.Drawing.Color.Transparent;
            this.Chap.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chap.Location = new System.Drawing.Point(24, 119);
            this.Chap.Name = "Chap";
            this.Chap.Size = new System.Drawing.Size(77, 21);
            this.Chap.TabIndex = 7;
            this.Chap.Text = "Cờ chấp";
            this.Chap.UseVisualStyleBackColor = false;
            // 
            // ChonThoiGian
            // 
            this.ChonThoiGian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChonThoiGian.Enabled = false;
            this.ChonThoiGian.FormattingEnabled = true;
            this.ChonThoiGian.Items.AddRange(new object[] {
            "5 phút",
            "10 phút",
            "15 phút"});
            this.ChonThoiGian.Location = new System.Drawing.Point(230, 119);
            this.ChonThoiGian.Name = "ChonThoiGian";
            this.ChonThoiGian.Size = new System.Drawing.Size(93, 21);
            this.ChonThoiGian.TabIndex = 8;
            // 
            // NewGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = global::Board.Properties.Resources.MessBox_bg1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(332, 194);
            this.Controls.Add(this.ChonThoiGian);
            this.Controls.Add(this.Chap);
            this.Controls.Add(this.TinhThoiGian);
            this.Controls.Add(this.Close_NewGame_Form);
            this.Controls.Add(this.BatDau);
            this.Controls.Add(this.TenQuanDen);
            this.Controls.Add(this.TenQuanDo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewGame";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewGame";
            this.TransparencyKey = System.Drawing.Color.DimGray;
            ((System.ComponentModel.ISupportInitialize)(this.BatDau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close_NewGame_Form)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TenQuanDo;
        private System.Windows.Forms.PictureBox BatDau;
        private System.Windows.Forms.PictureBox Close_NewGame_Form;
        private System.Windows.Forms.CheckBox TinhThoiGian;
        private System.Windows.Forms.CheckBox Chap;
        private System.Windows.Forms.ComboBox ChonThoiGian;
        private System.Windows.Forms.TextBox TenQuanDen;
    }
}
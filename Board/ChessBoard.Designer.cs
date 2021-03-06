using System.Drawing;
using System.Windows.Forms;
namespace Board
{
    partial class ChessBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessBoard));
            this.NewGame = new System.Windows.Forms.PictureBox();
            this.Undo = new System.Windows.Forms.PictureBox();
            this.Save = new System.Windows.Forms.PictureBox();
            this.Open = new System.Windows.Forms.PictureBox();
            this.Options = new System.Windows.Forms.PictureBox();
            this.Exit = new System.Windows.Forms.PictureBox();
            this.LuuVanCo = new System.Windows.Forms.Panel();
            this.Closepnl = new System.Windows.Forms.PictureBox();
            this.Cancel = new System.Windows.Forms.PictureBox();
            this.Ok = new System.Windows.Forms.PictureBox();
            this.TenQuanDo = new System.Windows.Forms.Label();
            this.TenQuanDen = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Mini = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.NewGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Undo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Open)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Options)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exit)).BeginInit();
            this.LuuVanCo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Closepnl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mini)).BeginInit();
            this.SuspendLayout();
            // 
            // NewGame
            // 
            this.NewGame.BackColor = System.Drawing.Color.Transparent;
            this.NewGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NewGame.Image = global::Board.Properties.Resources.Newgame;
            this.NewGame.Location = new System.Drawing.Point(105, 28);
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(41, 41);
            this.NewGame.TabIndex = 7;
            this.NewGame.TabStop = false;
            this.toolTip1.SetToolTip(this.NewGame, "Tạo Ván cờ mới");
            this.NewGame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NewGame_MouseClick);
            this.NewGame.MouseEnter += new System.EventHandler(this.NewGame_MouseEnter);
            this.NewGame.MouseLeave += new System.EventHandler(this.NewGame_MouseLeave);
            // 
            // Undo
            // 
            this.Undo.BackColor = System.Drawing.Color.Transparent;
            this.Undo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Undo.Image = global::Board.Properties.Resources.Undo;
            this.Undo.Location = new System.Drawing.Point(173, 28);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(41, 41);
            this.Undo.TabIndex = 8;
            this.Undo.TabStop = false;
            this.toolTip1.SetToolTip(this.Undo, "Xin đi lại");
            this.Undo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Undo_MouseClick);
            this.Undo.MouseEnter += new System.EventHandler(this.Undo_MouseEnter);
            this.Undo.MouseLeave += new System.EventHandler(this.Undo_MouseLeave);
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.Color.Transparent;
            this.Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save.Image = global::Board.Properties.Resources.Save;
            this.Save.Location = new System.Drawing.Point(241, 28);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(41, 41);
            this.Save.TabIndex = 9;
            this.Save.TabStop = false;
            this.toolTip1.SetToolTip(this.Save, "Lưu ván cờ");
            this.Save.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Save_MouseClick);
            this.Save.MouseEnter += new System.EventHandler(this.Save_MouseEnter);
            this.Save.MouseLeave += new System.EventHandler(this.Save_MouseLeave);
            // 
            // Open
            // 
            this.Open.BackColor = System.Drawing.Color.Transparent;
            this.Open.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Open.Image = global::Board.Properties.Resources.Open;
            this.Open.Location = new System.Drawing.Point(309, 28);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(41, 41);
            this.Open.TabIndex = 10;
            this.Open.TabStop = false;
            this.toolTip1.SetToolTip(this.Open, "Mở ván cờ");
            this.Open.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Open_MouseClick);
            this.Open.MouseEnter += new System.EventHandler(this.Open_MouseEnter);
            this.Open.MouseLeave += new System.EventHandler(this.Open_MouseLeave);
            // 
            // Options
            // 
            this.Options.BackColor = System.Drawing.Color.Transparent;
            this.Options.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Options.Image = global::Board.Properties.Resources.Options;
            this.Options.Location = new System.Drawing.Point(377, 28);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(41, 41);
            this.Options.TabIndex = 11;
            this.Options.TabStop = false;
            this.toolTip1.SetToolTip(this.Options, "Tùy chỉnh");
            this.Options.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Options_MouseClick);
            this.Options.MouseEnter += new System.EventHandler(this.Options_MouseEnter);
            this.Options.MouseLeave += new System.EventHandler(this.Options_MouseLeave);
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.Transparent;
            this.Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit.Image = global::Board.Properties.Resources.Exit;
            this.Exit.Location = new System.Drawing.Point(445, 28);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(41, 41);
            this.Exit.TabIndex = 12;
            this.Exit.TabStop = false;
            this.toolTip1.SetToolTip(this.Exit, "Thoát");
            this.Exit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Exit_MouseClick);
            this.Exit.MouseEnter += new System.EventHandler(this.Exit_MouseEnter);
            this.Exit.MouseLeave += new System.EventHandler(this.Exit_MouseLeave);
            // 
            // LuuVanCo
            // 
            this.LuuVanCo.BackColor = System.Drawing.Color.Transparent;
            this.LuuVanCo.BackgroundImage = global::Board.Properties.Resources.luu;
            this.LuuVanCo.Controls.Add(this.Closepnl);
            this.LuuVanCo.Controls.Add(this.Cancel);
            this.LuuVanCo.Controls.Add(this.Ok);
            this.LuuVanCo.Location = new System.Drawing.Point(556, 9);
            this.LuuVanCo.Name = "LuuVanCo";
            this.LuuVanCo.Size = new System.Drawing.Size(266, 93);
            this.LuuVanCo.TabIndex = 13;
            // 
            // Closepnl
            // 
            this.Closepnl.Image = global::Board.Properties.Resources.Close;
            this.Closepnl.Location = new System.Drawing.Point(242, 7);
            this.Closepnl.Name = "Closepnl";
            this.Closepnl.Size = new System.Drawing.Size(15, 15);
            this.Closepnl.TabIndex = 2;
            this.Closepnl.TabStop = false;
            this.Closepnl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Closepnl_MouseClick);
            this.Closepnl.MouseEnter += new System.EventHandler(this.Closepnl_MouseEnter);
            this.Closepnl.MouseLeave += new System.EventHandler(this.Closepnl_MouseLeave);
            // 
            // Cancel
            // 
            this.Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Cancel.Image")));
            this.Cancel.Location = new System.Drawing.Point(226, 58);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(25, 25);
            this.Cancel.TabIndex = 1;
            this.Cancel.TabStop = false;
            this.Cancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Cancel_MouseClick);
            this.Cancel.MouseEnter += new System.EventHandler(this.Cancel_MouseEnter);
            this.Cancel.MouseLeave += new System.EventHandler(this.Cancel_MouseLeave);
            // 
            // Ok
            // 
            this.Ok.Image = global::Board.Properties.Resources.Ok;
            this.Ok.Location = new System.Drawing.Point(193, 58);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(25, 25);
            this.Ok.TabIndex = 0;
            this.Ok.TabStop = false;
            this.Ok.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Ok_MouseClick);
            this.Ok.MouseEnter += new System.EventHandler(this.Ok_MouseEnter);
            this.Ok.MouseLeave += new System.EventHandler(this.Ok_MouseLeave);
            // 
            // TenQuanDo
            // 
            this.TenQuanDo.AutoSize = true;
            this.TenQuanDo.BackColor = System.Drawing.Color.Transparent;
            this.TenQuanDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TenQuanDo.ForeColor = System.Drawing.Color.Maroon;
            this.TenQuanDo.Location = new System.Drawing.Point(628, 162);
            this.TenQuanDo.Name = "TenQuanDo";
            this.TenQuanDo.Size = new System.Drawing.Size(0, 24);
            this.TenQuanDo.TabIndex = 14;
            // 
            // TenQuanDen
            // 
            this.TenQuanDen.AutoSize = true;
            this.TenQuanDen.BackColor = System.Drawing.Color.Transparent;
            this.TenQuanDen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TenQuanDen.ForeColor = System.Drawing.Color.Black;
            this.TenQuanDen.Location = new System.Drawing.Point(628, 423);
            this.TenQuanDen.Name = "TenQuanDen";
            this.TenQuanDen.Size = new System.Drawing.Size(0, 24);
            this.TenQuanDen.TabIndex = 15;
            // 
            // Mini
            // 
            this.Mini.BackColor = System.Drawing.Color.Transparent;
            this.Mini.Image = global::Board.Properties.Resources.Mini;
            this.Mini.Location = new System.Drawing.Point(828, 9);
            this.Mini.Name = "Mini";
            this.Mini.Size = new System.Drawing.Size(30, 30);
            this.Mini.TabIndex = 16;
            this.Mini.TabStop = false;
            this.Mini.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Mini_MouseClick);
            this.Mini.MouseEnter += new System.EventHandler(this.Mini_MouseEnter);
            this.Mini.MouseLeave += new System.EventHandler(this.Mini_MouseLeave);
            // 
            // ChessBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Board.Properties.Resources.BackGround;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(870, 630);
            this.Controls.Add(this.Mini);
            this.Controls.Add(this.TenQuanDen);
            this.Controls.Add(this.TenQuanDo);
            this.Controls.Add(this.LuuVanCo);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Undo);
            this.Controls.Add(this.NewGame);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChessBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cờ Tướng";
            this.Load += new System.EventHandler(this.ChessBoard_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.NewGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Undo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Open)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Options)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exit)).EndInit();
            this.LuuVanCo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Closepnl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mini)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox NewGame;
        private System.Windows.Forms.PictureBox Undo;
        private System.Windows.Forms.PictureBox Save;
        private System.Windows.Forms.PictureBox Open;
        private System.Windows.Forms.PictureBox Options;
        private System.Windows.Forms.PictureBox Exit;
        private Panel LuuVanCo;
        private PictureBox Ok;
        private PictureBox Cancel;
        private PictureBox Closepnl;
        private Label TenQuanDo;
        private Label TenQuanDen;
        private ToolTip toolTip1;
        private PictureBox Mini;

     }
}


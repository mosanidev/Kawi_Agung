namespace Kawi_Agung
{
    partial class FormLogin
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
			this.panelWindowsButton = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.textBoxUsername = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panelUsername = new System.Windows.Forms.Panel();
			this.buttonNextUsername = new ePOSOne.btnProduct.Button_WOC();
			this.panelBuatPassword = new System.Windows.Forms.Panel();
			this.textBoxRePassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.btnNextBuatPassword = new ePOSOne.btnProduct.Button_WOC();
			this.textBoxNewPassword = new System.Windows.Forms.TextBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panelPassword = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.btnNextPassword = new ePOSOne.btnProduct.Button_WOC();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.dragControl1 = new Kawi_Agung.DragControl();
			this.btnClose = new System.Windows.Forms.PictureBox();
			this.btnMinimize = new System.Windows.Forms.PictureBox();
			this.panelWindowsButton.SuspendLayout();
			this.panelUsername.SuspendLayout();
			this.panelBuatPassword.SuspendLayout();
			this.panelPassword.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
			this.SuspendLayout();
			// 
			// panelWindowsButton
			// 
			this.panelWindowsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.panelWindowsButton.Controls.Add(this.btnClose);
			this.panelWindowsButton.Controls.Add(this.btnMinimize);
			this.panelWindowsButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelWindowsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelWindowsButton.Location = new System.Drawing.Point(0, 0);
			this.panelWindowsButton.Name = "panelWindowsButton";
			this.panelWindowsButton.Size = new System.Drawing.Size(471, 46);
			this.panelWindowsButton.TabIndex = 12;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.panel1.Location = new System.Drawing.Point(232, 110);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(160, 4);
			this.panel1.TabIndex = 14;
			// 
			// textBoxUsername
			// 
			this.textBoxUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.textBoxUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxUsername.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
			this.textBoxUsername.Location = new System.Drawing.Point(230, 72);
			this.textBoxUsername.Multiline = true;
			this.textBoxUsername.Name = "textBoxUsername";
			this.textBoxUsername.Size = new System.Drawing.Size(162, 37);
			this.textBoxUsername.TabIndex = 15;
			this.textBoxUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUsername_KeyDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 18F);
			this.label1.ForeColor = System.Drawing.Color.AliceBlue;
			this.label1.Location = new System.Drawing.Point(79, 74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(146, 30);
			this.label1.TabIndex = 17;
			this.label1.Text = "username :";
			// 
			// panelUsername
			// 
			this.panelUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.panelUsername.Controls.Add(this.label1);
			this.panelUsername.Controls.Add(this.buttonNextUsername);
			this.panelUsername.Controls.Add(this.textBoxUsername);
			this.panelUsername.Controls.Add(this.panel1);
			this.panelUsername.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelUsername.Location = new System.Drawing.Point(0, 46);
			this.panelUsername.Name = "panelUsername";
			this.panelUsername.Size = new System.Drawing.Size(471, 254);
			this.panelUsername.TabIndex = 14;
			// 
			// buttonNextUsername
			// 
			this.buttonNextUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.buttonNextUsername.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.buttonNextUsername.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonNextUsername.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.buttonNextUsername.FlatAppearance.BorderSize = 0;
			this.buttonNextUsername.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.buttonNextUsername.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.buttonNextUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonNextUsername.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonNextUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.buttonNextUsername.Location = new System.Drawing.Point(135, 161);
			this.buttonNextUsername.Name = "buttonNextUsername";
			this.buttonNextUsername.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.buttonNextUsername.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.buttonNextUsername.OnHoverTextColor = System.Drawing.Color.White;
			this.buttonNextUsername.Size = new System.Drawing.Size(194, 39);
			this.buttonNextUsername.TabIndex = 16;
			this.buttonNextUsername.Text = "Next";
			this.buttonNextUsername.TextColor = System.Drawing.Color.White;
			this.buttonNextUsername.UseVisualStyleBackColor = true;
			this.buttonNextUsername.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// panelBuatPassword
			// 
			this.panelBuatPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.panelBuatPassword.Controls.Add(this.textBoxRePassword);
			this.panelBuatPassword.Controls.Add(this.label3);
			this.panelBuatPassword.Controls.Add(this.panel5);
			this.panelBuatPassword.Controls.Add(this.label2);
			this.panelBuatPassword.Controls.Add(this.btnNextBuatPassword);
			this.panelBuatPassword.Controls.Add(this.textBoxNewPassword);
			this.panelBuatPassword.Controls.Add(this.panel4);
			this.panelBuatPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.panelBuatPassword.Location = new System.Drawing.Point(562, 323);
			this.panelBuatPassword.Name = "panelBuatPassword";
			this.panelBuatPassword.Size = new System.Drawing.Size(448, 253);
			this.panelBuatPassword.TabIndex = 22;
			// 
			// textBoxRePassword
			// 
			this.textBoxRePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.textBoxRePassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxRePassword.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxRePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
			this.textBoxRePassword.Location = new System.Drawing.Point(273, 98);
			this.textBoxRePassword.Multiline = true;
			this.textBoxRePassword.Name = "textBoxRePassword";
			this.textBoxRePassword.PasswordChar = '*';
			this.textBoxRePassword.Size = new System.Drawing.Size(160, 37);
			this.textBoxRePassword.TabIndex = 23;
			this.textBoxRePassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRePassword_KeyDown);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 18F);
			this.label3.ForeColor = System.Drawing.Color.AliceBlue;
			this.label3.Location = new System.Drawing.Point(48, 103);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(219, 30);
			this.label3.TabIndex = 22;
			this.label3.Text = "ulangi password :";
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.panel5.Location = new System.Drawing.Point(273, 138);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(160, 4);
			this.panel5.TabIndex = 19;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 18F);
			this.label2.ForeColor = System.Drawing.Color.AliceBlue;
			this.label2.Location = new System.Drawing.Point(48, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(138, 30);
			this.label2.TabIndex = 21;
			this.label2.Text = "password :";
			// 
			// btnNextBuatPassword
			// 
			this.btnNextBuatPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.btnNextBuatPassword.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.btnNextBuatPassword.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnNextBuatPassword.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.btnNextBuatPassword.FlatAppearance.BorderSize = 0;
			this.btnNextBuatPassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.btnNextBuatPassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.btnNextBuatPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNextBuatPassword.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNextBuatPassword.Location = new System.Drawing.Point(135, 161);
			this.btnNextBuatPassword.Name = "btnNextBuatPassword";
			this.btnNextBuatPassword.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.btnNextBuatPassword.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.btnNextBuatPassword.OnHoverTextColor = System.Drawing.Color.White;
			this.btnNextBuatPassword.Size = new System.Drawing.Size(194, 39);
			this.btnNextBuatPassword.TabIndex = 20;
			this.btnNextBuatPassword.Text = "Next";
			this.btnNextBuatPassword.TextColor = System.Drawing.Color.White;
			this.btnNextBuatPassword.UseVisualStyleBackColor = true;
			this.btnNextBuatPassword.Click += new System.EventHandler(this.btnNextBuatPassword_Click);
			// 
			// textBoxNewPassword
			// 
			this.textBoxNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.textBoxNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxNewPassword.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
			this.textBoxNewPassword.Location = new System.Drawing.Point(273, 45);
			this.textBoxNewPassword.Multiline = true;
			this.textBoxNewPassword.Name = "textBoxNewPassword";
			this.textBoxNewPassword.PasswordChar = '*';
			this.textBoxNewPassword.Size = new System.Drawing.Size(161, 37);
			this.textBoxNewPassword.TabIndex = 19;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.panel4.Location = new System.Drawing.Point(274, 83);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(160, 4);
			this.panel4.TabIndex = 18;
			// 
			// panelPassword
			// 
			this.panelPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.panelPassword.Controls.Add(this.label4);
			this.panelPassword.Controls.Add(this.btnNextPassword);
			this.panelPassword.Controls.Add(this.textBoxPassword);
			this.panelPassword.Controls.Add(this.panel3);
			this.panelPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.panelPassword.Location = new System.Drawing.Point(650, 52);
			this.panelPassword.Name = "panelPassword";
			this.panelPassword.Size = new System.Drawing.Size(360, 195);
			this.panelPassword.TabIndex = 18;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Century Gothic", 18F);
			this.label4.ForeColor = System.Drawing.Color.AliceBlue;
			this.label4.Location = new System.Drawing.Point(79, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(138, 30);
			this.label4.TabIndex = 17;
			this.label4.Text = "password :";
			// 
			// btnNextPassword
			// 
			this.btnNextPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.btnNextPassword.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.btnNextPassword.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnNextPassword.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.btnNextPassword.FlatAppearance.BorderSize = 0;
			this.btnNextPassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.btnNextPassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.btnNextPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNextPassword.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNextPassword.Location = new System.Drawing.Point(135, 161);
			this.btnNextPassword.Name = "btnNextPassword";
			this.btnNextPassword.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.btnNextPassword.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.btnNextPassword.OnHoverTextColor = System.Drawing.Color.White;
			this.btnNextPassword.Size = new System.Drawing.Size(194, 39);
			this.btnNextPassword.TabIndex = 16;
			this.btnNextPassword.Text = "Next";
			this.btnNextPassword.TextColor = System.Drawing.Color.White;
			this.btnNextPassword.UseVisualStyleBackColor = true;
			this.btnNextPassword.Click += new System.EventHandler(this.btnNextPassword_Click);
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
			this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxPassword.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
			this.textBoxPassword.Location = new System.Drawing.Point(232, 72);
			this.textBoxPassword.Multiline = true;
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '*';
			this.textBoxPassword.Size = new System.Drawing.Size(160, 37);
			this.textBoxPassword.TabIndex = 15;
			this.textBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPassword_KeyDown);
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(139)))), ((int)(((byte)(50)))));
			this.panel3.Location = new System.Drawing.Point(232, 110);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(160, 4);
			this.panel3.TabIndex = 14;
			// 
			// dragControl1
			// 
			this.dragControl1.SelectControl = this.panelWindowsButton;
			// 
			// btnClose
			// 
			this.btnClose.Image = global::Kawi_Agung.Properties.Resources.close2;
			this.btnClose.Location = new System.Drawing.Point(437, 12);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(20, 20);
			this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.btnClose.TabIndex = 3;
			this.btnClose.TabStop = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnMinimize
			// 
			this.btnMinimize.ErrorImage = null;
			this.btnMinimize.Image = global::Kawi_Agung.Properties.Resources.minimize;
			this.btnMinimize.Location = new System.Drawing.Point(404, 14);
			this.btnMinimize.Name = "btnMinimize";
			this.btnMinimize.Size = new System.Drawing.Size(20, 15);
			this.btnMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.btnMinimize.TabIndex = 2;
			this.btnMinimize.TabStop = false;
			this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
			// 
			// FormLogin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(39)))), ((int)(((byte)(67)))));
			this.ClientSize = new System.Drawing.Size(471, 300);
			this.Controls.Add(this.panelPassword);
			this.Controls.Add(this.panelBuatPassword);
			this.Controls.Add(this.panelUsername);
			this.Controls.Add(this.panelWindowsButton);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(39)))), ((int)(((byte)(67)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "FormLogin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.panelWindowsButton.ResumeLayout(false);
			this.panelUsername.ResumeLayout(false);
			this.panelUsername.PerformLayout();
			this.panelBuatPassword.ResumeLayout(false);
			this.panelBuatPassword.PerformLayout();
			this.panelPassword.ResumeLayout(false);
			this.panelPassword.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelWindowsButton;
        private System.Windows.Forms.PictureBox btnMinimize;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxUsername;
        private ePOSOne.btnProduct.Button_WOC buttonNextUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelUsername;
		private System.Windows.Forms.Panel panelBuatPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label2;
		private ePOSOne.btnProduct.Button_WOC btnNextBuatPassword;
		private System.Windows.Forms.TextBox textBoxNewPassword;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.TextBox textBoxRePassword;
		private System.Windows.Forms.Panel panelPassword;
		private System.Windows.Forms.Label label4;
		private ePOSOne.btnProduct.Button_WOC btnNextPassword;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Panel panel3;
		private DragControl dragControl1;
	}
}


namespace Kawi_Agung
{
	partial class FormTambahUser
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
			this.textBoxNama = new System.Windows.Forms.TextBox();
			this.textBoxUsername = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonCreate = new System.Windows.Forms.Button();
			this.comboBoxJabatanUser = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(45, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Nama :";
			// 
			// textBoxNama
			// 
			this.textBoxNama.Location = new System.Drawing.Point(126, 26);
			this.textBoxNama.Name = "textBoxNama";
			this.textBoxNama.Size = new System.Drawing.Size(175, 20);
			this.textBoxNama.TabIndex = 1;
			// 
			// textBoxUsername
			// 
			this.textBoxUsername.Location = new System.Drawing.Point(126, 64);
			this.textBoxUsername.Name = "textBoxUsername";
			this.textBoxUsername.Size = new System.Drawing.Size(175, 20);
			this.textBoxUsername.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(45, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Username :";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Location = new System.Drawing.Point(126, 102);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.Size = new System.Drawing.Size(175, 20);
			this.textBoxPassword.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(45, 105);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Password :";
			// 
			// buttonCreate
			// 
			this.buttonCreate.Location = new System.Drawing.Point(148, 185);
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.Size = new System.Drawing.Size(75, 23);
			this.buttonCreate.TabIndex = 6;
			this.buttonCreate.Text = "Create";
			this.buttonCreate.UseVisualStyleBackColor = true;
			this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
			// 
			// comboBoxJabatanUser
			// 
			this.comboBoxJabatanUser.FormattingEnabled = true;
			this.comboBoxJabatanUser.Location = new System.Drawing.Point(126, 139);
			this.comboBoxJabatanUser.Name = "comboBoxJabatanUser";
			this.comboBoxJabatanUser.Size = new System.Drawing.Size(175, 21);
			this.comboBoxJabatanUser.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(45, 142);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Jabatan :";
			// 
			// FormTambahUser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(358, 238);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboBoxJabatanUser);
			this.Controls.Add(this.buttonCreate);
			this.Controls.Add(this.textBoxPassword);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxUsername);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxNama);
			this.Controls.Add(this.label1);
			this.Name = "FormTambahUser";
			this.Text = "FormTambahUser";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxNama;
		private System.Windows.Forms.TextBox textBoxUsername;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonCreate;
		private System.Windows.Forms.ComboBox comboBoxJabatanUser;
		private System.Windows.Forms.Label label4;
	}
}
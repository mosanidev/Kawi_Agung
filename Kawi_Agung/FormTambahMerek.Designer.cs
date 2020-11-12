namespace Kawi_Agung
{
	partial class FormTambahMerek
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelJudul = new System.Windows.Forms.Label();
			this.textBoxTambahMerekBarang = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonTambahMerek = new ePOSOne.btnProduct.Button_WOC();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.panel1.Controls.Add(this.labelJudul);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(455, 59);
			this.panel1.TabIndex = 0;
			// 
			// labelJudul
			// 
			this.labelJudul.AutoSize = true;
			this.labelJudul.Font = new System.Drawing.Font("Century Gothic", 18F);
			this.labelJudul.ForeColor = System.Drawing.Color.AliceBlue;
			this.labelJudul.Location = new System.Drawing.Point(13, 14);
			this.labelJudul.Name = "labelJudul";
			this.labelJudul.Size = new System.Drawing.Size(199, 30);
			this.labelJudul.TabIndex = 2;
			this.labelJudul.Text = "Tambah Merek ";
			// 
			// textBoxTambahMerekBarang
			// 
			this.textBoxTambahMerekBarang.Location = new System.Drawing.Point(189, 122);
			this.textBoxTambahMerekBarang.Name = "textBoxTambahMerekBarang";
			this.textBoxTambahMerekBarang.Size = new System.Drawing.Size(215, 20);
			this.textBoxTambahMerekBarang.TabIndex = 37;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label1.Location = new System.Drawing.Point(44, 119);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(138, 21);
			this.label1.TabIndex = 36;
			this.label1.Text = "Merek Barang :";
			// 
			// buttonTambahMerek
			// 
			this.buttonTambahMerek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonTambahMerek.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonTambahMerek.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonTambahMerek.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonTambahMerek.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonTambahMerek.FlatAppearance.BorderSize = 0;
			this.buttonTambahMerek.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonTambahMerek.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonTambahMerek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTambahMerek.Font = new System.Drawing.Font("Century Gothic", 11F);
			this.buttonTambahMerek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonTambahMerek.Location = new System.Drawing.Point(159, 196);
			this.buttonTambahMerek.Name = "buttonTambahMerek";
			this.buttonTambahMerek.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonTambahMerek.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonTambahMerek.OnHoverTextColor = System.Drawing.Color.AliceBlue;
			this.buttonTambahMerek.Size = new System.Drawing.Size(120, 40);
			this.buttonTambahMerek.TabIndex = 38;
			this.buttonTambahMerek.Text = "Tambah";
			this.buttonTambahMerek.TextColor = System.Drawing.Color.White;
			this.buttonTambahMerek.UseVisualStyleBackColor = false;
			this.buttonTambahMerek.Click += new System.EventHandler(this.buttonTambahMerek_Click);
			// 
			// FormTambahMerek
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 261);
			this.Controls.Add(this.buttonTambahMerek);
			this.Controls.Add(this.textBoxTambahMerekBarang);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTambahMerek";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelJudul;
		private System.Windows.Forms.TextBox textBoxTambahMerekBarang;
		private System.Windows.Forms.Label label1;
		private ePOSOne.btnProduct.Button_WOC buttonTambahMerek;
	}
}
namespace Kawi_Agung
{
	partial class FormTambahJenis
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
			this.buttonTambahJenis = new ePOSOne.btnProduct.Button_WOC();
			this.textBoxTambahJenisBarang = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
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
			this.panel1.TabIndex = 39;
			// 
			// labelJudul
			// 
			this.labelJudul.AutoSize = true;
			this.labelJudul.Font = new System.Drawing.Font("Century Gothic", 18F);
			this.labelJudul.ForeColor = System.Drawing.Color.AliceBlue;
			this.labelJudul.Location = new System.Drawing.Point(13, 14);
			this.labelJudul.Name = "labelJudul";
			this.labelJudul.Size = new System.Drawing.Size(177, 30);
			this.labelJudul.TabIndex = 2;
			this.labelJudul.Text = "Tambah Jenis";
			// 
			// buttonTambahJenis
			// 
			this.buttonTambahJenis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonTambahJenis.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonTambahJenis.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonTambahJenis.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonTambahJenis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonTambahJenis.FlatAppearance.BorderSize = 0;
			this.buttonTambahJenis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonTambahJenis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonTambahJenis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTambahJenis.Font = new System.Drawing.Font("Century Gothic", 11F);
			this.buttonTambahJenis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonTambahJenis.Location = new System.Drawing.Point(159, 196);
			this.buttonTambahJenis.Name = "buttonTambahJenis";
			this.buttonTambahJenis.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonTambahJenis.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonTambahJenis.OnHoverTextColor = System.Drawing.Color.AliceBlue;
			this.buttonTambahJenis.Size = new System.Drawing.Size(120, 40);
			this.buttonTambahJenis.TabIndex = 42;
			this.buttonTambahJenis.Text = "Tambah";
			this.buttonTambahJenis.TextColor = System.Drawing.Color.White;
			this.buttonTambahJenis.UseVisualStyleBackColor = false;
			this.buttonTambahJenis.Click += new System.EventHandler(this.buttonTambahJenis_Click);
			// 
			// textBoxTambahJenisBarang
			// 
			this.textBoxTambahJenisBarang.Location = new System.Drawing.Point(182, 123);
			this.textBoxTambahJenisBarang.Name = "textBoxTambahJenisBarang";
			this.textBoxTambahJenisBarang.Size = new System.Drawing.Size(215, 20);
			this.textBoxTambahJenisBarang.TabIndex = 41;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label1.Location = new System.Drawing.Point(52, 122);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 21);
			this.label1.TabIndex = 40;
			this.label1.Text = "Jenis Barang :";
			// 
			// FormTambahJenis
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 261);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.buttonTambahJenis);
			this.Controls.Add(this.textBoxTambahJenisBarang);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormTambahJenis";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelJudul;
		private ePOSOne.btnProduct.Button_WOC buttonTambahJenis;
		private System.Windows.Forms.TextBox textBoxTambahJenisBarang;
		private System.Windows.Forms.Label label1;
	}
}
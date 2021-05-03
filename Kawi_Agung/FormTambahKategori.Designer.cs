namespace Kawi_Agung
{
	partial class FormTambahKategori
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
			this.buttonTambahKategori = new ePOSOne.btnProduct.Button_WOC();
			this.textBoxTambahKategoriBarang = new System.Windows.Forms.TextBox();
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
			this.labelJudul.Size = new System.Drawing.Size(224, 30);
			this.labelJudul.TabIndex = 2;
			this.labelJudul.Text = "Tambah Kategori ";
			// 
			// buttonTambahKategori
			// 
			this.buttonTambahKategori.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonTambahKategori.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonTambahKategori.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonTambahKategori.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonTambahKategori.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonTambahKategori.FlatAppearance.BorderSize = 0;
			this.buttonTambahKategori.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonTambahKategori.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonTambahKategori.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonTambahKategori.Font = new System.Drawing.Font("Century Gothic", 11F);
			this.buttonTambahKategori.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonTambahKategori.Location = new System.Drawing.Point(159, 196);
			this.buttonTambahKategori.Name = "buttonTambahKategori";
			this.buttonTambahKategori.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonTambahKategori.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonTambahKategori.OnHoverTextColor = System.Drawing.Color.AliceBlue;
			this.buttonTambahKategori.Size = new System.Drawing.Size(120, 40);
			this.buttonTambahKategori.TabIndex = 42;
			this.buttonTambahKategori.Text = "Tambah";
			this.buttonTambahKategori.TextColor = System.Drawing.Color.White;
			this.buttonTambahKategori.UseVisualStyleBackColor = false;
			this.buttonTambahKategori.Click += new System.EventHandler(this.buttonTambahKategori_Click);
			// 
			// textBoxTambahKategoriBarang
			// 
			this.textBoxTambahKategoriBarang.Location = new System.Drawing.Point(196, 122);
			this.textBoxTambahKategoriBarang.Name = "textBoxTambahKategoriBarang";
			this.textBoxTambahKategoriBarang.Size = new System.Drawing.Size(215, 20);
			this.textBoxTambahKategoriBarang.TabIndex = 41;
			this.textBoxTambahKategoriBarang.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTambahKategoriBarang_KeyDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label1.Location = new System.Drawing.Point(39, 119);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 21);
			this.label1.TabIndex = 40;
			this.label1.Text = "Kategori Barang :";
			// 
			// FormTambahKategori
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 261);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.buttonTambahKategori);
			this.Controls.Add(this.textBoxTambahKategoriBarang);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.Name = "FormTambahKategori";
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
		private ePOSOne.btnProduct.Button_WOC buttonTambahKategori;
		private System.Windows.Forms.TextBox textBoxTambahKategoriBarang;
		private System.Windows.Forms.Label label1;
	}
}
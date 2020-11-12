namespace Kawi_Agung
{
	partial class FormUbahKategori
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
			this.textBoxUbahKategoriBarang = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonUbahKategori = new ePOSOne.btnProduct.Button_WOC();
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
			this.panel1.TabIndex = 43;
			// 
			// labelJudul
			// 
			this.labelJudul.AutoSize = true;
			this.labelJudul.Font = new System.Drawing.Font("Century Gothic", 18F);
			this.labelJudul.ForeColor = System.Drawing.Color.AliceBlue;
			this.labelJudul.Location = new System.Drawing.Point(13, 14);
			this.labelJudul.Name = "labelJudul";
			this.labelJudul.Size = new System.Drawing.Size(189, 30);
			this.labelJudul.TabIndex = 2;
			this.labelJudul.Text = "Ubah Kategori ";
			// 
			// textBoxUbahKategoriBarang
			// 
			this.textBoxUbahKategoriBarang.Location = new System.Drawing.Point(196, 122);
			this.textBoxUbahKategoriBarang.Name = "textBoxUbahKategoriBarang";
			this.textBoxUbahKategoriBarang.Size = new System.Drawing.Size(215, 20);
			this.textBoxUbahKategoriBarang.TabIndex = 45;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label1.Location = new System.Drawing.Point(39, 119);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 21);
			this.label1.TabIndex = 44;
			this.label1.Text = "Kategori Barang :";
			// 
			// buttonUbahKategori
			// 
			this.buttonUbahKategori.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUbahKategori.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahKategori.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahKategori.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonUbahKategori.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahKategori.FlatAppearance.BorderSize = 0;
			this.buttonUbahKategori.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahKategori.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahKategori.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonUbahKategori.Font = new System.Drawing.Font("Century Gothic", 11F);
			this.buttonUbahKategori.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahKategori.Location = new System.Drawing.Point(159, 196);
			this.buttonUbahKategori.Name = "buttonUbahKategori";
			this.buttonUbahKategori.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUbahKategori.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUbahKategori.OnHoverTextColor = System.Drawing.Color.AliceBlue;
			this.buttonUbahKategori.Size = new System.Drawing.Size(120, 40);
			this.buttonUbahKategori.TabIndex = 46;
			this.buttonUbahKategori.Text = "Simpan";
			this.buttonUbahKategori.TextColor = System.Drawing.Color.White;
			this.buttonUbahKategori.UseVisualStyleBackColor = false;
			this.buttonUbahKategori.Click += new System.EventHandler(this.buttonUbahKategori_Click);
			// 
			// FormUbahKategori
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 261);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.buttonUbahKategori);
			this.Controls.Add(this.textBoxUbahKategoriBarang);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormUbahKategori";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormUbahKategori_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelJudul;
		private ePOSOne.btnProduct.Button_WOC buttonUbahKategori;
		private System.Windows.Forms.TextBox textBoxUbahKategoriBarang;
		private System.Windows.Forms.Label label1;
	}
}
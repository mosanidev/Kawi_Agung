namespace Kawi_Agung
{
	partial class FormUbahJenis
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
			this.buttonUbahJenis = new ePOSOne.btnProduct.Button_WOC();
			this.textBoxUbahJenisBarang = new System.Windows.Forms.TextBox();
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
			this.panel1.TabIndex = 43;
			// 
			// labelJudul
			// 
			this.labelJudul.AutoSize = true;
			this.labelJudul.Font = new System.Drawing.Font("Century Gothic", 18F);
			this.labelJudul.ForeColor = System.Drawing.Color.AliceBlue;
			this.labelJudul.Location = new System.Drawing.Point(13, 14);
			this.labelJudul.Name = "labelJudul";
			this.labelJudul.Size = new System.Drawing.Size(142, 30);
			this.labelJudul.TabIndex = 2;
			this.labelJudul.Text = "Ubah Jenis";
			// 
			// buttonUbahJenis
			// 
			this.buttonUbahJenis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUbahJenis.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahJenis.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahJenis.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonUbahJenis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahJenis.FlatAppearance.BorderSize = 0;
			this.buttonUbahJenis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahJenis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahJenis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonUbahJenis.Font = new System.Drawing.Font("Century Gothic", 11F);
			this.buttonUbahJenis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahJenis.Location = new System.Drawing.Point(159, 196);
			this.buttonUbahJenis.Name = "buttonUbahJenis";
			this.buttonUbahJenis.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUbahJenis.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUbahJenis.OnHoverTextColor = System.Drawing.Color.AliceBlue;
			this.buttonUbahJenis.Size = new System.Drawing.Size(120, 40);
			this.buttonUbahJenis.TabIndex = 46;
			this.buttonUbahJenis.Text = "Simpan";
			this.buttonUbahJenis.TextColor = System.Drawing.Color.White;
			this.buttonUbahJenis.UseVisualStyleBackColor = false;
			this.buttonUbahJenis.Click += new System.EventHandler(this.buttonUbahJenis_Click);
			// 
			// textBoxUbahJenisBarang
			// 
			this.textBoxUbahJenisBarang.Location = new System.Drawing.Point(182, 122);
			this.textBoxUbahJenisBarang.Name = "textBoxUbahJenisBarang";
			this.textBoxUbahJenisBarang.Size = new System.Drawing.Size(215, 20);
			this.textBoxUbahJenisBarang.TabIndex = 45;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label1.Location = new System.Drawing.Point(52, 119);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 21);
			this.label1.TabIndex = 44;
			this.label1.Text = "Jenis Barang :";
			// 
			// FormUbahJenis
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 261);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.buttonUbahJenis);
			this.Controls.Add(this.textBoxUbahJenisBarang);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormUbahJenis";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormUbahJenis_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelJudul;
		private ePOSOne.btnProduct.Button_WOC buttonUbahJenis;
		private System.Windows.Forms.TextBox textBoxUbahJenisBarang;
		private System.Windows.Forms.Label label1;
	}
}
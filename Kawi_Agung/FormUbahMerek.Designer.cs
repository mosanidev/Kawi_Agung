namespace Kawi_Agung
{
	partial class FormUbahMerek
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
			this.buttonUbahMerek = new ePOSOne.btnProduct.Button_WOC();
			this.textBoxUbahMerekBarang = new System.Windows.Forms.TextBox();
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
			this.labelJudul.Size = new System.Drawing.Size(164, 30);
			this.labelJudul.TabIndex = 2;
			this.labelJudul.Text = "Ubah Merek ";
			// 
			// buttonUbahMerek
			// 
			this.buttonUbahMerek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUbahMerek.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahMerek.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahMerek.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonUbahMerek.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahMerek.FlatAppearance.BorderSize = 0;
			this.buttonUbahMerek.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahMerek.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahMerek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonUbahMerek.Font = new System.Drawing.Font("Century Gothic", 11F);
			this.buttonUbahMerek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahMerek.Location = new System.Drawing.Point(159, 208);
			this.buttonUbahMerek.Name = "buttonUbahMerek";
			this.buttonUbahMerek.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUbahMerek.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUbahMerek.OnHoverTextColor = System.Drawing.Color.AliceBlue;
			this.buttonUbahMerek.Size = new System.Drawing.Size(120, 40);
			this.buttonUbahMerek.TabIndex = 42;
			this.buttonUbahMerek.Text = "Simpan";
			this.buttonUbahMerek.TextColor = System.Drawing.Color.White;
			this.buttonUbahMerek.UseVisualStyleBackColor = false;
			this.buttonUbahMerek.Click += new System.EventHandler(this.buttonUbahMerek_Click);
			// 
			// textBoxUbahMerekBarang
			// 
			this.textBoxUbahMerekBarang.Location = new System.Drawing.Point(189, 134);
			this.textBoxUbahMerekBarang.Name = "textBoxUbahMerekBarang";
			this.textBoxUbahMerekBarang.Size = new System.Drawing.Size(215, 20);
			this.textBoxUbahMerekBarang.TabIndex = 41;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label1.Location = new System.Drawing.Point(44, 131);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(138, 21);
			this.label1.TabIndex = 40;
			this.label1.Text = "Merek Barang :";
			// 
			// FormUbahMerek
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 261);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.buttonUbahMerek);
			this.Controls.Add(this.textBoxUbahMerekBarang);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormUbahMerek";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormUbahMerek_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelJudul;
		private ePOSOne.btnProduct.Button_WOC buttonUbahMerek;
		private System.Windows.Forms.TextBox textBoxUbahMerekBarang;
		private System.Windows.Forms.Label label1;
	}
}
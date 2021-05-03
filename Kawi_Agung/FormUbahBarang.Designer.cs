namespace Kawi_Agung
{
	partial class FormUbahBarang
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
			this.comboBoxUbahSatuanBarang = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.labelHasilHargaJual = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.labelTambahBarangPersen = new System.Windows.Forms.Label();
			this.numericUpDownUbahBarangDiskon = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownUbahBarangHargaJual = new System.Windows.Forms.NumericUpDown();
			this.comboBoxUbahBarangMerekBarang = new System.Windows.Forms.ComboBox();
			this.comboBoxUbahBarangKategoriBarang = new System.Windows.Forms.ComboBox();
			this.comboBoxUbahBarangJenisBarang = new System.Windows.Forms.ComboBox();
			this.textBoxUbahBarangKodeBarang = new System.Windows.Forms.TextBox();
			this.buttonUbahBarang = new ePOSOne.btnProduct.Button_WOC();
			this.buttonUnggahFotoBarang = new ePOSOne.btnProduct.Button_WOC();
			this.pictureBoxUbahBarangGambarBarang = new System.Windows.Forms.PictureBox();
			this.labelTambahBarangRP = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.labelJudul = new System.Windows.Forms.Label();
			this.textBoxUbahBarangNamaBarang = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonHapusFoto = new ePOSOne.btnProduct.Button_WOC();
			this.label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownUbahBarangDiskon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownUbahBarangHargaJual)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxUbahBarangGambarBarang)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxUbahSatuanBarang
			// 
			this.comboBoxUbahSatuanBarang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxUbahSatuanBarang.FormattingEnabled = true;
			this.comboBoxUbahSatuanBarang.Location = new System.Drawing.Point(187, 233);
			this.comboBoxUbahSatuanBarang.Name = "comboBoxUbahSatuanBarang";
			this.comboBoxUbahSatuanBarang.Size = new System.Drawing.Size(215, 21);
			this.comboBoxUbahSatuanBarang.TabIndex = 107;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label7.Location = new System.Drawing.Point(28, 230);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(79, 21);
			this.label7.TabIndex = 106;
			this.label7.Text = "Satuan :";
			// 
			// labelHasilHargaJual
			// 
			this.labelHasilHargaJual.AutoSize = true;
			this.labelHasilHargaJual.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.labelHasilHargaJual.Location = new System.Drawing.Point(183, 347);
			this.labelHasilHargaJual.Name = "labelHasilHargaJual";
			this.labelHasilHargaJual.Size = new System.Drawing.Size(41, 21);
			this.labelHasilHargaJual.TabIndex = 103;
			this.labelHasilHargaJual.Text = "Rp0";
			this.labelHasilHargaJual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label5.Location = new System.Drawing.Point(28, 288);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 21);
			this.label5.TabIndex = 102;
			this.label5.Text = "Diskon :";
			// 
			// labelTambahBarangPersen
			// 
			this.labelTambahBarangPersen.AutoSize = true;
			this.labelTambahBarangPersen.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
			this.labelTambahBarangPersen.Location = new System.Drawing.Point(231, 290);
			this.labelTambahBarangPersen.Name = "labelTambahBarangPersen";
			this.labelTambahBarangPersen.Size = new System.Drawing.Size(21, 18);
			this.labelTambahBarangPersen.TabIndex = 101;
			this.labelTambahBarangPersen.Text = "%";
			// 
			// numericUpDownUbahBarangDiskon
			// 
			this.numericUpDownUbahBarangDiskon.Location = new System.Drawing.Point(187, 290);
			this.numericUpDownUbahBarangDiskon.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.numericUpDownUbahBarangDiskon.Name = "numericUpDownUbahBarangDiskon";
			this.numericUpDownUbahBarangDiskon.Size = new System.Drawing.Size(38, 20);
			this.numericUpDownUbahBarangDiskon.TabIndex = 100;
			this.numericUpDownUbahBarangDiskon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDownUbahBarangDiskon_KeyDown);
			// 
			// numericUpDownUbahBarangHargaJual
			// 
			this.numericUpDownUbahBarangHargaJual.Location = new System.Drawing.Point(221, 263);
			this.numericUpDownUbahBarangHargaJual.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownUbahBarangHargaJual.Name = "numericUpDownUbahBarangHargaJual";
			this.numericUpDownUbahBarangHargaJual.Size = new System.Drawing.Size(181, 20);
			this.numericUpDownUbahBarangHargaJual.TabIndex = 99;
			// 
			// comboBoxUbahBarangMerekBarang
			// 
			this.comboBoxUbahBarangMerekBarang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxUbahBarangMerekBarang.FormattingEnabled = true;
			this.comboBoxUbahBarangMerekBarang.Location = new System.Drawing.Point(187, 204);
			this.comboBoxUbahBarangMerekBarang.Name = "comboBoxUbahBarangMerekBarang";
			this.comboBoxUbahBarangMerekBarang.Size = new System.Drawing.Size(215, 21);
			this.comboBoxUbahBarangMerekBarang.TabIndex = 98;
			// 
			// comboBoxUbahBarangKategoriBarang
			// 
			this.comboBoxUbahBarangKategoriBarang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxUbahBarangKategoriBarang.FormattingEnabled = true;
			this.comboBoxUbahBarangKategoriBarang.Location = new System.Drawing.Point(187, 176);
			this.comboBoxUbahBarangKategoriBarang.Name = "comboBoxUbahBarangKategoriBarang";
			this.comboBoxUbahBarangKategoriBarang.Size = new System.Drawing.Size(215, 21);
			this.comboBoxUbahBarangKategoriBarang.TabIndex = 97;
			// 
			// comboBoxUbahBarangJenisBarang
			// 
			this.comboBoxUbahBarangJenisBarang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxUbahBarangJenisBarang.FormattingEnabled = true;
			this.comboBoxUbahBarangJenisBarang.Location = new System.Drawing.Point(187, 148);
			this.comboBoxUbahBarangJenisBarang.Name = "comboBoxUbahBarangJenisBarang";
			this.comboBoxUbahBarangJenisBarang.Size = new System.Drawing.Size(215, 21);
			this.comboBoxUbahBarangJenisBarang.TabIndex = 96;
			// 
			// textBoxUbahBarangKodeBarang
			// 
			this.textBoxUbahBarangKodeBarang.Enabled = false;
			this.textBoxUbahBarangKodeBarang.Location = new System.Drawing.Point(187, 91);
			this.textBoxUbahBarangKodeBarang.Name = "textBoxUbahBarangKodeBarang";
			this.textBoxUbahBarangKodeBarang.Size = new System.Drawing.Size(215, 20);
			this.textBoxUbahBarangKodeBarang.TabIndex = 94;
			// 
			// buttonUbahBarang
			// 
			this.buttonUbahBarang.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahBarang.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahBarang.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonUbahBarang.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahBarang.FlatAppearance.BorderSize = 0;
			this.buttonUbahBarang.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahBarang.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUbahBarang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonUbahBarang.Font = new System.Drawing.Font("Century Gothic", 11F);
			this.buttonUbahBarang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUbahBarang.Location = new System.Drawing.Point(221, 384);
			this.buttonUbahBarang.Name = "buttonUbahBarang";
			this.buttonUbahBarang.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUbahBarang.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUbahBarang.OnHoverTextColor = System.Drawing.Color.AliceBlue;
			this.buttonUbahBarang.Size = new System.Drawing.Size(232, 40);
			this.buttonUbahBarang.TabIndex = 93;
			this.buttonUbahBarang.Text = "Simpan";
			this.buttonUbahBarang.TextColor = System.Drawing.Color.White;
			this.buttonUbahBarang.UseVisualStyleBackColor = false;
			this.buttonUbahBarang.Click += new System.EventHandler(this.buttonUbahBarang_Click);
			// 
			// buttonUnggahFotoBarang
			// 
			this.buttonUnggahFotoBarang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUnggahFotoBarang.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUnggahFotoBarang.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUnggahFotoBarang.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonUnggahFotoBarang.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUnggahFotoBarang.FlatAppearance.BorderSize = 0;
			this.buttonUnggahFotoBarang.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUnggahFotoBarang.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonUnggahFotoBarang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonUnggahFotoBarang.Font = new System.Drawing.Font("Century Gothic", 11F);
			this.buttonUnggahFotoBarang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonUnggahFotoBarang.Location = new System.Drawing.Point(491, 260);
			this.buttonUnggahFotoBarang.Name = "buttonUnggahFotoBarang";
			this.buttonUnggahFotoBarang.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUnggahFotoBarang.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonUnggahFotoBarang.OnHoverTextColor = System.Drawing.Color.AliceBlue;
			this.buttonUnggahFotoBarang.Size = new System.Drawing.Size(146, 34);
			this.buttonUnggahFotoBarang.TabIndex = 92;
			this.buttonUnggahFotoBarang.Text = "Unggah";
			this.buttonUnggahFotoBarang.TextColor = System.Drawing.Color.White;
			this.buttonUnggahFotoBarang.UseVisualStyleBackColor = false;
			this.buttonUnggahFotoBarang.Click += new System.EventHandler(this.buttonUnggahFotoBarang_Click);
			// 
			// pictureBoxUbahBarangGambarBarang
			// 
			this.pictureBoxUbahBarangGambarBarang.Image = global::Kawi_Agung.Properties.Resources.box;
			this.pictureBoxUbahBarangGambarBarang.Location = new System.Drawing.Point(461, 80);
			this.pictureBoxUbahBarangGambarBarang.Name = "pictureBoxUbahBarangGambarBarang";
			this.pictureBoxUbahBarangGambarBarang.Size = new System.Drawing.Size(207, 174);
			this.pictureBoxUbahBarangGambarBarang.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxUbahBarangGambarBarang.TabIndex = 91;
			this.pictureBoxUbahBarangGambarBarang.TabStop = false;
			// 
			// labelTambahBarangRP
			// 
			this.labelTambahBarangRP.AutoSize = true;
			this.labelTambahBarangRP.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.labelTambahBarangRP.Location = new System.Drawing.Point(183, 259);
			this.labelTambahBarangRP.Name = "labelTambahBarangRP";
			this.labelTambahBarangRP.Size = new System.Drawing.Size(32, 21);
			this.labelTambahBarangRP.TabIndex = 90;
			this.labelTambahBarangRP.Text = "Rp";
			this.labelTambahBarangRP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label4.Location = new System.Drawing.Point(28, 259);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(110, 21);
			this.label4.TabIndex = 89;
			this.label4.Text = "Harga Jual :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label3.Location = new System.Drawing.Point(28, 117);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(137, 21);
			this.label3.TabIndex = 88;
			this.label3.Text = "Nama Barang :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label2.Location = new System.Drawing.Point(28, 173);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 21);
			this.label2.TabIndex = 87;
			this.label2.Text = "Kategori :";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label16.Location = new System.Drawing.Point(28, 201);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73, 21);
			this.label16.TabIndex = 85;
			this.label16.Text = "Merek :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label1.Location = new System.Drawing.Point(28, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(129, 21);
			this.label1.TabIndex = 86;
			this.label1.Text = "Kode Barang :";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Century Gothic", 12.5F);
			this.label17.Location = new System.Drawing.Point(28, 145);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(59, 21);
			this.label17.TabIndex = 84;
			this.label17.Text = "Jenis :";
			// 
			// labelJudul
			// 
			this.labelJudul.AutoSize = true;
			this.labelJudul.Font = new System.Drawing.Font("Century Gothic", 22F);
			this.labelJudul.ForeColor = System.Drawing.Color.AliceBlue;
			this.labelJudul.Location = new System.Drawing.Point(16, 11);
			this.labelJudul.Name = "labelJudul";
			this.labelJudul.Size = new System.Drawing.Size(207, 37);
			this.labelJudul.TabIndex = 1;
			this.labelJudul.Text = "Ubah Barang";
			// 
			// textBoxUbahBarangNamaBarang
			// 
			this.textBoxUbahBarangNamaBarang.Location = new System.Drawing.Point(187, 120);
			this.textBoxUbahBarangNamaBarang.Name = "textBoxUbahBarangNamaBarang";
			this.textBoxUbahBarangNamaBarang.Size = new System.Drawing.Size(215, 20);
			this.textBoxUbahBarangNamaBarang.TabIndex = 95;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.panel1.Controls.Add(this.labelJudul);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(694, 59);
			this.panel1.TabIndex = 83;
			// 
			// buttonHapusFoto
			// 
			this.buttonHapusFoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonHapusFoto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonHapusFoto.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(39)))), ((int)(((byte)(69)))));
			this.buttonHapusFoto.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonHapusFoto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonHapusFoto.FlatAppearance.BorderSize = 0;
			this.buttonHapusFoto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonHapusFoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.buttonHapusFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonHapusFoto.Font = new System.Drawing.Font("Century Gothic", 11F);
			this.buttonHapusFoto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
			this.buttonHapusFoto.Location = new System.Drawing.Point(491, 300);
			this.buttonHapusFoto.Name = "buttonHapusFoto";
			this.buttonHapusFoto.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonHapusFoto.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(159)))));
			this.buttonHapusFoto.OnHoverTextColor = System.Drawing.Color.AliceBlue;
			this.buttonHapusFoto.Size = new System.Drawing.Size(146, 34);
			this.buttonHapusFoto.TabIndex = 108;
			this.buttonHapusFoto.Text = "Hapus Foto";
			this.buttonHapusFoto.TextColor = System.Drawing.Color.White;
			this.buttonHapusFoto.UseVisualStyleBackColor = false;
			this.buttonHapusFoto.Click += new System.EventHandler(this.buttonHapusFoto_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Tai Le", 8F);
			this.label6.Location = new System.Drawing.Point(187, 316);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(193, 14);
			this.label6.TabIndex = 111;
			this.label6.Text = "Tekan ENTER setelah mengisi diskon";
			// 
			// FormUbahBarang
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(694, 438);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.buttonHapusFoto);
			this.Controls.Add(this.comboBoxUbahSatuanBarang);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.labelHasilHargaJual);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelTambahBarangPersen);
			this.Controls.Add(this.numericUpDownUbahBarangDiskon);
			this.Controls.Add(this.numericUpDownUbahBarangHargaJual);
			this.Controls.Add(this.comboBoxUbahBarangMerekBarang);
			this.Controls.Add(this.comboBoxUbahBarangKategoriBarang);
			this.Controls.Add(this.comboBoxUbahBarangJenisBarang);
			this.Controls.Add(this.textBoxUbahBarangKodeBarang);
			this.Controls.Add(this.buttonUbahBarang);
			this.Controls.Add(this.buttonUnggahFotoBarang);
			this.Controls.Add(this.pictureBoxUbahBarangGambarBarang);
			this.Controls.Add(this.labelTambahBarangRP);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.textBoxUbahBarangNamaBarang);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "FormUbahBarang";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormUbahBarang_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownUbahBarangDiskon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownUbahBarangHargaJual)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxUbahBarangGambarBarang)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxUbahSatuanBarang;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label labelHasilHargaJual;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labelTambahBarangPersen;
		private System.Windows.Forms.NumericUpDown numericUpDownUbahBarangDiskon;
		private System.Windows.Forms.NumericUpDown numericUpDownUbahBarangHargaJual;
		private System.Windows.Forms.ComboBox comboBoxUbahBarangMerekBarang;
		private System.Windows.Forms.ComboBox comboBoxUbahBarangKategoriBarang;
		private System.Windows.Forms.ComboBox comboBoxUbahBarangJenisBarang;
		private System.Windows.Forms.TextBox textBoxUbahBarangKodeBarang;
		private ePOSOne.btnProduct.Button_WOC buttonUbahBarang;
		private ePOSOne.btnProduct.Button_WOC buttonUnggahFotoBarang;
		private System.Windows.Forms.PictureBox pictureBoxUbahBarangGambarBarang;
		private System.Windows.Forms.Label labelTambahBarangRP;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label labelJudul;
		private System.Windows.Forms.TextBox textBoxUbahBarangNamaBarang;
		private System.Windows.Forms.Panel panel1;
		private ePOSOne.btnProduct.Button_WOC buttonHapusFoto;
		private System.Windows.Forms.Label label6;
	}
}
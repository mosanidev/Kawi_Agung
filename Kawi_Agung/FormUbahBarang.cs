﻿using Kawi_Agung.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kawi_Agung
{
	public partial class FormUbahBarang : Form
	{
		public FormUbahBarang()
		{
			InitializeComponent();
		}

		private List<JenisBarang> listJenis = new List<JenisBarang>();
		private List<KategoriBarang> listKategori = new List<KategoriBarang>();
		private List<MerekBarang> listMerek = new List<MerekBarang>();

		private string pathFoto = "";
		private int hasilHargaJual = 0;

		private FormMaster mainForm = null;

		public FormUbahBarang(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}
		
		private void FormUbahBarang_Load(object sender, EventArgs e)
		{
			string hasilBacaJenis = JenisBarang.BacaData("", "", listJenis);
			string hasilBacaKategori = KategoriBarang.BacaData("", "", listKategori);
			string hasilBacaMerek = MerekBarang.BacaData("", "", listMerek);

			textBoxUbahBarangKodeBarang.Text = FormMaster.listSelectedBarang[0].KodeBarang;
			textBoxUbahBarangNamaBarang.Text = FormMaster.listSelectedBarang[0].Nama;
			numericUpDownUbahBarangHargaJual.Value = CountPriceBeforeDiscount(FormMaster.listSelectedBarang[0].HargaJual, FormMaster.listSelectedBarang[0].DiskonPersenJual);
			numericUpDownUbahBarangDiskon.Value = FormMaster.listSelectedBarang[0].DiskonPersenJual;

			if (FormMaster.listSelectedBarang[0].Foto != "")
			{
				string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\barang";
				string folderName = Path.Combine(projectPath, FormMaster.listSelectedBarang[0].KodeBarang);
				pictureBoxUbahBarangGambarBarang.ImageLocation = folderName + "\\" + "foto" + FormMaster.listSelectedBarang[0].Foto;
			}
			else
			{
				pictureBoxUbahBarangGambarBarang.Image = Resources.profile_picture;
			}

			labelHasilHargaJual.Text = ConvertToRupiah(FormMaster.listSelectedBarang[0].HargaJual);

			comboBoxUbahSatuanBarang.Items.Add("PC");
			comboBoxUbahSatuanBarang.Items.Add("SET");
			comboBoxUbahSatuanBarang.SelectedItem = FormMaster.listSelectedBarang[0].Satuan;

			if (hasilBacaJenis == "1")
			{
				foreach (var item in listJenis)
				{
					comboBoxUbahBarangJenisBarang.Items.Add(item.IdJenisBarang + " - " + item.Nama);
					
				}
				comboBoxUbahBarangJenisBarang.SelectedItem = FormMaster.listSelectedBarang[0].Jenis.IdJenisBarang + " - " + FormMaster.listSelectedBarang[0].Jenis.Nama;
			}

			if (hasilBacaKategori == "1")
			{
				foreach (var item in listKategori)
				{
					comboBoxUbahBarangKategoriBarang.Items.Add(item.IdKategoriBarang + " - " + item.Nama);
				}
				comboBoxUbahBarangKategoriBarang.SelectedItem = FormMaster.listSelectedBarang[0].Kategori.IdKategoriBarang + " - " + FormMaster.listSelectedBarang[0].Kategori.Nama;
			}

			if (hasilBacaMerek == "1")
			{
				foreach (var item in listMerek)
				{
					comboBoxUbahBarangMerekBarang.Items.Add(item.IdMerekBarang + " - " + item.Nama);
				}
				comboBoxUbahBarangMerekBarang.SelectedItem = FormMaster.listSelectedBarang[0].Merek.IdMerekBarang + " - " + FormMaster.listSelectedBarang[0].Merek.Nama;
			}
		}

		string ConvertToRupiah(int number)
		{
			CultureInfo culture = new CultureInfo("id-ID");

			return string.Format(culture, "{0:c0}", number);
		}

		int CountPriceBeforeDiscount(int salePrice, int discount)
		{
			double discountDecimal = (double)discount / 100;

			double discount_x = 1 - discountDecimal;

			return Convert.ToInt32((double)salePrice / discount_x);

		}

		private void buttonUnggahFotoBarang_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Pilih Foto Barang";
			openFileDialog.InitialDirectory =
				Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			openFileDialog.Filter = "Images Files (*.png; *.jpeg; *.jpg)|*.png;*jpeg;*jpg";
			openFileDialog.Multiselect = false;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				// limit jadi 16 Mib
				try
				{
					pathFoto = openFileDialog.FileName;
					pictureBoxUbahBarangGambarBarang.ImageLocation = openFileDialog.FileName;
					pictureBoxUbahBarangGambarBarang.Tag = "Unggahan";
				}
				catch (IOException ex)
				{
					MessageBox.Show(ex.Message.ToString());
				}
			}

			openFileDialog.Dispose();
		}

		private void buttonUbahBarang_Click(object sender, EventArgs e)
		{
			if (textBoxUbahBarangKodeBarang.Text == "" || textBoxUbahBarangNamaBarang.Text == "" || comboBoxUbahBarangJenisBarang.Text == "" || comboBoxUbahBarangKategoriBarang.Text == "" || comboBoxUbahBarangMerekBarang.Text == "" || comboBoxUbahSatuanBarang.Text == "")
			{
				MessageBox.Show("Data harus diisi semua terlebih dahulu");
			}
			else
			{
				JenisBarang jenis = new JenisBarang();
				jenis.IdJenisBarang = int.Parse(comboBoxUbahBarangJenisBarang.Text.Split('-')[0]);
				jenis.Nama = comboBoxUbahBarangJenisBarang.Text.Split('-')[1];
				
				KategoriBarang kategori = new KategoriBarang();
				kategori.IdKategoriBarang = int.Parse(comboBoxUbahBarangKategoriBarang.Text.Split('-')[0]);
				kategori.Nama = comboBoxUbahBarangKategoriBarang.Text.Split('-')[1];

				MerekBarang merek = new MerekBarang();
				merek.IdMerekBarang = int.Parse(comboBoxUbahBarangMerekBarang.Text.Split('-')[0]);
				merek.Nama = comboBoxUbahBarangMerekBarang.Text.Split('-')[1];

				Barang barang = new Barang();
				barang.IdBarang = FormMaster.listSelectedBarang[0].IdBarang;
				barang.KodeBarang = textBoxUbahBarangKodeBarang.Text;
				barang.Nama = textBoxUbahBarangNamaBarang.Text;
				barang.Jenis = jenis;
				barang.Kategori = kategori;
				barang.Merek = merek;
				barang.HargaJual = hitungDiskon(Convert.ToInt32(numericUpDownUbahBarangHargaJual.Value), Convert.ToInt32(numericUpDownUbahBarangDiskon.Value)); 
				barang.DiskonPersenJual = Convert.ToInt32(numericUpDownUbahBarangDiskon.Value);
				barang.Satuan = comboBoxUbahSatuanBarang.Text;

				if (pathFoto != "")
				{
					barang.Foto = Path.GetExtension(pathFoto);
				}

				hasilHargaJual = hitungDiskon(Convert.ToInt32(numericUpDownUbahBarangHargaJual.Value), Convert.ToInt32(numericUpDownUbahBarangDiskon.Value));

				List<Barang> listBarang = new List<Barang>();

				string hasilBaca = Barang.BacaDataBarang("exclude", FormMaster.listSelectedBarang[0].KodeBarang, listBarang);

				string hasilUbah = "";

				if (hasilBaca == "1")
				{
					if (pictureBoxUbahBarangGambarBarang.Tag == "Default")
					{
						hasilUbah = Barang.UbahData(barang, listBarang, "Hapus");
					} 
					else if (pictureBoxUbahBarangGambarBarang.Tag == "Unggahan")
					{
						string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\barang";
						string folderName = Path.Combine(projectPath, FormMaster.listSelectedBarang[0].KodeBarang);
						Directory.CreateDirectory(folderName);
						Array.ForEach(Directory.GetFiles(@folderName + "\\"), File.Delete);
						File.Copy(pathFoto, folderName + "\\" + "foto" + barang.Foto);
						hasilUbah = Barang.UbahData(barang, listBarang, "Ada");
					} 
					else if (pictureBoxUbahBarangGambarBarang.Tag == null)
					{
						hasilUbah = Barang.UbahData(barang, listBarang, "Tidak Ada");
					}
				}

				if (hasilUbah == "1")
				{
					MessageBox.Show("Data berhasil disimpan");

					this.mainForm.textBoxSearchBarang.Clear();
					this.mainForm.PopulateBarangTable("", "");
					this.Close();
				}
				else
				{
					MessageBox.Show(hasilUbah);
				}
			}
		}

		int hitungDiskon(int harga, int diskon)
		{
			double diskonDesimal = (double)diskon / 100.0;
			return Convert.ToInt32((double)harga - ((double)harga * diskonDesimal));
		}

		private void numericUpDownUbahBarangDiskon_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				int hargaJual = Convert.ToInt32(numericUpDownUbahBarangHargaJual.Value);
				int diskon = Convert.ToInt32(numericUpDownUbahBarangDiskon.Value);
				hasilHargaJual = hitungDiskon(hargaJual, diskon);
				labelHasilHargaJual.Text = ConvertToRupiah(hasilHargaJual);
			}
		}

		private void buttonHapusFoto_Click(object sender, EventArgs e)
		{
			pictureBoxUbahBarangGambarBarang.Image = Resources.box;
			pictureBoxUbahBarangGambarBarang.Tag = "Default";
			pathFoto = "";
		}
	}
}

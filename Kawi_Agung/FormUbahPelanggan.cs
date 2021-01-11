using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kawi_Agung
{
	public partial class FormUbahPelanggan : Form
	{
		public FormUbahPelanggan()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormUbahPelanggan(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void FormUbahPelanggan_Load(object sender, EventArgs e)
		{
			textBoxUbahNamaPelanggan.Text = FormMaster.listSelectedPelanggan[0].Nama;
			richTextBoxUbahAlamatPelanggan.Text = FormMaster.listSelectedPelanggan[0].Alamat;
			textBoxUbahNoTelpPelanggan.Text = FormMaster.listSelectedPelanggan[0].NoTelp;

			textBoxUbahNamaPelanggan.SelectionStart = textBoxUbahNamaPelanggan.Text.Length;
			textBoxUbahNamaPelanggan.SelectionLength = 0;
		}

		private void buttonUbahPelanggan_Click(object sender, EventArgs e)
		{
			int IdPelanggan = FormMaster.listSelectedPelanggan[0].IdPelanggan;

			Pelanggan pelanggan = new Pelanggan();
			pelanggan.IdPelanggan = IdPelanggan;
			pelanggan.Nama = textBoxUbahNamaPelanggan.Text;
			pelanggan.Alamat = richTextBoxUbahAlamatPelanggan.Text;
			pelanggan.NoTelp = textBoxUbahNoTelpPelanggan.Text;

			List<Pelanggan> listPelanggan = new List<Pelanggan>();
			string hasilBaca = Pelanggan.BacaData("exclude", FormMaster.listSelectedPelanggan[0].Nama, listPelanggan);
			string hasilUbah = "";

			if (hasilBaca == "1")
			{
				hasilUbah = Pelanggan.UbahData(pelanggan, listPelanggan);
			}

			if (textBoxUbahNamaPelanggan.Text == "" || textBoxUbahNoTelpPelanggan.Text == "" || richTextBoxUbahAlamatPelanggan.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else
			{
				if (hasilUbah == "1")
				{
					MessageBox.Show("Proses ubah berhasil", "Info");

					this.mainForm.textBoxSearchNamaPelanggan.Clear();
					this.mainForm.FormMaster_Load(buttonUbahPelanggan, e);
					this.Close();
				}
				else if (hasilUbah == "Nama pelanggan sudah ada")
				{
					DialogResult dialogResult = MessageBox.Show("Nama pelanggan sudah ada. Apakah Anda ingin menyimpan data dengan nama pelanggan tersebut?", "", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes)
					{
						List<Pelanggan> listKosong = new List<Pelanggan>(); // kirim list kosong sebagai parameter method ubah data, agar bisa disimpan ke database
						string hasilTambah_ = Pelanggan.UbahData(pelanggan, listKosong);

						MessageBox.Show("Proses ubah berhasil");

						this.mainForm.textBoxSearchNamaPelanggan.Clear();
						this.mainForm.FormMaster_Load(buttonUbahPelanggan, e);
						this.Close();
					}
				}
			}
		}
	}
}

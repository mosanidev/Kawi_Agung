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
	public partial class FormTambahPelanggan : Form
	{
		public FormTambahPelanggan()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormTambahPelanggan(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void buttonTambahPelanggan_Click(object sender, EventArgs e)
		{
			if (textBoxTambahNamaPelanggan.Text == "" || textBoxTambahNoTelpPelanggan.Text == "" || richTextBoxTambahAlamatPelanggan.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu", "Info");
			}
			else
			{
				Pelanggan pelanggan = new Pelanggan();
				pelanggan.Nama = textBoxTambahNamaPelanggan.Text.Trim();
				pelanggan.Alamat = richTextBoxTambahAlamatPelanggan.Text.Trim();
				pelanggan.NoTelp = textBoxTambahNoTelpPelanggan.Text.Trim();

				string hasilTambah = Pelanggan.TambahData(pelanggan, this.mainForm.listPelanggan);

				if (hasilTambah == "1")
				{
					MessageBox.Show("Proses tambah berhasil", "Info");

					this.mainForm.textBoxSearchNamaPelanggan.Clear();
					this.mainForm.FormMaster_Load(buttonTambahPelanggan, e);
					this.Close();
				}
				else if (hasilTambah == "Nama pelanggan sudah ada") // apabila ada nama pelanggan yang sama di database
				{
					DialogResult dialogResult = MessageBox.Show("Nama pelanggan sudah ada. Apakah Anda ingin menyimpan data dengan nama pelanggan tersebut?", "Tambah Pelanggan", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes)
					{
						List<Pelanggan> listKosong = new List<Pelanggan>(); // kirim list kosong sebagai parameter method tambah data, agar bisa ditambahkan ke database
						string hasilTambah_ = Pelanggan.TambahData(pelanggan, listKosong);

						MessageBox.Show("Proses tambah berhasil", "Info");

						this.mainForm.textBoxSearchNamaPelanggan.Clear();
						this.mainForm.FormMaster_Load(buttonTambahPelanggan, e);
						this.Close();
					}
				}
			}
		}
	}
}

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
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else
			{
				Pelanggan pelanggan = new Pelanggan();
				pelanggan.Nama = textBoxTambahNamaPelanggan.Text.Trim();
				pelanggan.Alamat = richTextBoxTambahAlamatPelanggan.Text.Trim();
				pelanggan.NoTelp = textBoxTambahNoTelpPelanggan.Text.Trim();

				string hasilTambah = Pelanggan.TambahData(pelanggan);

				if (hasilTambah == "1")
				{
					MessageBox.Show("Proses tambah berhasil");

					this.mainForm.textBoxSearchNamaPelanggan.Clear();
					this.mainForm.PopulatePelangganTable("", "");
					this.Close();
				}
				else
				{
					MessageBox.Show(hasilTambah);
				}
			}
		}

		private void richTextBoxTambahAlamatPelanggan_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonTambahPelanggan_Click(sender, e);
			}
		}
	}
}

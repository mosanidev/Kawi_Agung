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
				Pelanggan pelanggan = new Pelanggan(textBoxTambahNamaPelanggan.Text, richTextBoxTambahAlamatPelanggan.Text, textBoxTambahNoTelpPelanggan.Text);

				string hasil = Pelanggan.TambahData(pelanggan);

				if (hasil == "1")
				{
					MessageBox.Show("Proses tambah berhasil");

					this.mainForm.FormMaster_Load(buttonTambahPelanggan, e);
					this.Close();
				}
			}
		}
	}
}

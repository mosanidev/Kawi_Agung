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

			if (textBoxUbahNamaPelanggan.Text == "" || textBoxUbahNoTelpPelanggan.Text == "" || richTextBoxUbahAlamatPelanggan.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else
			{
				Pelanggan pelanggan = new Pelanggan();
				pelanggan.IdPelanggan = IdPelanggan;
				pelanggan.Nama = textBoxUbahNamaPelanggan.Text;
				pelanggan.Alamat = richTextBoxUbahAlamatPelanggan.Text;
				pelanggan.NoTelp = textBoxUbahNoTelpPelanggan.Text;


				string hasilUbah = Pelanggan.UbahData(pelanggan);

				if (hasilUbah == "1")
				{
					MessageBox.Show("Proses ubah berhasil");

					this.mainForm.textBoxSearchNamaPelanggan.Clear();
					this.mainForm.PopulatePelangganTable("", "");
					this.Close();
				}
				else 
				{
					MessageBox.Show(hasilUbah);
				}
			}
		}

		private void richTextBoxUbahAlamatPelanggan_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonUbahPelanggan_Click(sender, e);
			}
		}
	}
}

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
	public partial class FormUbahJenis : Form
	{
		public FormUbahJenis()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormUbahJenis(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void buttonUbahJenis_Click(object sender, EventArgs e)
		{
			int IdJenis = FormMaster.listSelectedJenis[0].IdJenisBarang;

			JenisBarang jenis = new JenisBarang();
			jenis.IdJenisBarang = IdJenis;
			jenis.Nama = textBoxUbahJenisBarang.Text;

			List<JenisBarang> listJenis = new List<JenisBarang>();

			string hasilBaca = JenisBarang.BacaData("exclude", FormMaster.listSelectedJenis[0].Nama, listJenis);

			string hasilUbah = "";

			if (hasilBaca == "1")
			{
				hasilUbah = JenisBarang.UbahData(jenis, listJenis);
			}

			if (textBoxUbahJenisBarang.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else if (textBoxUbahJenisBarang.Text != "")
			{
				if (hasilUbah == "1")
				{
					MessageBox.Show("Proses ubah berhasil", "Ubah Jenis");

					this.mainForm.textBoxSearchJenisBrg.Clear();
					this.mainForm.FormMaster_Load(buttonUbahJenis, e);
					this.Close();
				}
				else if (hasilUbah == "Nama jenis sudah ada")
				{
					DialogResult dialogResult = MessageBox.Show("Jenis sudah ada. Apakah Anda ingin menyimpan data dengan nama jenis tersebut?", "Tambah Jenis", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes)
					{
						List<JenisBarang> listKosong = new List<JenisBarang>(); // kirim list kosong sebagai parameter method ubah data, agar bisa ditambahkan ke database
						string hasilTambah_ = JenisBarang.UbahData(jenis, listKosong);

						MessageBox.Show("Proses ubah berhasil", "Ubah Jenis");	

						this.mainForm.textBoxSearchJenisBrg.Clear();
						this.mainForm.FormMaster_Load(buttonUbahJenis, e);
						this.Close();
					}
				}
			}
		}

		private void FormUbahJenis_Load(object sender, EventArgs e)
		{
			textBoxUbahJenisBarang.Text = FormMaster.listSelectedJenis[0].Nama;

			textBoxUbahJenisBarang.SelectionStart = textBoxUbahJenisBarang.Text.Length;
			textBoxUbahJenisBarang.SelectionLength = 0;
		}
	}
}

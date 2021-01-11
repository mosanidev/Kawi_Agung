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
	public partial class FormTambahMerek : Form
	{
		public FormTambahMerek()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormTambahMerek(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void buttonTambahMerek_Click(object sender, EventArgs e)
		{
			if (textBoxTambahMerekBarang.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else
			{
				MerekBarang merek = new MerekBarang();
				merek.Nama = textBoxTambahMerekBarang.Text.Trim();

				string hasilTambah = MerekBarang.TambahData(merek, this.mainForm.listMerek);

				if (hasilTambah == "1")
				{
					MessageBox.Show("Proses tambah berhasil", "Info");

					this.mainForm.textBoxSearchMerekBrg.Clear();
					this.mainForm.FormMaster_Load(buttonTambahMerek, e);
					this.Close();
				}
				else if (hasilTambah == "Nama merek sudah ada")
				{
					DialogResult dialogResult = MessageBox.Show("Merek sudah ada. Apakah Anda ingin menyimpan data dengan nama merek tersebut?", "Tambah Merek", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes)
					{
						List<MerekBarang> listKosong = new List<MerekBarang>(); // kirim list kosong sebagai parameter method tambah data, agar bisa ditambahkan ke database
						string hasilTambah_ = MerekBarang.TambahData(merek, listKosong);

						MessageBox.Show("Proses tambah berhasil", "Info");

						this.mainForm.textBoxSearchMerekBrg.Clear();
						this.mainForm.FormMaster_Load(buttonTambahMerek, e);
						this.Close();
					}
				}
			}
		}
	}
}

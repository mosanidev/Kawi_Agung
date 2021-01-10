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
	public partial class FormTambahPegawai : Form
	{
		private List<Jabatan> listJabatan = new List<Jabatan>();

		public FormTambahPegawai()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormTambahPegawai(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void buttonTambahBarang_Click(object sender, EventArgs e)
		{
			if (textBoxPegawaiNama.Text == "" || textBoxUsernamePegawai.Text == "" || textBoxNoTelpPegawai.Text == "" || textBoxAlamatPegawai.Text == "" || comboBoxJabatanPegawai.Text == "" || comboBoxJenisKelaminPegawai.Text == "")
			{
				MessageBox.Show("Harap isi semua informasi terlebih dahulu");
			}
			else
			{

			}
		}

		private void FormTambahPegawai_Load(object sender, EventArgs e)
		{
			string hasilBaca = Jabatan.BacaData("", "", listJabatan);

			if (hasilBaca == "1")
			{
				foreach (var item in listJabatan)
				{
					comboBoxJabatanPegawai.Items.Add(item.IdJabatan + " - " + item.Nama);
				}
			}

			comboBoxJenisKelaminPegawai.Items.Add("Laki-laki");
			comboBoxJenisKelaminPegawai.Items.Add("Perempuan");
		}
	}
}

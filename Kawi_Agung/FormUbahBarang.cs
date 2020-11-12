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
			numericUpDownUbahBarangHargaJual.Value = FormMaster.listSelectedBarang[0].HargaJual;

			if (hasilBacaJenis == "1")
			{
				foreach (var item in listJenis)
				{
					comboBoxUbahBarangJenisBarang.Items.Add(item.IdJenisBarang + " - " + item.Nama);
				}
			}

			if (hasilBacaKategori == "1")
			{
				foreach (var item in listKategori)
				{
					comboBoxUbahBarangKategoriBarang.Items.Add(item.IdKategoriBarang + " - " + item.Nama);
				}
			}

			if (hasilBacaMerek == "1")
			{
				foreach (var item in listMerek)
				{
					comboBoxUbahBarangMerekBarang.Items.Add(item.IdMerekBarang + " - " + item.NamaMerekBarang);
				}
			}


		}
	}
}

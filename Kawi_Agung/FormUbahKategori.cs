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
	public partial class FormUbahKategori : Form
	{
		public FormUbahKategori()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormUbahKategori(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void FormUbahKategori_Load(object sender, EventArgs e)
		{
			textBoxUbahKategoriBarang.Text = FormMaster.listSelectedKategori[0].Nama;

			textBoxUbahKategoriBarang.SelectionStart = textBoxUbahKategoriBarang.Text.Length;
			textBoxUbahKategoriBarang.SelectionLength = 0;
		}

		private void buttonUbahKategori_Click(object sender, EventArgs e)
		{
			int IdKategori = FormMaster.listSelectedKategori[0].IdKategoriBarang;

			KategoriBarang kategori = new KategoriBarang(IdKategori, textBoxUbahKategoriBarang.Text);

			string hasil = KategoriBarang.UbahData(kategori);

			if (textBoxUbahKategoriBarang.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else if (textBoxUbahKategoriBarang.Text != "")
			{
				if (hasil == "1")
				{
					MessageBox.Show("Proses ubah berhasil");

					this.mainForm.FormMaster_Load(buttonUbahKategori, e);
					this.Close();
				}
			}
		}
	}
}

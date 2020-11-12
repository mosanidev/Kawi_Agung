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
	public partial class FormTambahKategori : Form
	{
		public FormTambahKategori()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormTambahKategori(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void buttonTambahKategori_Click(object sender, EventArgs e)
		{
			if (textBoxTambahKategoriBarang.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else 
			{
				KategoriBarang kategori = new KategoriBarang(textBoxTambahKategoriBarang.Text);

				string hasil = KategoriBarang.TambahData(kategori);

				if (hasil == "1")
				{
					MessageBox.Show("Proses tambah berhasil");

					this.mainForm.FormMaster_Load(buttonTambahKategori, e);
					this.Close();
				}
			}
		}

	}
}

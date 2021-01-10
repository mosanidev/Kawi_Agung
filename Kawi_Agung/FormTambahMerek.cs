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
				merek.Nama = textBoxTambahMerekBarang.Text;

				string hasilTambah = MerekBarang.TambahData(merek, this.mainForm.listMerek);

				if (hasilTambah == "1")
				{
					MessageBox.Show("Proses tambah berhasil");

					this.mainForm.textBoxSearchMerekBrg.Clear();
					this.mainForm.FormMaster_Load(buttonTambahMerek, e);
					this.Close();
				}
				else
				{
					MessageBox.Show(hasilTambah);
				}
			}
		}
	}
}

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
	public partial class FormUbahMerek : Form
	{
		public FormUbahMerek()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormUbahMerek(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void buttonUbahMerek_Click(object sender, EventArgs e)
		{
			int IdMerek = FormMaster.listSelectedMerek[0].IdMerekBarang;

			MerekBarang merek = new MerekBarang();
			merek.IdMerekBarang = IdMerek;
			merek.Nama = textBoxUbahMerekBarang.Text;

			List<MerekBarang> listMerek = new List<MerekBarang>();

			string hasilBaca = MerekBarang.BacaData("exclude", FormMaster.listSelectedMerek[0].Nama, listMerek);
			string hasilUbah = "";

			if (hasilBaca == "1")
			{
				hasilUbah = MerekBarang.UbahData(merek, listMerek);
			}

			if (textBoxUbahMerekBarang.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else if (textBoxUbahMerekBarang.Text != "")
			{
				if (hasilUbah == "1")
				{
					MessageBox.Show("Proses ubah berhasil");

					this.mainForm.textBoxSearchMerekBrg.Clear();
					this.mainForm.FormMaster_Load(buttonUbahMerek, e);
					this.Close();
				}
				else
				{
					MessageBox.Show(hasilUbah);
				}
			}
		}

		private void FormUbahMerek_Load(object sender, EventArgs e)
		{
			textBoxUbahMerekBarang.Text = FormMaster.listSelectedMerek[0].Nama;

			textBoxUbahMerekBarang.SelectionStart = textBoxUbahMerekBarang.Text.Length;
			textBoxUbahMerekBarang.SelectionLength = 0;
		}
	}
}

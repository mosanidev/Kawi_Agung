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

			if (textBoxUbahMerekBarang.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else 
			{
				MerekBarang merek = new MerekBarang();
				merek.IdMerekBarang = IdMerek;
				merek.Nama = textBoxUbahMerekBarang.Text.Trim();

				string hasilUbah = MerekBarang.UbahData(merek);

				if (hasilUbah == "1")
				{
					MessageBox.Show("Proses ubah berhasil");

					this.mainForm.textBoxSearchMerekBrg.Clear();
					this.mainForm.PopulateMerekTable("", "");
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

		private void textBoxUbahMerekBarang_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonUbahMerek_Click(sender, e);
			}
		}
	}
}

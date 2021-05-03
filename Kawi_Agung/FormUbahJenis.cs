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

			if (textBoxUbahJenisBarang.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else
			{
				jenis.Nama = textBoxUbahJenisBarang.Text.Trim();

				JenisBarang.UbahData(jenis);

				MessageBox.Show("Proses ubah berhasil");

				this.mainForm.textBoxSearchJenisBrg.Clear();
				this.mainForm.PopulateJenisTable("", "");
				this.Close();
				
			}
		}

		private void FormUbahJenis_Load(object sender, EventArgs e)
		{
			textBoxUbahJenisBarang.Text = FormMaster.listSelectedJenis[0].Nama;

			textBoxUbahJenisBarang.SelectionStart = textBoxUbahJenisBarang.Text.Length;
			textBoxUbahJenisBarang.SelectionLength = 0;
		}

		private void textBoxUbahJenisBarang_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonUbahJenis_Click(sender, e);
			}
		}
	}
}

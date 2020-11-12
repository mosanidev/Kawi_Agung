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
	public partial class FormTambahSupplier : Form
	{
		public FormTambahSupplier()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormTambahSupplier(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void buttonTambahSupplier_Click(object sender, EventArgs e)
		{
			if (textBoxTambahNamaSupplier.Text == "" || textBoxTambahNoTelpSupplier.Text == "" ||richTextBoxTambahAlamatSupplier.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else
			{
				Supplier supplier = new Supplier(textBoxTambahNamaSupplier.Text, richTextBoxTambahAlamatSupplier.Text, textBoxTambahNoTelpSupplier.Text);

				string hasil = Supplier.TambahData(supplier);

				if (hasil == "1")
				{
					MessageBox.Show("Proses tambah berhasil");

					this.mainForm.FormMaster_Load(buttonTambahSupplier, e);
					this.Close();
				}
			}
		}
	}
}

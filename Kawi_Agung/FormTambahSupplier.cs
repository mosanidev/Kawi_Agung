using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ePOSOne.btnProduct;

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
				Supplier supplier = new Supplier();
				supplier.Nama = textBoxTambahNamaSupplier.Text.Trim();
				supplier.Alamat = richTextBoxTambahAlamatSupplier.Text.Trim();
				supplier.NoTelp = textBoxTambahNoTelpSupplier.Text.Trim();

				string hasilTambah = Supplier.TambahData(supplier);

				if (hasilTambah == "1")
				{
					MessageBox.Show("Proses tambah berhasil");

					this.mainForm.textBoxSearchNamaSupplier.Clear();
					this.mainForm.PopulateSupplierTable("", "");
					this.Close();
				}
				else
				{
					MessageBox.Show(hasilTambah);
				}
			}
		}

		private void richTextBoxTambahAlamatSupplier_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonTambahSupplier_Click(sender, e);
			}
		}
	}
}

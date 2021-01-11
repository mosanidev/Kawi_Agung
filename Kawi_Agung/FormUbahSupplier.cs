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
	public partial class FormUbahSupplier : Form
	{
		public FormUbahSupplier()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormUbahSupplier(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void FormUbahSupplier_Load(object sender, EventArgs e)
		{
			textBoxUbahNamaSupplier.Text = FormMaster.listSelectedSupplier[0].Nama;
			richTextBoxUbahAlamatSupplier.Text = FormMaster.listSelectedSupplier[0].Alamat;
			textBoxUbahNoTelpSupplier.Text = FormMaster.listSelectedSupplier[0].NoTelp;

			textBoxUbahNamaSupplier.SelectionStart = textBoxUbahNamaSupplier.Text.Length;
			textBoxUbahNamaSupplier.SelectionLength = 0;
		}

		private void buttonUbahSupplier_Click(object sender, EventArgs e)
		{
			int IdSupplier = FormMaster.listSelectedSupplier[0].IdSupplier;

			Supplier supplier = new Supplier();
			supplier.IdSupplier = IdSupplier;
			supplier.Nama = textBoxUbahNamaSupplier.Text.Trim();
			supplier.Alamat = richTextBoxUbahAlamatSupplier.Text.Trim();
			supplier.NoTelp = textBoxUbahNoTelpSupplier.Text.Trim();

			List<Supplier> listSupplier = new List<Supplier>();
			string hasilBaca = Supplier.BacaData("exclude", FormMaster.listSelectedSupplier[0].Nama, listSupplier);
			string hasilUbah = "";

			if (hasilBaca == "1")
			{
				hasilUbah = Supplier.UbahData(supplier, listSupplier);
			}

			if (textBoxUbahNamaSupplier.Text == "" || textBoxUbahNoTelpSupplier.Text == "" || richTextBoxUbahAlamatSupplier.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else if (textBoxUbahNamaSupplier.Text != "" || textBoxUbahNoTelpSupplier.Text != "" || richTextBoxUbahAlamatSupplier.Text != "")
			{
				if (hasilUbah == "1")
				{
					MessageBox.Show("Proses ubah berhasil");

					this.mainForm.textBoxSearchNamaSupplier.Clear();
					this.mainForm.FormMaster_Load(buttonUbahSupplier, e);
					this.Close();
				}
				else if (hasilUbah == "Nama supplier sudah ada")
				{
					DialogResult dialogResult = MessageBox.Show("Nama supplier sudah ada. Apakah Anda ingin menyimpan data dengan nama supplier tersebut?", "", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes)
					{
						List<Supplier> listKosong = new List<Supplier>(); // kirim list kosong sebagai parameter method ubah data, agar bisa disimpan ke database
						string hasilTambah_ = Supplier.UbahData(supplier, listKosong);

						MessageBox.Show("Proses ubah berhasil");

						this.mainForm.textBoxSearchNamaSupplier.Clear();
						this.mainForm.FormMaster_Load(buttonUbahSupplier, e);
						this.Close();
					}
				}
			}
		}
	}
}

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

				string hasilTambah = Supplier.TambahData(supplier, this.mainForm.listSupplier);

				if (hasilTambah == "1")
				{
					MessageBox.Show("Proses tambah berhasil");

					this.mainForm.textBoxSearchNamaSupplier.Clear();
					this.mainForm.FormMaster_Load(buttonTambahSupplier, e);
					this.Close();
				}
				else if (hasilTambah == "Nama supplier sudah ada") // apabila ada nama supplier yang sama di database
				{
					DialogResult dialogResult = MessageBox.Show("Nama supplier sudah ada. Apakah Anda ingin menyimpan data dengan nama supplier tersebut?", "Tambah Supplier", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes)
					{
						List<Supplier> listKosong = new List<Supplier>(); // kirim list kosong sebagai parameter method tambah data, agar bisa ditambahkan ke database
						string hasilTambah_ = Supplier.TambahData(supplier, listKosong);

						MessageBox.Show("Proses tambah berhasil");

						this.mainForm.textBoxSearchNamaSupplier.Clear();
						this.mainForm.FormMaster_Load(buttonTambahSupplier, e);
						this.Close();
					}
				}
			}
		}
	}
}

﻿using System;
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

			Supplier supplier = new Supplier(IdSupplier, textBoxUbahNamaSupplier.Text, richTextBoxUbahAlamatSupplier.Text, textBoxUbahNoTelpSupplier.Text);

			string hasil = Supplier.UbahData(supplier);

			if (textBoxUbahNamaSupplier.Text == "" || textBoxUbahNoTelpSupplier.Text == "" || richTextBoxUbahAlamatSupplier.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else if (textBoxUbahNamaSupplier.Text != "" || textBoxUbahNoTelpSupplier.Text != "" || richTextBoxUbahAlamatSupplier.Text != "")
			{
				if (hasil == "1")
				{
					MessageBox.Show("Proses ubah berhasil");

					this.mainForm.FormMaster_Load(buttonUbahSupplier, e);
					this.Close();
				}
			}
		}
	}
}
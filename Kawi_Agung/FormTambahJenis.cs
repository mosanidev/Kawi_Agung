﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Kawi_Agung
{
	public partial class FormTambahJenis : Form
	{
		public FormTambahJenis()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormTambahJenis(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void buttonTambahJenis_Click(object sender, EventArgs e)
		{
			if (textBoxTambahJenisBarang.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else
			{
				JenisBarang jenis = new JenisBarang();
				jenis.Nama = textBoxTambahJenisBarang.Text;

				string hasilTambah = JenisBarang.TambahData(jenis, this.mainForm.listJenis);

				if (hasilTambah == "1")
				{
					MessageBox.Show("Proses tambah berhasil");

					this.mainForm.textBoxSearchJenisBrg.Clear();
					this.mainForm.FormMaster_Load(buttonTambahJenis, e);
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

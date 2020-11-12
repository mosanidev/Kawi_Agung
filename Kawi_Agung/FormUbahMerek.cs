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

			MerekBarang merek = new MerekBarang(IdMerek, textBoxUbahMerekBarang.Text);

			string hasil = MerekBarang.UbahData(merek);

			if (textBoxUbahMerekBarang.Text == "")
			{
				MessageBox.Show("Harap di isi terlebih dahulu");
			}
			else if (textBoxUbahMerekBarang.Text != "")
			{
				if (hasil == "1")
				{
					MessageBox.Show("Proses ubah berhasil");

					this.mainForm.FormMaster_Load(buttonUbahMerek, e);
					this.Close();
				}
			}
		}

		private void FormUbahMerek_Load(object sender, EventArgs e)
		{
			textBoxUbahMerekBarang.Text = FormMaster.listSelectedMerek[0].NamaMerekBarang;

			textBoxUbahMerekBarang.SelectionStart = textBoxUbahMerekBarang.Text.Length;
			textBoxUbahMerekBarang.SelectionLength = 0;
		}
	}
}

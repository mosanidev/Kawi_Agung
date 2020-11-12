using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Kawi_Agung
{
	public partial class FormTambahUser : Form
	{
		public List<Jabatan> listJabatan = new List<Jabatan>();
		public FormTambahUser()
		{
			InitializeComponent();

			string hasilBaca = Jabatan.BacaData("", "", listJabatan);
			if (hasilBaca == "1")
			{
				comboBoxJabatanUser.Items.Clear();

				for (int i = 0; i < listJabatan.Count; i++)
				{
					comboBoxJabatanUser.Items.Add(listJabatan[i].Nama);
				}
			}
			else
			{
				MessageBox.Show("Gagal membaca data jabatan");
			}
		}

		private void buttonCreate_Click(object sender, EventArgs e)
		{
			Jabatan jabatanUser = listJabatan[comboBoxJabatanUser.SelectedIndex];

			User usr = new User(textBoxUsername.Text, textBoxPassword.Text, "Aktif", textBoxNama.Text, "", "", DateTime.Now, "08998765444", null, "SAPRI", "084444444447", "BRI", jabatanUser);
			
			string hasilTambah = User.TambahData(usr);

			if (hasilTambah == "1")
			{
				MessageBox.Show("Proses tambah berhasil");
			}
			else
			{
				MessageBox.Show("Terjadi Kesalahan. Pesan Kesalahan :" + hasilTambah);
			}
		}

	}
}

using Kawi_Agung.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kawi_Agung
{
	public partial class FormTambahPegawai : Form
	{
		private List<Jabatan> listJabatan = new List<Jabatan>();
		private string pathFoto = "";
		public FormTambahPegawai()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		public FormTambahPegawai(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void FormTambahPegawai_Load(object sender, EventArgs e)
		{
			string hasilBaca = Jabatan.BacaData(listJabatan);

			if (hasilBaca == "1")
			{
				foreach (var item in listJabatan)
				{
					comboBoxJabatanPegawai.Items.Add(item.IdJabatan + " - " + item.Nama);
				}
			}

			comboBoxJenisKelaminPegawai.Items.Add("Laki-laki");
			comboBoxJenisKelaminPegawai.Items.Add("Perempuan");

		}

		private void buttonTambahPegawai_Click(object sender, EventArgs e)
		{
			if (textBoxPegawaiNama.Text == "" || textBoxUsernamePegawai.Text == "" || textBoxNoTelpPegawai.Text == "" || textBoxAlamatPegawai.Text == "" || comboBoxJabatanPegawai.Text == "" || comboBoxJenisKelaminPegawai.Text == "")
			{
				MessageBox.Show("Harap isi semua informasi terlebih dahulu");
			}
			else if (textBoxUsernamePegawai.Text.Length < 6)
			{
				MessageBox.Show("Username harus sama/lebih dari 6 karakter");
			}
			else
			{
				Jabatan jabatan = new Jabatan();
				jabatan.IdJabatan = Convert.ToInt32(comboBoxJabatanPegawai.Text.Split('-')[0]);
				jabatan.Nama = comboBoxJabatanPegawai.Text.Split('-')[1];

				User user = new User();
				user.Nama = textBoxPegawaiNama.Text.Trim();
				user.JenisKelamin = comboBoxJenisKelaminPegawai.Text.Trim();
				user.TanggalLahir = DateTime.Parse(dateTimePickerTanggalLahirPegawai.Value.ToString());
				user.NoTelp = textBoxNoTelpPegawai.Text.Trim();
				user.Username = textBoxUsernamePegawai.Text.Trim();
				user.Alamat = textBoxAlamatPegawai.Text.Trim();
				user.Jabatan = jabatan;

				if (pathFoto != "")
				{
					user.Foto = Path.GetExtension(pathFoto);
					string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\users";
					string folderName = Path.Combine(projectPath, textBoxUsernamePegawai.Text.Trim());
					Directory.CreateDirectory(folderName);
					Array.ForEach(Directory.GetFiles(@folderName + "\\"), File.Delete);
					File.Copy(pathFoto, folderName + "\\" + "foto" + user.Foto);
				}

				List<User> listUser = new List<User>();

				User.BacaPegawai("cari username", textBoxUsernamePegawai.Text, listUser);

				if (listUser.Count == 0)
				{
					string hasilTambah = User.TambahPegawai(user);

					if (hasilTambah == "1")
					{
						MessageBox.Show("Proses tambah berhasil");

						this.mainForm.textBoxSearchPegawai.Clear();
						this.mainForm.PopulatePegawaiTable("", "");
						this.Close();
					}
					else
					{
						MessageBox.Show(hasilTambah);
					}
				}

			}
		}

		private void buttonUnggahFotoUser_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Pilih Foto Profil";
			openFileDialog.InitialDirectory =
				Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			openFileDialog.Filter = "Images Files (*.png; *.jpeg; *.jpg)|*.png;*jpeg;*jpg";
			openFileDialog.Multiselect = false;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				// limit jadi 16 Mib
				//if (new FileInfo(openFileDialog.FileName).Length > (64 * 1024))
				//{
				//	MessageBox.Show("Ukuran file tidak boleh lebih dari 64 kb");
				//}
				try
				{
					pathFoto = openFileDialog.FileName;
					pictureBoxTambahFotoUser.ImageLocation = openFileDialog.FileName;
				}
				catch (IOException ex)
				{
					MessageBox.Show(ex.Message.ToString());
				}

				openFileDialog.Dispose();
			}
		}

		private void buttonHapusFoto_Click(object sender, EventArgs e)
		{
			pictureBoxTambahFotoUser.Image = Resources.profile_picture;
			pathFoto = "";
		}

		private void textBoxAlamatPegawai_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonTambahPegawai_Click(sender, e);
			}
		}
	}
}

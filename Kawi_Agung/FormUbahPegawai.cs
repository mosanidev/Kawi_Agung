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
	public partial class FormUbahPegawai : Form
	{
		public FormUbahPegawai()
		{
			InitializeComponent();
		}

		private List<Jabatan> listJabatan = new List<Jabatan>();
		private FormMaster mainForm = null;

		private string pathFoto = "";
		string statusUser = "";

		public FormUbahPegawai(Form callingForm, string keteranganStatus)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();

			statusUser = keteranganStatus;

			if (keteranganStatus == "Belum Aktif")
			{
				comboBoxStatusPegawai.Visible = false;
				labelStatus.Text = "Belum Aktif";
				comboBoxJabatanPegawai.Focus();
			}
			else
			{
				labelStatus.Visible = false;
				comboBoxStatusPegawai.Focus();
			}
		}

		private void FormUbahPegawai_Load(object sender, EventArgs e)
		{
			labelNama.Text = FormMaster.listSelectedPegawai[0].Nama;
			textBoxNoTelpPegawai.Text = FormMaster.listSelectedPegawai[0].NoTelp;
			labelUsername.Text = FormMaster.listSelectedPegawai[0].Username;
			textBoxAlamatPegawai.Text = FormMaster.listSelectedPegawai[0].Alamat;
			labelTanggalLahir.Text = FormMaster.listSelectedPegawai[0].TanggalLahir.ToString("dd MMMM yyyy");

			if (FormMaster.listSelectedPegawai[0].Foto != "")
			{
				string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\users";
				string folderName = Path.Combine(projectPath, FormMaster.listSelectedPegawai[0].Username);
				pictureBoxUbahFotoUser.ImageLocation = folderName + "\\" + "foto" + FormMaster.listSelectedPegawai[0].Foto.ToString();
			}
			else
			{
				pictureBoxUbahFotoUser.Image = Resources.profile_picture;
			}

			labelJenisKelamin.Text = FormMaster.listSelectedPegawai[0].JenisKelamin;

			comboBoxStatusPegawai.Items.Add("Aktif");
			comboBoxStatusPegawai.Items.Add("Blokir");
			comboBoxStatusPegawai.SelectedItem = FormMaster.listSelectedPegawai[0].Status;

			string hasilBacaJabatan = Jabatan.BacaData(listJabatan);

			if (hasilBacaJabatan == "1")
			{
				foreach (var item in listJabatan)
				{
					comboBoxJabatanPegawai.Items.Add(item.IdJabatan + " - " + item.Nama);
				}
				comboBoxJabatanPegawai.SelectedItem = FormMaster.listSelectedPegawai[0].Jabatan.IdJabatan + " - " + FormMaster.listSelectedPegawai[0].Jabatan.Nama;
			}

			textBoxNoTelpPegawai.SelectionStart = textBoxNoTelpPegawai.Text.Length;
			textBoxNoTelpPegawai.SelectionLength = 0;

		}

		private void buttonHapusFoto_Click(object sender, EventArgs e)
		{
			pictureBoxUbahFotoUser.Image = Resources.profile_picture;
			pictureBoxUbahFotoUser.Tag = "Default";
			pathFoto = "";
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
					pictureBoxUbahFotoUser.ImageLocation = openFileDialog.FileName;
					pictureBoxUbahFotoUser.Tag = "Unggahan";
				}
				catch (IOException ex)
				{
					MessageBox.Show(ex.Message.ToString());
				}
			}

			openFileDialog.Dispose();
		}

		private void buttonUbahPegawai_Click(object sender, EventArgs e)
		{
			if (textBoxNoTelpPegawai.Text == "" || textBoxAlamatPegawai.Text == "")
			{
				MessageBox.Show("Data harus diisi semua terlebih dahulu");
			}
			else
			{
				Jabatan jabatan = new Jabatan();
				jabatan.IdJabatan = int.Parse(comboBoxJabatanPegawai.Text.Split('-')[0]);
				jabatan.Nama = comboBoxJabatanPegawai.Text.Split('-')[1];

				User user = new User();
				user.IdUser = FormMaster.listSelectedPegawai[0].IdUser;
				user.NoTelp = textBoxNoTelpPegawai.Text.Trim();

				if (pathFoto != "")
				{
					user.Foto = Path.GetExtension(pathFoto);
				}

				if (statusUser == "Belum Aktif")
				{
					user.Status = labelStatus.Text;
				}
				else
				{
					user.Status = comboBoxStatusPegawai.Text;
				}

				user.Alamat = textBoxAlamatPegawai.Text.Trim();
				user.Jabatan = jabatan;

				string hasilUbah = "";

				if (pictureBoxUbahFotoUser.Tag == "Default")
				{
					hasilUbah = User.UbahPegawai(user, "Hapus");
				} else if (pictureBoxUbahFotoUser.Tag == "Unggahan")
				{
					string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\users";
					string folderName = Path.Combine(projectPath, FormMaster.listSelectedPegawai[0].Username);
					Directory.CreateDirectory(folderName);
					Array.ForEach(Directory.GetFiles(@folderName + "\\"), File.Delete);
					File.Copy(pathFoto, folderName + "\\" + "foto" + user.Foto);
					hasilUbah = User.UbahPegawai(user, "Ada");
				} else if (pictureBoxUbahFotoUser.Tag == null)
				{
					hasilUbah = User.UbahPegawai(user, "Tidak Ada");
				}

				if (hasilUbah == "1")
				{
					MessageBox.Show("Data berhasil disimpan");

					this.mainForm.textBoxSearchPegawai.Clear();
					this.mainForm.PopulatePegawaiTable("", "");
					this.Close();
				}
			}
		}

		private void textBoxAlamatPegawai_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonUbahPegawai_Click(sender, e);
			}
		}
	}
}

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
		bool photoBool = true;
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
			}
			else
			{
				labelStatus.Visible = false;
			}
		}

		private void FormUbahPegawai_Load(object sender, EventArgs e)
		{
			textBoxPegawaiNama.Text = FormMaster.listSelectedPegawai[0].Nama;
			textBoxNoTelpPegawai.Text = FormMaster.listSelectedPegawai[0].NoTelp;
			labelUsername.Text = FormMaster.listSelectedPegawai[0].Username;
			textBoxAlamatPegawai.Text = FormMaster.listSelectedPegawai[0].Alamat;
			dateTimePickerTanggalLahirPegawai.Value = FormMaster.listSelectedPegawai[0].TanggalLahir;

			if (FormMaster.listSelectedPegawai[0].Foto != null)
			{
				pictureBoxUbahFotoUser.Image = ConvertBinaryToImage(FormMaster.listSelectedPegawai[0].Foto);
			}

			comboBoxJenisKelaminPegawai.Items.Add("Laki-laki");
			comboBoxJenisKelaminPegawai.Items.Add("Perempuan");
			comboBoxJenisKelaminPegawai.SelectedItem = FormMaster.listSelectedPegawai[0].JenisKelamin;

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

			textBoxPegawaiNama.SelectionStart = textBoxPegawaiNama.Text.Length;
			textBoxPegawaiNama.SelectionLength = 0;
			
		}

		Image ConvertBinaryToImage(byte[] data)
		{
			var img = Resources.box;

			if (data == null)
				return img; ;
			using (MemoryStream ms = new MemoryStream(data))
			{
				try
				{
					return Image.FromStream(ms);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message.ToString());

					return img;
				}
			}
		}

		private void buttonHapusFoto_Click(object sender, EventArgs e)
		{
			pictureBoxUbahFotoUser.Image = Resources.profile_picture;
			pictureBoxUbahFotoUser.Tag = "Default";
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

				pictureBoxUbahFotoUser.Image = new Bitmap(openFileDialog.FileName);
				pathFoto = openFileDialog.FileName;
				pictureBoxUbahFotoUser.Tag = "Unggahan";
			}
		}

		private void buttonUbahPegawai_Click(object sender, EventArgs e)
		{
			if (textBoxPegawaiNama.Text == "" || textBoxNoTelpPegawai.Text == "" || textBoxAlamatPegawai.Text == "")
			{
				MessageBox.Show("Data harus diisi semua terlebih dahulu");
			}
			else
			{
				byte[] foto = null;

				Jabatan jabatan = new Jabatan();
				jabatan.IdJabatan = int.Parse(comboBoxJabatanPegawai.Text.Split('-')[0]);
				jabatan.Nama = comboBoxJabatanPegawai.Text.Split('-')[1];

				if (pathFoto != "")
				{
					foto = ConvertImageToBinary(Image.FromFile(pathFoto));
					photoBool = false;
				}

				User user = new User();
				user.IdUser = FormMaster.listSelectedPegawai[0].IdUser;
				user.Nama = textBoxPegawaiNama.Text.Trim();
				user.JenisKelamin = comboBoxJenisKelaminPegawai.Text;
				user.TanggalLahir = dateTimePickerTanggalLahirPegawai.Value;
				user.NoTelp = textBoxNoTelpPegawai.Text.Trim();

				if (statusUser == "Belum Aktif")
				{
					user.Status = labelStatus.Text;
				}
				else
				{
					user.Status = comboBoxStatusPegawai.Text;
				}

				user.Alamat = textBoxAlamatPegawai.Text.Trim();
				user.Foto = foto;
				user.Jabatan = jabatan;

				List<User> listUser = new List<User>();

				string hasilBaca = User.BacaPegawai("exclude", FormMaster.listSelectedPegawai[0].IdUser.ToString(), listUser);

				string hasilUbah = "";

				if (hasilBaca == "1")
				{
					if (pictureBoxUbahFotoUser.Tag == "Default")
					{
						hasilUbah = User.UbahPegawai(user, listUser, "Kosong");
					} else if (pictureBoxUbahFotoUser.Tag == "Unggahan")
					{
						hasilUbah = User.UbahPegawai(user, listUser, "Ada");
					} else if (pictureBoxUbahFotoUser.Tag == null)
					{
						hasilUbah = User.UbahPegawai(user, listUser, "Tidak Ada");
					}
				}

				if (hasilUbah == "1")
				{
					MessageBox.Show("Data berhasil disimpan");

					this.mainForm.textBoxSearchPegawai.Clear();
					this.mainForm.FormMaster_Load(buttonUbahPegawai, e);
					this.Close();
				}
				else
				{
					MessageBox.Show(hasilUbah + ".Harap input username yang lain");
				}
			}
		}

		byte[] ConvertImageToBinary(Image img)
		{
			if (img == null)
				return null;
			using (MemoryStream ms = new MemoryStream())
			{
				//System.Drawing.Imaging.ImageFormat.Jpeg
				img.Save(ms, img.RawFormat);
				return ms.ToArray();

			}
		}
	}
}

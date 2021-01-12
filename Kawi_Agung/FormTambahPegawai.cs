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

		private void buttonTambahPegawai_Click(object sender, EventArgs e)
		{
			if (textBoxPegawaiNama.Text == "" || textBoxUsernamePegawai.Text == "" || textBoxNoTelpPegawai.Text == "" || textBoxAlamatPegawai.Text == "" || comboBoxJabatanPegawai.Text == "" || comboBoxJenisKelaminPegawai.Text == "")
			{
				MessageBox.Show("Harap isi semua informasi terlebih dahulu");
			}
			else if (textBoxUsernamePegawai.Text.Length < 8)
			{
				MessageBox.Show("Username harus sama/lebih dari 8 karakter");
			}
			else
			{
				byte[] foto = null;

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
					foto = ConvertImageToBinary(Image.FromFile(pathFoto));
				}

				user.Foto = foto;

				string hasilTambah = User.TambahPegawai(user, this.mainForm.listUser);

				if (hasilTambah == "1")
				{
					MessageBox.Show("Proses tambah berhasil", "Info");

					this.mainForm.textBoxSearchPegawai.Clear();
					this.mainForm.FormMaster_Load(buttonTambahPegawai, e);
					this.Close();
				}
				else if (hasilTambah == "Username sudah ada") // apabila ada username yang sama di database
				{
					MessageBox.Show(hasilTambah + ". Harap input username yang lain");
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

				pictureBoxTambahFotoUser.Image = new Bitmap(openFileDialog.FileName);
				pathFoto = openFileDialog.FileName;
			}
		}

		private void buttonHapusFoto_Click(object sender, EventArgs e)
		{
			pictureBoxTambahFotoUser.Image = Resources.profile_picture;
			pathFoto = "";
		}

		private void buttonHapusFoto_Click_1(object sender, EventArgs e)
		{

		}
	}
}

using Kawi_Agung.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kawi_Agung
{
	public partial class FormTambahBarang : Form
	{
		public FormTambahBarang()
		{
			InitializeComponent();
		}

		private List<JenisBarang> listJenis = new List<JenisBarang>();
		private List<KategoriBarang> listKategori = new List<KategoriBarang>();
		private List<MerekBarang> listMerek = new List<MerekBarang>();

		private string pathFoto = "";
		private int hasilHargaJual = 0;

		private FormMaster mainForm = null;
		public FormTambahBarang(Form callingForm)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
		}

		private void FormTambahBarang_Load(object sender, EventArgs e)
		{
			
			string hasilBacaJenis = JenisBarang.BacaData("", "", listJenis);
			string hasilBacaKategori = KategoriBarang.BacaData("", "", listKategori);
			string hasilBacaMerek = MerekBarang.BacaData("", "", listMerek);

			if (hasilBacaJenis == "1")
			{
				foreach (var item in listJenis)
				{
					comboBoxTambahBarangJenisBarang.Items.Add(item.IdJenisBarang + " - " + item.Nama);
				}
			}

			if (hasilBacaKategori == "1")
			{
				foreach (var item in listKategori)
				{
					comboBoxTambahBarangKategoriBarang.Items.Add(item.IdKategoriBarang + " - " + item.Nama);
				}
			}

			if (hasilBacaMerek == "1")
			{
				foreach (var item in listMerek)
				{
					comboBoxTambahBarangMerekBarang.Items.Add(item.IdMerekBarang + " - " + item.Nama);
				}
			}

			comboBoxSatuanBarang.Items.Add("PC");
			comboBoxSatuanBarang.Items.Add("SET");
		}

		private void buttonTambahBarang_Click(object sender, EventArgs e)
		{
			if (textBoxTambahBarangKodeBarang.Text == "" || textBoxTambahBarangNamaBarang.Text == "" || comboBoxTambahBarangJenisBarang.Text == "" || comboBoxTambahBarangKategoriBarang.Text == "" || comboBoxTambahBarangMerekBarang.Text == "" || comboBoxSatuanBarang.Text == "")
			{
				MessageBox.Show("Data harus diisi semua terlebih dahulu");
			}
			else
			{
				byte[] foto = null;

				JenisBarang jenis = new JenisBarang();
				jenis.IdJenisBarang = int.Parse(comboBoxTambahBarangJenisBarang.Text.Split('-')[0]);
				jenis.Nama = comboBoxTambahBarangJenisBarang.Text.Split('-')[1];
				
				KategoriBarang kategori = new KategoriBarang();
				kategori.IdKategoriBarang = int.Parse(comboBoxTambahBarangKategoriBarang.Text.Split('-')[0]);
				kategori.Nama = comboBoxTambahBarangKategoriBarang.Text.Split('-')[1];
				
				MerekBarang merek = new MerekBarang();
				merek.IdMerekBarang = int.Parse(comboBoxTambahBarangMerekBarang.Text.Split('-')[0]);
				merek.Nama = comboBoxTambahBarangMerekBarang.Text.Split('-')[1];

				if (pathFoto != "")
				{
					foto = ConvertImageToBinary(Image.FromFile(pathFoto));
				}

				hasilHargaJual = hitungDiskon(Convert.ToInt32(numericUpDownTambahBarangHargaJual.Value), Convert.ToInt32(numericUpDownTambahBarangDiskon.Value));

				Barang barang = new Barang();
				barang.KodeBarang = textBoxTambahBarangKodeBarang.Text;
				barang.Nama = textBoxTambahBarangNamaBarang.Text;
				barang.Jenis = jenis;
				barang.Kategori = kategori;
				barang.Merek = merek;
				barang.HargaJual = hasilHargaJual;
				barang.DiskonPersenJual = Convert.ToInt32(numericUpDownTambahBarangDiskon.Value);
				barang.Satuan = comboBoxSatuanBarang.Text;
				barang.Foto = foto;

				string hasilTambah = Barang.TambahData(barang, this.mainForm.listBarang);

				if (hasilTambah == "1")
				{
					MessageBox.Show("Data berhasil ditambahkan");

					this.mainForm.textBoxSearchBarang.Clear();
					this.mainForm.FormMaster_Load(buttonTambahBarang, e);
					this.Close();
				}
				else
				{
					MessageBox.Show(hasilTambah);
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

		private void buttonUnggahFotoBarang_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Pilih Foto Barang";
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
				
				pictureBoxTambahBarangGambarBarang.Image = new Bitmap(openFileDialog.FileName);
				pathFoto = openFileDialog.FileName;
			}
		}

		int hitungDiskon(int harga, int diskon)
		{
			double diskonDesimal = (double)diskon / 100.0;
			return Convert.ToInt32((double)harga - ((double)harga * diskonDesimal));
		}

		string ConvertToRupiah(int number)
		{
			CultureInfo culture = new CultureInfo("id-ID");

			return string.Format(culture, "{0:c0}", number);
		}

		private void numericUpDownTambahBarangDiskon_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				int hargaJual = Convert.ToInt32(numericUpDownTambahBarangHargaJual.Value);
				int diskon = Convert.ToInt32(numericUpDownTambahBarangDiskon.Value);
				hasilHargaJual = hitungDiskon(hargaJual, diskon);
				labelHasilHargaJual.Text = ConvertToRupiah(hasilHargaJual);
			}
		}

		private void buttonHapusFoto_Click(object sender, EventArgs e)
		{
			pictureBoxTambahBarangGambarBarang.Image = Resources.box;
			pathFoto = "";
		}
	}
}

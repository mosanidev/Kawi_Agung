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
	public partial class FormTambahNotaJual : Form
	{
		public FormTambahNotaJual()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;

		private List<Pelanggan> listPelanggan = new List<Pelanggan>();

		private string keterangan = "";

		private int rowIndex = 0;

		private int hargaJual = 0;

		private int idUser = 0;

		public FormTambahNotaJual(Form callingForm, int pIdUser)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();

			idUser = pIdUser;
		}

		private void FormTambahNotaJual_Load(object sender, EventArgs e)
		{
			textBoxNoFaktur.Select();

			string hasilPelanggan = Pelanggan.BacaData("", "", listPelanggan);

			if (hasilPelanggan == "1")
			{
				foreach (var item in listPelanggan)
				{
					comboBoxPelanggan.Items.Add(item.IdPelanggan + " - " + item.Nama);
				}
			}
		}

		string convertToRupiah(int number)
		{
			CultureInfo culture = new CultureInfo("id-ID");

			return string.Format(culture, "{0:c0}", number);
		}

		int hitungDiskon(int harga, int diskon)
		{
			double diskonDesimal = (double)diskon / 100.0;
			return Convert.ToInt32((double)harga - ((double)harga * diskonDesimal));
		}

		Image ConvertBinaryToImage(byte[] data)
		{
			var img = Resources.profile_picture;

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

		private void textBoxKodeBarangKeluar_KeyDown(object sender, KeyEventArgs e)
		{
			List<Barang> listBarang = new List<Barang>();

			if (e.KeyCode == Keys.Enter)
			{
				string barang = Barang.BacaDataBarang("cari barang", textBoxKodeBarangKeluar.Text.ToString(), listBarang);

				if (listBarang.Count == 1)
				{
					implementDataToView(listBarang);
				}
				else
				{
					MessageBox.Show("Kode barang yang anda masukkan tidak ada di sistem");

					kosongkanInfoBarang();
				}
			}
		}

		private void implementDataToView(List<Barang> listBarang)
		{
			Image imgBarang = null;

			if (listBarang[0].Foto == null)
			{
				iconPictureBoxBarangKeluar.Image = Resources.box;
			}
			else
			{
				imgBarang = ConvertBinaryToImage(listBarang[0].Foto);
				iconPictureBoxBarangKeluar.Image = imgBarang;
			}

			labelIdBarang.Text = listBarang[0].IdBarang.ToString();
			labelKodeBarang.Text = listBarang[0].KodeBarang.ToString();
			labelNamaBarang.Text = listBarang[0].Nama.ToString();
			labelKategoriBarang.Text = listBarang[0].Kategori.Nama;
			labelMerekBarang.Text = listBarang[0].Merek.Nama;
			labelJenisBarang.Text = listBarang[0].Jenis.Nama;
			labelJumlahStok.Text = listBarang[0].JumlahStok.ToString();
			labelSatuan.Text = listBarang[0].Satuan;

			hargaJual = listBarang[0].HargaJual;
			labelHargaBarang.Text = convertToRupiah(hargaJual);
			labelDiskonPersen.Text = listBarang[0].DiskonPersenJual.ToString() + " %";
		}

		int hitungGrandTotal()
		{
			int totalHarga = 0;

			for (int i = 0; i < dataGridViewBarangKeluar.Rows.Count; i++)
			{
				int subTotal = Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[4].Value);
				totalHarga = totalHarga + subTotal * Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[5].Value);

			}

			return totalHarga;

		}

		public void kosongkanInfoBarang()
		{
			hargaJual = 0;
			textBoxKodeBarangKeluar.Clear();
			iconPictureBoxBarangKeluar.Image = null;
			labelKodeBarang.Text = "-";
			labelNamaBarang.Text = "-";
			labelKategoriBarang.Text = "-";
			labelMerekBarang.Text = "-";
			labelJenisBarang.Text = "-";
			labelSatuan.Text = "-";
			labelJumlahStok.Text = "-";
			labelHargaBarang.Text = "Rp0"; 
			numericUpDownJumlahBarangKeluar.Value = 1;
			labelDiskonPersen.Text = "-";
		}

		private void numericUpDownJumlahBarangKeluar_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (labelKodeBarang.Text == "-")
				{
					MessageBox.Show("Info barang tidak dapat disimpan");
				}
				else if (numericUpDownJumlahBarangKeluar.Value > int.Parse(labelJumlahStok.Text)) // jika jumlah barang yang dijual melebihi kapasitas jumlah stok barang
				{
					MessageBox.Show("Jumlah barang melebihi jumlah stok barang di gudang");
				}
				else
				{
					//int subTotal = Convert.ToInt32(numericUpDownHargaBeli.Value * numericUpDownJumlahBarangMasuk.Value);

					foreach (DataGridViewRow row in dataGridViewBarangKeluar.Rows)
					{
						if (row.Cells[1].Value.ToString() == labelKodeBarang.Text.ToString())
						{
							keterangan = "Ada nama barang di datagridview";
							rowIndex = row.Index;
						}
						else
						{
							keterangan = "Tidak ada nama barang di datagridview";
						}
					}

					if (keterangan == "Ada nama barang di datagridview")
					{
						MessageBox.Show("Sudah ada nama barang di tabel");

						keterangan = "";

						kosongkanInfoBarang();

					}
					else
					{
						dataGridViewBarangKeluar.Rows.Add(labelIdBarang.Text, labelKodeBarang.Text, labelNamaBarang.Text, labelSatuan.Text, hargaJual, numericUpDownJumlahBarangKeluar.Value, "X");

						keterangan = "";

						kosongkanInfoBarang();

						labelTotalHargaBarangKeluar.Text = convertToRupiah(hitungGrandTotal());
					}
				}
			}
		}

		private void dataGridViewBarangKeluar_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridViewBarangKeluar.Columns[e.ColumnIndex].Name == "ColumnAction")
			{
				if (MessageBox.Show("Apakah anda yakin mau menghapus data ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					//if (numericUpDownTotalDiskonBeli.Value == 0)
					//{
					dataGridViewBarangKeluar.Rows.RemoveAt(dataGridViewBarangKeluar.CurrentCell.RowIndex);

					labelTotalHargaBarangKeluar.Text = convertToRupiah(hitungGrandTotal());
					//}
					//else
					//{
					//	int afterDiskon = hitungDiskon(hitungGrandTotal(), Convert.ToInt32(numericUpDownTotalDiskonBeli.Value));
					//	int hargaBeli = Convert.ToInt32(dataGridViewBarangMasuk.Rows[dataGridViewBarangMasuk.CurrentCell.RowIndex].Cells[4].Value);
					//	int jumlah = Convert.ToInt32(dataGridViewBarangMasuk.Rows[dataGridViewBarangMasuk.CurrentCell.RowIndex].Cells[5].Value);
					//	int total = hargaBeli * jumlah;
					//	int hasil = afterDiskon - total;

					//	labelTotalHargaBarangMasuk.Text = convertToRupiah(hasil);

					//	numericUpDownTotalDiskonBeli.Enabled = false;

					//	dataGridViewBarangMasuk.Rows.RemoveAt(dataGridViewBarangMasuk.CurrentCell.RowIndex);
					//}
				}
			}
		}

		private void numericUpDownTotalDiskonJual_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				int hasil = hitungDiskon(hitungGrandTotal(), Convert.ToInt32(numericUpDownTotalDiskonJual.Value));
				labelTotalHargaBarangKeluar.Text = convertToRupiah(hasil);

				numericUpDownTotalDiskonJual.Enabled = false;
			}
		}

		private void buttonSimpan_Click(object sender, EventArgs e)
		{
			if (textBoxNoFaktur.Text == "" || comboBoxPelanggan.Text == "" || dataGridViewBarangKeluar.RowCount == 0)
			{
				MessageBox.Show("Harap isi informasi nota jual secara lengkap", "Warning");
			}
			else
			{
				List<NotaJualDetil> listNotaJualDetil = new List<NotaJualDetil>();

				foreach (DataGridViewRow row in dataGridViewBarangKeluar.Rows)
				{
					Barang b = new Barang();
					b.IdBarang = Convert.ToInt32(row.Cells[0].Value);

					NotaJual nj = new NotaJual();
					nj.NoFaktur = textBoxNoFaktur.Text;

					NotaJualDetil njd = new NotaJualDetil();
					njd.Barang = b;
					njd.NotaJual = nj;
					njd.Qty = Convert.ToInt32(row.Cells[5].Value);
					njd.SubTotal = Convert.ToInt32(row.Cells[4].Value);
					njd.Total = hitungDiskon(hitungGrandTotal(), Convert.ToInt32(numericUpDownTotalDiskonJual.Value));
					njd.DiskonPersen = Convert.ToInt32(numericUpDownTotalDiskonJual.Value);

					listNotaJualDetil.Add(njd);
				}

				Pelanggan p = new Pelanggan();
				p.IdPelanggan = int.Parse(comboBoxPelanggan.Text.Split('-')[0]);
				p.Nama = comboBoxPelanggan.Text.Split('-')[1];

				User u = new User();
				u.IdUser = idUser;

				NotaJual n = new NotaJual();
				n.NoFaktur = textBoxNoFaktur.Text.ToString();
				n.Tanggal = dateTimePickerTanggalNotaJual.Value;
				n.Pelanggan = p;
				n.User = u;
				n.ListNotaJualDetil = listNotaJualDetil;

				string hasil = NotaJual.TambahData(n, this.mainForm.listNotaJual);

				if (hasil == "1")
				{
					MessageBox.Show("Data berhasil ditambahkan");

					this.mainForm.textBoxSearchBarangKeluar.Clear();
					this.mainForm.FormMaster_Load(buttonSimpan, e);
					this.Close();
				}
				else
				{
					MessageBox.Show(hasil);
				}
			}
		}
	}
}

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
	public partial class FormTambahNotaBeli : Form
	{
		public FormTambahNotaBeli()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;

		private List<Supplier> listSupplier = new List<Supplier>();

		private string keterangan = "";

		private int rowIndex = 0;

		private int idUser = 0;

		public FormTambahNotaBeli(Form callingForm, int pIdUser)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();

			idUser = pIdUser;
		}

		private void FormTambahNotaBeli_Load(object sender, EventArgs e)
		{
			textBoxNoFaktur.Select();

			string hasilSupplier = Supplier.BacaData("", "", listSupplier);

			if (hasilSupplier == "1")
			{
				foreach (var item in listSupplier)
				{
					comboBoxSupplier.Items.Add(item.IdSupplier + " - " + item.Nama);
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

		private void textBoxKodeBarangMasuk_KeyDown(object sender, KeyEventArgs e)
		{
			List<Barang> listBarang = new List<Barang>();

			if (e.KeyCode == Keys.Enter)
			{
				string barang = Barang.BacaDataBarang("cari barang", textBoxKodeBarangMasuk.Text.ToString(), listBarang);

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
			if (listBarang[0].Foto == "")
			{
				iconPictureBoxBarangMasuk.Image = Resources.box;
			}
			else
			{
				string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\barang";
				string folderName = Path.Combine(projectPath, textBoxKodeBarangMasuk.Text);
				iconPictureBoxBarangMasuk.ImageLocation = folderName + "\\" + "foto" + listBarang[0].Foto;
			}

			labelIdBarang.Text = listBarang[0].IdBarang.ToString();
			labelKodeBarang.Text = listBarang[0].KodeBarang.ToString();	
			labelNamaBarang.Text = listBarang[0].Nama.ToString();
			labelKategoriBarang.Text = listBarang[0].Kategori.Nama;
			labelMerekBarang.Text = listBarang[0].Merek.Nama;
			labelJenisBarang.Text = listBarang[0].Jenis.Nama;
			labelJumlahStok.Text = listBarang[0].JumlahStok.ToString();
			labelSatuan.Text = listBarang[0].Satuan;
		}

		private void numericUpDownTotalDiskonBeli_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				int hasil = hitungDiskon(hitungGrandTotal(), Convert.ToInt32(numericUpDownTotalDiskonBeli.Value));
				labelTotalHargaBarangMasuk.Text = convertToRupiah(hasil);

				numericUpDownTotalDiskonBeli.Enabled = false;
			}
		}

		private void numericUpDownJumlahBarangMasuk_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (labelKodeBarang.Text == "-")
				{
					MessageBox.Show("Info barang tidak dapat disimpan");
				} 
				else 
				{
					//int subTotal = Convert.ToInt32(numericUpDownHargaBeli.Value * numericUpDownJumlahBarangMasuk.Value);

					foreach (DataGridViewRow row in dataGridViewBarangMasuk.Rows)
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
						dataGridViewBarangMasuk.Rows.Add(labelIdBarang.Text, labelKodeBarang.Text, labelNamaBarang.Text, labelSatuan.Text, numericUpDownHargaBeli.Value, numericUpDownJumlahBarangMasuk.Value, "X");

						keterangan = "";

						kosongkanInfoBarang();

						labelTotalHargaBarangMasuk.Text = convertToRupiah(hitungGrandTotal());
					}
				}
			}
		}

		int hitungGrandTotal()
		{
			int totalHarga = 0;

			for (int i = 0; i < dataGridViewBarangMasuk.Rows.Count; i++)
			{
				int subTotal = Convert.ToInt32(dataGridViewBarangMasuk.Rows[i].Cells[4].Value);
				totalHarga = totalHarga + subTotal * Convert.ToInt32(dataGridViewBarangMasuk.Rows[i].Cells[5].Value);
				
			}

			return totalHarga;

		}

		public void kosongkanInfoBarang()
		{
			textBoxKodeBarangMasuk.Clear();
			iconPictureBoxBarangMasuk.Image = null;
			labelKodeBarang.Text = "-";
			labelNamaBarang.Text = "-";
			labelKategoriBarang.Text = "-";
			labelMerekBarang.Text = "-";
			labelJenisBarang.Text = "-";
			labelSatuan.Text = "-";
			labelJumlahStok.Text = "-";
			numericUpDownHargaBeli.Value = 0;
			numericUpDownJumlahBarangMasuk.Value = 1;
		}

		private void dataGridViewBarangMasuk_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridViewBarangMasuk.Columns[e.ColumnIndex].Name == "ColumnAction")
			{
				if (MessageBox.Show("Apakah anda yakin mau menghapus data ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					//if (numericUpDownTotalDiskonBeli.Value == 0)
					//{
					dataGridViewBarangMasuk.Rows.RemoveAt(dataGridViewBarangMasuk.CurrentCell.RowIndex);

					labelTotalHargaBarangMasuk.Text = convertToRupiah(hitungGrandTotal());
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

		private void buttonSimpan_Click(object sender, EventArgs e)
		{
			if (textBoxNoFaktur.Text == "" || comboBoxSupplier.Text == "" || dataGridViewBarangMasuk.RowCount == 0)
			{
				MessageBox.Show("Harap isi informasi nota beli secara lengkap");
			}
			else 
			{
				List<NotaBeli> lstNotaBeli = new List<NotaBeli>();
				NotaBeli.BacaData("cek no faktur", textBoxNoFaktur.Text.ToString(), "", lstNotaBeli);

				if (lstNotaBeli.Count == 0)
				{
					List<NotaBeliDetil> listNotaBeliDetil = new List<NotaBeliDetil>();

					foreach (DataGridViewRow row in dataGridViewBarangMasuk.Rows)
					{
						Barang b = new Barang();
						b.IdBarang = Convert.ToInt32(row.Cells[0].Value);

						NotaBeli nb = new NotaBeli();
						nb.NoFaktur = textBoxNoFaktur.Text;

						NotaBeliDetil nbd = new NotaBeliDetil();
						nbd.Barang = b;
						nbd.NotaBeli = nb;
						nbd.Qty = Convert.ToInt32(row.Cells[5].Value);
						nbd.SubTotal = Convert.ToInt32(row.Cells[4].Value);
						nbd.Total = hitungDiskon(hitungGrandTotal(), Convert.ToInt32(numericUpDownTotalDiskonBeli.Value));
						nbd.DiskonPersen = Convert.ToInt32(numericUpDownTotalDiskonBeli.Value);

						listNotaBeliDetil.Add(nbd);
					}

					Supplier s = new Supplier();
					s.IdSupplier = int.Parse(comboBoxSupplier.Text.Split('-')[0]);
					s.Nama = comboBoxSupplier.Text.Split('-')[1];

					User u = new User();
					u.IdUser = idUser;

					NotaBeli n = new NotaBeli();
					n.NoFaktur = textBoxNoFaktur.Text.ToString();
					n.Tanggal = dateTimePickerTanggalNotaBeli.Value;
					n.Supplier = s;
					n.User = u;
					n.ListNotaBeliDetil = listNotaBeliDetil;

					string hasil = NotaBeli.TambahData(n);

					if (hasil == "1")
					{
						MessageBox.Show("Data berhasil ditambahkan");

						this.mainForm.textBoxSearchBarangMasuk.Clear();
						this.mainForm.PopulateNotaBeliTable("", "", "");
						this.Close();
					}
					else
					{
						MessageBox.Show(hasil);
					}
				}
				else
				{
					MessageBox.Show("Nomor faktur sudah ada. Harap masukkan nomor faktur yang berbeda");
				}
				
			}
		}
	}
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Kawi_Agung
{
	public class NotaBeli
	{
		#region DATAMEMBERS

		private int idNotaBeli;
		private string noFaktur;
		private DateTime tanggal;
		private Supplier supplier;
		private User user;
		private List<NotaBeliDetil> listNotaBeliDetil;
		private int totalPengeluaran;

		#endregion

		#region PROPERTIES

		public int IdNotaBeli { get => idNotaBeli; set => idNotaBeli = value; }
		public string NoFaktur { get => noFaktur; set => noFaktur = value; }
		public DateTime Tanggal { get => tanggal; set => tanggal = value; }
		public Supplier Supplier { get => supplier; set => supplier = value; }
		public User User { get => user; set => user = value; }
		public int TotalPengeluaran { get => totalPengeluaran; set => totalPengeluaran = value; }
		public List<NotaBeliDetil> ListNotaBeliDetil { get => listNotaBeliDetil; set => listNotaBeliDetil = value; }

		#endregion

		#region CONSTRUCTORS

		public NotaBeli()
		{
			IdNotaBeli = 0;
			NoFaktur = "";
			TotalPengeluaran = 0;
			ListNotaBeliDetil = new List<NotaBeliDetil>();
		}

		public NotaBeli(int pIdNotaBeli, string pNoFaktur, DateTime pTanggal, Supplier pSupplier, User pUser, List<NotaBeliDetil> pListNotaBeliDetil)
		{
			IdNotaBeli = pIdNotaBeli;
			NoFaktur = pNoFaktur;
			Tanggal = pTanggal;
			Supplier = pSupplier;
			User = pUser;
			ListNotaBeliDetil = pListNotaBeliDetil;
		}

		#endregion

		#region METHODS

		public static string BacaData(string kriteria, string nilaiKriteria, string nilaiKriteria2, List<NotaBeli> listNotaBeli)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT nb.idnota_beli, nb.no_faktur, nb.tanggal, s.nama, s.alamat, s.no_telp, u.nama FROM nota_beli nb INNER JOIN supplier s ON nb.idsupplier=s.idsupplier INNER JOIN user u ON nb.iduser=u.iduser ORDER BY nb.tanggal";
			}
			else if (kriteria == "nb.tanggal")
			{
				sql = "SELECT nb.idnota_beli, nb.no_faktur, nb.tanggal, s.nama, s.alamat, s.no_telp, u.nama FROM nota_beli nb INNER JOIN supplier s ON nb.idsupplier=s.idsupplier INNER JOIN user u ON nb.iduser=u.iduser WHERE " + kriteria + " BETWEEN '" + nilaiKriteria + "' AND '" + nilaiKriteria2 + "' ORDER BY nb.tanggal";
			}
			else
			{
				sql = "SELECT nb.idnota_beli, nb.no_faktur, nb.tanggal, s.nama, s.alamat, s.no_telp, u.nama FROM nota_beli nb INNER JOIN supplier s ON nb.idsupplier=s.idsupplier INNER JOIN user u ON nb.iduser=u.iduser WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY nb.tanggal";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Supplier s = new Supplier();
					s.Nama = hasil.GetValue(3).ToString();
					s.Alamat = hasil.GetValue(4).ToString();
					s.NoTelp = hasil.GetValue(5).ToString();

					User u = new User();
					u.Nama = hasil.GetValue(6).ToString();

					NotaBeli notaBeli = new NotaBeli();

					notaBeli.IdNotaBeli = Convert.ToInt32(hasil.GetValue(0));
					notaBeli.NoFaktur = hasil.GetValue(1).ToString();
					notaBeli.Tanggal = DateTime.Parse(hasil.GetValue(2).ToString());
					notaBeli.Supplier = s;
					notaBeli.User = u;

					listNotaBeli.Add(notaBeli);
				}
				return "1";
			}
			catch (Exception ex)
			{
				return "Terjadi Kesalahan. Pesan Kesalahan : " + ex.Message;
			}
			finally
			{
				cmd.Dispose();
				hasil.Dispose();
			}
		}

		public static string JalankanPerintahDML(string pSql)
		{
			try
			{
				Koneksi k = new Koneksi();
				k.Connect();

				MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);

				c.ExecuteNonQuery();

				return "1";
			}
			catch (MySqlException e)
			{
				return e.Message;
			}
		}

		public static string JalankanPerintahDML2(string pSql)
		{
			try
			{
				Koneksi k = new Koneksi();
				k.Connect();

				MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);

				return c.ExecuteScalar().ToString();

			}
			catch (MySqlException e)
			{
				return e.Message;
			}
		}

		public static string TambahData(NotaBeli notaBeli, List<NotaBeli> listNB)
		{
			using (var transcope = new TransactionScope(TransactionScopeOption.RequiresNew))
			{
				// tuliskan perintah SQL 1 : menambahkan nota beli ke tabel Nota Beli
				string sql1 = "INSERT INTO nota_beli(no_faktur, tanggal, idsupplier, iduser) VALUES('" + notaBeli.NoFaktur + "', '" + notaBeli.Tanggal.ToString("yyyy-MM-dd") + "', " + notaBeli.Supplier.IdSupplier + ", " + notaBeli.User.IdUser + "); SELECT last_insert_id();";

				try
				{
					for (int i = 0; i < listNB.Count; i++)
					{
						if (notaBeli.NoFaktur.ToLower() == listNB[i].NoFaktur.ToLower())
						{
							return "Nomor Faktur sudah ada. Harap masukkan nomor faktur yang lain";
						}
					}

					// menjalankan perintah untuk menambahkan ke tabel nota beli
					string idNotaBeli = JalankanPerintahDML2(sql1);

					string hasilUpdateStok = "";

					if (idNotaBeli != null)
					{
						// mendapatkan semua barang yang terjual dalam nota (nota beli detil)
						for (int i = 0; i < notaBeli.ListNotaBeliDetil.Count; i++)
						{
							// tuliskan perintah SQL 2 : menambahkan barang-barang yang terbeli ke Nota Beli Detil
							string sql2 = "INSERT INTO nota_beli_detil(idbarang, idnota_beli, qty, sub_total, total, diskon_persen) VALUES(" + notaBeli.ListNotaBeliDetil[i].Barang.IdBarang + ", " + idNotaBeli + ", " + notaBeli.ListNotaBeliDetil[i].Qty + ", " + notaBeli.ListNotaBeliDetil[i].SubTotal + ", " + notaBeli.ListNotaBeliDetil[i].Total + ", " + notaBeli.ListNotaBeliDetil[i].DiskonPersen + ")";

							// menjalankan perintah untuk menambahkan ke tabel nota beli detil
							string hasilTambahNotaBeliDetil = JalankanPerintahDML(sql2);

							if (hasilTambahNotaBeliDetil == "1")
							{
								// panggil method untuk mengupdate/menambah stok barang
								hasilUpdateStok = Barang.TambahStok(notaBeli.ListNotaBeliDetil[i].Barang.IdBarang, notaBeli.ListNotaBeliDetil[i].Qty);

							}
							else
							{
								transcope.Dispose();

								return hasilTambahNotaBeliDetil + " HASIL TAMBAH NOTA BELI DETIL ";
							}
						}
					}
					else
					{
						transcope.Dispose();

						return "Terjadi Kesalahan : " + idNotaBeli;

					}

					if (hasilUpdateStok != "1")
					{
						transcope.Dispose();

						return hasilUpdateStok + " HASIL TAMBAH STOK ";
					}
					else
					{
						transcope.Complete();

						return "1";
					}

					// jika semua perintah DML berhasil dijalankan
					transcope.Dispose();
					return idNotaBeli.ToString() + " HASIL TAMBAH NOTA BELI ";
				}
				catch (Exception e)
				{
					// jika ada kegagalan perintah DML
					transcope.Dispose();

					return e.Message;
				}
			}
		}

		public static List<string> HapusData(List<NotaBeli> listNotaBeli)
		{
			List<string> listKeterangan = new List<string>();
			string sql1 = "";
			string sql2 = "";
			string sql3 = "";

			foreach (NotaBeli nb in listNotaBeli)
			{
				sql1 = "DELETE FROM nota_beli_detil WHERE idnota_beli=" + nb.IdNotaBeli;
				sql2 = "DELETE FROM nota_beli WHERE idnota_beli=" + nb.IdNotaBeli;

				List<NotaBeliDetil> listNBD = new List<NotaBeliDetil>();

				string hasilBacaNotaDetil = NotaBeliDetil.BacaData("nb.idnota_beli", nb.IdNotaBeli.ToString(), listNBD);

				try
				{
					Koneksi k = new Koneksi();
					k.Connect();

					MySqlCommand cmd1 = new MySqlCommand(sql1, k.KoneksiDB);
					MySqlCommand cmd2 = new MySqlCommand(sql2, k.KoneksiDB);
					MySqlCommand cmd3 = new MySqlCommand(sql3, k.KoneksiDB);

					if (hasilBacaNotaDetil == "1")
					{
						int idBarang = listNBD[0].Barang.IdBarang;
						int	qty = listNBD[0].Qty;

						sql3 = "UPDATE barang SET jumlah_stok=jumlah_stok-" + qty + " WHERE idbarang=" + idBarang;
					}

					MySqlCommand cmd4 = new MySqlCommand(sql3, k.KoneksiDB);

					cmd4.ExecuteNonQuery(); 
					cmd1.ExecuteNonQuery();
					cmd2.ExecuteNonQuery();


					listKeterangan.Add("berhasil");
				}
				catch (MySqlException ex)
				{
					if (ex.Number == 1451)
					{
						listKeterangan.Add("data barang masih ada di nota pembelian atau nota penjualan");
					}
					else
					{
						listKeterangan.Add(ex.Message);
					}

					// error sql lain selain error diatas belum direkam
				}
			}
			return listKeterangan;
		}

		public static string CetakNota(string pKriteria, string pNilaiKriteria, string pNilaiKriteria2, string pNamaFile)
		{
			try
			{
				List<NotaBeli> listNotaBeli = new List<NotaBeli>();

				// baca data nota tertentu yang akan dicetak
				string hasilBaca = NotaBeli.BacaData(pKriteria, pNilaiKriteria, pNilaiKriteria2, listNotaBeli);

				// simpan dulu isi nota yang akan ditampilkan ke objek file (StreamWriter)
				StreamWriter file = new StreamWriter(pNamaFile);

				for (int i = 0; i < listNotaBeli.Count; i++)
				{
					// tampilkan info perusahaan
					file.WriteLine("");
					file.WriteLine("UD. Kawi Agung");
					file.WriteLine("Jl. Bungurasih Tengah No. 03B");
					file.WriteLine("031-9551739-8537536");
					file.WriteLine("SURABAYA");
					file.WriteLine("=".PadRight(50, '='));

					// tampilkan header nota
					file.WriteLine("No. Faktur : " + listNotaBeli[i].NoFaktur);
					file.WriteLine("Tanggal : " + listNotaBeli[i].Tanggal.ToString("dd MMMM yyyy"));
					file.WriteLine("");
					file.WriteLine("Supplier : " + listNotaBeli[i].Supplier.Nama);
					file.WriteLine("Alamat : " + listNotaBeli[i].Supplier.Alamat);
					file.WriteLine("No. Telp : " + listNotaBeli[i].Supplier.NoTelp);
					file.WriteLine("=".PadRight(50, '='));

					
				}

				List<NotaBeliDetil> listNBD = new List<NotaBeliDetil>();
				NotaBeliDetil.BacaData("nb.no_faktur", listNotaBeli[0].NoFaktur, listNBD);

				// tampilkan barang yang dibeli 
				int grandTotal = 0; // untuk menampilkan grand total nota
				for (int j = 0; j > listNBD.Count; j++)
				{
					string nama = listNBD[j].Barang.Nama;
					// jika nama barang terlalu panjang maka hanya ambil 30 karakter awal saja
					if (nama.Length > 30)
					{
						nama = nama.Substring(0, 30);
					}
					int jumlah = listNBD[j].Qty;
					int subTotal = listNBD[j].SubTotal;
					int harga = subTotal / jumlah;
					file.WriteLine(nama.PadRight(30, ' '));
					file.WriteLine(jumlah.ToString().PadRight(3, ' '));
					file.WriteLine(harga.ToString("0,###").PadLeft(7, ' ')); // agar harga ditampilkan dengan pemisah ribuan
					file.WriteLine(subTotal.ToString("0,###").PadLeft(10, ' ')); // agar sub total  ditampilkan dengan pemisah ribuan
					file.WriteLine("");
					//hitung grandtotal nota
					grandTotal = grandTotal + jumlah * harga;
				}
				file.WriteLine("=".PadRight(50, '='));
				file.WriteLine("TOTAL : " + grandTotal.ToString("0, ###"));
				file.WriteLine("=".PadRight(50, '='));
				file.WriteLine("");
				file.Close();
				// cetak ke printer 
				Cetak c = new Cetak(pNamaFile, "Courier New", 9, 10, 10, 10, 10);
				c.CetakKePrinter("tulisan");
				return "1";

			}
			catch (MySqlException e)
			{
				return e.Message;
			}
		}

		public static string BacaDataTotalPengeluaran(string tanggalAwal, string tanggalAkhir, List<NotaBeli> listNotaBeli)
		{
			string sql = "select nb.tanggal, sum(nb.total) as pengeluaran from (select distinct nb.tanggal, nbd.total from nota_beli nb inner join nota_beli_detil nbd on nb.idnota_beli = nbd.idnota_beli where nb.tanggal between '" + tanggalAwal + "' and '" + tanggalAkhir + "') as nb group by nb.tanggal";
			Koneksi conn = new Koneksi();

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					NotaBeli nb = new NotaBeli();
					nb.Tanggal = DateTime.Parse(hasil.GetValue(0).ToString());
					nb.TotalPengeluaran = Convert.ToInt32(hasil.GetValue(1));

					listNotaBeli.Add(nb);
				}
				return "1";
			}
			catch (Exception ex)
			{
				return "Terjadi Kesalahan. Pesan Kesalahan : " + ex.Message;
			}
			finally
			{
				cmd.Dispose();
				hasil.Dispose();
			}
		}

		#endregion
	}
}

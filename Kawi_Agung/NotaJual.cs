using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Kawi_Agung
{
	public class NotaJual
	{
		#region DATA MEMBERS

		private int idNotaJual;
		private string noFaktur;
		private DateTime tanggal;
		private Pelanggan pelanggan;
		private User user;
		private int totalPemasukan;
		private List<NotaJualDetil> listNotaJualDetil;

		#endregion

		#region PROPERTIES

		public int IdNotaJual { get => idNotaJual; set => idNotaJual = value; }
		public string NoFaktur { get => noFaktur; set => noFaktur = value; }
		public DateTime Tanggal { get => tanggal; set => tanggal = value; }
		public Pelanggan Pelanggan { get => pelanggan; set => pelanggan = value; }
		public User User { get => user; set => user = value; }
		public int TotalPemasukan { get => totalPemasukan; set => totalPemasukan = value; }
		public List<NotaJualDetil> ListNotaJualDetil { get => listNotaJualDetil; set => listNotaJualDetil = value; }

		#endregion

		#region CONSTRUCTORS

		public NotaJual()
		{
			IdNotaJual = 0;
			TotalPemasukan = 0;
			NoFaktur = "";
			ListNotaJualDetil = new List<NotaJualDetil>();
		}

		public NotaJual(int pIdNotaJual, string pNoFaktur, DateTime pTanggal, Pelanggan pPelanggan, User pUser, List<NotaJualDetil> pListNotaJualDetil)
		{
			IdNotaJual = pIdNotaJual;
			NoFaktur = pNoFaktur;
			Tanggal = pTanggal;
			Pelanggan = pPelanggan;
			User = pUser;
			ListNotaJualDetil = pListNotaJualDetil;
		}

		#endregion

		#region METHODS

		public static string BacaData(string kriteria, string nilaiKriteria, string nilaiKriteria2, List<NotaJual> listNotaJual)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT nj.idnota_jual, nj.no_faktur, nj.tanggal, p.nama, u.nama FROM nota_jual nj INNER JOIN pelanggan p ON nj.idpelanggan=p.idpelanggan INNER JOIN user u ON nj.iduser=u.iduser ORDER BY nj.no_faktur";
			}
			else if (kriteria == "nj.tanggal")
			{
				sql = "SELECT nj.idnota_jual, nj.no_faktur, nj.tanggal, p.nama, u.nama FROM nota_jual nj INNER JOIN pelanggan p ON nj.idpelanggan=p.idpelanggan INNER JOIN user u ON nj.iduser=u.iduser WHERE " + kriteria + " BETWEEN '" + nilaiKriteria + "' AND '" + nilaiKriteria2 + "' ORDER BY nj.no_faktur";
			}
			else
			{
				sql = "SELECT nj.idnota_jual, nj.no_faktur, nj.tanggal, p.nama, u.nama FROM nota_jual nj INNER JOIN pelanggan p ON nj.idpelanggan=p.idpelanggan INNER JOIN user u ON nj.iduser=u.iduser WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY nj.no_faktur";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Pelanggan p = new Pelanggan();
					p.Nama = hasil.GetValue(3).ToString();

					User u = new User();
					u.Nama = hasil.GetValue(4).ToString();

					NotaJual notaJual = new NotaJual();

					notaJual.IdNotaJual = Convert.ToInt32(hasil.GetValue(0));
					notaJual.NoFaktur = hasil.GetValue(1).ToString();
					notaJual.Tanggal = DateTime.Parse(hasil.GetValue(2).ToString());
					notaJual.Pelanggan = p;
					notaJual.User = u;

					listNotaJual.Add(notaJual);
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

		public static string TambahData(NotaJual notaJual, List<NotaJual> listNJ)
		{
			using (var transcope = new TransactionScope(TransactionScopeOption.RequiresNew))
			{
				// tuliskan perintah SQL 1 : menambahkan nota beli ke tabel Nota Beli
				string sql1 = "INSERT INTO nota_jual(no_faktur, tanggal, idpelanggan, iduser) VALUES('" + notaJual.NoFaktur + "', '" + notaJual.Tanggal.ToString("yyyy-MM-dd") + "', " + notaJual.Pelanggan.IdPelanggan + ", " + notaJual.User.IdUser + "); SELECT last_insert_id();";

				try
				{
					for (int i = 0; i < listNJ.Count; i++)
					{
						if (notaJual.NoFaktur.ToLower() == listNJ[i].NoFaktur.ToLower())
						{
							return "Nomor Faktur sudah ada. Harap masukkan nomor faktur yang lain";
						}
					}

					// menjalankan perintah untuk menambahkan ke tabel nota jual
					string idNotaJual = JalankanPerintahDML2(sql1);

					string hasilUpdateStok = "";

					if (idNotaJual != null)
					{
						// mendapatkan semua barang yang terjual dalam nota (nota beli detil)
						for (int i = 0; i < notaJual.ListNotaJualDetil.Count; i++)
						{
							// tuliskan perintah SQL 2 : menambahkan barang-barang yang terbeli ke Nota Jual Detil
							string sql2 = "INSERT INTO nota_jual_detil(idbarang, idnota_jual, qty, sub_total, total, diskon_persen) VALUES(" + notaJual.ListNotaJualDetil[i].Barang.IdBarang + ", " + idNotaJual + ", " + notaJual.ListNotaJualDetil[i].Qty + ", " + notaJual.ListNotaJualDetil[i].SubTotal + ", " + notaJual.ListNotaJualDetil[i].Total + ", " + notaJual.ListNotaJualDetil[i].DiskonPersen + ")";

							// menjalankan perintah untuk menambahkan ke tabel nota beli detil
							string hasilTambahNotaJualDetil = JalankanPerintahDML(sql2);

							if (hasilTambahNotaJualDetil == "1")
							{
								// panggil method untuk mengupdate/mengurangi stok barang
								hasilUpdateStok = Barang.KurangiStok(notaJual.ListNotaJualDetil[i].Barang.IdBarang, notaJual.ListNotaJualDetil[i].Qty);

							}
							else
							{
								transcope.Dispose();

								return hasilTambahNotaJualDetil + " HASIL TAMBAH NOTA JUAL DETIL ";
							}
						}
					}
					else
					{
						transcope.Dispose();

						return "Terjadi Kesalahan : " + idNotaJual;

					}

					if (hasilUpdateStok != "1")
					{
						transcope.Dispose();

						return hasilUpdateStok + " HASIL KURANGI STOK ";
					}
					else
					{
						transcope.Complete();

						return "1";
					}

					// jika semua perintah DML berhasil dijalankan
					transcope.Dispose();
					return idNotaJual.ToString() + " HASIL TAMBAH NOTA BELI ";
				}
				catch (Exception e)
				{
					// jika ada kegagalan perintah DML
					transcope.Dispose();

					return e.Message;
				}
			}
		}

		public static List<string> HapusData(List<NotaJual> listNotaJual)
		{
			List<string> listKeterangan = new List<string>();
			string sql1 = "";
			string sql2 = "";
			string sql3 = "";

			foreach (NotaJual nj in listNotaJual)
			{
				sql1 = "DELETE FROM nota_jual_detil WHERE idnota_jual=" + nj.IdNotaJual;
				sql2 = "DELETE FROM nota_jual WHERE idnota_jual=" + nj.IdNotaJual;

				List<NotaJualDetil> listNJD = new List<NotaJualDetil>();

				string hasilBacaNotaDetil = NotaJualDetil.BacaData("nj.idnota_jual", nj.IdNotaJual.ToString(), listNJD);

				try
				{
					Koneksi k = new Koneksi();
					k.Connect();

					MySqlCommand cmd1 = new MySqlCommand(sql1, k.KoneksiDB);
					MySqlCommand cmd2 = new MySqlCommand(sql2, k.KoneksiDB);
					MySqlCommand cmd3 = new MySqlCommand(sql3, k.KoneksiDB);

					if (hasilBacaNotaDetil == "1")
					{
						int idBarang = listNJD[0].Barang.IdBarang;
						int qty = listNJD[0].Qty;

						sql3 = "UPDATE barang SET jumlah_stok=jumlah_stok+" + qty + " WHERE idbarang=" + idBarang;
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

		public static string BacaDataTotalPemasukan(string tanggalAwal, string tanggalAkhir, List<NotaJual> listNotaJual)
		{
			string sql = "select nj.tanggal, sum(nj.total) as pemasukan from (select distinct nj.tanggal, njd.total from nota_jual nj inner join nota_jual_detil njd on nj.idnota_jual = njd.idnota_jual where nj.tanggal between '" + tanggalAwal + "' and '" + tanggalAkhir + "') as nj group by nj.tanggal";
			Koneksi conn = new Koneksi();

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					NotaJual nj = new NotaJual();
					nj.Tanggal = DateTime.Parse(hasil.GetValue(0).ToString());
					nj.TotalPemasukan = Convert.ToInt32(hasil.GetValue(1));

					listNotaJual.Add(nj);
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

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Kawi_Agung
{
	public class NotaBeli
	{
		#region DATAMEMBERS

		private int no;
		private string noFaktur;
		private DateTime tanggal;
		private Supplier supplier;
		private User user;
		private List<NotaBeliDetil> listNotaBeliDetil;

		#endregion

		#region PROPERTIES

		public int No { get => no; set => no = value; }
		public string NoFaktur { get => noFaktur; set => noFaktur = value; }
		public DateTime Tanggal { get => tanggal; set => tanggal = value; }
		public Supplier Supplier { get => supplier; set => supplier = value; }
		public User User { get => user; set => user = value; }
		public List<NotaBeliDetil> ListNotaBeliDetil { get => listNotaBeliDetil; set => listNotaBeliDetil = value; }

		#endregion

		#region CONSTRUCTORS

		public NotaBeli()
		{
			No = 0;
			NoFaktur = "";
			ListNotaBeliDetil = new List<NotaBeliDetil>();
		}

		public NotaBeli(int pNo, string pNoFaktur, DateTime pTanggal, Supplier pSupplier, User pUser, List<NotaBeliDetil> pListNotaBeliDetil)
		{
			No = pNo;
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
				sql = "SELECT nb.no_faktur, nb.tanggal, s.nama, u.nama FROM nota_beli nb INNER JOIN supplier s ON nb.idsupplier=s.idsupplier INNER JOIN user u ON nb.iduser=u.iduser ORDER BY nb.no_faktur";
			}
			else if (kriteria == "nb.tanggal")
			{
				sql = "SELECT nb.no_faktur, nb.tanggal, s.nama, u.nama FROM nota_beli nb INNER JOIN supplier s ON nb.idsupplier=s.idsupplier INNER JOIN user u ON nb.iduser=u.iduser WHERE " + kriteria + " BETWEEN '" + nilaiKriteria + "' AND '" + nilaiKriteria2 + "' ORDER BY nb.no_faktur";
			}
			else
			{
				sql = "SELECT nb.no_faktur, nb.tanggal, s.nama, u.nama FROM nota_beli nb INNER JOIN supplier s ON nb.idsupplier=s.idsupplier INNER JOIN user u ON nb.iduser=u.iduser WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY nb.no_faktur";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Supplier s = new Supplier();
					s.Nama = hasil.GetValue(2).ToString();

					User u = new User();
					u.Nama = hasil.GetValue(3).ToString();

					NotaBeli notaBeli = new NotaBeli();

					notaBeli.NoFaktur = hasil.GetValue(0).ToString();
					notaBeli.Tanggal = DateTime.Parse(hasil.GetValue(1).ToString());
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

		public static string TambahData(NotaBeli notaBeli)
		{
			using (var transcope = new TransactionScope(TransactionScopeOption.RequiresNew))
			{
				// tuliskan perintah SQL 1 : menambahkan nota beli ke tabel Nota Beli
				string sql1 = "INSERT INTO nota_beli(no_faktur, tanggal, idsupplier, iduser) VALUES('" + notaBeli.NoFaktur + "', '" + notaBeli.Tanggal.ToString("yyyy-MM-dd") + "', " + notaBeli.Supplier.IdSupplier + ", " + notaBeli.User.IdUser + ")";

				try
				{
					// menjalankan perintah untuk menambahkan ke tabel nota beli
					string hasilTambahNotaBeli = JalankanPerintahDML(sql1);

					string hasilUpdateStok = "";

					if (hasilTambahNotaBeli == "1")
					{
						// mendapatkan semua barang yang terjual dalam nota (nota beli detil)
						for (int i = 0; i < notaBeli.ListNotaBeliDetil.Count; i++)
						{
							// tuliskan perintah SQL 2 : menambahkan barang-barang yang terbeli ke Nota Beli Detil
							string sql2 = "INSERT INTO nota_beli_detil(idbarang, no_faktur, qty, sub_total, total, diskon_persen) VALUES(" + notaBeli.ListNotaBeliDetil[i].IdBarang.IdBarang + ", '" + notaBeli.ListNotaBeliDetil[i].NoFaktur.NoFaktur + "', " + notaBeli.ListNotaBeliDetil[i].Qty + ", " + notaBeli.ListNotaBeliDetil[i].SubTotal + ", " + notaBeli.ListNotaBeliDetil[i].Total + ", " + notaBeli.ListNotaBeliDetil[i].DiskonPersen + ")";

							// menjalankan perintah untuk menambahkan ke tabel nota beli detil
							string hasilTambahNotaBeliDetil = JalankanPerintahDML(sql2);

							if (hasilTambahNotaBeliDetil == "1")
							{
								// panggil method untuk mengupdate/menambah stok barang
								hasilUpdateStok = Barang.TambahStok(notaBeli.ListNotaBeliDetil[i].IdBarang.IdBarang, notaBeli.ListNotaBeliDetil[i].Qty);

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

						if (hasilTambahNotaBeli.Contains("Duplicate entry"))
						{
							return "Terjadi Kesalahan : " + " No faktur yang anda masukkan sudah ada";
						}
						else
						{
							return "Terjadi Kesalahan : " + hasilTambahNotaBeli;

						}
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
					return hasilTambahNotaBeli + " HASIL TAMBAH NOTA BELI ";
				}
				catch (Exception e)
				{
					// jika ada kegagalan perintah DML
					transcope.Dispose();

					return e.Message;
				}
			}
		}

		#endregion
	}
}

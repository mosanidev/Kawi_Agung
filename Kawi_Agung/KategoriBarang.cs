using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class KategoriBarang
	{
		#region DATAMEMBERS

		private int idKategoriBarang;
		private string nama;

		#endregion

		#region PROPERTIES

		public int IdKategoriBarang { get => idKategoriBarang; set => idKategoriBarang = value; }
		public string Nama { get => nama; set => nama = value; }

		#endregion

		#region CONSTRUCTORS

		public KategoriBarang()
		{
			IdKategoriBarang = 0;
			Nama = "";
		}


		#endregion

		#region METHODS

		public static void JalankanPerintahDML(string pSql)
		{
			Koneksi k = new Koneksi();
			k.Connect();

			MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);

			c.ExecuteNonQuery();
		}

		public static string BacaData(string kriteria, string nilaiKriteria, List<KategoriBarang> listKategori)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT * FROM kategori_barang ORDER BY idkategori_barang";

			}
			else if (kriteria == "exclude") // kriteria khusus untuk perintah sql yang menyeleksi semua jenis barang terkecuali jenis tertentu
			{
				sql = "SELECT * FROM kategori_barang WHERE NOT nama='" + nilaiKriteria + "' ORDER BY idkategori_barang";
			}
			else
			{
				sql = "SELECT * FROM kategori_barang WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY idkategori_barang";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					KategoriBarang kategori = new KategoriBarang();

					kategori.IdKategoriBarang = int.Parse(hasil.GetValue(0).ToString());
					kategori.Nama = hasil.GetValue(1).ToString();

					listKategori.Add(kategori);
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

		public static string TambahData(KategoriBarang kategori, List<KategoriBarang> listKategori)
		{
			string sql = "INSERT INTO kategori_barang(nama) VALUES('" + kategori.Nama + "')";

			try
			{
				for (int i = 0; i < listKategori.Count; i++)
				{
					if (kategori.Nama.ToLower() == listKategori[i].Nama.ToLower())
					{
						return "Nama kategori sudah ada";
					}
				}

				JalankanPerintahDML(sql);
				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
		}

		public static string UbahData(KategoriBarang kategori, List<KategoriBarang> listKategori)
		{
			string sql = "UPDATE kategori_barang SET nama='" + kategori.Nama + "' WHERE idkategori_barang=" + kategori.IdKategoriBarang;

			try
			{
				for (int i = 0; i < listKategori.Count; i++)
				{
					if (kategori.Nama.ToLower() == listKategori[i].Nama.ToLower())
					{
						return "Nama kategori sudah ada";
					}
				}

				JalankanPerintahDML(sql);
				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
		}

		public static List<string> HapusData(List<KategoriBarang> listKategori)
		{
			List<string> listKeterangan = new List<string>();
			string sql = "";

			foreach (KategoriBarang kategori in listKategori)
			{
				sql = "DELETE FROM kategori_barang WHERE idkategori_barang=" + kategori.IdKategoriBarang;

				try
				{
					JalankanPerintahDML(sql);
					listKeterangan.Add("berhasil");
				}
				catch (MySqlException ex)
				{
					if (ex.Number == 1451)
					{
						listKeterangan.Add("masih ada barang dengan kategori tersebut di daftar barang");
					}

					// error sql lain selain error diatas belum direkam
				}
			}
			return listKeterangan;
		}
		#endregion
	}
}

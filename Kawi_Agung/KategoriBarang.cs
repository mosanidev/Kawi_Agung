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

		private int no; // untuk nomor di tabel
		private int idKategoriBarang;
		private string nama;

		#endregion

		#region PROPERTIES

		public int No { get => no; set => no = value; }
		public int IdKategoriBarang { get => idKategoriBarang; set => idKategoriBarang = value; }
		public string Nama { get => nama; set => nama = value; }

		#endregion

		#region CONSTRUCTORS

		public KategoriBarang()
		{
			No = 1;
			IdKategoriBarang = 0;
			Nama = "";
		}

		public KategoriBarang(string pNama)
		{
			Nama = pNama;
		}
		public KategoriBarang(int pIdKategoriBarang, string pNama)
		{
			IdKategoriBarang = pIdKategoriBarang;
			Nama = pNama;
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

			if (kriteria == "")
			{
				sql = "SELECT * FROM kategori_barang";

			}
			else
			{
				sql = "SELECT * FROM kategori_barang WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
			}
			try
			{
				MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

				int i = 1;
				while (hasil.Read())
				{
					KategoriBarang kategori = new KategoriBarang();

					kategori.No = i++;
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

		}

		public static string TambahData(KategoriBarang kategori)
		{
			string sql = "INSERT INTO kategori_barang(nama) VALUES('" + kategori.Nama + "')";

			try
			{
				JalankanPerintahDML(sql);
				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
		}

		public static string UbahData(KategoriBarang kategori)
		{
			string sql = "UPDATE kategori_barang SET nama='" + kategori.Nama + "' WHERE idkategori_barang=" + kategori.IdKategoriBarang;

			try
			{
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

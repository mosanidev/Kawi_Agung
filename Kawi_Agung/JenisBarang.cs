using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Kawi_Agung
{
	public class JenisBarang
	{
		#region DATAMEMBERS

		private int idJenisBarang;
		private string nama;

		#endregion

		#region PROPERTIES

		public int IdJenisBarang { get => idJenisBarang; set => idJenisBarang = value; }
		public string Nama { get => nama; set => nama = value; }

		#endregion

		#region CONSTRUCTORS
		public JenisBarang()
		{
			IdJenisBarang = 0;
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

		public static string BacaData(string kriteria, string nilaiKriteria, List<JenisBarang> listJenis)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT * FROM jenis_barang ORDER BY idjenis_barang";
			}
			else if (kriteria == "exclude") // kriteria khusus untuk perintah sql yang menyeleksi semua jenis barang terkecuali jenis tertentu
			{
				sql = "SELECT * FROM jenis_barang WHERE NOT nama='"  + nilaiKriteria + "' ORDER BY idjenis_barang";
			}
			else
			{
				sql = "SELECT * FROM jenis_barang WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY idjenis_barang";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					JenisBarang jenis = new JenisBarang();

					jenis.IdJenisBarang = int.Parse(hasil.GetValue(0).ToString());
					jenis.Nama = hasil.GetValue(1).ToString();

					listJenis.Add(jenis);
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

		public static string TambahData(JenisBarang jenis, List<JenisBarang> listJenis)
		{
			string sql = "INSERT INTO jenis_barang(nama) VALUES('" + jenis.Nama + "')";

			try
			{
				for (int i = 0; i < listJenis.Count; i++)
				{
					if (jenis.Nama.ToLower() == listJenis[i].Nama.ToLower())
					{
						return "Nama jenis sudah ada. Harap masukkan nama yang lain";
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

		public static string UbahData(JenisBarang jenis, List<JenisBarang> listJenis)
		{
			string sql = "UPDATE jenis_barang SET nama='" + jenis.Nama + "' WHERE idjenis_barang=" + jenis.idJenisBarang;

			try
			{
				for (int i = 0; i < listJenis.Count; i++)
				{
					if (jenis.Nama.ToLower() == listJenis[i].Nama.ToLower())
					{
						return "Nama jenis sudah ada. Harap masukkan nama yang lain";
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

		public static List<string> HapusData(List<JenisBarang> listJenis)
		{
			List<string> listKeterangan = new List<string>();
			string sql = "";

			foreach (JenisBarang jenis in listJenis)
			{
				sql = "DELETE FROM jenis_barang WHERE idjenis_barang=" + jenis.IdJenisBarang;

				try
				{
					JalankanPerintahDML(sql);
					listKeterangan.Add("berhasil");
				}
				catch (MySqlException ex)
				{
					if (ex.Number == 1451)
					{
						listKeterangan.Add("masih ada barang dengan jenis tersebut di daftar barang");
					}

					// error sql lain selain error diatas belum direkam
				}
			}
			return listKeterangan;
		}
		#endregion
	}
}

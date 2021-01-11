using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class MerekBarang
	{
		#region DATAMEMBERS

		private int idMerekBarang;
		private string nama;

		#endregion

		#region PROPERTIES
		public int IdMerekBarang { get => idMerekBarang; set => idMerekBarang = value; }
		public string Nama { get => nama; set => nama = value; }

		#endregion

		#region CONSTRUCTORS

		public MerekBarang()
		{
			IdMerekBarang = Interlocked.Increment(ref idMerekBarang);
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

		public static string BacaData(string kriteria, string nilaiKriteria, List<MerekBarang> listMerek)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT idmerek_barang, nama FROM merek_barang ORDER BY idmerek_barang";
			}
			else if (kriteria == "exclude") // kriteria khusus untuk perintah sql yang menyeleksi semua merek barang terkecuali merek tertentu
			{
				sql = "SELECT idmerek_barang, nama FROM merek_barang WHERE NOT nama='" + nilaiKriteria + "' ORDER BY idmerek_barang";
			}
			else
			{
				sql = "SELECT idmerek_barang, nama FROM merek_barang WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					MerekBarang merek = new MerekBarang();

					merek.IdMerekBarang = int.Parse(hasil.GetValue(0).ToString());
					merek.Nama = hasil.GetValue(1).ToString();

					listMerek.Add(merek);
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

		public static string TambahData(MerekBarang merek, List<MerekBarang> listMerek)
		{
			string sql = "INSERT INTO merek_barang(nama) VALUES('" + merek.Nama + "')";

			try
			{
				for (int i = 0; i < listMerek.Count; i++)
				{
					if (merek.Nama.ToLower() == listMerek[i].Nama.ToLower())
					{
						return "Nama merek sudah ada";
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

		public static string UbahData(MerekBarang merek, List<MerekBarang> listMerek)
		{
			string sql = "UPDATE merek_barang SET nama='" + merek.Nama + "' WHERE idmerek_barang=" + merek.IdMerekBarang;

			try
			{
				for (int i = 0; i < listMerek.Count; i++)
				{
					if (merek.Nama.ToLower() == listMerek[i].Nama.ToLower())
					{
						return "Nama merek sudah ada";
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

		public static List<string> Hapusdata(List<MerekBarang> listMerek)
		{
			List<string> listKeterangan = new List<string>();
			string sql = "";

			foreach (MerekBarang merek in listMerek)
			{
				sql = "DELETE FROM merek_barang WHERE idmerek_barang=" + merek.IdMerekBarang;

				try
				{
					JalankanPerintahDML(sql);
					listKeterangan.Add("berhasil");
				}
				catch (MySqlException ex)
				{
					if (ex.Number == 1451)
					{
						listKeterangan.Add("masih ada barang dengan merek tersebut di daftar barang");
					}

					// error sql lain selain error diatas belum direkam
				}
			}

			return listKeterangan;
		}

		#endregion
	}
}

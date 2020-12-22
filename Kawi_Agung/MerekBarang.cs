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

		private int no; // untuk nomor di tabel
		private int idMerekBarang;
		private string namaMerekBarang;

		#endregion

		#region PROPERTIES
		public int No { get => no; set => no = value; }
		public int IdMerekBarang { get => idMerekBarang; set => idMerekBarang = value; }
		public string NamaMerekBarang { get => namaMerekBarang; set => namaMerekBarang = value; }

		#endregion

		#region CONSTRUCTORS

		public MerekBarang()
		{
			No = 1;
			IdMerekBarang = Interlocked.Increment(ref idMerekBarang);
			NamaMerekBarang = "";
		}

		public MerekBarang(string pNamaMerekBarang)
		{
			NamaMerekBarang = pNamaMerekBarang;
		}

		public MerekBarang(int pIdMerekBarang, string pNamaMerekBarang)
		{
			IdMerekBarang = pIdMerekBarang;
			NamaMerekBarang = pNamaMerekBarang;
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

			if (kriteria == "")
			{
				sql = "SELECT idmerek_barang, nama FROM merek_barang ORDER BY idmerek_barang";
			}
			else
			{
				sql = "SELECT idmerek_barang, nama FROM merek_barang WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY idmerek_barang";
			}
			try
			{
				MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

				int i = 1;
				while (hasilData.Read())
				{
					MerekBarang merek = new MerekBarang();

					merek.No = i++;
					merek.IdMerekBarang = int.Parse(hasilData.GetValue(0).ToString());
					merek.NamaMerekBarang = hasilData.GetValue(1).ToString();

					listMerek.Add(merek);
				}
				return "1";
			}
			catch (Exception ex)
			{
				return "Terjadi Kesalahan. Pesan Kesalahan : " + ex.Message;
			}
		}

		public static string TambahData(MerekBarang merek)
		{
			string sql = "INSERT INTO merek_barang(nama) VALUES('" + merek.NamaMerekBarang + "')";

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

		public static string UbahData(MerekBarang merek)
		{
			string sql = "UPDATE merek_barang SET nama='" + merek.NamaMerekBarang + "' WHERE idmerek_barang=" + merek.IdMerekBarang;

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

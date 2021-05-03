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

		public static string BacaData(string kriteria, string nilaiKriteria, List<JenisBarang> listJenis)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT * FROM jenis_barang ORDER BY nama ASC";
			}
			else if (kriteria == "cari jenis")
			{
				sql = "SELECT * FROM jenis_barang WHERE nama='" + nilaiKriteria + "'";
			}
			else if (kriteria == "exclude") // kriteria khusus untuk perintah sql yang menyeleksi semua jenis barang terkecuali jenis tertentu
			{
				sql = "SELECT * FROM jenis_barang WHERE NOT nama='" + nilaiKriteria + "' ORDER BY nama ASC";
			}
			else
			{
				sql = "SELECT * FROM jenis_barang WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY nama ASC";
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
				hasil.Close();
				conn.KoneksiDB.Close();
			}
		}

		public static string TambahData(JenisBarang jenis)
		{
			string sql = "INSERT INTO jenis_barang(nama) VALUES('" + jenis.Nama + "')";
			Koneksi k = new Koneksi();
			MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

			try
			{
				c.ExecuteNonQuery();

				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
			finally
			{
				c.Dispose();
				k.KoneksiDB.Close();
			}
		}

		public static string UbahData(JenisBarang jenis)
		{
			string sql = "UPDATE jenis_barang SET nama='" + jenis.Nama + "' WHERE idjenis_barang=" + jenis.idJenisBarang;
			Koneksi k = new Koneksi();
			MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

			try
			{
				c.ExecuteNonQuery();
				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
			finally
			{
				c.Dispose();
				k.KoneksiDB.Close();
			}
		}

		public static List<string> HapusData(List<JenisBarang> listJenis)
		{
			List<string> listKeterangan = new List<string>();
			string sql = "";
			Koneksi k = new Koneksi();
			MySqlCommand c = null;

			foreach (JenisBarang jenis in listJenis)
			{
				sql = "DELETE FROM jenis_barang WHERE idjenis_barang=" + jenis.IdJenisBarang;

				try
				{
					k.Connect();
					c = new MySqlCommand(sql, k.KoneksiDB);
					c.ExecuteNonQuery();
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
				finally
				{
					c.Dispose();
					k.KoneksiDB.Close();
				}
			}

			return listKeterangan;
		}
		#endregion
	}
}

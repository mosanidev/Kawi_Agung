using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class Jabatan
	{
		#region DATAMEMBERS

		private int idJabatan;
		private string nama;

		#endregion

		#region PROPERTIES
		public int IdJabatan { get => idJabatan; set => idJabatan = value; }
		public string Nama { get => nama; set => nama = value; }

		#endregion

		#region CONSTRUCTORS

		public Jabatan()
		{
			this.idJabatan = 0;
			this.nama = "";
		}

		#endregion

		#region METHODS

		public static string BacaData(List<Jabatan> listJabatan)
		{
			string sql = "SELECT idjabatan, nama FROM jabatan WHERE NOT nama='Manajer'";
			Koneksi conn = new Koneksi();

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Jabatan jabatan = new Jabatan();
					jabatan.IdJabatan = int.Parse(hasil.GetValue(0).ToString());
					jabatan.Nama = hasil.GetValue(1).ToString();
					listJabatan.Add(jabatan);
				}
				return "1";
			}
			catch (MySqlException ex)
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

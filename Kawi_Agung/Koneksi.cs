using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace Kawi_Agung
{
	public class Koneksi
	{
		private string namaServer;
		private string namaDatabase;
		private string username;
		private string password;
		private MySqlConnection koneksiDB;

		#region Properties
		public string NamaServer { get => namaServer; set => namaServer = value; }
		public string NamaDatabase { get => namaDatabase; set => namaDatabase = value; }
		public string Username { get => username; set => username = value; }
		public string Password { get => password; set => password = value; }
		public MySqlConnection KoneksiDB { get => koneksiDB; set => koneksiDB = value; }

		#endregion

		#region Method

		public string Connect()
		{
			try
			{
				if (koneksiDB.State == System.Data.ConnectionState.Open)
				{
					koneksiDB.Close();
				}
				koneksiDB.Open();
				return "Koneksi sukses";
			}
			catch (MySqlException e)
			{
				return $"Koneksi gagal. Pesan error : {e.Message}";
			}
		}

		public void updateAppConfig(string connectionString)
		{
			//Buka konfigurasi app.config
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			//set app.config pada nama tag koneksi string yg dimasukkan pengguna
			config.ConnectionStrings.ConnectionStrings["KonefigurasiKoneksi"].ConnectionString = connectionString;

			//simpan app.config yg diperbarui
			config.Save(ConfigurationSaveMode.Modified, true);

			//reload app.config dgn pengaturan baru
			ConfigurationManager.RefreshSection("connectionStrings");
		}

		//public static MySqlDataReader JalankanPerintahQuery(string pSql)
		//{
		//	Koneksi conn = new Koneksi();
		//	conn.Connect();

		//	MySqlCommand cmd = new MySqlCommand(pSql, conn.KoneksiDB);

		//	MySqlDataReader hasil = cmd.ExecuteReader();

		//	return hasil;
		//}

		//public static string JalankanPerintahDML(string pSql)
		//{
		//	try
		//	{
		//		Koneksi k = new Koneksi();
		//		k.Connect();

		//		MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);

		//		c.ExecuteNonQuery();

		//		return "1";
		//	}
		//	catch (MySqlException e)
		//	{
		//		return e.Message;
		//	}
		//}

		#endregion

		#region Constructor

		public Koneksi()
		{
			KoneksiDB = new MySqlConnection();

			// set connection sesuai yg di app config
			KoneksiDB.ConnectionString = ConfigurationManager.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString;

			// panggil method connect
			string hasilConnect = Connect();

		}

		public Koneksi(string pNamaServer, string pNamaDatabase, string pUsername, string pPassword)
		{
			NamaServer = pNamaServer;
			NamaDatabase = pNamaDatabase;
			Username = pUsername;
			Password = pPassword;

			KoneksiDB = new MySqlConnection();

			// set connections sesuai dengan server yg dimasukkan
			string strCon = "server=" + namaServer + "; database=" + namaDatabase + "; uid=" + username + "; pwd=" + password + ";";

			KoneksiDB.ConnectionString = strCon;

			// panggil method connect
			string hasilConnect = Connect();

			if (hasilConnect == "Koneksi sukses")
			{
				//ubah dgn memanggil method updateconfig
				updateAppConfig(strCon);
			}
		}

		#endregion
	}
}

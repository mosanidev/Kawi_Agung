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

		#region Constructor

		public Koneksi()
		{
			KoneksiDB = new MySqlConnection();

			KoneksiDB.ConnectionString = ConfigurationManager.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString;

			string hasilConnect = Connect();

		}

		public Koneksi(string pNamaServer, string pNamaDatabase, string pUsername, string pPassword)
		{
			NamaServer = pNamaServer;
			NamaDatabase = pNamaDatabase;
			Username = pUsername;
			Password = pPassword;

			string strCon = "server=" + namaServer + "; database=" + namaDatabase + "; uid=" + username + "; pwd=" + password + ";";

			KoneksiDB.ConnectionString = strCon;

			updateAppConfig(strCon);
			
		}

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
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			config.ConnectionStrings.ConnectionStrings["KonefigurasiKoneksi"].ConnectionString = connectionString;

			config.Save(ConfigurationSaveMode.Modified, true);

			ConfigurationManager.RefreshSection("connectionStrings");

		}

		public static MySqlDataReader JalankanPerintahQuery(string pSql)
		{
			Koneksi conn = new Koneksi();

			MySqlCommand cmd = new MySqlCommand(pSql, conn.KoneksiDB);

			MySqlDataReader hasil = cmd.ExecuteReader();

			return hasil;
		}


		#endregion
	}
}

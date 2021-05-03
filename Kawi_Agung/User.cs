using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Kawi_Agung
{
	public class User
	{
		#region DATAMEMBERS

		private int idUser;
		private string username;
		private string password;
		private string status;
		private string nama;
		private string jenisKelamin;
		private string alamat;
		private DateTime tanggalLahir;
		private string noTelp;
		private string foto;
		private Jabatan jabatan;
		private string namaRekening;
		private string noRekening;
		private string namaBank;

		#endregion

		#region PROPERTIES

		public int IdUser { get => idUser; set => idUser = value; }
		public string Username { get => username; set => username = value; }
		public string Password { get => password; set => password = value; }
		public string Status { get => status; set => status = value; }
		public string Nama { get => nama; set => nama = value; }
		public string JenisKelamin { get => jenisKelamin; set => jenisKelamin = value; }
		public string Alamat { get => alamat; set => alamat = value; }
		public DateTime TanggalLahir { get => tanggalLahir; set => tanggalLahir = value; }
		public string NoTelp { get => noTelp; set => noTelp = value; }
		public string Foto { get => foto; set => foto = value; }
		public Jabatan Jabatan { get => jabatan; set => jabatan = value; }
		public string NamaRekening { get => namaRekening; set => namaRekening = value; }
		public string NoRekening { get => noRekening; set => noRekening = value; }
		public string NamaBank { get => namaBank; set => namaBank = value; }


		#endregion

		#region CONSTRUCTORS

		public User()
		{
			IdUser = Interlocked.Increment(ref idUser);
			Username = "";
			Password = "";
			Status = "";
			Alamat = "";
			Nama = "";
			JenisKelamin = "";
			TanggalLahir = DateTime.Now;
			NoTelp = "";
			NamaRekening = "";
			NoRekening = "";
			Foto = "";
			NamaBank = ""; 
		}

		#endregion

		#region METHODS


		public static string UbahPegawai(User user, string image)
		{
			string sql = "";
			Koneksi k = new Koneksi();
			MySqlCommand c = null;

			if (image == "Ada")
			{
				sql = "UPDATE user SET no_telp='" + user.NoTelp + "', idjabatan=" + user.Jabatan.IdJabatan + ", status='" + user.Status + "', alamat='" + user.Alamat + "', foto='" + user.Foto + "' WHERE iduser=" + user.IdUser;
			}
			else if (image == "Hapus")
			{
				sql = "UPDATE user SET no_telp='" + user.NoTelp + "', idjabatan=" + user.Jabatan.IdJabatan + ", status='" + user.Status + "', alamat='" + user.Alamat + "', foto=NULL WHERE iduser=" + user.IdUser;
			}
			else if (image == "Tidak Ada")
			{
				sql = "UPDATE user SET no_telp='" + user.NoTelp + "', idjabatan=" + user.Jabatan.IdJabatan + ", status='" + user.Status + "', alamat='" + user.Alamat + "' WHERE iduser=" + user.IdUser;
			}

			try
			{
				c = new MySqlCommand(sql, k.KoneksiDB); 
				
				c.ExecuteReader();

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
		public static string UbahData(User user, string image)
		{
			string sql = "";
			Koneksi k = new Koneksi();
			MySqlCommand c = null;

			if (image == "Hapus") // kalau image dihapus
			{
				sql = "UPDATE user SET password='" + EncryptPassword(user.Password) + "', alamat='" + user.Alamat + "', no_telp='" + user.NoTelp + "', foto=NULL WHERE iduser=" + user.idUser;

			}
			else if (image == "Tidak Ada") // kalau image tidak di update
			{
				sql = "UPDATE user SET password='" + EncryptPassword(user.Password) + "', alamat='" + user.Alamat + "', no_telp='" + user.NoTelp + "' WHERE iduser=" + user.idUser;

			}
			else if (image == "Ada") // kalau image diunggah dari komputer
			{

				sql = "UPDATE user SET password='" + EncryptPassword(user.Password) + "', alamat='" + user.Alamat + "', no_telp='" + user.NoTelp + "', foto='" + user.Foto + "' WHERE iduser=" + user.idUser;

			}

			try
			{
				c = new MySqlCommand(sql, k.KoneksiDB);

				c.ExecuteReader();

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

		public static string TambahPegawai(User user)
		{
			string sql = "INSERT INTO user(nama, jenis_kelamin, tanggal_lahir, no_telp, username, idjabatan, alamat, status, foto) VALUES ('" + user.Nama + "','" + user.JenisKelamin + "','" + user.TanggalLahir.ToString("yyyy-MM-dd") + "','" + user.NoTelp + "','" + user.Username + "', " + user.Jabatan.IdJabatan + ",'" + user.Alamat + "', 'Belum Aktif', '" + user.Foto + "')";

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

		public static string AktifkanUser(string username, string passwordBaru)
		{
			string sql = "UPDATE user SET user.password = '"+ EncryptPassword(passwordBaru) + "', user.status='Aktif' WHERE user.username='" + username + "'";
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

		private static string EncryptPassword(string password)
		{
			byte[] data = UTF8Encoding.UTF8.GetBytes(password);
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			UTF8Encoding utf8 = new UTF8Encoding();
			byte[] keys = md5.ComputeHash(utf8.GetBytes("EXtr@_S@Lt"));
			TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
			ICryptoTransform transform = tripDES.CreateEncryptor();
			byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
			return Convert.ToBase64String(results, 0, results.Length);
		}

		public static string BacaPegawai(string kriteria, string nilaiKriteria, List<User> listUser)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT u.iduser, u.nama, u.username, j.nama, u.status, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE NOT j.nama='Manajer' ORDER BY u.nama ASC";
			}
			else if (kriteria == "exclude")
			{
				sql = "SELECT u.iduser, u.nama, u.username, j.nama, u.status, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE NOT j.nama='Manajer' AND NOT u.iduser =" + nilaiKriteria;
			}
			else if (kriteria == "u.nama")
			{
				sql = "SELECT u.iduser, u.nama, u.username, j.nama, u.status, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE NOT j.nama='Manajer' AND " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY u.nama ASC";
			}
			else if (kriteria == "cari username")
			{
				sql = "SELECT u.iduser, u.nama, u.username, j.nama, u.status, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE u.username='" + nilaiKriteria + "'";
			}
			else if (kriteria == "u.iduser")
			{
				sql = "SELECT u.iduser, u.nama, u.username, j.nama, u.status, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE NOT j.nama='Pemilik' AND " + kriteria + " =" + nilaiKriteria;
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Jabatan j = new Jabatan();
					j.IdJabatan = Convert.ToInt32(hasil.GetValue(10));
					j.Nama = hasil.GetValue(3).ToString();

					User user = new User();

					user.IdUser = Convert.ToInt32(hasil.GetValue(0));
					user.Nama = hasil.GetValue(1).ToString();
					user.Username = hasil.GetValue(2).ToString();
					user.Jabatan = j;
					user.Status = hasil.GetValue(4).ToString();
					user.JenisKelamin = hasil.GetValue(5).ToString();
					user.TanggalLahir = DateTime.Parse(hasil.GetValue(6).ToString());
					user.Alamat = hasil.GetValue(7).ToString();
					user.NoTelp = hasil.GetValue(8).ToString();

					// jika kolom foto sudah diisi maka ditampung 
					if (hasil.GetValue(9) != System.DBNull.Value)
					{
						user.Foto = hasil.GetValue(9).ToString();
					}

					listUser.Add(user);
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

		public static string BacaData(string kriteria, string nilaiKriteria, List<User> listUser)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT u.iduser, u.username, u.password, u.status, u.nama, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan, j.nama FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan";
			}
			else
			{
				sql = "SELECT u.iduser, u.username, u.password, u.status, u.nama, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan, j.nama FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE " + kriteria + " = '" + nilaiKriteria + "'";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					User user = new User();

					user.IdUser = int.Parse(hasil.GetValue(0).ToString());
					user.Username = hasil.GetValue(1).ToString();
					user.Password = hasil.GetValue(2).ToString();
					user.Status = hasil.GetValue(3).ToString();
					user.Nama = hasil.GetValue(4).ToString();
					user.JenisKelamin = hasil.GetValue(5).ToString();
					user.TanggalLahir = DateTime.Parse(hasil.GetValue(6).ToString());
					user.Alamat = hasil.GetValue(7).ToString();
					user.NoTelp = hasil.GetValue(8).ToString();

					// jika kolom foto sudah diisi maka ditampung 
					if (hasil.GetValue(9) != System.DBNull.Value)
					{
						//user.Foto = (byte[])hasil.GetValue(9);
						user.Foto = hasil.GetValue(9).ToString();
					}

					Jabatan jabatan = new Jabatan();
					jabatan.IdJabatan = int.Parse(hasil.GetValue(10).ToString());
					jabatan.Nama = hasil.GetValue(11).ToString();

					user.Jabatan = jabatan;

					listUser.Add(user);
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

		public static string HapusData(User user)
		{
			string sql = "DELETE FROM user WHERE iduser=" + user.IdUser;
			Koneksi k = new Koneksi();
			MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

			try
			{
				c.ExecuteNonQuery();

				return "1";
			}
			catch (MySqlException ex)
			{
				if (ex.Number == 1451)
				{
					return "Hapus data gagal. Data pegawai masih ada di nota pembelian/penjualan";
				}

				// error sql lain selain error diatas belum direkam
				return ex.Message;
			}
			finally
			{
				c.Dispose();
				k.KoneksiDB.Close();
			}
		}

		#endregion
	}
}

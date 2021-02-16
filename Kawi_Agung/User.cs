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
		private byte[]? foto;
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
		public byte[] Foto { get => foto; set => foto = value; }
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
			Foto = null;
			NamaBank = ""; 
		}

		#endregion

		#region METHODS

		private static void JalankanPerintahDML(string pSql)
		{
			Koneksi k = new Koneksi();
			k.Connect();

			MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);

			c.ExecuteNonQuery();
		}

		public static string UbahPegawai(User user, string image)
		{
			string sql = "";
			MySqlParameter fotoParam = null;

			if (image == "Tidak Ada")
			{
				sql = "UPDATE user SET no_telp='" + user.NoTelp + "', idjabatan=" + user.Jabatan.IdJabatan + ", status='" + user.Status + "', alamat='" + user.Alamat + "' WHERE iduser=" + user.IdUser;
			}
			else if (image == "Kosong")
			{
				sql = "UPDATE user SET no_telp='" + user.NoTelp + "', idjabatan=" + user.Jabatan.IdJabatan + ", status='" + user.Status + "', alamat='" + user.Alamat + "', foto=NULL WHERE iduser=" + user.IdUser;
			}
			else if (image == "Ada")
			{
				sql = "UPDATE user SET no_telp='" + user.NoTelp + "', idjabatan=" + user.Jabatan.IdJabatan + ", status='" + user.Status + "', alamat='" + user.Alamat + "', foto=@foto WHERE iduser=" + user.IdUser;

				fotoParam = new MySqlParameter("foto", MySqlDbType.Blob);

				fotoParam.Value = user.foto;
			}
			try
			{
				Koneksi k = new Koneksi();

				k.Connect();

				MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

				// if blob type must do parameterized query not concat string
				if (image == "Ada")
				{
					c.Parameters.Add(fotoParam);
				}

				c.ExecuteReader();

				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}

		}
		public static string UbahData(User user, string image)
		{
			string sql = "";
			MySqlParameter fotoParam = null;

			if (image == "Kosong") // kalau image dihapus
			{
				sql = "UPDATE user SET password='" + EncryptPassword(user.Password) + "', alamat='" + user.Alamat + "', no_telp='" + user.NoTelp + "', nama_rekening='" + user.NamaRekening + "', no_rekening='" + user.NoRekening + "', nama_bank='" + user.NamaBank + "', foto=NULL WHERE iduser=" + user.idUser;

			}
			else if (image == "Tidak Ada") // kalau image tidak di update
			{
				sql = "UPDATE user SET password='" + EncryptPassword(user.Password) + "', alamat='" + user.Alamat + "', no_telp='" + user.NoTelp + "', nama_rekening='" + user.NamaRekening + "', no_rekening='" + user.NoRekening + "', nama_bank='" + user.NamaBank + "' WHERE iduser=" + user.idUser;

			}
			else if (image == "Ada") // kalau image diunggah dari komputer
			{

				sql = "UPDATE user SET password='" + EncryptPassword(user.Password) + "', alamat='" + user.Alamat + "', no_telp='" + user.NoTelp + "', foto=@foto, nama_rekening='" + user.NamaRekening + "', no_rekening='" + user.NoRekening + "', nama_bank='" + user.NamaBank + "' WHERE iduser=" + user.idUser;

				fotoParam = new MySqlParameter("foto", MySqlDbType.Blob);

				fotoParam.Value = user.foto;

			}

			try
			{
				Koneksi k = new Koneksi();

				k.Connect();

				MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

				// if blob type must do parameterized query not concat string
				if (image == "Ada")
				{
					c.Parameters.Add(fotoParam);
				}

				c.ExecuteReader();

				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
		}

		public static string TambahPegawai(User user, List<User> listUser)
		{
			string sql = "INSERT INTO user(nama, jenis_kelamin, tanggal_lahir, no_telp, username, idjabatan, alamat, status, foto) VALUES ('" + user.Nama + "','" + user.JenisKelamin + "','" + user.TanggalLahir.ToString("yyyy-MM-dd") + "','" + user.NoTelp + "','" + user.Username + "', " + user.Jabatan.IdJabatan + ",'" + user.Alamat + "', 'Belum Aktif', @foto)";

			var fotoParam = new MySqlParameter("foto", MySqlDbType.Blob);

			fotoParam.Value = user.Foto;

			try
			{
				for (int i = 0; i < listUser.Count; i++)
				{
					if (user.Username.ToLower() == listUser[i].Username.ToLower())
					{
						return "Username sudah ada";
					}
				}

				Koneksi k = new Koneksi();

				k.Connect();

				MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

				// if blob type must do parameterized query not concat string
				c.Parameters.Add(fotoParam);

				c.ExecuteNonQuery();

				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
		}

		public static string AktifkanUser(string username, string passwordBaru)
		{
			string sql = "UPDATE user SET user.password = '"+ EncryptPassword(passwordBaru) + "', user.status='Aktif' WHERE user.username='" + username + "'";

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
				sql = "SELECT u.iduser, u.nama, u.username, j.nama, u.status, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE NOT j.nama='Manajer' ORDER BY u.nama";
			}
			else if (kriteria == "exclude")
			{
				sql = "SELECT u.iduser, u.nama, u.username, j.nama, u.status, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE NOT j.nama='Manajer' AND NOT u.iduser =" + nilaiKriteria;
			}
			else if (kriteria == "u.nama")
			{
				sql = "SELECT u.iduser, u.nama, u.username, j.nama, u.status, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE NOT j.nama='Manajer' AND " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY u.nama";
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
						user.Foto = (byte[])hasil.GetValue(9);
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
				hasil.Dispose();
			}
		}

		public static string BacaData(string kriteria, string nilaiKriteria, List<User> listUser)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT u.iduser, u.username, u.password, u.status, u.nama, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan, j.nama, u.nama_rekening, u.no_rekening, u.nama_bank FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan";
			}
			else
			{
				sql = "SELECT u.iduser, u.username, u.password, u.status, u.nama, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan, j.nama, u.nama_rekening, u.no_rekening, u.nama_bank FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE " + kriteria + " = '" + nilaiKriteria + "'";
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
						user.Foto = (byte[])hasil.GetValue(9);
					}

					Jabatan jabatan = new Jabatan();
					jabatan.IdJabatan = int.Parse(hasil.GetValue(10).ToString());
					jabatan.Nama = hasil.GetValue(11).ToString();

					user.Jabatan = jabatan;
					user.NamaRekening = hasil.GetValue(12).ToString();
					user.NoRekening = hasil.GetValue(13).ToString();
					user.NamaBank = hasil.GetValue(14).ToString();

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
				hasil.Dispose();
			}
		}

		public static string HapusData(User user)
		{
			string sql = "DELETE FROM user WHERE iduser=" + user.IdUser;

			try
			{
				JalankanPerintahDML(sql);

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
		}

		#endregion
	}
}

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

		private int no;
		private int idUser;
		private string username;
		private string password;
		private string status;
		private string nama;
		private string jenisKelamin;
		private string alamat;
		private DateTime tanggalLahir;
		private string noTelp;
		private byte[] foto;
		private Jabatan jabatan;
		private string namaRekening;
		private string noRekening;
		private string namaBank;

		#endregion

		#region PROPERTIES

		public int No { get => no; set => no = value; }
		public int IdUser { get => idUser; set => idUser = value; }
		public string Username { get => username; set => username = value; }
		public string Password { get => password; set => password = value; }
		public string Status { get => status; set => status = value; }
		public string Nama { get => nama; set => nama = value; }
		public string JenisKelamin { get => nama; set => nama = value; }
		public string Alamat { get => nama; set => nama = value; }
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
			No = 0;
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

		public User(string pUsername, string pPassword, string pAlamat, string pStatus, string pNama, string pJenisKelamin, DateTime pTanggalLahir, string pNoTelp, byte[] pFoto, string pNamaRekening, string pNoRekening, string pNamaBank, Jabatan pJabatan)
		{
			Username = pUsername;
			Password = pPassword;
			Status = pStatus;
			Nama = pNama;
			JenisKelamin = pJenisKelamin;
			TanggalLahir = pTanggalLahir;
			Alamat = pAlamat;
			NoTelp = pNoTelp;
			Foto = pFoto;
			NamaRekening = pNamaRekening;
			NoRekening = pNoRekening;
			NamaBank = pNamaBank;
			Jabatan = pJabatan;
		}

		public User(int pIdUser, string pPassword, string pAlamat, string pNoTelp, string pNamaRekening, string pNamaBank, string pNoRekening, byte[] pFoto)
		{
			IdUser = pIdUser;
			Password = pPassword;
			Alamat = pAlamat;
			NoTelp = pNoTelp;
			NamaRekening = pNamaRekening;
			NoRekening = pNoRekening;
			Foto = pFoto;
			NamaBank = pNamaBank;
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

		public static string UbahData(User user)
		{
			string sql = "UPDATE user SET password='" + EncryptPassword(user.Password) + "', alamat='" + user.Alamat + "', no_telp='" + user.NoTelp  + "', foto=@foto, nama_rekening='" + user.NamaRekening +"', no_rekening='" + user.NoRekening +"', nama_bank='" + user.NamaBank +"' WHERE iduser=" + user.idUser;

			var fotoParam = new MySqlParameter("foto", MySqlDbType.Blob);

			fotoParam.Value = user.foto;

			try
			{
				Koneksi k = new Koneksi();

				k.Connect();

				MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

				// if blob type must do parameterized query not concat string
				c.Parameters.Add(fotoParam);

				c.ExecuteReader();

				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
		}

		public static string TambahData(User user)
		{
			string sql = "INSERT INTO user(username, password, status, nama, jenis_kelamin, tanggal_lahir, alamat, no_telp, foto, idjabatan, nama_rekening, no_rekening, nama_bank) VALUES ('" + user.Username + "','" + EncryptPassword(user.Password) + "','" + user.Status + "','" + user.Nama + "','" + user.JenisKelamin + "','" + user.TanggalLahir + "','" + user.Alamat + "','" + user.NoTelp + "','" + user.Foto + "','" + user.Jabatan.IdJabatan + "','" + user.NamaRekening + "','" + user.NoRekening + "','" + user.NamaBank + "')";

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

		public static string BacaData(string kriteria, string nilaiKriteria, List<User> listUser)
		{
			string sql = "";

			if (kriteria == "")
			{
				sql = "SELECT u.iduser, u.username, u.password, u.status, u.nama, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan, j.nama, u.nama_rekening, u.no_rekening, u.nama_bank FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan";
			}
			else
			{
				sql = "SELECT u.iduser, u.username, u.password, u.status, u.nama, u.jenis_kelamin, u.tanggal_lahir, u.alamat, u.no_telp, u.foto, u.idjabatan, j.nama, u.nama_rekening, u.no_rekening, u.nama_bank FROM user u INNER JOIN jabatan j ON u.idjabatan=j.idjabatan WHERE " + kriteria + " = '" + nilaiKriteria + "'";
			}
			try
			{
				MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

				int i = 1;
				while (hasilData.Read())
				{
					User user = new User();

					user.No = i++;
					user.IdUser = int.Parse(hasilData.GetValue(0).ToString());
					user.Username = hasilData.GetValue(1).ToString();
					user.Password = hasilData.GetValue(2).ToString();
					user.Status = hasilData.GetValue(3).ToString();
					user.Nama = hasilData.GetValue(4).ToString();
					user.JenisKelamin = hasilData.GetValue(5).ToString();
					user.TanggalLahir = DateTime.Parse(hasilData.GetValue(6).ToString());
					user.Alamat = hasilData.GetValue(7).ToString();
					user.NoTelp = hasilData.GetValue(8).ToString();
					user.Foto = (byte[])hasilData.GetValue(9);

					Jabatan jabatan = new Jabatan(int.Parse(hasilData.GetValue(10).ToString()), hasilData.GetValue(11).ToString());

					user.Jabatan = jabatan;
					user.NamaRekening = hasilData.GetValue(12).ToString();
					user.NoRekening = hasilData.GetValue(13).ToString();
					user.NamaBank = hasilData.GetValue(14).ToString();

					listUser.Add(user);
				}
				return "1";
			}
			catch (Exception ex)
			{
				return "Terjadi Kesalahan. Pesan Kesalahan : " + ex.Message;
			}
		}

		#endregion
	}
}

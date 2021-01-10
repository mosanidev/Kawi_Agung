﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class Pelanggan
	{
		#region DATAMEMBERS

		private int idPelanggan;
		private string nama;
		private string alamat;
		private string noTelp;

		#endregion

		#region PROPERTIES

		public int IdPelanggan { get => idPelanggan; set => idPelanggan = value; }
		public string Nama { get => nama; set => nama = value; }
		public string Alamat { get => alamat; set => alamat = value; }
		public string NoTelp { get => noTelp; set => noTelp = value; }

		#endregion

		#region CONSTRUCTORS

		public Pelanggan()
		{
			IdPelanggan = 0;
			Nama = "";
			Alamat = "";
			NoTelp = "";
		}

		public Pelanggan(int pIdPelanggan, string pNama, string pAlamat, string pNoTelp)
		{
			IdPelanggan = pIdPelanggan;
			Nama = pNama;
			Alamat = pAlamat;
			NoTelp = pNoTelp;
		}

		public Pelanggan(string pNama, string pAlamat, string pNoTelp)
		{
			Nama = pNama;
			Alamat = pAlamat;
			NoTelp = pNoTelp;
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

		public static string BacaData(string kriteria, string nilaiKriteria, List<Pelanggan> listPelanggan)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT * FROM pelanggan";
			}
			else
			{
				sql = "SELECT * FROM pelanggan WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Pelanggan pelanggan = new Pelanggan();

					pelanggan.IdPelanggan = int.Parse(hasil.GetValue(0).ToString());
					pelanggan.Nama = hasil.GetValue(1).ToString();
					pelanggan.Alamat = hasil.GetValue(2).ToString();
					pelanggan.NoTelp = hasil.GetValue(3).ToString();

					listPelanggan.Add(pelanggan);
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

		public static string TambahData(Pelanggan pelanggan)
		{
			string sql = "INSERT INTO pelanggan(nama, alamat, no_telp) VALUES('" + pelanggan.Nama + "', '" + pelanggan.Alamat + "', '" + pelanggan.NoTelp + "')";

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

		public static string UbahData(Pelanggan pelanggan)
		{
			string sql = "UPDATE pelanggan SET nama='" + pelanggan.Nama + "', alamat='" + pelanggan.Alamat + "', no_telp='" + pelanggan.NoTelp + "' WHERE idpelanggan=" + pelanggan.IdPelanggan;

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

		public static List<string> HapusData(List<Pelanggan> listPelanggan)
		{
			List<string> listKeterangan = new List<string>();
			string sql = "";

			foreach (Pelanggan pelanggan in listPelanggan)
			{
				sql = "DELETE FROM pelanggan WHERE idpelanggan=" + pelanggan.IdPelanggan;

				try
				{
					JalankanPerintahDML(sql);
					listKeterangan.Add("berhasil");
				}
				catch (MySqlException ex)
				{
					if (ex.Number == 1451)
					{
						listKeterangan.Add("data pelanggan masih ada di nota penjualan");
					}

					// error sql lain selain error diatas belum direkam
				}
			}
			return listKeterangan;
		}


		#endregion


	}
}

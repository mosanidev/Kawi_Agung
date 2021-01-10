using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class Supplier
	{
		#region DATAMEMBERS

		private int idSupplier;
		private string nama;
		private string alamat;
		private string noTelp;

		#endregion

		#region PROPERTIES

		public int IdSupplier { get => idSupplier; set => idSupplier = value; }
		public string Nama { get => nama; set => nama = value; }
		public string Alamat { get => alamat; set => alamat = value; }
		public string NoTelp { get => noTelp; set => noTelp = value; }

		#endregion

		#region CONSTRUCTORS

		public Supplier()
		{
			IdSupplier = 0;
			Nama = "";
			Alamat = "";
			NoTelp = "";
		}

		//public Supplier(int pIdSupplier, string pNama, string pAlamat, string pNoTelp)
		//{
		//	IdSupplier = pIdSupplier;
		//	Nama = pNama;
		//	Alamat = pAlamat;
		//	NoTelp = pNoTelp;
		//}

		//public Supplier(string pNama, string pAlamat, string pNoTelp)
		//{
		//	Nama = pNama;
		//	Alamat = pAlamat;
		//	NoTelp = pNoTelp;
		//}

		#endregion

		#region METHODS

		public static void JalankanPerintahDML(string pSql)
		{
			Koneksi k = new Koneksi();
			k.Connect();

			MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);

			c.ExecuteNonQuery();
		}

		public static string BacaData(string kriteria, string nilaiKriteria, List<Supplier> listSupplier)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT * FROM supplier ORDER BY idsupplier";
			}
			else if (kriteria == "exclude") // // kriteria khusus untuk perintah sql yang menyeleksi semua supplier terkecuali kode barang tertentu
			{
				sql = "SELECT * FROM supplier WHERE NOT nama='" + nilaiKriteria + "' ORDER BY idsupplier";
			}
			else
			{
				sql = "SELECT * FROM supplier WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY idsupplier";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Supplier supplier = new Supplier();

					supplier.IdSupplier = int.Parse(hasil.GetValue(0).ToString());
					supplier.Nama = hasil.GetValue(1).ToString();
					supplier.Alamat = hasil.GetValue(2).ToString();
					supplier.NoTelp = hasil.GetValue(3).ToString();

					listSupplier.Add(supplier);
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

		public static string TambahData(Supplier supplier, List<Supplier> listSupplier)
		{
			string sql = "INSERT INTO supplier(nama, alamat, no_telp) VALUES('" + supplier.Nama + "', '" + supplier.Alamat + "', '" + supplier.NoTelp + "')";

			try
			{
				for (int i = 0; i < listSupplier.Count; i++)
				{
					if (supplier.Nama.ToLower() == listSupplier[i].Nama.ToLower())
					{
						return "Nama supplier sudah ada. Harap masukkan nama yang lain";
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

		public static string UbahData(Supplier supplier, List<Supplier> listSupplier)
		{
			string sql = "UPDATE supplier SET nama='" + supplier.Nama + "', alamat='" + supplier.Alamat + "', no_telp='" + supplier.NoTelp + "' WHERE idsupplier=" + supplier.IdSupplier;
			 
			try
			{
				for (int i = 0; i < listSupplier.Count; i++)
				{
					if (supplier.Nama.ToLower() == listSupplier[i].Nama.ToLower())
					{
						return "Nama supplier sudah ada. Harap masukkan nama yang lain";
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

		public static List<string> HapusData(List<Supplier> listSupplier)
		{
			List<string> listKeterangan = new List<string>();
			string sql = "";

			foreach (Supplier supplier in listSupplier)
			{
				sql = "DELETE FROM supplier WHERE idsupplier=" + supplier.IdSupplier;

				try
				{
					JalankanPerintahDML(sql);
					listKeterangan.Add("berhasil");
				}
				catch (MySqlException ex)
				{
					if (ex.Number == 1451)
					{
						listKeterangan.Add("data supplier masih ada di nota pembelian");
					}

					// error sql lain selain error diatas belum direkam
				}
			}
			return listKeterangan;
		}
		#endregion
	}
}

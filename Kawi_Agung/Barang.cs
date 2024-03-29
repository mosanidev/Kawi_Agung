﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Kawi_Agung
{
	public class Barang
	{
		#region DATAMEMBERS

		private int idBarang;
		private string kodeBarang;
		private string nama;
		private int hargaJual;
		private int diskonPersenJual;
		private string foto;
		private KategoriBarang kategori;
		private JenisBarang jenis;
		private MerekBarang merek;
		private int jumlahStok;
		private string satuan;
		private int totalJumlahStok;
		private int totalBarangStokKurang;

		#endregion

		#region PROPERTIES

		public int IdBarang { get => idBarang; set => idBarang = value; }
		public string KodeBarang { get => kodeBarang; set => kodeBarang = value; }
		public string Nama { get => nama; set => nama = value; }
		public int HargaJual { get => hargaJual; set => hargaJual = value; }
		public int DiskonPersenJual { get => diskonPersenJual; set => diskonPersenJual = value; }
		public string Foto { get => foto; set => foto = value; }
		public KategoriBarang Kategori { get => kategori; set => kategori = value; }
		public JenisBarang Jenis { get => jenis; set => jenis = value; }
		public MerekBarang Merek { get => merek; set => merek = value; }
		public int JumlahStok { get => jumlahStok; set => jumlahStok = value; }
		public string Satuan { get => satuan; set => satuan = value; }
		public int TotalJumlahStok { get => totalJumlahStok; set => totalJumlahStok = value; }
		public int TotalBarangStokKurang { get => totalBarangStokKurang; set => totalBarangStokKurang = value; }

		#endregion

		#region CONSTRUCTORS

		public Barang()
		{
			IdBarang = 0;
			KodeBarang = "";
			Nama = "";
			HargaJual = 0;
			JumlahStok = 0;
			Satuan = "";
			DiskonPersenJual = 0;
			Foto = "";
		}

		#endregion

		#region METHODS

		public static string BacaDataBarang(string kriteria, string nilaiKriteria, List<Barang> listBarang)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT b.idbarang, b.kode_barang, b.nama, j.nama AS jenis, k.nama AS kategori, m.nama As merek, b.harga_jual, b.foto, b.diskon_persen_jual, b.satuan, b.jumlah_stok, j.idjenis_barang, k.idkategori_barang, m.idmerek_barang FROM barang b INNER JOIN kategori_barang k ON b.idkategori_barang=k.idkategori_barang INNER JOIN jenis_barang j ON b.idjenis_barang=j.idjenis_barang INNER JOIN merek_barang m ON b.idmerek_barang=m.idmerek_barang ORDER BY b.kode_barang";
			}
			else if (kriteria == "b.idbarang")
			{
				sql = "SELECT b.idbarang, b.kode_barang, b.nama, j.nama AS jenis, k.nama AS kategori, m.nama As merek, b.harga_jual, b.foto, b.diskon_persen_jual, b.satuan, b.jumlah_stok, j.idjenis_barang, k.idkategori_barang, m.idmerek_barang FROM barang b INNER JOIN kategori_barang k ON b.idkategori_barang=k.idkategori_barang INNER JOIN jenis_barang j ON b.idjenis_barang=j.idjenis_barang INNER JOIN merek_barang m ON b.idmerek_barang=m.idmerek_barang WHERE b.idbarang=" + nilaiKriteria + " ORDER BY b.kode_barang";
			}
			else if (kriteria == "cari barang") // kriteria khusus untuk perintah sql yang menyeleksi kode barang tertentu
			{
				sql = "SELECT b.idbarang, b.kode_barang, b.nama, j.nama AS jenis, k.nama AS kategori, m.nama As merek, b.harga_jual, b.foto, b.diskon_persen_jual, b.satuan, b.jumlah_stok, j.idjenis_barang, k.idkategori_barang, m.idmerek_barang FROM barang b INNER JOIN kategori_barang k ON b.idkategori_barang=k.idkategori_barang INNER JOIN jenis_barang j ON b.idjenis_barang=j.idjenis_barang INNER JOIN merek_barang m ON b.idmerek_barang=m.idmerek_barang WHERE b.kode_barang='" + nilaiKriteria + "' ORDER BY b.kode_barang";
			}
			else if (kriteria == "exclude") // kriteria khusus untuk perintah sql yang menyeleksi semua barang terkecuali kode barang tertentu
			{
				sql = "SELECT b.idbarang, b.kode_barang, b.nama, j.nama AS jenis, k.nama AS kategori, m.nama As merek, b.harga_jual, b.foto, b.diskon_persen_jual, b.satuan, b.jumlah_stok, j.idjenis_barang, k.idkategori_barang, m.idmerek_barang FROM barang b INNER JOIN kategori_barang k ON b.idkategori_barang=k.idkategori_barang INNER JOIN jenis_barang j ON b.idjenis_barang=j.idjenis_barang INNER JOIN merek_barang m ON b.idmerek_barang=m.idmerek_barang WHERE NOT b.kode_barang ='" + nilaiKriteria + "' ORDER BY b.kode_barang";
			}
			else
			{
				sql = "SELECT b.idbarang, b.kode_barang, b.nama, j.nama AS jenis, k.nama AS kategori, m.nama As merek, b.harga_jual, b.foto, b.diskon_persen_jual, b.satuan, b.jumlah_stok, j.idjenis_barang, k.idkategori_barang, m.idmerek_barang FROM barang b INNER JOIN kategori_barang k ON b.idkategori_barang=k.idkategori_barang INNER JOIN jenis_barang j ON b.idjenis_barang=j.idjenis_barang INNER JOIN merek_barang m ON b.idmerek_barang=m.idmerek_barang WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER By b.kode_barang";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					JenisBarang jenis = new JenisBarang();
					jenis.IdJenisBarang = Convert.ToInt32(hasil.GetValue(11));
					jenis.Nama = hasil.GetValue(3).ToString();

					KategoriBarang kategori = new KategoriBarang();
					kategori.IdKategoriBarang = Convert.ToInt32(hasil.GetValue(12));
					kategori.Nama = hasil.GetValue(4).ToString();

					MerekBarang merek = new MerekBarang();
					merek.IdMerekBarang = Convert.ToInt32(hasil.GetValue(13));
					merek.Nama = hasil.GetValue(5).ToString();

					Barang b = new Barang();
					b.IdBarang = Convert.ToInt32(hasil.GetValue(0));
					b.KodeBarang = hasil.GetValue(1).ToString();
					b.Nama = hasil.GetValue(2).ToString();
					b.Jenis = jenis;
					b.Kategori = kategori;
					b.Merek = merek;
					b.HargaJual = Convert.ToInt32(hasil.GetValue(6));
					if (hasil.GetValue(7) != System.DBNull.Value)
					{
						b.Foto = hasil.GetValue(7).ToString();
					}
					b.DiskonPersenJual = Convert.ToInt32(hasil.GetValue(8));
					b.Satuan = hasil.GetValue(9).ToString();
					b.JumlahStok = Convert.ToInt32(hasil.GetValue(10));

					listBarang.Add(b);
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

		public static string BacaStokBarang(string kriteria, string nilaiKriteria, List<Barang> listBarang)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT kode_barang, nama, jumlah_stok, (select sum(jumlah_stok) FROM barang) as total_stok, (select count(jumlah_stok) from barang where jumlah_stok<=2) AS stok_kurang FROM barang ORDER BY kode_barang";
			}
			else if (kriteria == "kode_barang")
			{
				sql = "SELECT kode_barang, nama, jumlah_stok, (select sum(jumlah_stok) FROM barang) as total_stok, (select count(jumlah_stok) from barang where jumlah_stok<=2) AS stok_kurang FROM barang WHERE kode_barang LIKE '%" + nilaiKriteria + "%' ORDER by kode_barang";
			}
			else if (kriteria == "nama")
			{
				sql = "SELECT kode_barang, nama, jumlah_stok, (select sum(jumlah_stok) FROM barang) as total_stok, (select count(jumlah_stok) from barang where jumlah_stok<=2) AS stok_kurang FROM barang WHERE nama LIKE '%" + nilaiKriteria + "%' ORDER by kode_barang";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Barang barang = new Barang();
					barang.KodeBarang = hasil.GetValue(0).ToString();
					barang.Nama = hasil.GetValue(1).ToString();
					barang.JumlahStok = Convert.ToInt32(hasil.GetValue(2));
					barang.TotalJumlahStok = Convert.ToInt32(hasil.GetValue(3));
					barang.TotalBarangStokKurang = Convert.ToInt32(hasil.GetValue(4));

					listBarang.Add(barang);
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

		public static string TambahData(Barang barang)
		{
			string sql = "INSERT INTO barang(kode_barang, nama, idjenis_barang, idkategori_barang, idmerek_barang, harga_jual, diskon_persen_jual, satuan, jumlah_stok, foto) VALUES ('" + barang.KodeBarang + "', '" + barang.Nama + "', " + barang.Jenis.IdJenisBarang + ", " + barang.Kategori.IdKategoriBarang + ", " + barang.Merek.IdMerekBarang + ", " + barang.HargaJual + ", " + barang.DiskonPersenJual + ", '" + barang.Satuan + "', 0, '" + barang.Foto + "')";

			Koneksi k = new Koneksi();

			MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

			try
			{
				k.Connect();

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

		public static string UbahData(Barang barang, List<Barang> listBarang, string image)
		{
			string sql = "";
			MySqlParameter fotoParam = null;

			if (image == "Tidak Ada")
			{
				sql = "UPDATE barang SET kode_barang='" + barang.KodeBarang + "', nama='" + barang.Nama + "', diskon_persen_jual=" + barang.DiskonPersenJual + ", harga_jual=" + barang.HargaJual + ", idkategori_barang=" + barang.Kategori.IdKategoriBarang + ", idjenis_barang=" + barang.Jenis.IdJenisBarang + ", idmerek_barang=" + barang.Merek.IdMerekBarang + ", satuan='" + barang.Satuan + "' WHERE idbarang=" + barang.IdBarang;

			}
			else if (image == "Hapus")
			{
				sql = "UPDATE barang SET kode_barang='" + barang.KodeBarang + "', nama='" + barang.Nama + "', diskon_persen_jual=" + barang.DiskonPersenJual + ", harga_jual=" + barang.HargaJual + ", idkategori_barang=" + barang.Kategori.IdKategoriBarang + ", idjenis_barang=" + barang.Jenis.IdJenisBarang + ", idmerek_barang=" + barang.Merek.IdMerekBarang + ", satuan='" + barang.Satuan + "', foto=NULL WHERE idbarang=" + barang.IdBarang;
			}
			else if (image == "Ada")
			{

				sql = "UPDATE barang SET kode_barang='" + barang.KodeBarang + "', nama='" + barang.Nama + "', diskon_persen_jual=" + barang.DiskonPersenJual + ", harga_jual=" + barang.HargaJual + ", idkategori_barang=" + barang.Kategori.IdKategoriBarang + ", idjenis_barang=" + barang.Jenis.IdJenisBarang + ", idmerek_barang=" + barang.Merek.IdMerekBarang + ", satuan='" + barang.Satuan + "', foto='" + barang.Foto + "' WHERE idbarang=" + barang.IdBarang;

			}

			Koneksi k = new Koneksi();
			MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

			try
			{
				for (int i = 0; i < listBarang.Count; i++)
				{
					if (barang.KodeBarang.ToLower() == listBarang[i].KodeBarang.ToLower())
					{
						return "Kode barang sudah ada. Harap masukkan kode barang yang lain";
					}
				}

				k.Connect();

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

		public static string TambahStok(int idBarang, int jumlah)
		{
			string sql = "UPDATE barang SET jumlah_stok=jumlah_stok+" + jumlah + " WHERE idbarang=" + idBarang;
			Koneksi k = new Koneksi();
			MySqlCommand cmd = new MySqlCommand(sql, k.KoneksiDB);

			try
			{
				k.Connect();

				cmd.ExecuteNonQuery();

				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
			finally
			{
				cmd.Dispose();
				k.KoneksiDB.Close();
			}
		}

		public static string KurangiStok(int idBarang, int jumlah)
		{
			string sql = "UPDATE barang SET jumlah_stok=jumlah_stok-" + jumlah + " WHERE idbarang=" + idBarang;
			Koneksi k = new Koneksi();
			MySqlCommand cmd = new MySqlCommand(sql, k.KoneksiDB);

			try
			{
				k.Connect();

				cmd.ExecuteNonQuery();

				return "1";
			}
			catch (MySqlException ex)
			{
				return ex.Message + ". Perintah sql : " + sql;
			}
			finally
			{
				cmd.Dispose();
				k.KoneksiDB.Close();
			}
		}

		public static List<string> HapusData(List<Barang> listBarang)
		{
			List<string> listKeterangan = new List<string>();
			string sql = "";
			Koneksi k = new Koneksi();
			MySqlCommand c = null;

			foreach (Barang barang in listBarang)
			{
				sql = "DELETE FROM barang WHERE idbarang=" + barang.IdBarang;

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
						listKeterangan.Add("data barang masih ada di nota pembelian atau nota penjualan");
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

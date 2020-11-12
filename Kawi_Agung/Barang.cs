using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class Barang
	{
		#region DATAMEMBERS

		private int no; // untuk nomor di tabel
		private int idBarang;
		private string kodeBarang;
		private string nama;
		private int hargaJual;
		private int diskonPersenJual;
		private int stokMinimal;
		private byte[] foto;
		private KategoriBarang kategori;
		private JenisBarang jenis;
		private MerekBarang merek;
		private int jumlahStok;
		private string statusStok;
		private string satuan;
		private int diskonPersenBeli;
		private int hargaBeli;

		#endregion

		#region PROPERTIES

		public int No { get => no; set => no = value; }
		public int IdBarang { get => idBarang; set => idBarang = value; }
		public string KodeBarang { get => kodeBarang; set => kodeBarang = value; }
		public string Nama { get => nama; set => nama = value; }
		public int HargaJual { get => hargaJual; set => hargaJual = value; }
		public int DiskonPersenJual { get => diskonPersenJual; set => diskonPersenJual = value; }
		public int StokMinimal { get => stokMinimal; set => stokMinimal = value; }
		public byte[] Foto { get => foto; set => foto = value; }
		public KategoriBarang Kategori { get => kategori; set => kategori = value; }
		public JenisBarang Jenis { get => jenis; set => jenis = value; }
		public MerekBarang Merek { get => merek; set => merek = value; }
		public int JumlahStok { get => jumlahStok; set => jumlahStok = value; }
		public string StatusStok { get => statusStok; set => statusStok = value; }
		public string Satuan { get => satuan; set => satuan = value; }
		public int DiskonPersenBeli { get => diskonPersenBeli; set => diskonPersenBeli = value; }
		public int HargaBeli { get => hargaBeli; set => hargaBeli = value; }

		#endregion

		#region CONSTRUCTORS

		public Barang()
		{
			No = 1;
			IdBarang = 0;
			KodeBarang = "";
			Nama = "";
			HargaJual = 0;
			StokMinimal = 0;
			JumlahStok = 0;
			StatusStok = "";
			Satuan = "";
			DiskonPersenJual = 0;
			DiskonPersenBeli = 0;
			HargaBeli = 0;
		}

		//public Barang(int pIdBarang, string pKodeBarang, string pNama, int pHargaJual, int pStokMinimal, byte[] pFoto, int pJumlahStok, string pStatusStok, string pSatuan, double pDiskonPersenBeli, int pHargaBeli)
		//{
		//	IdBarang = pIdBarang;
		//	KodeBarang = pKodeBarang;
		//	Nama = pNama;
		//	HargaJual = pHargaJual;
		//	StokMinimal = pStokMinimal;
		//	Foto = pFoto;
		//	JumlahStok = pJumlahStok;
		//	StatusStok = pStatusStok;
		//	Satuan = pSatuan;
		//	DiskonPersenBeli = pDiskonPersenBeli;
		//	HargaBeli = pHargaBeli;
		//}

		public Barang(int pNo, int pIdBarang, string pKodeBarang, string pNama, JenisBarang pJenis,	KategoriBarang pKategori, MerekBarang pMerek, int pHargaJual, int pDiskonPersenJual)
		{
			No = pNo;
			IdBarang = pIdBarang;
			KodeBarang = pKodeBarang;
			Nama = pNama;
			Jenis = pJenis;
			Kategori = pKategori;
			Merek = pMerek;
			HargaJual = pHargaJual;
			DiskonPersenJual = pDiskonPersenJual;
		}

		public Barang(int pNo, int pIdBarang, string pKodeBarang, string pNama, JenisBarang pJenis, KategoriBarang pKategori, MerekBarang pMerek, int pHargaJual, int pDiskonPersenJual, byte[] pFoto)
		{
			No = pNo;
			IdBarang = pIdBarang;
			KodeBarang = pKodeBarang;
			Nama = pNama;
			Jenis = pJenis;
			Kategori = pKategori;
			Merek = pMerek;
			HargaJual = pHargaJual;
			DiskonPersenJual = pDiskonPersenJual;
			Foto = pFoto;
		}

		public Barang(string pKodeBarang, string pNama, JenisBarang pJenis, KategoriBarang pKategori, MerekBarang pMerek, int pHargaJual, int pDiskonJual, byte[] pFoto)
		{
			KodeBarang = pKodeBarang;
			Nama = pNama;
			Jenis = pJenis;
			Kategori = pKategori;
			Merek = pMerek;
			HargaJual = pHargaJual;
			DiskonPersenJual = pDiskonJual;
			Foto = pFoto;
		}

		public Barang(int pNo, int pIdBarang, string pKodeBarang, string pNama, JenisBarang pJenis, KategoriBarang pKategori, MerekBarang pMerek, int pHargaJual)
		{
			No = pNo;
			IdBarang = pIdBarang;
			KodeBarang = pKodeBarang;
			Nama = pNama;
			Jenis = pJenis;
			Kategori = pKategori;
			Merek = pMerek;
			HargaJual = pHargaJual;
		}

		#endregion

		#region METHODS

		public static string BacaDataBarang(string kriteria, string nilaiKriteria, List<Barang> listBarang)
		{
			string sql = "";

			if (kriteria == "")
			{
				sql = "SELECT b.idbarang, b.kode_barang, b.nama, j.nama, k.nama, m.nama, b.harga_jual FROM barang b INNER JOIN kategori_barang k ON b.idkategori_barang=k.idkategori_barang INNER JOIN jenis_barang j ON b.idjenis_barang=j.idjenis_barang INNER JOIN merek_barang m ON b.idmerek_barang=m.idmerek_barang";
			}
			else
			{
				sql = "SELECT b.idbarang, b.kode_barang, b.nama, j.nama, k.nama, m.nama, b.harga_jual FROM barang b INNER JOIN kategori_barang k ON b.idkategori_barang=k.idkategori_barang INNER JOIN jenis_barang j ON b.idjenis_barang=j.idjenis_barang INNER JOIN merek_barang m ON b.idmerek_barang=m.idmerek_barang WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
			}
			try
			{
				MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

				int i = 1;
				while (hasil.Read())
				{

					JenisBarang jenis = new JenisBarang(hasil.GetValue(3).ToString());

					KategoriBarang kategori = new KategoriBarang(hasil.GetValue(4).ToString());

					MerekBarang merek = new MerekBarang(hasil.GetValue(5).ToString());

					Barang barang = new Barang(i++, int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(), jenis, kategori, merek, int.Parse(hasil.GetValue(6).ToString()));

					listBarang.Add(barang);
				}
				return "1";
			}
			catch (Exception ex)
			{
				return "Terjadi Kesalahan. Pesan Kesalahan : " + ex.Message;
			}
		}

		public static string BacaDetailBarang(string kriteria, string nilaiKriteria, List<Barang> listBarang)
		{
			string sql = "";

			if (kriteria == "")
			{
				return "Kriteria harus diisi";
			}
			else
			{
				sql = "SELECT b.idbarang, b.kode_barang, b.nama, b.idjenis_barang, j.nama, b.idkategori_barang, k.nama, b.idmerek_barang, m.nama, b.harga_jual, b.diskon_persen_jual, b.foto FROM barang b INNER JOIN kategori_barang k ON b.idkategori_barang=k.idkategori_barang INNER JOIN jenis_barang j ON b.idjenis_barang=j.idjenis_barang INNER JOIN merek_barang m ON b.idmerek_barang=m.idmerek_barang WHERE " + kriteria + "=" + nilaiKriteria;
			}

			try
			{
				MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

				int i = 1;
				while (hasil.Read())
				{
					JenisBarang jenis = new JenisBarang(int.Parse(hasil.GetValue(3).ToString()), hasil.GetValue(4).ToString());

					KategoriBarang kategori = new KategoriBarang(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(6).ToString());

					MerekBarang merek = new MerekBarang(int.Parse(hasil.GetValue(7).ToString()), hasil.GetValue(8).ToString());

					Barang barang = new Barang(i++, int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(), jenis, kategori, merek, int.Parse(hasil.GetValue(9).ToString()), int.Parse(hasil.GetValue(10).ToString()), (byte[])hasil.GetValue(11));

					listBarang.Add(barang);
				}
				return "1";
			}
			catch (Exception ex)
			{
				return "Terjadi Kesalahan. Pesan Kesalahan : " + ex.Message;
			}
		}

		public static string TambahData(Barang barang)
		{
			string sql = "INSERT INTO barang(kode_barang, nama, idjenis_barang, idkategori_barang, idmerek_barang, harga_jual, diskon_persen_jual, foto) VALUES ('" + barang.KodeBarang + "', '" + barang.Nama + "', " + barang.Jenis.IdJenisBarang + ", " + barang.Kategori.IdKategoriBarang + ", " + barang.Merek.IdMerekBarang + ", " + barang.HargaJual + ", " + barang.DiskonPersenJual + ", @foto)";

			var fotoParam = new MySqlParameter("foto", MySqlDbType.Blob);

			fotoParam.Value = barang.foto;

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
		#endregion


	}
}

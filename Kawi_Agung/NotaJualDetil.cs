using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class NotaJualDetil
	{
		#region DATAMEMBERS

		private Barang barang;
		private NotaJual notaJual;
		private int qty;
		private int subTotal;
		private int total;
		private int diskonPersen;

		#endregion

		#region PROPERTIES
		public Barang Barang { get => barang; set => barang = value; }
		public NotaJual NotaJual { get => notaJual; set => notaJual = value; }
		public int Qty { get => qty; set => qty = value; }
		public int SubTotal { get => subTotal; set => subTotal = value; }
		public int Total { get => total; set => total = value; }
		public int DiskonPersen { get => diskonPersen; set => diskonPersen = value; }

		#endregion

		#region CONSTRUCTORS

		public NotaJualDetil()
		{
			Qty = 0;
			SubTotal = 0;
			Total = 0;
			DiskonPersen = 0;
		}

		public NotaJualDetil(Barang pBarang, NotaJual pNotaJual, int pQty, int pSubTotal, int pTotal, int pDiskonPersen)
		{
			Barang = pBarang;
			NotaJual = pNotaJual;
			Qty = pQty;
			SubTotal = pSubTotal;
			Total = pTotal;
			DiskonPersen = pDiskonPersen;

		}

		#endregion

		#region METHODS

		public static string BacaData(string kriteria, string nilaiKriteria, List<NotaJualDetil> listNotaJualDetil)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT njd.idbarang, b.kode_barang, b.nama, b.satuan, nj.idnota_jual, nj.no_faktur, njd.qty, njd.sub_total, njd.total, njd.diskon_persen, nj.tanggal, p.nama FROM nota_jual_detil njd INNER JOIN barang b ON njd.idbarang=b.idbarang INNER JOIN nota_jual nj ON njd.idnota_jual=nj.idnota_jual INNER JOIN pelanggan p ON nj.idpelanggan=p.idpelanggan";
			}
			else
			{
				sql = "SELECT njd.idbarang, b.kode_barang, b.nama, b.satuan, nj.idnota_jual, nj.no_faktur, njd.qty, njd.sub_total, njd.total, njd.diskon_persen, nj.tanggal, p.nama FROM nota_jual_detil njd INNER JOIN barang b ON njd.idbarang=b.idbarang INNER JOIN nota_jual nj ON njd.idnota_jual=nj.idnota_jual INNER JOIN pelanggan p ON nj.idpelanggan=p.idpelanggan WHERE " + kriteria + "='" + nilaiKriteria + "'";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Pelanggan p = new Pelanggan();
					p.Nama = hasil.GetValue(11).ToString();

					Barang b = new Barang();
					b.IdBarang = Convert.ToInt32(hasil.GetValue(0));
					b.KodeBarang = hasil.GetValue(1).ToString();
					b.Nama = hasil.GetValue(2).ToString();
					b.Satuan = hasil.GetValue(3).ToString();

					NotaJual nj = new NotaJual();
					nj.IdNotaJual = Convert.ToInt32(hasil.GetValue(4));
					nj.NoFaktur = hasil.GetValue(5).ToString();
					nj.Tanggal = DateTime.Parse(hasil.GetValue(10).ToString());
					nj.Pelanggan = p;

					NotaJualDetil notaJualDetil = new NotaJualDetil(b, nj, Convert.ToInt32(hasil.GetValue(6)), Convert.ToInt32(hasil.GetValue(7)), Convert.ToInt32(hasil.GetValue(8)), Convert.ToInt32(hasil.GetValue(9)));

					listNotaJualDetil.Add(notaJualDetil);
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


		#endregion
	}
}

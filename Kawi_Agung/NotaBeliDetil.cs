using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class NotaBeliDetil
	{
		#region DATAMEMBERS

		private Barang barang;
		private NotaBeli notaBeli;
		private int qty;
		private int subTotal;
		private int total;
		private int diskonPersen;

		#endregion

		#region PROPERTIES
		public Barang Barang { get => barang; set => barang = value; }
		public NotaBeli NotaBeli { get => notaBeli; set => notaBeli = value; }
		public int Qty { get => qty; set => qty = value; }
		public int SubTotal { get => subTotal; set => subTotal = value; }
		public int Total { get => total; set => total = value; }
		public int DiskonPersen { get => diskonPersen; set => diskonPersen = value; }

		#endregion

		#region CONSTRUCTORS

		public NotaBeliDetil()
		{
			Qty = 0;
			SubTotal = 0;
			Total = 0;
			DiskonPersen = 0;
		}

		public NotaBeliDetil(Barang pBarang, NotaBeli pNotaBeli, int pQty, int pSubTotal, int pTotal, int pDiskonPersen)
		{
			Barang = pBarang;
			NotaBeli = pNotaBeli;
			Qty = pQty;
			SubTotal = pSubTotal;
			Total = pTotal;
			DiskonPersen = pDiskonPersen;

		}

		#endregion

		#region METHODS

		public static string BacaData(string kriteria, string nilaiKriteria, List<NotaBeliDetil> listNotaBeliDetil)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT nbd.idbarang, b.kode_barang, b.nama, b.satuan, nb.idnota_beli, nb.no_faktur, nbd.qty, nbd.sub_total, nbd.total, nbd.diskon_persen, nb.tanggal, s.nama FROM nota_beli_detil nbd INNER JOIN barang b ON nbd.idbarang=b.idbarang INNER JOIN nota_beli nb ON nbd.idnota_beli=nb.idnota_beli INNER JOIN supplier s ON nb.idsupplier=s.idsupplier";
			}
			else
			{
				sql = "SELECT nbd.idbarang, b.kode_barang, b.nama, b.satuan, nb.idnota_beli, nb.no_faktur, nbd.qty, nbd.sub_total, nbd.total, nbd.diskon_persen, nb.tanggal, s.nama FROM nota_beli_detil nbd INNER JOIN barang b ON nbd.idbarang=b.idbarang INNER JOIN nota_beli nb ON nbd.idnota_beli=nb.idnota_beli INNER JOIN supplier s ON nb.idsupplier=s.idsupplier WHERE " + kriteria + "='" + nilaiKriteria + "'";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Supplier s = new Supplier();
					s.Nama = hasil.GetValue(11).ToString();

					Barang b = new Barang();
					b.IdBarang = Convert.ToInt32(hasil.GetValue(0));
					b.KodeBarang = hasil.GetValue(1).ToString();
					b.Nama = hasil.GetValue(2).ToString();
					b.Satuan = hasil.GetValue(3).ToString();

					NotaBeli nb = new NotaBeli();
					nb.IdNotaBeli = Convert.ToInt32(hasil.GetValue(4));
					nb.NoFaktur = hasil.GetValue(5).ToString();
					nb.Tanggal = DateTime.Parse(hasil.GetValue(10).ToString());
					nb.Supplier = s;

					NotaBeliDetil notaBeliDetil = new NotaBeliDetil(b, nb, Convert.ToInt32(hasil.GetValue(6)), Convert.ToInt32(hasil.GetValue(7)), Convert.ToInt32(hasil.GetValue(8)), Convert.ToInt32(hasil.GetValue(9)));

					listNotaBeliDetil.Add(notaBeliDetil);
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

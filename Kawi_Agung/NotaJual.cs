using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class NotaJual
	{
		#region DATA MEMBERS

		private int no;
		private string noFaktur;
		private DateTime tanggal;
		private Pelanggan pelanggan;
		private User user;
		private List<NotaJualDetil> listNotaJualDetil;

		#endregion

		#region PROPERTIES

		public int No { get => no; set => no = value; }
		public string NoFaktur { get => noFaktur; set => noFaktur = value; }
		public DateTime Tanggal { get => tanggal; set => tanggal = value; }
		public Pelanggan Pelanggan { get => pelanggan; set => pelanggan = value; }
		public User User { get => user; set => user = value; }
		public List<NotaJualDetil> ListNotaJualDetil { get => listNotaJualDetil; set => listNotaJualDetil = value; }

		#endregion

		#region CONSTRUCTORS

		public NotaJual()
		{
			No = 0;
			NoFaktur = "";
			ListNotaJualDetil = new List<NotaJualDetil>();
		}

		public NotaJual(int pNo, string pNoFaktur, DateTime pTanggal, Pelanggan pPelanggan, User pUser, List<NotaJualDetil> pListNotaJualDetil)
		{
			No = pNo;
			NoFaktur = pNoFaktur;
			Tanggal = pTanggal;
			Pelanggan = pPelanggan;
			User = pUser;
			ListNotaJualDetil = pListNotaJualDetil;
		}

		#endregion

		#region METHODS

		public static string BacaData(string kriteria, string nilaiKriteria, string nilaiKriteria2, List<NotaJual> listNotaJual)
		{
			string sql = "";
			Koneksi conn = new Koneksi();

			if (kriteria == "")
			{
				sql = "SELECT nj.no_faktur, nj.tanggal, p.nama, u.nama FROM nota_jual nj INNER JOIN pelanggan p ON nj.idpelanggan=p.idpelanggan INNER JOIN user u ON nj.iduser=u.iduser ORDER BY nj.no_faktur";
			}
			else if (kriteria == "nb.tanggal")
			{
				sql = "SELECT nj.no_faktur, nj.tanggal, p.nama, u.nama FROM nota_jual nj INNER JOIN pelanggan p ON nj.idpelanggan=p.idpelanggan INNER JOIN user u ON nj.iduser=u.iduser WHERE " + kriteria + " BETWEEN '" + nilaiKriteria + "' AND '" + nilaiKriteria2 + "' ORDER BY nj.no_faktur";
			}
			else
			{
				sql = "SELECT nj.no_faktur, nj.tanggal, p.nama, u.nama FROM nota_jual nj INNER JOIN pelanggan p ON nj.idpelanggan=p.idpelanggan INNER JOIN user u ON nj.iduser=u.iduser WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%' ORDER BY nj.no_faktur";
			}

			MySqlCommand cmd = new MySqlCommand(sql, conn.KoneksiDB);
			MySqlDataReader hasil = cmd.ExecuteReader();

			try
			{
				while (hasil.Read())
				{
					Pelanggan p = new Pelanggan();
					p.Nama = hasil.GetValue(2).ToString();

					User u = new User();
					u.Nama = hasil.GetValue(3).ToString();

					NotaJual notaJual = new NotaJual();

					notaJual.NoFaktur = hasil.GetValue(0).ToString();
					notaJual.Tanggal = DateTime.Parse(hasil.GetValue(1).ToString());
					notaJual.Pelanggan = p;
					notaJual.User = u;

					listNotaJual.Add(notaJual);
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

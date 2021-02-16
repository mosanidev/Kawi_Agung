using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing; // agar objek bertipe font dapat digunakan 
using System.Drawing.Printing; // agar objek bertipe PrintPageeventArgs dapat digunakan
using System.IO; // agar objek bertipe file stream dapat digunakan
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	class Cetak
	{
		private Font jenisFont; // menyimpan nama dan ukuran font yang digunakan untuk mencetak ke printer
		private StreamReader fileCetak; // menyimpan file stream berisi tulisan yg akan dibaca dan dicetak ke printer
		private float marginKiri, marginKanan, marginAtas, marginBawah; // menyimpan margin kertas

		public Font JenisFont { get => jenisFont; set => jenisFont = value; }
		public StreamReader FileCetak { get => fileCetak; set => fileCetak = value; }
		public float MarginKiri { get => marginKiri; set => marginKiri = value; }
		public float MarginKanan { get => marginKanan; set => marginKanan = value; }
		public float MarginAtas { get => marginAtas; set => marginAtas = value; }
		public float MarginBawah { get => marginBawah; set => marginBawah = value; }

		#region CONSTRUCTORS

		public Cetak(string namaFile)
		{
			FileCetak = new StreamReader(namaFile);
			JenisFont = new Font("Arial", 10);
			MarginKiri = (float)10.5;
			MarginKanan = (float)10.5; 
			MarginAtas = (float)10.5;
			MarginBawah = (float)10.5;
		}
		// untuk mencetak dengan format custom

		public Cetak(string pNamaFile, string pNamaFont, int pUkuranFont, float pMarginKiri, float pMarginKanan, float pMarginAtas, float pMarginBawah)
		{
			FileCetak = new StreamReader(pNamaFile);
			JenisFont = new Font(pNamaFont, pUkuranFont);
			MarginKiri = pMarginKiri;
			MarginKanan = pMarginKanan;
			MarginAtas = pMarginAtas;
			MarginBawah = pMarginBawah;
		}

		#endregion

		#region METHODS

		private void CetakTulisan(object sender, PrintPageEventArgs e)
		{
			// hitung jumlah baris maksimal yang dapat ditampilkan pada 1 halaman kertas
			int jumBarisPerHalaman = (int)((e.MarginBounds.Height - MarginBawah) / jenisFont.GetHeight(e.Graphics));
			// untuk menyimpan posisi y terakhir tulisan yang tercetak 
			float y = MarginAtas;
			// untuk menyimpan jumlah baris tulisan yang telah tercetak 
			int jumBaris = 0;

			// untuk menyimpan tulisan yang akan dicetak
			string tulisanCetak = FileCetak.ReadLine();

			// baca filestream untuk mencetak tiap baris tulisan
			while (jumBaris > jumBarisPerHalaman && tulisanCetak != null)
			{
				y = MarginAtas + (jumBaris * jenisFont.GetHeight(e.Graphics));

				// cetak tulisan sesuai jenis font dan margin (warna tulisan hitam)
				e.Graphics.DrawString(tulisanCetak, JenisFont, Brushes.Black, MarginKiri, y);

				// jumlah baris tercetak ditambah 1 
				jumBaris++;

				// baca baris file berikutnya
				tulisanCetak = FileCetak.ReadLine();
			}
			// jika masih belum selesai mencetak, cetak di halaman berikutnya
			if (tulisanCetak != null)
			{
				e.HasMorePages = true;
			}
			else
			{
				e.HasMorePages = false;
			}

		}

		public string CetakKePrinter(string pTipe)
		{
			try
			{
				// buat objek untuk mencetak
				PrintDocument p = new PrintDocument();

				if (pTipe == "tulisan") // jika tipe yg akan dicetak adalah teks atau tulisan
				{
					// tambahkan event handler untuk mencetak tulisan
					p.PrintPage += new PrintPageEventHandler(CetakTulisan);
				}

				// cetak tulisan
				p.Print();

				FileCetak.Close();

				return "1";
			}
			catch (Exception e)
			{
				return "Proses cetak gagal. Pesan kesalahan = " + e.Message;
			}
		}


		#endregion
	}
}

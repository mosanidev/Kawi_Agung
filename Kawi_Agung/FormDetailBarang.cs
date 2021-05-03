using Kawi_Agung.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kawi_Agung
{
	public partial class FormDetailBarang : Form
	{
		public FormDetailBarang()
		{
			InitializeComponent();
		}

		private void FormDetailBarang_Load(object sender, EventArgs e)
		{
			textBoxKodeBarang.Text = FormMaster.listSelectedBarang[0].KodeBarang;
			textBoxKodeBarang.SelectionStart = textBoxKodeBarang.TextLength;
			labelJumlahStok.Text = FormMaster.listSelectedBarang[0].JumlahStok.ToString();
			labelDetailNamaBarang.Text = FormMaster.listSelectedBarang[0].Nama;
			labelDetailJenisBarang.Text = FormMaster.listSelectedBarang[0].Jenis.Nama;
			labelDetailKategoriBarang.Text = FormMaster.listSelectedBarang[0].Kategori.Nama;
			labelDetailMerekBarang.Text = FormMaster.listSelectedBarang[0].Merek.Nama;
			labelDetailSatuanBarang.Text = FormMaster.listSelectedBarang[0].Satuan;
			labelTotalHargaJual.Text = ConvertToRupiah(FormMaster.listSelectedBarang[0].HargaJual);
			labelDetailDiskonJual.Text = FormMaster.listSelectedBarang[0].DiskonPersenJual.ToString();
			labelDetailHargaJual.Text = ConvertToRupiah(CountPriceBeforeDiscount(FormMaster.listSelectedBarang[0].HargaJual, FormMaster.listSelectedBarang[0].DiskonPersenJual));

			if (FormMaster.listSelectedBarang[0].Foto != "")
			{
				string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\barang";
				string folderName = Path.Combine(projectPath, FormMaster.listSelectedBarang[0].KodeBarang);
				pictureBoxDetailGambarBarang.ImageLocation = folderName + "\\" + "foto" + FormMaster.listSelectedBarang[0].Foto.ToString();
			}
			else
			{
				pictureBoxDetailGambarBarang.Image = Resources.profile_picture;
			}
		}

		string ConvertToRupiah(int number)
		{
			CultureInfo culture = new CultureInfo("id-ID");

			return string.Format(culture, "{0:c0}", number);
		}

		int CountPriceBeforeDiscount(int salePrice, int discount)
		{
			double discountDecimal = (double)discount / 100;

			double discount_x = 1 - discountDecimal;

			return Convert.ToInt32((double)salePrice / discount_x);

		}
	}
}

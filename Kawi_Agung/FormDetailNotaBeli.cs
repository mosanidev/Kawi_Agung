using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
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
	public partial class FormDetailNotaBeli : Form
	{
		public FormDetailNotaBeli()
		{
			InitializeComponent();
		}

		private void FormDetailNotaBeli_Load(object sender, EventArgs e)
		{
			labelNoFaktur.Text = FormMaster.listSelectedNotaBeliDetil[0].NotaBeli.NoFaktur.ToString();
			labelTanggal.Text = FormMaster.listSelectedNotaBeliDetil[0].NotaBeli.Tanggal.ToString("dd MMMM yyyy");
			labelSupplier.Text = FormMaster.listSelectedNotaBeliDetil[0].NotaBeli.Supplier.Nama.ToString();

			labelTotalHargaBarangMasuk.Text = convertToRupiah(FormMaster.listSelectedNotaBeliDetil[0].Total);

			labelTotalDiskonBeli.Text = FormMaster.listSelectedNotaBeliDetil[0].DiskonPersen.ToString() + " %";

			foreach (var item in FormMaster.listSelectedNotaBeliDetil)
			{
				dataGridViewBarangMasuk.Rows.Add(item.Barang.KodeBarang, item.Barang.Nama, item.Barang.Satuan, item.SubTotal.ToString(), item.Qty.ToString());
			}
		}

		string convertToRupiah(int number)
		{
			CultureInfo culture = new CultureInfo("id-ID");

			return string.Format(culture, "{0:c0}", number);
		}

	}
}

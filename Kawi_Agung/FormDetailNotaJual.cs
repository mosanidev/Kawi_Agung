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
using Kawi_Agung.CrystalReports;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

namespace Kawi_Agung
{
	public partial class FormDetailNotaJual : Form
	{
		public FormDetailNotaJual()
		{
			InitializeComponent();
		}

		private FormMaster mainForm = null;
		List<NotaJualDetil> listBarang = new List<NotaJualDetil>();
		string hakAkses = "";
		public FormDetailNotaJual(Form callingForm, string user)
		{
			mainForm = callingForm as FormMaster;
			InitializeComponent();
			hakAkses = user;
		}

		private void FormDetailNotaJual_Load(object sender, EventArgs e)
		{
			if (hakAkses == "Manajer")
			{
				buttonLihatNotaJual.Visible = false;
			}

			textBoxNoFaktur.Text = FormMaster.listSelectedNotaJualDetil[0].NotaJual.NoFaktur.ToString();
			textBoxNoFaktur.SelectionStart = textBoxNoFaktur.TextLength;
			labelTanggal.Text = FormMaster.listSelectedNotaJualDetil[0].NotaJual.Tanggal.ToString("dd MMMM yyyy");
			labelPelanggan.Text = FormMaster.listSelectedNotaJualDetil[0].NotaJual.Pelanggan.Nama.ToString();

			labelTotalHargaBarangKeluar.Text = convertToRupiah(FormMaster.listSelectedNotaJualDetil[0].Total);

			labelTotalDiskonJual.Text = FormMaster.listSelectedNotaJualDetil[0].DiskonPersen.ToString() + " %";

			DataTable t = new DataTable();
			t.Columns.Add("Nomor Faktur", typeof(string));
			t.Columns.Add("Tanggal", typeof(string));
			t.Columns.Add("Pelanggan", typeof(string));
			t.Columns.Add("Pegawai", typeof(string));
			t.Columns.Add("Kode Barang", typeof(string));
			t.Columns.Add("Nama Barang", typeof(string));
			t.Columns.Add("Sat", typeof(string));
			t.Columns.Add("Qty", typeof(int));
			t.Columns.Add("Harga", typeof(int));
			t.Columns.Add("JUMLAH", typeof(int));
			//t.Columns.Add("SUBTOTAL", typeof(int));
			t.Columns.Add("TOTAL", typeof(int));
			t.Columns.Add("DISKON", typeof(int));

			//foreach (var item in FormMaster.listSelectedNotaJualDetil)
			//{
			//	dataGridViewBarangKeluar.Rows.Add(item.Barang.KodeBarang, item.Barang.Nama, item.Barang.Satuan, item.SubTotal.ToString(), item.Qty.ToString());
			//}
			//NumberFormatInfo nfi = (NumberFormatInfo)
			//CultureInfo.InvariantCulture.NumberFormat.Clone();
			//nfi.NumberGroupSeparator = ".";

			foreach (var item in FormMaster.listSelectedNotaJualDetil)
			{
				t.Rows.Add(item.NotaJual.NoFaktur, item.NotaJual.Tanggal.ToString("dd-MM-yyyy"), item.NotaJual.Pelanggan.Nama, item.NotaJual.User.Nama, item.Barang.KodeBarang, item.Barang.Nama, item.Barang.Satuan, item.Qty, item.SubTotal, item.Qty*item.SubTotal, item.Total, item.DiskonPersen);
			}

			dataGridViewBarangKeluar.DataSource = t;

			dataGridViewBarangKeluar.Columns[0].Visible = false;
			dataGridViewBarangKeluar.Columns[1].Visible = false;
			dataGridViewBarangKeluar.Columns[2].Visible = false;
			dataGridViewBarangKeluar.Columns[3].Visible = false;
			dataGridViewBarangKeluar.Columns[10].Visible = false;
			dataGridViewBarangKeluar.Columns[11].Visible = false;


			//for (int i = 0; i < 17; i++)
			//{

			//	dataGridViewBarangKeluar.Rows.Add("item_kode", "item_nama", "item_satuan", 69, 69);

			//}
		}

		private void buttonSimpanPDF_Click(object sender, EventArgs e)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();

			dt.Columns.Add("Nomor Faktur", typeof(string));
			dt.Columns.Add("Tanggal", typeof(string));
			dt.Columns.Add("Pelanggan", typeof(string));
			dt.Columns.Add("Pegawai", typeof(string));
			dt.Columns.Add("Kode Barang", typeof(string));
			dt.Columns.Add("Nama Barang", typeof(string));
			dt.Columns.Add("Sat", typeof(string));
			dt.Columns.Add("Qty", typeof(int));
			dt.Columns.Add("Harga", typeof(int));
			dt.Columns.Add("JUMLAH", typeof(int));
			dt.Columns.Add("TOTAL", typeof(int));
			dt.Columns.Add("DISKON", typeof(int));

			foreach (DataGridViewRow dgv in dataGridViewBarangKeluar.Rows)
			{
				dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value, dgv.Cells[5].Value, dgv.Cells[6].Value, dgv.Cells[7].Value, dgv.Cells[8].Value, dgv.Cells[9].Value, dgv.Cells[10].Value, dgv.Cells[11].Value);
			}

			ds.Tables.Add(dt);
			ds.WriteXmlSchema("notajual.xml");

			string APPPATH = Environment.CurrentDirectory + "\\CrystalReports\\CrystalReport1.rpt";

			ReportDocument cr = new ReportDocument();
			cr.Load(APPPATH);
			cr.SetDataSource(ds);
			FormReportViewer frm = new FormReportViewer();
			frm.crystalReportViewer1.ReportSource = cr;
			frm.ShowDialog();

			cr.Dispose();
			cr.Close();
		}

		private void simpanPDFPart1()
		{
			//double width = 9.5 * 75; // dikali 72 karena di itextsharp ukurannya 72 user unit per inch 
			//double height = 5.5 * 75;
			//iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(width), Convert.ToInt32(height)); // 9.5 x 11 inch
			//Document doc = new Document(pageSize);
			
		}

		string convertToRupiah(int number)
		{
			CultureInfo culture = new CultureInfo("id-ID");

			return string.Format(culture, "{0:c0}", number);
		}

		int hitungDiskon(int harga, int diskon)
		{
			double diskonDesimal = (double)diskon / 100.0;
			return Convert.ToInt32((double)harga - ((double)harga * diskonDesimal));
		}
	}
}

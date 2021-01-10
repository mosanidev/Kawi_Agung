using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
			labelNoFaktur.Text = FormMaster.listSelectedNotaBeliDetil[0].NoFaktur.NoFaktur.ToString();
			labelTanggal.Text = FormMaster.listSelectedNotaBeliDetil[0].NoFaktur.Tanggal.ToString("dd MMMM yyyy");
			labelSupplier.Text = FormMaster.listSelectedNotaBeliDetil[0].NoFaktur.Supplier.Nama.ToString();

			labelTotalHargaBarangMasuk.Text = convertToRupiah(FormMaster.listSelectedNotaBeliDetil[0].Total);

			labelTotalDiskonBeli.Text = FormMaster.listSelectedNotaBeliDetil[0].DiskonPersen.ToString();

			foreach (var item in FormMaster.listSelectedNotaBeliDetil)
			{
				dataGridViewBarangMasuk.Rows.Add(item.IdBarang.KodeBarang, item.IdBarang.Nama, item.IdBarang.Satuan, item.SubTotal.ToString(), item.Qty.ToString());
			}
		}

		string convertToRupiah(int number)
		{
			CultureInfo culture = new CultureInfo("id-ID");

			return string.Format(culture, "{0:c0}", number);
		}
	}
}

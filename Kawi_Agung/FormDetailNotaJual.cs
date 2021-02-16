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
				buttonSimpanPDF.Visible = false;
			}

			labelNoFaktur.Text = FormMaster.listSelectedNotaJualDetil[0].NotaJual.NoFaktur.ToString();
			labelTanggal.Text = FormMaster.listSelectedNotaJualDetil[0].NotaJual.Tanggal.ToString("dd MMMM yyyy");
			labelPelanggan.Text = FormMaster.listSelectedNotaJualDetil[0].NotaJual.Pelanggan.Nama.ToString();

			labelTotalHargaBarangKeluar.Text = convertToRupiah(FormMaster.listSelectedNotaJualDetil[0].Total);

			labelTotalDiskonJual.Text = FormMaster.listSelectedNotaJualDetil[0].DiskonPersen.ToString() + " %";

			foreach (var item in FormMaster.listSelectedNotaJualDetil)
			{
				dataGridViewBarangKeluar.Rows.Add(item.Barang.KodeBarang, item.Barang.Nama, item.Barang.Satuan, item.SubTotal.ToString(), item.Qty.ToString());
			}
			//for (int i = 0; i < 17; i++)
			//{

			//	dataGridViewBarangKeluar.Rows.Add("item_kode", "item_nama", "item_satuan", 69, 69);

			//}
		}

		private void buttonSimpanPDF_Click(object sender, EventArgs e)
		{
			//double result = dataGridViewBarangKeluar.RowCount / 10.0;
			//MessageBox.Show(Math.Ceiling(result).ToString());
			simpanPDFPart1();
		}

		private void simpanPDFPart1()
		{
			using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file|*.pdf", ValidateNames = true })
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					double width = 9.5 * 75; // dikali 72 karena di itextsharp ukurannya 72 user unit per inch 
					double height = 5.5 * 75;
					iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(width), Convert.ToInt32(height)); // 9.5 x 11 inch
					Document doc = new Document(pageSize);
					try
					{
						List<User> listManajer = new List<User>();

						PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
						doc.Open();

						// Report header 
						BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.DARK_GRAY);
						Paragraph prgHeading = new Paragraph();
						prgHeading.Alignment = Element.ALIGN_CENTER;
						prgHeading.Add(new Chunk("Nota Penjualan", fntHead));
						prgHeading.Add("\n");
						doc.Add(prgHeading);

						// baca data manajer 
						User.BacaData("j.nama", "Manajer", listManajer);

						//Profil perusahaan
						Paragraph prgAuthor = new Paragraph();
						BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
						Chunk glue = new Chunk(new VerticalPositionMark());

						prgAuthor.Add(new Chunk("No Faktur\u00a0\u00a0\u00a0\u00a0:" + labelNoFaktur.Text, fntAuthor)); ;
						prgAuthor.Add("\n");
						prgAuthor.Add(new Chunk("Bank A/C\u00a0\u00a0\u00a0\u00a0:" + listManajer[0].NamaBank + " / " + listManajer[0].NoRekening, fntAuthor));
						prgAuthor.Add(new Chunk(glue));
						prgAuthor.Add(new Chunk("Surabaya:\u00a0\u00a0\u00a0" + labelTanggal.Text, fntAuthor));
						prgAuthor.Add("\n");
						prgAuthor.Add(new Chunk("A/N\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0:" + listManajer[0].NamaRekening, fntAuthor));
						prgAuthor.Add(new Chunk(glue));
						prgAuthor.Add(new Chunk("Kepada YTH:\u00a0\u00a0\u00a0" + labelPelanggan.Text, fntAuthor));
						//\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0
						prgAuthor.Add("\n");
						prgAuthor.Add("\n");
						doc.Add(prgAuthor);

						// Write the table
						PdfPTable table = new PdfPTable(6);
						table.DefaultCell.Padding = 0;
						table.WidthPercentage = 100;
						table.SetWidths(new float[] { 1, 3, 1, 1, 1, 1 });

						// Table header
						BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntColumnHeader = new iTextSharp.text.Font(btnColumnHeader, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

						// Table data
						BaseFont btnRow = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntRow = new iTextSharp.text.Font(btnRow, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

						string[] headerText = { "Kode Barang", "Nama Barang", "Sat", "Qty", "Harga" };

						// Table header
						for (int i = 0; i < 6; i++)
						{
							if (i == 5)
							{
								PdfPCell cell = new PdfPCell() { Padding = 2 };
								cell.BackgroundColor = BaseColor.WHITE;
								cell.AddElement(new Chunk("JUMLAH", fntColumnHeader));
								table.AddCell(cell);
							}
							else
							{
								PdfPCell cell = new PdfPCell() { Padding = 2 };
								cell.BackgroundColor = BaseColor.WHITE;
								cell.AddElement(new Chunk(headerText[i], fntColumnHeader));
								table.AddCell(cell);
							}
						}

						int subTotal = 0;

						if (dataGridViewBarangKeluar.RowCount > 10)
						{
							for (int i = 0; i < dataGridViewBarangKeluar.RowCount; i++)
							{
								if (i < 10)
								{
									subTotal += Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[3].Value) * Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[4].Value);
									PdfPCell[] cells = new PdfPCell[] {new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[0].Value.ToString(), fntRow)),
									new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[1].Value.ToString(), fntRow)),
									new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[2].Value.ToString(), fntRow)),
									new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[4].Value.ToString(), fntRow)),
									new PdfPCell(new Phrase (String.Format("{0:n0}", Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[3].Value)).Replace(",","."), fntRow)),
									new PdfPCell(new Phrase (String.Format("{0:n0}", Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[3].Value) * Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[4].Value)).Replace(",","."), fntRow)) };

									PdfPRow row = new PdfPRow(cells);

									table.Rows.Add(row);
								}
								else if (i > 10)
								{
									simpanPDFPart2();
									break;
								}
							}
						}
						else
						{
							for (int i = 0; i < 10; i++)
							{
								if (i < dataGridViewBarangKeluar.RowCount)
								{
									subTotal += Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[3].Value) * Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[4].Value);
									PdfPCell[] cells = new PdfPCell[] {new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[0].Value.ToString(), fntRow)),
									new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[1].Value.ToString(), fntRow)),
									new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[2].Value.ToString(), fntRow)),
									new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[4].Value.ToString(), fntRow)),
									new PdfPCell(new Phrase (String.Format("{0:n0}", Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[3].Value)).Replace(",","."), fntRow)),
									new PdfPCell(new Phrase (String.Format("{0:n0}", Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[3].Value) * Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[4].Value)).Replace(",","."), fntRow)) };

									PdfPRow row = new PdfPRow(cells);

									table.Rows.Add(row);
								}
								else
								{
									PdfPCell[] cells_ = new PdfPCell[] {new PdfPCell(new Phrase (" ", fntRow)),
									new PdfPCell(new Phrase (" ", fntRow)),
									new PdfPCell(new Phrase (" ", fntRow)),
									new PdfPCell(new Phrase (" ", fntRow)),
									new PdfPCell(new Phrase (" ", fntRow)),
									new PdfPCell(new Phrase (" ", fntRow))};

									PdfPRow row_ = new PdfPRow(cells_);

									table.Rows.Add(row_);
								}
							}
						}

						doc.Add(table);

						// Info Subtotal
						Paragraph prgInfoNominal = new Paragraph();
						Paragraph prgInfoRetur = new Paragraph();
						BaseFont btnNominal = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntNominal = new iTextSharp.text.Font(btnNominal, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
						BaseFont btnKecil = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntKecil = new iTextSharp.text.Font(btnNominal, 9, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.BLACK);

						prgInfoNominal.Alignment = Element.ALIGN_RIGHT;
						prgInfoRetur.Alignment = Element.ALIGN_LEFT;
						Chunk glue_ = new Chunk(new VerticalPositionMark());

						prgInfoNominal.TabSettings = new TabSettings(10);
						prgInfoNominal.Add(new Chunk("Subtotal\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0", fntNominal));
						prgInfoNominal.Add(new Chunk(convertToRupiah(subTotal), fntNominal));
						prgInfoNominal.Add("\n");
						prgInfoRetur.Add(new Chunk("Barang yang sudah dibeli tidak dapat ditukar/dikembalikan", fntKecil));
						prgInfoNominal.Add(new Chunk("Disc\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0", fntNominal));
						prgInfoNominal.Add(new Chunk(labelTotalDiskonJual.Text, fntNominal));
						prgInfoNominal.Add("\n");
						prgInfoNominal.Add(new Chunk("Total\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0", fntNominal));
						prgInfoNominal.Add(new Chunk(convertToRupiah(hitungDiskon(subTotal, int.Parse(labelTotalDiskonJual.Text.TrimEnd(new Char[] { '%' })))) + "\u00a0", fntNominal));
						prgInfoRetur.Add("\n");

						doc.Add(prgInfoNominal);
						doc.Add(prgInfoRetur);

						LineSeparator sep = new LineSeparator();
						sep.Offset = -5;
						doc.Add(sep);

						Paragraph prgMengetahui = new Paragraph();
						prgMengetahui.Alignment = Element.ALIGN_RIGHT;

						prgMengetahui.Add(new Chunk("Mengetahui,\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0", fntNominal));
						doc.Add(prgMengetahui);

					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					finally
					{
						doc.Close();
					}
				}
			}
		}

		private void simpanPDFPart2()
		{
			using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file|*.pdf", ValidateNames = true })
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					double width = 9.5 * 75; // dikali 72 karena di itextsharp ukurannya 72 user unit per inch 
					double height = 5.5 * 75;
					iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(Convert.ToInt32(width), Convert.ToInt32(height)); // 9.5 x 11 inch
					Document doc = new Document(pageSize);
					try
					{
						List<User> listManajer = new List<User>();

						PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
						doc.Open();

						// Report header 
						BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.DARK_GRAY);
						Paragraph prgHeading = new Paragraph();
						prgHeading.Alignment = Element.ALIGN_CENTER;
						prgHeading.Add(new Chunk("Nota Penjualan", fntHead));
						prgHeading.Add("\n");
						doc.Add(prgHeading);

						// baca data manajer 
						User.BacaData("j.nama", "Manajer", listManajer);

						//Profil perusahaan
						Paragraph prgAuthor = new Paragraph();
						BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
						Chunk glue = new Chunk(new VerticalPositionMark());

						prgAuthor.Add(new Chunk("No Faktur\u00a0\u00a0\u00a0\u00a0:" + labelNoFaktur.Text, fntAuthor)); ;
						prgAuthor.Add("\n");
						prgAuthor.Add(new Chunk("Bank A/C\u00a0\u00a0\u00a0\u00a0:" + listManajer[0].NamaBank + " / " + listManajer[0].NoRekening, fntAuthor));
						prgAuthor.Add(new Chunk(glue));
						prgAuthor.Add(new Chunk("Surabaya:\u00a0\u00a0\u00a0" + labelTanggal.Text, fntAuthor));
						prgAuthor.Add("\n");
						prgAuthor.Add(new Chunk("A/N\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0:" + listManajer[0].NamaRekening, fntAuthor));
						prgAuthor.Add(new Chunk(glue));
						prgAuthor.Add(new Chunk("Kepada YTH:\u00a0\u00a0\u00a0" + labelPelanggan.Text, fntAuthor));
						//\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0
						prgAuthor.Add("\n");
						prgAuthor.Add("\n");
						doc.Add(prgAuthor);

						// Write the table
						PdfPTable table = new PdfPTable(6);
						table.DefaultCell.Padding = 0;
						table.WidthPercentage = 100;
						table.SetWidths(new float[] { 1, 3, 1, 1, 1, 1 });

						// Table header
						BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntColumnHeader = new iTextSharp.text.Font(btnColumnHeader, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

						// Table data
						BaseFont btnRow = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntRow = new iTextSharp.text.Font(btnRow, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

						string[] headerText = { "Kode Barang", "Nama Barang", "Sat", "Qty", "Harga" };

						// Table header
						for (int i = 0; i < 6; i++)
						{
							if (i == 5)
							{
								PdfPCell cell = new PdfPCell() { Padding = 2 };
								cell.BackgroundColor = BaseColor.WHITE;
								cell.AddElement(new Chunk("JUMLAH", fntColumnHeader));
								table.AddCell(cell);
							}
							else
							{
								PdfPCell cell = new PdfPCell() { Padding = 2 };
								cell.BackgroundColor = BaseColor.WHITE;
								cell.AddElement(new Chunk(headerText[i], fntColumnHeader));
								table.AddCell(cell);
							}
						}

						int subTotal = 0;

						for (int i = 10; i < 20; i++)
						{
							if (i < dataGridViewBarangKeluar.RowCount)
							{
								subTotal += Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[3].Value) * Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[4].Value);
								PdfPCell[] cells = new PdfPCell[] {new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[0].Value.ToString(), fntRow)),
								new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[1].Value.ToString(), fntRow)),
								new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[2].Value.ToString(), fntRow)),
								new PdfPCell(new Phrase (dataGridViewBarangKeluar.Rows[i].Cells[4].Value.ToString(), fntRow)),
								new PdfPCell(new Phrase (String.Format("{0:n0}", Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[3].Value)).Replace(",","."), fntRow)),
								new PdfPCell(new Phrase (String.Format("{0:n0}", Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[3].Value) * Convert.ToInt32(dataGridViewBarangKeluar.Rows[i].Cells[4].Value)).Replace(",","."), fntRow)) };

								PdfPRow row = new PdfPRow(cells);

								table.Rows.Add(row);
							}
							else 
							{
								PdfPCell[] cells_ = new PdfPCell[] {new PdfPCell(new Phrase (" ", fntRow)),
								new PdfPCell(new Phrase (" ", fntRow)),
								new PdfPCell(new Phrase (" ", fntRow)),
								new PdfPCell(new Phrase (" ", fntRow)),
								new PdfPCell(new Phrase (" ", fntRow)),
								new PdfPCell(new Phrase (" ", fntRow))};

								PdfPRow row_ = new PdfPRow(cells_);

								table.Rows.Add(row_);
							}
						}

						doc.Add(table);

						// Info Subtotal
						Paragraph prgInfoNominal = new Paragraph();
						Paragraph prgInfoRetur = new Paragraph();
						BaseFont btnNominal = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntNominal = new iTextSharp.text.Font(btnNominal, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
						BaseFont btnKecil = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
						iTextSharp.text.Font fntKecil = new iTextSharp.text.Font(btnNominal, 9, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.BLACK);

						prgInfoNominal.Alignment = Element.ALIGN_RIGHT;
						prgInfoRetur.Alignment = Element.ALIGN_LEFT;
						Chunk glue_ = new Chunk(new VerticalPositionMark());

						prgInfoNominal.TabSettings = new TabSettings(10);
						prgInfoNominal.Add(new Chunk("Subtotal\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0", fntNominal));
						prgInfoNominal.Add(new Chunk(convertToRupiah(subTotal), fntNominal));
						prgInfoNominal.Add("\n");
						prgInfoRetur.Add(new Chunk("Barang yang sudah dibeli tidak dapat ditukar/dikembalikan", fntKecil));
						prgInfoNominal.Add(new Chunk("Disc\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0", fntNominal));
						prgInfoNominal.Add(new Chunk(labelTotalDiskonJual.Text, fntNominal));
						prgInfoNominal.Add("\n");
						prgInfoNominal.Add(new Chunk("Total\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0", fntNominal));
						prgInfoNominal.Add(new Chunk(convertToRupiah(hitungDiskon(subTotal, int.Parse(labelTotalDiskonJual.Text.TrimEnd(new Char[] { '%' })))) + "\u00a0", fntNominal));
						prgInfoRetur.Add("\n");

						doc.Add(prgInfoNominal);
						doc.Add(prgInfoRetur);

						LineSeparator sep = new LineSeparator();
						sep.Offset = -5;
						doc.Add(sep);

						Paragraph prgMengetahui = new Paragraph();
						prgMengetahui.Alignment = Element.ALIGN_RIGHT;

						prgMengetahui.Add(new Chunk("Mengetahui,\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0", fntNominal));
						doc.Add(prgMengetahui);

					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					finally
					{
						doc.Close();
					}
				}
			}
		}
		public PdfPCell getCell(String text, int alignment)
		{
			PdfPCell cell = new PdfPCell(new Phrase(text));
			cell.Padding = 0;
			cell.HorizontalAlignment = alignment;
			cell.Border = PdfPCell.NO_BORDER;
			return cell;
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

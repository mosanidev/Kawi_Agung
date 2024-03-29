﻿using ePOSOne.btnProduct;
using FontAwesome.Sharp;
using Kawi_Agung.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Kawi_Agung
{
    public partial class FormMaster : Form
    {
		#region GLOBAL VARIABLE 

		public ArrayList allPanels = new ArrayList();
		public ArrayList allMainButtons = new ArrayList();
		public ArrayList allFlowPanels = new ArrayList();

		public List<MerekBarang> listMerek = new List<MerekBarang>();
		public static List<MerekBarang> listSelectedMerek = new List<MerekBarang>();

		public List<KategoriBarang> listKategori = new List<KategoriBarang>();
		public static List<KategoriBarang> listSelectedKategori = new List<KategoriBarang>();

		public List<JenisBarang> listJenis = new List<JenisBarang>();
		public static List<JenisBarang> listSelectedJenis = new List<JenisBarang>();

		public List<Barang> listBarang = new List<Barang>();
		public static List<Barang> listSelectedBarang = new List<Barang>();

		public List<Supplier> listSupplier = new List<Supplier>();
		public static List<Supplier> listSelectedSupplier = new List<Supplier>();

		public List<Pelanggan> listPelanggan = new List<Pelanggan>();
		public static List<Pelanggan> listSelectedPelanggan = new List<Pelanggan>();

		public List<NotaBeli> listNotaBeli = new List<NotaBeli>();
		public static List<NotaBeli> listSelectedNotaBeli = new List<NotaBeli>();

		public static List<NotaBeliDetil> listSelectedNotaBeliDetil = new List<NotaBeliDetil>();

		public List<NotaJual> listNotaJual = new List<NotaJual>();
		public static List<NotaJual> listSelectedNotaJual = new List<NotaJual>();

		public static List<NotaJualDetil> listSelectedNotaJualDetil = new List<NotaJualDetil>();

		public List<User> listPegawai = new List<User>();
		public static List<User> listSelectedPegawai = new List<User>();

		public List<User> listUserInfo = new List<User>();
		public List<User> listUser = new List<User>();
		string pathFoto = "";

		#endregion

		public FormMaster()
        {
            InitializeComponent();
		}

		public void FormMaster_Load(object sender, EventArgs e)
		{
			timerRunningText.Start();

			allPanels.Clear();
			allFlowPanels.Clear();
			allMainButtons.Clear();

			addPanels();
			addFlowPanels();
			addButtons();

			CekJabatanUser();

			comboBoxKriteriaBarang.SelectedIndex = 0;
			comboBoxKriteriaBarangKeluar.SelectedIndex = 0;
			comboBoxKriteriaBarangMasuk.SelectedIndex = 0;
			comboBoxKriteriaLaporanStokBarang.SelectedIndex = 0;

			dateTimePickerTanggalAwalNotaBeli.Value = DateTime.Today;
			dateTimePickerTanggalAkhirNotaBeli.Value = dateTimePickerTanggalAwalNotaBeli.Value.AddDays(+1);

			ClearAllList();
			clearAllText();

			AddUserInfo();

			// query all user to list user
			//User.BacaData("", "", listUser);

			dateTimePickerTanggalAkhirNotaBeli.Value = DateTime.Now;
			dateTimePickerTanggalAwalNotaBeli.Value = DateTime.Now.AddDays(-7);

			dateTimePickerTanggalAkhirNotaJual.Value = DateTime.Now;
			dateTimePickerTglAwalNotaJual.Value = DateTime.Now.AddDays(-7);

			dateTimePickerTanggalAkhirLaporanPenjualan.Value = DateTime.Now;
			dateTimePickerTanggalAwalLaporanPenjualan.Value = DateTime.Now.AddDays(-7);

			dateTimePickerTanggalAkhirLaporanPembelian.Value = DateTime.Now;
			dateTimePickerTanggalAwalLaporanPembelian.Value = DateTime.Now.AddDays(-7);
		}

		public void clearAllText()
		{
			textBoxSearchBarang.Clear();
			textBoxSearchBarangMasuk.Clear();
			textBoxSearchBarangKeluar.Clear();
			textBoxSearchJenisBrg.Clear();
			textBoxSearchKategoriBrg.Clear();
			textBoxSearchMerekBrg.Clear();
			textBoxSearchNamaSupplier.Clear();
			textBoxSearchPegawai.Clear();
			textBoxSearchNamaPelanggan.Clear();
			textBoxSearchBrgInventaris.Clear();
		}

		private void ClearAllList()
		{
			listJenis.Clear();
			listMerek.Clear();
			listBarang.Clear();
			listKategori.Clear();
			listSupplier.Clear();
			listPelanggan.Clear();
			listUserInfo.Clear();
			listNotaJual.Clear();
			listNotaBeli.Clear();
			listPegawai.Clear();
			listUser.Clear();
		}

		private int PopulateLaporanPenjualanTable(string tanggalAwal, string tanggalAkhir)
		{
			listNotaJual.Clear();

			string hasilBacaLaporanPenjualan = NotaJual.BacaDataTotalPemasukan(tanggalAwal, tanggalAkhir, listNotaJual);

			dataGridViewLaporanPenjualan.Rows.Clear();

			int num = 1;
			int totalPemasukan = 0;

			for (int i = 0; i < listNotaJual.Count; i++)
			{
				dataGridViewLaporanPenjualan.Rows.Add(num++, listNotaJual[i].Tanggal.ToString("dd MMMM yyyy"), listNotaJual[i].TotalPemasukan);
				totalPemasukan += listNotaJual[i].TotalPemasukan;
			}

			labelNominalTotalPenjualan.Text = ConvertToRupiah(totalPemasukan);

			return listNotaJual.Count;
		}

		private int PopulateLaporanStokTable(string kriteria, string nilaiKriteria)
		{
			listBarang.Clear();

			string hasilBacaBarang = Barang.BacaStokBarang(kriteria, nilaiKriteria, listBarang);

			dataGridViewLaporanStokBarang.Rows.Clear();

			int num = 1;
			int totalJumlahBarang = 0;
			int totalBarangJumlahKurang = 0;

			for (int i = 0; i < listBarang.Count; i++)
			{
				totalJumlahBarang = listBarang[i].TotalJumlahStok;
				totalBarangJumlahKurang = listBarang[i].TotalBarangStokKurang;

				if (listBarang[i].JumlahStok <= 2)
				{
					dataGridViewLaporanStokBarang.Rows.Add(num++, listBarang[i].KodeBarang, listBarang[i].Nama, listBarang[i].JumlahStok, "Stok Kurang");
				}
				else
				{
					dataGridViewLaporanStokBarang.Rows.Add(num++, listBarang[i].KodeBarang, listBarang[i].Nama, listBarang[i].JumlahStok, "Stok Cukup");
				}
			}

			labelTotalJumlahBarang.Text = totalJumlahBarang.ToString();
			labelTotalJumlahBarangMinim.Text = totalBarangJumlahKurang.ToString();

			return listBarang.Count;
		}

		private int PopulateLaporanPembelianTable(string tanggalAwal, string tanggalAkhir)
		{
			listNotaBeli.Clear();

			string hasilBacaLaporanPembelian = NotaBeli.BacaDataTotalPengeluaran(tanggalAwal, tanggalAkhir, listNotaBeli);

			dataGridViewLaporanPembelian.Rows.Clear();

			int num = 1;
			int totalPengeluaran = 0;

			for (int i = 0; i < listNotaBeli.Count; i++)
			{
				dataGridViewLaporanPembelian.Rows.Add(num++, listNotaBeli[i].Tanggal.ToString("dd MMMM yyyy"), listNotaBeli[i].TotalPengeluaran);
				totalPengeluaran += listNotaBeli[i].TotalPengeluaran;
			}

			labelTotalPengeluaran.Text = ConvertToRupiah(totalPengeluaran);

			return listNotaBeli.Count;
		}

		public int PopulatePegawaiTable(string kriteria, string nilaiKriteria)
		{
			listPegawai.Clear();

			string hasilBaca = User.BacaPegawai(kriteria, nilaiKriteria, listPegawai);

			dataGridViewPegawai.Rows.Clear();

			int num = 1;

			for (int i = 0; i < listPegawai.Count; i++)
			{
				dataGridViewPegawai.Rows.Add(num++, listPegawai[i].IdUser, listPegawai[i].Nama, listPegawai[i].Username, listPegawai[i].Jabatan.IdJabatan, listPegawai[i].Jabatan.Nama, listPegawai[i].Status);
			}

			return listPegawai.Count;
		}

		public int PopulateNotaBeliTable(string kriteria, string nilaiKriteria, string nilaiKriteria2)
		{
			listNotaBeli.Clear();

			string hasilBaca = NotaBeli.BacaData(kriteria, nilaiKriteria, nilaiKriteria2, listNotaBeli);

			dataGridViewBarangMasuk.Rows.Clear();

			int num = 1;
			for (int i = 0; i < listNotaBeli.Count; i++)
			{
				dataGridViewBarangMasuk.Rows.Add(num++, listNotaBeli[i].IdNotaBeli, listNotaBeli[i].NoFaktur, listNotaBeli[i].Tanggal.ToString("dd MMMM yyyy"), listNotaBeli[i].Supplier.Nama, listNotaBeli[i].User.Nama);
			}

			return listNotaBeli.Count();
		}

		public int PopulateNotaJualTable(string kriteria, string nilaiKriteria, string nilaiKriteria2)
		{
			listNotaJual.Clear();

			string hasilBaca = NotaJual.BacaData(kriteria, nilaiKriteria, nilaiKriteria2, listNotaJual);

			dataGridViewBarangKeluar.Rows.Clear();

			int num = 1;
			for (int i = 0; i < listNotaJual.Count; i++)
			{
				dataGridViewBarangKeluar.Rows.Add(num++, listNotaJual[i].IdNotaJual, listNotaJual[i].NoFaktur, listNotaJual[i].Tanggal.ToString("dd MMMM yyyy"), listNotaJual[i].Pelanggan.Nama, listNotaJual[i].User.Nama);
			}

			return listNotaJual.Count();
		}

		public int PopulateBarangTable(string kriteria, string nilaiKriteria)
		{
			listBarang.Clear();

			string hasilBaca = Barang.BacaDataBarang(kriteria, nilaiKriteria, listBarang);

			dataGridViewDaftarBarang.Rows.Clear();

			int num = 1;
			for (int i = 0; i < listBarang.Count; i++)
			{
				dataGridViewDaftarBarang.Rows.Add(num++, listBarang[i].IdBarang, listBarang[i].KodeBarang, listBarang[i].Nama, listBarang[i].Jenis.IdJenisBarang, listBarang[i].Jenis.Nama, listBarang[i].Kategori.IdKategoriBarang, listBarang[i].Kategori.Nama, listBarang[i].Merek.IdMerekBarang, listBarang[i].Merek.Nama, ConvertToRupiah(listBarang[i].HargaJual), listBarang[i].DiskonPersenJual);
			}

			return listBarang.Count;
		}

		string ConvertToRupiah(int number)
		{
			CultureInfo culture = new CultureInfo("id-ID");

			return string.Format(culture, "{0:c0}", number);
		}

		private void AddUserInfo()
		{
			string hasilbaca = User.BacaData("u.username", labelNamaUser.Text, listUserInfo);

			FormLogin frm = new FormLogin();

			textBoxPasswordProfilUser.PasswordChar = '*';

			if (listUserInfo.Count > 0)
			{
				labelProfilIdUser.Text = listUserInfo[0].IdUser.ToString();
				labelNamaUser.Text = listUserInfo[0].Nama;
				labelProfilNama.Text = listUserInfo[0].Nama;
				textBoxNoTelpProfilUser.Text = listUserInfo[0].NoTelp;
				labelProfilTglLahir.Text = listUserInfo[0].TanggalLahir.ToString("dd MMMM yyyy");
				labelProfilUser.Text = listUserInfo[0].Username;
				labelProfilJabatan.Text = listUserInfo[0].Jabatan.Nama;
				richTextBoxAlamatProfilUser.Text = listUserInfo[0].Alamat;
				textBoxPasswordProfilUser.Text = frm.DecryptPassword(listUserInfo[0].Password);
				labelJenisKelaminProfilUser.Text = listUserInfo[0].JenisKelamin;

				if (listUserInfo[0].Foto != "")
				{
					string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\users";
					string folderName = Path.Combine(projectPath, labelProfilUser.Text);
					iconPictureBoxUser.ImageLocation = folderName + "\\" + "foto" + listUserInfo[0].Foto.ToString();
					iconPictureBoxFotoProfil.ImageLocation = folderName + "\\" + "foto" + listUserInfo[0].Foto.ToString();
				}
				else
				{
					iconPictureBoxUser.Image = Resources.profile_picture; 
					iconPictureBoxFotoProfil.Image = Resources.profile_picture;
				}
			}
		}

		public int PopulatePelangganTable(string kriteria, string nilaiKriteria)
		{
			listPelanggan.Clear();

			string hasilBaca = Pelanggan.BacaData(kriteria, nilaiKriteria, listPelanggan);

			dataGridViewDaftarPelanggan.Rows.Clear();

			int num = 1;
			for (int i = 0; i < listPelanggan.Count; i++)
			{
				dataGridViewDaftarPelanggan.Rows.Add(num++, listPelanggan[i].IdPelanggan, listPelanggan[i].Nama, listPelanggan[i].NoTelp, listPelanggan[i].Alamat);
			}
			
			return listPelanggan.Count;
		}

		public int PopulateSupplierTable(string kriteria, string nilaiKriteria)
		{
			listSupplier.Clear();

			string hasilBaca = Supplier.BacaData(kriteria, nilaiKriteria, listSupplier);

			dataGridViewDaftarSupplier.Rows.Clear();

			int num = 1;
			for (int i = 0; i < listSupplier.Count; i++)
			{
				dataGridViewDaftarSupplier.Rows.Add(num++, listSupplier[i].IdSupplier, listSupplier[i].Nama, listSupplier[i].NoTelp, listSupplier[i].Alamat);
			}

			return listSupplier.Count;

		}

		public int PopulateJenisTable(string kriteria, string nilaiKriteria)
		{
			listJenis.Clear();

			string hasilBaca = JenisBarang.BacaData(kriteria, nilaiKriteria, listJenis);

			dataGridViewDaftarJenisBrg.Rows.Clear();

			int num = 1;
			for (int i = 0; i < listJenis.Count; i++)
			{
				dataGridViewDaftarJenisBrg.Rows.Add(num++, listJenis[i].IdJenisBarang, listJenis[i].Nama);
			}

			return listJenis.Count;
		}

		public int PopulateMerekTable(string kriteria, string nilaiKriteria)
		{
			listMerek.Clear();

			string hasilBaca =  MerekBarang.BacaData(kriteria, nilaiKriteria, listMerek);

			dataGridViewSubMenuMerekBrg.Rows.Clear();

			int num = 1;
			for (int i = 0; i < listMerek.Count; i++)
			{
				dataGridViewSubMenuMerekBrg.Rows.Add(num++, listMerek[i].IdMerekBarang, listMerek[i].Nama);
			}

			return listMerek.Count;
			
		}

		public int PopulateKategoriTable(string kriteria, string nilaiKriteria)
		{
			listKategori.Clear();

			string hasilBaca = KategoriBarang.BacaData(kriteria, nilaiKriteria, listKategori); // panggil method baca data tabel kategori yang akan di isi ke listkategori

			dataGridViewSubMenuKategoriBrg.Rows.Clear();

			int num = 1;
			for (int i = 0; i < listKategori.Count; i++)
			{
				dataGridViewSubMenuKategoriBrg.Rows.Add(num++, listKategori[i].IdKategoriBarang, listKategori[i].Nama);
			}

			return listKategori.Count;
		}
		private void CekJabatanUser()
		{
			string jabatan = labelJabatanUser.Text.ToString();

			if (jabatan == "Manajer")
			{
				iconButtonPelanggan.Visible = false;

				// sembunyikan button di nota beli untuk hak akses manajer
				buttonTambahBarangMasuk.Visible = false;
				buttonHapusBarangMasuk.Visible = false;

				// sembunyikan button di nota jual untuk hak akses manajer
				buttonTambahNotaJual.Visible = false;
				buttonHapusNotaJual.Visible = false;

				iconButtonJenisBarang.Visible = false;
				iconButtonKategoriBarang.Visible = false;
				iconButtonMerekBarang.Visible = false;

				// button di modul barang
				buttonTambahDaftarBarang.Visible = false;
				buttonUbahDaftarBarang.Visible = false;
				buttonHapusDaftarBarang.Visible = false;

				iconButtonSupplier.Visible = false;
			}
			else if (jabatan == "Admin Pembelian")
			{
				iconButtonLaporan.Visible = false;
				iconButtonPelanggan.Visible = false;
				iconButtonPegawai.Visible = false;
				iconButtonBarangKeluar.Visible = false;
			}
			else if (jabatan == "Admin Penjualan")
			{
				iconButtonLaporan.Visible = false;
				iconButtonSupplier.Visible = false;
				iconButtonPegawai.Visible = false;
				iconButtonBarangMasuk.Visible = false;
			}
		}

		private void addButtons()
		{
			allMainButtons.Add(iconButtonBarang);
			allMainButtons.Add(iconButtonTransaksi);
			allMainButtons.Add(iconButtonPelanggan);
			allMainButtons.Add(iconButtonSupplier);
			allMainButtons.Add(iconButtonPegawai);
			allMainButtons.Add(iconButtonLaporan);
			allMainButtons.Add(iconButtonProfil);
		}
		private void addPanels()
		{
			allPanels.Add(panelContent);
			allPanels.Add(panelMenuBarangDashboard);
			allPanels.Add(panelSubMenuDaftarBarang);
			allPanels.Add(panelSubMenuJenisBarang);
			allPanels.Add(panelSubMenuKategoriBarang);
			allPanels.Add(panelSubMenuMerekBarang);
			allPanels.Add(panelMenuTransaksi);
			allPanels.Add(panelSubMenuTransaksiBarangMasuk);
			allPanels.Add(panelSubMenuTransaksiBarangKeluar);
			allPanels.Add(panelMenuLaporan);
			allPanels.Add(panelSubMenuLaporanPenjualan);
			allPanels.Add(panelSubMenuLaporanPembelian);
			allPanels.Add(panelSubMenuLaporanStokBarang);
			allPanels.Add(panelMenuPegawai);
			allPanels.Add(panelMenuSupplier);
			allPanels.Add(panelMenuProfil);
			allPanels.Add(panelMenuPelanggan);
		}

		private void addFlowPanels()
		{
			allFlowPanels.Add(flowLayoutPanelBarangKeluar);
			allFlowPanels.Add(flowLayoutPanelBarangMasuk);
			allFlowPanels.Add(flowLayoutPanelDaftarbarang);
			allFlowPanels.Add(flowLayoutPanelKategoriBarang);
			allFlowPanels.Add(flowLayoutPanelSubMenuJenisBarang);
			allFlowPanels.Add(flowLayoutPanelBarang);
			allFlowPanels.Add(flowLayoutPanelMerekBarang);
			allFlowPanels.Add(flowLayoutPanelTransaksi);
			allFlowPanels.Add(flowLayoutPanelMenuLaporan);
			allFlowPanels.Add(flowLayoutPanelLaporanPenjualan);
			allFlowPanels.Add(flowLayoutPanelLaporanStokBarang);
			allFlowPanels.Add(flowLayoutPanelLaporanPembelian);
			allFlowPanels.Add(flowLayoutPanelSupplier);
		}
		
		#region BUTTON HOVER 
		private void iconButtonLogout_MouseHover(object sender, EventArgs e)
		{
			//activeButtonIndicator(panelButtonIndicator2, iconButtonLogout, Color.FromArgb(161, 90, 115));
			panelButtonIndicator2.Visible = true;
			panelButtonIndicator2.Location = iconButtonLogout.Location;
			panelButtonIndicator2.BackColor = Color.FromArgb(161, 90, 115);
			iconButtonLogout.ForeColor = Color.FromArgb(161, 90, 115);
		}

		private void iconButtonLogout_MouseLeave(object sender, EventArgs e)
		{
			panelButtonIndicator2.Visible = false;
			iconButtonLogout.ForeColor = Color.AliceBlue;
		}
		private void iconButtonMerekBarang_MouseHover(object sender, EventArgs e)
		{
			iconButtonMerekBarang.ForeColor = Color.AliceBlue;
		}

		private void iconButtonBarangBarang_MouseHover(object sender, EventArgs e)
		{
			iconButtonBarangBarang.ForeColor = Color.AliceBlue;
		}

		private void iconButtonKategoriBarang_MouseHover(object sender, EventArgs e)
		{
			iconButtonKategoriBarang.ForeColor = Color.AliceBlue;
		}

		private void iconButtonJenisBarang_MouseHover(object sender, EventArgs e)
		{
			iconButtonJenisBarang.ForeColor = Color.AliceBlue;
		}

		private void iconButtonJenisBarang_MouseLeave(object sender, EventArgs e)
		{
			iconButtonJenisBarang.ForeColor = Color.DarkBlue;
		}

		private void iconButtonKategoriBarang_MouseLeave(object sender, EventArgs e)
		{
			iconButtonKategoriBarang.ForeColor = Color.DarkBlue;
		}

		private void iconButtonBarangBarang_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBarangBarang.ForeColor = Color.DarkBlue;
		}

		private void iconButtonMerekBarang_MouseLeave(object sender, EventArgs e)
		{
			iconButtonMerekBarang.ForeColor = Color.DarkBlue;
		}

		#endregion

		private void activeButtonIndicator(Panel indicator, ArrayList unActiveButton, IconButton activeButton, Color colorIndicator)
		{
			indicator.Visible = true;
			indicator.BackColor = colorIndicator;
			foreach (IconButton obj in unActiveButton)
			{
				obj.ForeColor = Color.AliceBlue;
			}
			indicator.Location = activeButton.Location;
			activeButton.ForeColor = colorIndicator;
			allMainButtons.Add(activeButton);

		}

		private void navigateTo(Panel nextPanel, string title)
		{
			labelJudul.Text = title;
			foreach (Panel obj in allPanels)
			{
				obj.Dock = DockStyle.None;
				obj.Location = new Point(1800,1800);
			}
			nextPanel.Location = new Point(245, 98);
			nextPanel.Dock = DockStyle.Fill;

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// code to run a running text
			labelRunningText.Location = new Point(labelRunningText.Location.X + 5, labelRunningText.Location.Y);
			if (labelRunningText.Location.X > panelRunningTextBack.Width)
			{
				labelRunningText.Location = new Point(0 - labelRunningText.Width, labelRunningText.Location.Y);
			}
		}

		#region BUTTON ON CLICK
		private void iconButtonBarang_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonBarang);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonBarang, Color.FromArgb(227, 139, 50));
			navigateTo(panelMenuBarangDashboard, "Menu Barang");
		}

		private void iconButtonTransaksi_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonTransaksi);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonTransaksi, Color.FromArgb(227, 139, 50));
			
			navigateTo(panelMenuTransaksi, "Menu Transaksi");
		}

		private void iconButtonLaporan_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonLaporan);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonLaporan, Color.FromArgb(227, 139, 50));
		
			navigateTo(panelMenuLaporan, "Menu Laporan");
		}

		private void iconButtonPegawai_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonPegawai);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonPegawai, Color.FromArgb(227, 139, 50));

			PopulatePegawaiTable("", "");
			navigateTo(panelMenuPegawai, "Daftar Pegawai");

		}

		private void iconButtonSupplier_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonSupplier);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonSupplier, Color.FromArgb(227, 139, 50));

			PopulateSupplierTable("", "");
			navigateTo(panelMenuSupplier, "Menu Supplier");
		}

		private void iconButtonPelanggan_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonPelanggan);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonPelanggan, Color.FromArgb(227, 139, 50));

			PopulatePelangganTable("", "");
			navigateTo(panelMenuPelanggan, "Menu Pelanggan");
		}

		private void iconButtonProfil_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonProfil);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonProfil, Color.FromArgb(227, 139, 50));

			navigateTo(panelMenuProfil, "Menu Profil");
		}

		private void iconButtonLogout_Click(object sender, EventArgs e)
		{
			Application.Restart();
		}

		#endregion

		private void textBoxSearchMenuBarang_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchBarang.Text == "")
			{
				panelUnderlineSearchBarang.BackColor = Color.FromArgb(12, 26, 46);

				PopulateBarangTable("", "");
			}
			else
			{
				panelUnderlineSearchBarang.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = -1;
				
				if (comboBoxKriteriaBarang.SelectedIndex == 0)
				{
					hasil = PopulateBarangTable("b.kode_barang", textBoxSearchBarang.Text);
				}
				else if (comboBoxKriteriaBarang.SelectedIndex == 1)
				{
					hasil = PopulateBarangTable("b.nama", textBoxSearchBarang.Text);
				}

				if (hasil == 0)
				{
					MessageBox.Show("Data barang tidak ditemukan");

					string textBarang = textBoxSearchBarang.Text.ToString();

					if (textBarang.Length > 0)
					{
						textBoxSearchBarang.Text = textBarang.Remove(textBarang.Length - 1);
					}

					// set focus
					textBoxSearchBarang.SelectionStart = textBoxSearchBarang.Text.Length;
					textBoxSearchBarang.SelectionLength = 0;
				}
			}
		}

		private void iconButtonBarangBarang_Click(object sender, EventArgs e)
		{
			PopulateBarangTable("", "");
			navigateTo(panelSubMenuDaftarBarang, "Daftar Barang");
		}

		private void iconButtonJenisBarang_Click(object sender, EventArgs e)
		{
			PopulateJenisTable("", "");
			navigateTo(panelSubMenuJenisBarang, "Daftar Jenis Barang");
		}

		private void iconButtonKategoriBarang_Click(object sender, EventArgs e)
		{
			PopulateKategoriTable("", "");
			navigateTo(panelSubMenuKategoriBarang, "Daftar Kategori Barang");
		}

		private void iconButtonMerekBarang_Click(object sender, EventArgs e)
		{
			PopulateMerekTable("", "");
			navigateTo(panelSubMenuMerekBarang, "Daftar Merek Barang");

		}

		private void iconButtonBarangMasuk_Click(object sender, EventArgs e)
		{
			PopulateNotaBeliTable("nb.tanggal", dateTimePickerTanggalAwalNotaBeli.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirNotaBeli.Value.ToString("yyyy-MM-dd"));
			navigateTo(panelSubMenuTransaksiBarangMasuk, "Daftar Barang Masuk");
		}

		private void iconButtonBarangKeluar_Click(object sender, EventArgs e)
		{
			PopulateNotaJualTable("nj.tanggal", dateTimePickerTglAwalNotaJual.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirNotaJual.Value.ToString("yyyy-MM-dd"));
			navigateTo(panelSubMenuTransaksiBarangKeluar, "Daftar Barang Keluar");
		}

		private void textBoxSearchJenisBrg_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchJenisBrg.Text.Length == 0)
			{
				panelUnderlineSearchJenisBrg.BackColor = Color.FromArgb(12, 26, 46);

				PopulateJenisTable("", "");
			}
			else
			{
				panelUnderlineSearchJenisBrg.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = PopulateJenisTable("nama", textBoxSearchJenisBrg.Text);

				if (hasil == 0)
				{
					MessageBox.Show("Data jenis tidak ditemukan");

					string textJenis = textBoxSearchJenisBrg.Text.ToString();

					if (textJenis.Length > 0)
					{
						textBoxSearchJenisBrg.Text = textJenis.Remove(textJenis.Length - 1);
					}

					// set focus
					textBoxSearchJenisBrg.SelectionStart = textBoxSearchJenisBrg.Text.Length;
					textBoxSearchJenisBrg.SelectionLength = 0;
				}
			}
		}

		private void textBoxSearchKategoriBrg_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchKategoriBrg.Text.Length == 0)
			{
				panelUnderlineSearchKategori.BackColor = Color.FromArgb(12, 26, 46);

				PopulateKategoriTable("", "");
			}
			else
			{
				panelUnderlineSearchKategori.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = PopulateKategoriTable("nama", textBoxSearchKategoriBrg.Text);

				if (hasil == 0)
				{
					MessageBox.Show("Data kategori tidak ditemukan");

					string textKategori = textBoxSearchKategoriBrg.Text.ToString();

					if (textKategori.Length > 0)
					{
						textBoxSearchKategoriBrg.Text = textKategori.Remove(textKategori.Length - 1);
					}

					// set focus
					textBoxSearchKategoriBrg.SelectionStart = textBoxSearchKategoriBrg.Text.Length;
					textBoxSearchKategoriBrg.SelectionLength = 0;
				}

			}
		}

		private void textBoxSearchMerekBrg_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchMerekBrg.Text.Length == 0)
			{
				panelUnderlineSearchMerek.BackColor = Color.FromArgb(12, 26, 46);

				PopulateMerekTable("", "");
			}
			else
			{
				panelUnderlineSearchMerek.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = PopulateMerekTable("nama", textBoxSearchMerekBrg.Text);

				if (hasil == 0)
				{
					MessageBox.Show("Data merek tidak ditemukan");

					string textMerek = textBoxSearchMerekBrg.Text.ToString();

					if (textMerek.Length > 0)
					{
						textBoxSearchMerekBrg.Text = textMerek.Remove(textMerek.Length - 1);
					}

					// set focus
					textBoxSearchMerekBrg.SelectionStart = textBoxSearchMerekBrg.Text.Length;
					textBoxSearchMerekBrg.SelectionLength = 0;
				}
			}
		}

		private void textBoxSearchNamaBrgInventaris_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchBrgInventaris.Text.Length == 0)
			{
				panelUnderlineSearchBrgInventaris.BackColor = Color.FromArgb(12, 26, 46);

				PopulateLaporanStokTable("", "");
			}
			else
			{
				panelUnderlineSearchBrgInventaris.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = -1;

				if (comboBoxKriteriaLaporanStokBarang.SelectedIndex == 0)
				{
					hasil = PopulateLaporanStokTable("kode_barang", textBoxSearchBrgInventaris.Text);
				}
				else if (comboBoxKriteriaLaporanStokBarang.SelectedIndex == 1)
				{
					hasil = PopulateLaporanStokTable("nama", textBoxSearchBrgInventaris.Text);
				}

				if (hasil == 0)
				{
					MessageBox.Show("Data barang tidak ditemukan");

					string textBarang = textBoxSearchBrgInventaris.Text.ToString();

					if (textBarang.Length > 0)
					{
						textBoxSearchBrgInventaris.Text = textBarang.Remove(textBarang.Length - 1);
					}

					// set focus
					textBoxSearchBrgInventaris.SelectionStart = textBoxSearchBrgInventaris.Text.Length;
					textBoxSearchBrgInventaris.SelectionLength = 0;
				}
			}
		}

		private void textBoxSearchNamaSupplier_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchNamaSupplier.Text.Length == 0)
			{
				panelUnderlineSearchSupplier.BackColor = Color.FromArgb(12, 26, 46);

				PopulateSupplierTable("", "");
			}
			else
			{
				panelUnderlineSearchSupplier.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = PopulateSupplierTable("nama", textBoxSearchNamaSupplier.Text);

				if (hasil == 0)
				{
					MessageBox.Show("Data supplier tidak ditemukan");

					string textSupplier = textBoxSearchNamaSupplier.Text.ToString();

					if (textSupplier.Length > 0)
					{
						textBoxSearchNamaSupplier.Text = textSupplier.Remove(textSupplier.Length - 1);
					}

					// set focus
					textBoxSearchNamaSupplier.SelectionStart = textBoxSearchNamaSupplier.Text.Length;
					textBoxSearchNamaSupplier.SelectionLength = 0;
				}
			}
		}

		private void iconButtonLaporanInventarisBarang_Click(object sender, EventArgs e)
		{
			PopulateLaporanStokTable("", "");
			navigateTo(panelSubMenuLaporanStokBarang, "Laporan Stok Barang");
		}

		private void iconButtonLaporanPembelian_Click(object sender, EventArgs e)
		{
			PopulateLaporanPembelianTable(dateTimePickerTanggalAwalLaporanPembelian.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirLaporanPembelian.Value.ToString("yyyy-MM-dd"));
			navigateTo(panelSubMenuLaporanPembelian, "Laporan Pembelian");
		}

		private void iconButtonLaporanPenjualan_Click(object sender, EventArgs e)
		{
			PopulateLaporanPenjualanTable(dateTimePickerTanggalAwalLaporanPenjualan.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirLaporanPenjualan.Value.ToString("yyyy-MM-dd"));
			navigateTo(panelSubMenuLaporanPenjualan, "Laporan Penjualan");
		}

		private void textBoxSearchPegawai_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchPegawai.Text.Length == 0)
			{
				panelUnderlineSearchPegawai.BackColor = Color.FromArgb(12, 26, 46);

				PopulatePegawaiTable("", "");
			}
			else
			{
				panelUnderlineSearchPegawai.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = PopulatePegawaiTable("u.nama", textBoxSearchPegawai.Text);

				if (hasil == 0)
				{
					MessageBox.Show("Data pegawai tidak ditemukan");

					string textPegawai = textBoxSearchPegawai.Text.ToString();

					if (textPegawai.Length > 0)
					{
						textBoxSearchPegawai.Text = textPegawai.Remove(textPegawai.Length - 1);
					}

					// set focus
					textBoxSearchPegawai.SelectionStart = textBoxSearchPegawai.Text.Length;
					textBoxSearchPegawai.SelectionLength = 0;
				}
			}
		}

		private void iconButtonBackLaporanPenjualan_Click(object sender, EventArgs e)
		{
			navigateTo(panelMenuLaporan, "Menu Laporan");
		}

		private void iconButtonBackBarangKeluar_Click(object sender, EventArgs e)
		{
			navigateTo(panelMenuTransaksi, "Menu Transaksi");
		}

		private void iconButtonBackLaporanPembelian_Click(object sender, EventArgs e)
		{
			navigateTo(panelMenuLaporan, "Menu Laporan");
		}

		private void iconButtonBackPanelBarangMasuk_Click(object sender, EventArgs e)
		{
			navigateTo(panelMenuTransaksi, "Menu Transaksi");
		}

		private void iconButtonBackPanelJenisBrg_Click(object sender, EventArgs e)
		{
			navigateTo(panelMenuBarangDashboard, "Menu Barang");
		}

		private void iconButtonBackPanelBarang_Click(object sender, EventArgs e)
		{
			navigateTo(panelMenuBarangDashboard, "Menu Barang");
		}

		private void iconButtonBackPanelKategoriBrg_Click(object sender, EventArgs e)
		{
			navigateTo(panelMenuBarangDashboard, "Menu Barang");
		}

		private void iconButtonBackPanelMerekBrg_Click(object sender, EventArgs e)
		{
			navigateTo(panelMenuBarangDashboard, "Menu Barang");
		}

		private void iconButtonBackLaporanInventaris_Click(object sender, EventArgs e)
		{
			navigateTo(panelMenuLaporan, "Menu Laporan");
		}

		private void comboBoxKriteriaBarangKeluar_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxKriteriaBarangKeluar.SelectedIndex == 0)
			{
				PopulateNotaJualTable("nj.tanggal", dateTimePickerTglAwalNotaJual.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirNotaJual.Value.ToString("yyyy-MM-dd"));

				dateTimePickerTglAwalNotaJual.Visible = true;
				dateTimePickerTanggalAkhirNotaJual.Visible = true;

				dateTimePickerTanggalAkhirNotaJual.Value = DateTime.Now;
				dateTimePickerTglAwalNotaJual.Value = DateTime.Now.AddDays(-7);

				labelSDNotaJual.Visible = true;
				textBoxSearchBarangKeluar.Visible = false;
				panelUnderlineSearchBarangKeluar.Visible = false;
			}
			else
			{
				PopulateNotaJualTable("", "", "");

				dateTimePickerTglAwalNotaJual.Visible = false;
				dateTimePickerTanggalAkhirNotaJual.Visible = false;
				labelSDNotaJual.Visible = false;
				textBoxSearchBarangKeluar.Visible = true;
				panelUnderlineSearchBarangKeluar.Visible = true;
			}
		}

		private void textBoxSearchBarangKeluar_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchBarangKeluar.Text == "")
			{
				panelUnderlineSearchBarangKeluar.BackColor = Color.FromArgb(12, 26, 46);

				PopulateNotaJualTable("", "", "");
			}
			else
			{
				panelUnderlineSearchBarangKeluar.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = -1;

				if (comboBoxKriteriaBarangKeluar.SelectedIndex == 1)
				{
					hasil = PopulateNotaJualTable("nj.no_faktur", textBoxSearchBarangKeluar.Text, "");
				}
				else if (comboBoxKriteriaBarangKeluar.SelectedIndex == 2)
				{
					hasil = PopulateNotaJualTable("p.nama", textBoxSearchBarangKeluar.Text, "");
				}

				if (hasil == 0)
				{
					MessageBox.Show("Data nota jual tidak ditemukan");

					string textNotaJual = textBoxSearchBarangKeluar.Text.ToString();

					if (textNotaJual.Length > 0)
					{
						textBoxSearchBarangKeluar.Text = textNotaJual.Remove(textNotaJual.Length - 1);
					}

					// set focus
					textBoxSearchBarangKeluar.SelectionStart = textBoxSearchBarangKeluar.Text.Length;
					textBoxSearchBarangKeluar.SelectionLength = 0;
				}
			}
		}

		private void textBoxSearchBarangMasuk_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchBarangMasuk.Text == "")
			{
				panelUnderlineSearchBarangMasuk.BackColor = Color.FromArgb(12, 26, 46);

				PopulateNotaBeliTable("", "", "");
			}
			else
			{
				panelUnderlineSearchBarangMasuk.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = -1;

				if (comboBoxKriteriaBarangMasuk.SelectedIndex == 1)
				{
					hasil = PopulateNotaBeliTable("nb.no_faktur", textBoxSearchBarangMasuk.Text, "");
				}
				else if (comboBoxKriteriaBarangMasuk.SelectedIndex == 2)
				{
					hasil = PopulateNotaBeliTable("s.nama", textBoxSearchBarangMasuk.Text, "");
				}

				if (hasil == 0)
				{
					MessageBox.Show("Data nota beli tidak ditemukan");

					string textNotaBeli = textBoxSearchBarangMasuk.Text.ToString();

					if (textNotaBeli.Length > 0)
					{
						textBoxSearchBarangMasuk.Text = textNotaBeli.Remove(textNotaBeli.Length - 1);
					}

					// set focus
					textBoxSearchBarangMasuk.SelectionStart = textBoxSearchBarangMasuk.Text.Length;
					textBoxSearchBarangMasuk.SelectionLength = 0;
				}
			}
		}

		private void iconButtonBackPanelBarangMasuk_Click_1(object sender, EventArgs e)
		{
			navigateTo(panelMenuTransaksi, "Menu Transaksi");
		}

		private void comboBoxKriteriaBarangMasuk_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxKriteriaBarangMasuk.SelectedIndex == 0)
			{
				PopulateNotaBeliTable("nb.tanggal", dateTimePickerTanggalAwalNotaBeli.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirNotaBeli.Value.ToString("yyyy-MM-dd"));

				dateTimePickerTanggalAwalNotaBeli.Visible = true;
				dateTimePickerTanggalAkhirNotaBeli.Visible = true;

				dateTimePickerTanggalAkhirNotaBeli.Value = DateTime.Now;
				dateTimePickerTanggalAwalNotaBeli.Value = DateTime.Now.AddDays(-7);

				labelSDNotaBeli.Visible = true;
				textBoxSearchBarangMasuk.Visible = false;
				panelUnderlineSearchBarangMasuk.Visible = false;
			}
			else
			{
				PopulateNotaBeliTable("", "", "");

				dateTimePickerTanggalAwalNotaBeli.Visible = false;
				dateTimePickerTanggalAkhirNotaBeli.Visible = false;
				labelSDNotaBeli.Visible = false;
				textBoxSearchBarangMasuk.Visible = true;
				panelUnderlineSearchBarangMasuk.Visible = true;
			}
		}

		private void buttonTambahMerekBarang_Click(object sender, EventArgs e)
		{
			FormTambahMerek frm = new FormTambahMerek(this);
			frm.Show();
		}

		private void buttonUbahMerekBarang_Click(object sender, EventArgs e)
		{
			listSelectedMerek.Clear();

			if (dataGridViewSubMenuMerekBrg.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu data di tabel untuk di ubah");
			}
			else if (dataGridViewSubMenuMerekBrg.SelectedRows.Count > 1)
			{
				MessageBox.Show("Hanya bisa pilih satu data di tabel untuk di ubah");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewSubMenuMerekBrg.SelectedRows)
				{
					MerekBarang merek = new MerekBarang();
					merek.IdMerekBarang = int.Parse(row.Cells[1].Value.ToString());
					merek.Nama = row.Cells[2].Value.ToString();

					listSelectedMerek.Add(merek);
				}

				FormUbahMerek frm = new FormUbahMerek(this);
				frm.Show();
			}
		}

		private void buttonHapusMerekBarang_Click(object sender, EventArgs e)
		{
			listSelectedMerek.Clear();

			if (dataGridViewSubMenuMerekBrg.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu atau lebih data di tabel untuk di hapus");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewSubMenuMerekBrg.SelectedRows)
				{
					MerekBarang merek = new MerekBarang();
					merek.IdMerekBarang = int.Parse(row.Cells[1].Value.ToString());
					merek.Nama = row.Cells[2].Value.ToString();

					listSelectedMerek.Add(merek);
				}

				DialogResult dialogResult = MessageBox.Show($"Apakah anda yakin untuk menghapus {listSelectedMerek.Count} merek barang ini?", "Hapus", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					List<string> hasil = MerekBarang.Hapusdata(listSelectedMerek);

					int jumlahBerhasil = 0;
					int jumlahGagal = 0;

					string keteranganGagal = "";

					foreach (var item in hasil)
					{
						if (item == "berhasil")
						{
							jumlahBerhasil++;
						}
						else
						{
							jumlahGagal++;
							keteranganGagal = item.ToString();
						}
					}

					if (jumlahGagal == 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus");
					}
					else if (jumlahGagal > 0 && jumlahBerhasil > 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus, {jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}
					else if (jumlahBerhasil == 0)
					{
						MessageBox.Show($"{jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}

					textBoxSearchMerekBrg.Clear();
					PopulateMerekTable("", "");
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void buttonTambahKategoriBarang_Click(object sender, EventArgs e)
		{
			FormTambahKategori frm = new FormTambahKategori(this);
			frm.Show();
		}

		private void buttonUbahKategoriBarang_Click(object sender, EventArgs e)
		{
			listSelectedKategori.Clear();

			if (dataGridViewSubMenuKategoriBrg.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu data di tabel untuk di ubah");
			}
			else if (dataGridViewSubMenuKategoriBrg.SelectedRows.Count > 1)
			{
				MessageBox.Show("Hanya bisa pilih satu data di tabel untuk di ubah");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewSubMenuKategoriBrg.SelectedRows)
				{
					KategoriBarang kategori = new KategoriBarang();
					kategori.IdKategoriBarang = int.Parse(row.Cells[1].Value.ToString());
					kategori.Nama = row.Cells[2].Value.ToString();

					listSelectedKategori.Add(kategori);
				}

				FormUbahKategori frm = new FormUbahKategori(this);
				frm.Show();
			}
		}

		private void buttonHapusKategoriBarang_Click(object sender, EventArgs e)
		{
			listSelectedKategori.Clear();

			if (dataGridViewSubMenuKategoriBrg.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu atau lebih data di tabel untuk di hapus");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewSubMenuKategoriBrg.SelectedRows)
				{
					KategoriBarang kategori = new KategoriBarang();
					kategori.IdKategoriBarang = int.Parse(row.Cells[1].Value.ToString());
					kategori.Nama = row.Cells[2].Value.ToString();

					listSelectedKategori.Add(kategori);
				}

				DialogResult dialogResult = MessageBox.Show($"Apakah anda yakin untuk menghapus {listSelectedKategori.Count} kategori barang ini?", "Hapus", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					List<string> hasil = KategoriBarang.HapusData(listSelectedKategori);

					int jumlahBerhasil = 0;
					int jumlahGagal = 0;

					string keteranganGagal = "";

					foreach (var item in hasil)
					{
						if (item == "berhasil")
						{
							jumlahBerhasil++;
						}
						else
						{
							jumlahGagal++;
							keteranganGagal = item.ToString();
						}
					}

					if (jumlahGagal == 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus");
					}
					else if (jumlahGagal > 0 && jumlahBerhasil > 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus, {jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}
					else if (jumlahBerhasil == 0)
					{
						MessageBox.Show($"{jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}

					textBoxSearchKategoriBrg.Clear();
					PopulateKategoriTable("", "");
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void dataGridViewDaftarBarang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			listSelectedBarang.Clear();

			string hasilBaca = "";

			foreach (DataGridViewRow row in dataGridViewDaftarBarang.SelectedRows)
			{
				hasilBaca = Barang.BacaDataBarang("b.idbarang", row.Cells[1].Value.ToString(), listSelectedBarang);
			}

			if (hasilBaca == "1")
			{
				FormDetailBarang frm = new FormDetailBarang();
				frm.Show();
			}
			else
			{
				MessageBox.Show(hasilBaca);
			}
		}

		private void buttonTambahJenisBarang_Click(object sender, EventArgs e)
		{
			FormTambahJenis frm = new FormTambahJenis(this);
			frm.Show();
		}

		private void buttonUbahJenisBarang_Click(object sender, EventArgs e)
		{
			listSelectedJenis.Clear();

			if (dataGridViewDaftarJenisBrg.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu data di tabel untuk di ubah");
			}
			else if (dataGridViewDaftarJenisBrg.SelectedRows.Count > 1)
			{
				MessageBox.Show("Hanya bisa pilih satu data di tabel untuk di ubah");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewDaftarJenisBrg.SelectedRows)
				{
					JenisBarang jenis = new JenisBarang();
					jenis.IdJenisBarang = int.Parse(row.Cells[1].Value.ToString());
					jenis.Nama = row.Cells[2].Value.ToString();

					listSelectedJenis.Add(jenis);
				}

				FormUbahJenis frm = new FormUbahJenis(this);
				frm.Show();
			}
		}

		private void buttonHapusJenisBarang_Click(object sender, EventArgs e)
		{
			listSelectedJenis.Clear();

			if (dataGridViewDaftarJenisBrg.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu atau lebih data di tabel untuk di hapus");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewDaftarJenisBrg.SelectedRows)
				{
					JenisBarang jenis = new JenisBarang();
					jenis.IdJenisBarang = int.Parse(row.Cells[1].Value.ToString());
					jenis.Nama = row.Cells[2].Value.ToString();

					listSelectedJenis.Add(jenis);
				}

				DialogResult dialogResult = MessageBox.Show($"Apakah anda yakin untuk menghapus {listSelectedJenis.Count} jenis barang ini?", "Hapus", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					List<string> hasil = JenisBarang.HapusData(listSelectedJenis);

					int jumlahBerhasil = 0;
					int jumlahGagal = 0;

					string keteranganGagal = "";

					foreach (var item in hasil)
					{
						if (item == "berhasil")
						{
							jumlahBerhasil++;
						}
						else
						{
							jumlahGagal++;
							keteranganGagal = item.ToString();
						}
					}

					if (jumlahGagal == 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus");
					}
					else if (jumlahGagal > 0 && jumlahBerhasil > 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus, {jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}
					else if (jumlahBerhasil == 0)
					{
						MessageBox.Show($"{jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}

					textBoxSearchJenisBrg.Clear();
					PopulateJenisTable("", "");
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void buttonTambahSupplier_Click(object sender, EventArgs e)
		{
			FormTambahSupplier frm = new FormTambahSupplier(this);
			frm.Show();
		}

		private void buttonUbahSupplier_Click(object sender, EventArgs e)
		{
			listSelectedSupplier.Clear();

			if (dataGridViewDaftarSupplier.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu data di tabel untuk di ubah");
			}
			else if (dataGridViewDaftarSupplier.SelectedRows.Count > 1)
			{
				MessageBox.Show("Hanya bisa pilih satu data di tabel untuk di ubah");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewDaftarSupplier.SelectedRows)
				{
					Supplier supplier = new Supplier();

					supplier.IdSupplier = int.Parse(row.Cells[1].Value.ToString());
					supplier.Nama = row.Cells[2].Value.ToString();
					supplier.NoTelp = row.Cells[3].Value.ToString();
					supplier.Alamat = row.Cells[4].Value.ToString();

					listSelectedSupplier.Add(supplier);
				}

				FormUbahSupplier frm = new FormUbahSupplier(this);
				frm.Show();

			}
		}

		private void buttonHapusSupplier_Click(object sender, EventArgs e)
		{
			listSelectedSupplier.Clear();

			if (dataGridViewDaftarSupplier.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu atau lebih data di tabel untuk di hapus");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewDaftarSupplier.SelectedRows)
				{
					Supplier supplier = new Supplier();
					supplier.IdSupplier = int.Parse(row.Cells[1].Value.ToString());
					supplier.Nama = row.Cells[2].Value.ToString();
					supplier.Alamat = row.Cells[3].Value.ToString();
					supplier.NoTelp = row.Cells[4].Value.ToString();

					listSelectedSupplier.Add(supplier);
				}

				DialogResult dialogResult = MessageBox.Show($"Apakah anda yakin untuk menghapus {listSelectedSupplier.Count} supplier barang ini?", "Hapus", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					List<string> hasil = Supplier.HapusData(listSelectedSupplier);

					int jumlahBerhasil = 0;
					int jumlahGagal = 0;

					string keteranganGagal = "";

					foreach (var item in hasil)
					{
						if (item == "berhasil")
						{
							jumlahBerhasil++;
						}
						else
						{
							jumlahGagal++;
							keteranganGagal = item.ToString();
						}
					}

					if (jumlahGagal == 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus");
					}
					else if (jumlahGagal > 0 && jumlahBerhasil > 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus, {jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}
					else if (jumlahBerhasil == 0)
					{
						MessageBox.Show($"{jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}

					textBoxSearchNamaSupplier.Clear();
					PopulateSupplierTable("", "");
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void textBoxSearchNamaPelanggan_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchNamaPelanggan.Text.Length == 0)
			{
				panelUnderlineSearchPelanggan.BackColor = Color.FromArgb(12, 26, 46);

				PopulatePelangganTable("", "");
			}
			else
			{
				panelUnderlineSearchPelanggan.BackColor = Color.FromArgb(7, 104, 159);

				int hasil = PopulatePelangganTable("nama", textBoxSearchNamaPelanggan.Text);

				if (hasil == 0)
				{
					MessageBox.Show("Data pelanggan tidak ditemukan");

					string textPelanggan = textBoxSearchNamaPelanggan.Text.ToString();

					if (textPelanggan.Length > 0)
					{
						textBoxSearchNamaPelanggan.Text = textPelanggan.Remove(textPelanggan.Length - 1);
					}

					// set focus
					textBoxSearchNamaPelanggan.SelectionStart = textBoxSearchNamaPelanggan.Text.Length;
					textBoxSearchNamaPelanggan.SelectionLength = 0;
				}
			}
		}

		private void buttonTambahPelanggan_Click(object sender, EventArgs e)
		{
			FormTambahPelanggan frm = new FormTambahPelanggan(this);
			frm.Show();
		}

		private void buttonUbahPelanggan_Click(object sender, EventArgs e)
		{
			listSelectedPelanggan.Clear();

			if (dataGridViewDaftarPelanggan.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu data di tabel untuk di ubah");
			}
			else if (dataGridViewDaftarPelanggan.SelectedRows.Count > 1)
			{
				MessageBox.Show("Hanya bisa pilih satu data di tabel untuk di ubah");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewDaftarPelanggan.SelectedRows)
				{
					Pelanggan pelanggan = new Pelanggan();

					pelanggan.IdPelanggan = int.Parse(row.Cells[1].Value.ToString());
					pelanggan.Nama = row.Cells[2].Value.ToString();
					pelanggan.NoTelp = row.Cells[3].Value.ToString();
					pelanggan.Alamat = row.Cells[4].Value.ToString();

					listSelectedPelanggan.Add(pelanggan);
				}

				FormUbahPelanggan frm = new FormUbahPelanggan(this);
				frm.Show();
			}
		}

		private void buttonHapusPelanggan_Click(object sender, EventArgs e)
		{
			listSelectedPelanggan.Clear();

			if (dataGridViewDaftarPelanggan.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu atau lebih data di tabel untuk di hapus");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewDaftarPelanggan.SelectedRows)
				{
					Pelanggan pelanggan = new Pelanggan();
					pelanggan.IdPelanggan = int.Parse(row.Cells[1].Value.ToString());
					pelanggan.Nama = row.Cells[2].Value.ToString();
					pelanggan.Alamat = row.Cells[3].Value.ToString();
					pelanggan.NoTelp = row.Cells[4].Value.ToString();

					listSelectedPelanggan.Add(pelanggan);
				}

				DialogResult dialogResult = MessageBox.Show($"Apakah anda yakin untuk menghapus {listSelectedPelanggan.Count} pelanggan ini?", "Hapus", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					List<string> hasil = Pelanggan.HapusData(listSelectedPelanggan);

					int jumlahBerhasil = 0;
					int jumlahGagal = 0;

					string keteranganGagal = "";

					foreach (var item in hasil)
					{
						if (item == "berhasil")
						{
							jumlahBerhasil++;
						}
						else
						{
							jumlahGagal++;
							keteranganGagal = item.ToString();
						}
					}

					if (jumlahGagal == 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus");
					}
					else if (jumlahGagal > 0 && jumlahBerhasil > 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus, {jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}
					else if (jumlahBerhasil == 0)
					{
						MessageBox.Show($"{jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}

					textBoxSearchNamaPelanggan.Clear();
					PopulatePelangganTable("", "");
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void buttonProfilUbahInfo_Click(object sender, EventArgs e)
		{
			User user = new User();
			user.IdUser = int.Parse(labelProfilIdUser.Text);
			user.Password = textBoxPasswordProfilUser.Text.Trim();
			user.Alamat = richTextBoxAlamatProfilUser.Text.Trim();
			user.NoTelp = textBoxNoTelpProfilUser.Text.Trim();

			if (pathFoto != "")
			{
				user.Foto = Path.GetExtension(pathFoto);
			}

			if (textBoxPasswordProfilUser.Text.Length >= 8)
			{
				if (iconPictureBoxFotoProfil.Tag == "Unggahan")
				{
					string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\users";
					string folderName = Path.Combine(projectPath, labelProfilUser.Text);
					Directory.CreateDirectory(folderName);
					Array.ForEach(Directory.GetFiles(@folderName + "\\"), File.Delete);
					File.Copy(pathFoto, folderName + "\\" + "foto" + user.Foto);
					pathFoto = folderName + "\\" + "foto" + user.Foto;
					iconPictureBoxFotoProfil.ImageLocation = pathFoto;
					User.UbahData(user, "Ada");
					MessageBox.Show("Data telah disimpan");
					iconPictureBoxUser.ImageLocation = pathFoto;
					iconPictureBoxFotoProfil.Tag = null;
				}
				else if (iconPictureBoxFotoProfil.Tag == "Default")
				{
					User.UbahData(user, "Hapus");
					MessageBox.Show("Data telah disimpan");
					iconPictureBoxUser.Image = Resources.profile_picture;
				}
				else if (iconPictureBoxFotoProfil.Tag == null)
				{
					User.UbahData(user, "Tidak Ada");
					MessageBox.Show("Data telah disimpan");
				}
			}
			else 
			{
				MessageBox.Show("Password minimal 8 karakter");
			}

		}

		private void checkBoxTampilPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxTampilPassword.Checked)
			{
				textBoxPasswordProfilUser.PasswordChar = '\0';
			}
			else
			{
				textBoxPasswordProfilUser.PasswordChar = '*';
			}
		}

		private void buttonUnggahFotoProfilUser_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Pilih Foto Profil";
			openFileDialog.InitialDirectory =
				Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			openFileDialog.Filter = "Images Files (*.png; *.jpeg; *.jpg)|*.png;*jpeg;*jpg" ;
			openFileDialog.Multiselect = false;
 			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					iconPictureBoxFotoProfil.Tag = "Unggahan";
					pathFoto = openFileDialog.FileName;
					iconPictureBoxFotoProfil.ImageLocation = openFileDialog.FileName;
				}
				catch (IOException ex)
				{
					MessageBox.Show(ex.Message.ToString());
				}
			}

			openFileDialog.Dispose();
		}

		byte[] ConvertImageToBinary(Image img)
		{
			if (img == null)
				return null;
			using (MemoryStream ms = new MemoryStream())
			{
				//System.Drawing.Imaging.ImageFormat.Jpeg
				img.Save(ms, img.RawFormat);
				return ms.ToArray();
				
			}
		}

		private void buttonTambahDaftarBarang_Click(object sender, EventArgs e)
		{
			FormTambahBarang frm = new FormTambahBarang(this);
			frm.Show();
		}

		private void buttonUbahDaftarBarang_Click(object sender, EventArgs e)
		{
			listSelectedBarang.Clear();

			if (dataGridViewDaftarBarang.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu data di tabel untuk di ubah");
			}
			else if (dataGridViewDaftarBarang.SelectedRows.Count > 1)
			{
				MessageBox.Show("Hanya bisa pilih satu data di tabel untuk di ubah");
			}
			else
			{
				string hasilBaca = "";

				foreach (DataGridViewRow row in dataGridViewDaftarBarang.SelectedRows)
				{
					hasilBaca = Barang.BacaDataBarang("b.idbarang", row.Cells[1].Value.ToString(), listSelectedBarang);
				}

				if (hasilBaca == "1")
				{
					FormUbahBarang frm = new FormUbahBarang(this);
					frm.Show();
				}
				else
				{
					MessageBox.Show(hasilBaca);
				}
			}
		}

		private void buttonHapusDaftarBarang_Click(object sender, EventArgs e)
		{
			listSelectedBarang.Clear();

			if (dataGridViewDaftarBarang.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu atau lebih data di tabel untuk di hapus");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewDaftarBarang.SelectedRows)
				{
					Barang barang = new Barang();
					barang.IdBarang = int.Parse(row.Cells[1].Value.ToString());

					listSelectedBarang.Add(barang);
				}

				DialogResult dialogResult = MessageBox.Show($"Apakah anda yakin untuk menghapus {listSelectedBarang.Count} barang ini?", "Hapus", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					List<string> hasil = Barang.HapusData(listSelectedBarang);

					int jumlahBerhasil = 0;
					int jumlahGagal = 0;

					string keteranganGagal = "";

					foreach (var item in hasil)
					{
						if (item == "berhasil")
						{
							jumlahBerhasil++;
						}
						else
						{
							jumlahGagal++;
							keteranganGagal = item.ToString();
						}
					}

					if (jumlahGagal == 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus");
					}
					else if (jumlahGagal > 0 && jumlahBerhasil > 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus, {jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}
					else if (jumlahBerhasil == 0)
					{
						MessageBox.Show($"{jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}

					textBoxSearchBarang.Clear();
					PopulateBarangTable("", "");
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void buttonTambahBarangMasuk_Click(object sender, EventArgs e)
		{
			FormTambahNotaBeli frm = new FormTambahNotaBeli(this, int.Parse(labelProfilIdUser.Text));
			frm.Owner = this;
			frm.Show();
		}

		private void dateTimePickerTanggalAwalNotaBeli_ValueChanged(object sender, EventArgs e)
		{
			// secara default karena datetimepicker sudah di ubah di code di dalam formmaster_load maka pasti masuk else
			// maka otomatis saat user baru buka modul nota beli, maka terfilter transaksi seminggu terakhir
			if (dateTimePickerTanggalAwalNotaBeli.Value > dateTimePickerTanggalAkhirNotaBeli.Value)
			{
				MessageBox.Show("Mohon input tanggal awal dan akhir dengan benar");

				dataGridViewBarangMasuk.Rows.Clear();
			}
			else 
			{
				PopulateNotaBeliTable("nb.tanggal", dateTimePickerTanggalAwalNotaBeli.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirNotaBeli.Value.ToString("yyyy-MM-dd"));
			}
		}

		private void dateTimePickerTanggalAkhirNotaBeli_ValueChanged(object sender, EventArgs e)
		{
			// secara default karena datetimepicker sudah di ubah di code di dalam formmaster_load maka pasti masuk else
			// maka otomatis saat user baru buka modul nota beli, maka terfilter transaksi seminggu terakhir
			if (dateTimePickerTanggalAkhirNotaBeli.Value < dateTimePickerTanggalAwalNotaBeli.Value)
			{
				//dateTimePickerTanggalAkhirNotaBeli.Value = dateTimePickerTanggalAwalNotaBeli.Value.AddDays(+1);
				MessageBox.Show("Mohon input tanggal awal dan akhir dengan benar");

				dataGridViewBarangMasuk.Rows.Clear();
			}
			else
			{
				PopulateNotaBeliTable("nb.tanggal", dateTimePickerTanggalAwalNotaBeli.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirNotaBeli.Value.ToString("yyyy-MM-dd"));
			}
		}

		private void comboBoxKriteriaBarang_SelectionChangeCommitted(object sender, EventArgs e)
		{
			PopulateBarangTable("", "");

			textBoxSearchBarang.Clear();
		}

		private void comboBoxKriteriaBarangKeluar_SelectedValueChanged(object sender, EventArgs e)
		{
			textBoxSearchBarangKeluar.Clear();
		}

		private void comboBoxKriteriaBarangMasuk_SelectionChangeCommitted(object sender, EventArgs e)
		{
			textBoxSearchBarangMasuk.Clear();
		}

		private void dataGridViewBarangMasuk_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			listSelectedNotaBeliDetil.Clear();

			string hasilBaca = "";

			foreach (DataGridViewRow row in dataGridViewBarangMasuk.SelectedRows)
			{
				hasilBaca = NotaBeliDetil.BacaData("nb.idnota_beli", row.Cells[1].Value.ToString(), listSelectedNotaBeliDetil);
			}

			if (hasilBaca == "1")
			{
				FormDetailNotaBeli frm = new FormDetailNotaBeli();
				frm.Show();
			}
			else
			{
				MessageBox.Show(hasilBaca);
			}
		}

		private void dataGridViewPegawai_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			listSelectedPegawai.Clear();

			string hasilBaca = "";

			foreach (DataGridViewRow row in dataGridViewPegawai.SelectedRows)
			{
				hasilBaca = User.BacaPegawai("u.iduser", row.Cells[1].Value.ToString(), listSelectedPegawai);
			}

			if (hasilBaca == "1")
			{
				FormDetailPegawai frm = new FormDetailPegawai();
				frm.Show();
			}
			else
			{
				MessageBox.Show(hasilBaca);
			}
		}

		private void buttonTambahPegawai_Click(object sender, EventArgs e)
		{
			FormTambahPegawai frm = new FormTambahPegawai(this);
			frm.Show();
		}

		private void buttonHapusFoto_Click(object sender, EventArgs e)
		{
			iconPictureBoxFotoProfil.Image = Resources.profile_picture;
			iconPictureBoxFotoProfil.Tag = "Default";
		}

		private void buttonUbahPegawai_Click(object sender, EventArgs e)
		{
			listSelectedPegawai.Clear();
			string keteranganStatus = "";

			foreach (DataGridViewRow row in dataGridViewPegawai.SelectedRows)
			{
				if (row.Cells[6].Value.ToString() == "Belum Aktif")
				{
					keteranganStatus = "Belum Aktif";
				}
				else
				{
					keteranganStatus = "Aktif/Blokir";
				}
			}

			if (dataGridViewPegawai.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu data di tabel untuk di ubah");
			}
			else if (dataGridViewPegawai.SelectedRows.Count > 1)
			{
				MessageBox.Show("Hanya bisa pilih satu data di tabel untuk di ubah");
			}
			else
			{
				int idUser = -1;
				foreach (DataGridViewRow row in dataGridViewPegawai.SelectedRows)
				{
					idUser = Convert.ToInt32(row.Cells[1].Value);
				}

				User user = new User();
				user.IdUser = idUser;

				string hasilBaca = User.BacaPegawai("u.iduser", idUser.ToString(), listSelectedPegawai);

				if (hasilBaca == "1")
				{
					FormUbahPegawai frm = new FormUbahPegawai(this, keteranganStatus);
					frm.Show();
				}

			}
		}

		private void buttonHapusPegawai_Click(object sender, EventArgs e)
		{
			listSelectedPegawai.Clear();

			if (dataGridViewPegawai.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu atau lebih data di tabel untuk di hapus");
			}
			else if (dataGridViewPegawai.SelectedRows.Count > 1)
			{
				MessageBox.Show("Tidak dapat hapus lebih dari 1 data di tabel. Pilih satu data saja");
			}
			else if (dataGridViewPegawai.SelectedRows.Count == 1)
			{
				User user = null;

				foreach (DataGridViewRow row in dataGridViewPegawai.SelectedRows)
				{
					user = new User();
					user.IdUser = int.Parse(row.Cells[1].Value.ToString());
					user.Username = row.Cells[3].Value.ToString();
				}

				DialogResult dialogResult = MessageBox.Show($"Apakah anda yakin untuk menghapus akun pegawai ini?", "Hapus", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					string hasilHapus = User.HapusData(user);

					if (hasilHapus == "1")
					{
						MessageBox.Show("Akun pegawai berhasil di hapus", "Info");
					}
					else
					{
						MessageBox.Show(hasilHapus, "Info");
					}

					textBoxSearchPegawai.Clear();
					PopulatePegawaiTable("", "");
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void buttonHapusBarangMasuk_Click(object sender, EventArgs e)
		{
			listSelectedNotaBeli.Clear();

			if (dataGridViewBarangMasuk.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu atau lebih data di tabel untuk di hapus");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewBarangMasuk.SelectedRows)
				{
					NotaBeli nb = new NotaBeli();
					nb.IdNotaBeli = Convert.ToInt32(row.Cells[1].Value);

					listSelectedNotaBeli.Add(nb);
				}

				DialogResult dialogResult = MessageBox.Show($"Apakah anda yakin untuk menghapus {listSelectedNotaBeli.Count} data nota beli ini?", "Hapus", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					List<string> hasil = NotaBeli.HapusData(listSelectedNotaBeli);

					int jumlahBerhasil = 0;
					int jumlahGagal = 0;

					string keteranganGagal = "";

					foreach (var item in hasil)
					{
						if (item == "berhasil")
						{
							jumlahBerhasil++;
						}
						else
						{
							jumlahGagal++;
							keteranganGagal = item.ToString();
						}
					}

					if (jumlahGagal == 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus");
					}
					else if (jumlahGagal > 0 && jumlahBerhasil > 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus, {jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}
					else if (jumlahBerhasil == 0)
					{
						MessageBox.Show($"{jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}

					textBoxSearchBarangMasuk.Clear();
					PopulateNotaBeliTable("", "", "");
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void buttonTambahNotaJual_Click(object sender, EventArgs e)
		{
			FormTambahNotaJual frm = new FormTambahNotaJual(this, int.Parse(labelProfilIdUser.Text));
			frm.Owner = this;
			frm.Show();
		}

		private void buttonHapusNotaJual_Click(object sender, EventArgs e)
		{
			listSelectedNotaJual.Clear();

			if (dataGridViewBarangKeluar.SelectedRows.Count == 0)
			{
				MessageBox.Show("Pilih satu atau lebih data di tabel untuk di hapus");
			}
			else
			{
				foreach (DataGridViewRow row in dataGridViewBarangKeluar.SelectedRows)
				{
					NotaJual nj = new NotaJual();
					nj.IdNotaJual = Convert.ToInt32(row.Cells[1].Value);

					listSelectedNotaJual.Add(nj);
				}

				DialogResult dialogResult = MessageBox.Show($"Apakah anda yakin untuk menghapus {listSelectedNotaJual.Count} data nota jual ini?", "Hapus", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					List<string> hasil = NotaJual.HapusData(listSelectedNotaJual);

					int jumlahBerhasil = 0;
					int jumlahGagal = 0;

					string keteranganGagal = "";

					foreach (var item in hasil)
					{
						if (item == "berhasil")
						{
							jumlahBerhasil++;
						}
						else
						{
							jumlahGagal++;
							keteranganGagal = item.ToString();
						}
					}

					if (jumlahGagal == 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus");
					}
					else if (jumlahGagal > 0 && jumlahBerhasil > 0)
					{
						MessageBox.Show($"{jumlahBerhasil} data berhasil di hapus, {jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}
					else if (jumlahBerhasil == 0)
					{
						MessageBox.Show($"{jumlahGagal} data gagal dihapus karena {keteranganGagal}");
					}

					textBoxSearchBarangKeluar.Clear();
					PopulateNotaJualTable("", "", "");
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void dataGridViewBarangKeluar_DoubleClick(object sender, EventArgs e)
		{
			listSelectedNotaJualDetil.Clear();

			string hasilBaca = "";

			foreach (DataGridViewRow row in dataGridViewBarangKeluar.SelectedRows)
			{
				hasilBaca = NotaJualDetil.BacaData("nj.idnota_jual", row.Cells[1].Value.ToString(), listSelectedNotaJualDetil);
			}

			if (hasilBaca == "1")
			{
				FormDetailNotaJual frm = new FormDetailNotaJual(this, labelJabatanUser.Text);
				frm.Show();
			}
			else
			{
				MessageBox.Show(hasilBaca);
			}
		}

		private void dateTimePickerTglAwalNotaJual_ValueChanged(object sender, EventArgs e)
		{
			// secara default karena datetimepicker sudah di ubah di code di dalam formmaster_load maka pasti masuk else
			// maka otomatis saat user baru buka modul nota jual, maka terfilter transaksi seminggu terakhir
			if (dateTimePickerTglAwalNotaJual.Value > dateTimePickerTanggalAkhirNotaJual.Value)
			{
				MessageBox.Show("Mohon input tanggal awal dan akhir dengan benar");

				dataGridViewBarangKeluar.Rows.Clear();
			}
			else
			{
				PopulateNotaJualTable("nj.tanggal", dateTimePickerTglAwalNotaJual.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirNotaJual.Value.ToString("yyyy-MM-dd"));
			}
		}

		private void dateTimePickerTanggalAkhirNotaJual_ValueChanged(object sender, EventArgs e)
		{
			// secara default karena datetimepicker sudah di ubah di code di dalam formmaster_load maka pasti masuk else
			// maka otomatis saat user baru buka modul nota jual, maka terfilter transaksi seminggu terakhir
			if (dateTimePickerTanggalAkhirNotaJual.Value < dateTimePickerTglAwalNotaJual.Value)
			{
				MessageBox.Show("Mohon input tanggal awal dan akhir dengan benar");

				dataGridViewBarangKeluar.Rows.Clear();
			}
			else
			{
				PopulateNotaJualTable("nj.tanggal", dateTimePickerTglAwalNotaJual.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirNotaJual.Value.ToString("yyyy-MM-dd"));
			}
		}

		private void dateTimePickerTanggalAwalLaporanPenjualan_ValueChanged(object sender, EventArgs e)
		{
			// secara default karena datetimepicker sudah di ubah di code di dalam formmaster_load maka pasti masuk else
			// maka otomatis saat user baru buka modul nota beli, maka terfilter transaksi seminggu terakhir
			if (dateTimePickerTanggalAwalLaporanPenjualan.Value > dateTimePickerTanggalAkhirLaporanPenjualan.Value)
			{
				MessageBox.Show("Mohon input tanggal awal dan akhir dengan benar");

				dataGridViewLaporanPenjualan.Rows.Clear();
			}
			else
			{
				PopulateLaporanPenjualanTable(dateTimePickerTanggalAwalLaporanPenjualan.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirLaporanPenjualan.Value.ToString("yyyy-MM-dd"));
			}
		}

		private void dateTimePickerTanggalAkhirLaporanPenjualan_ValueChanged(object sender, EventArgs e)
		{
			// secara default karena datetimepicker sudah di ubah di code di dalam formmaster_load maka pasti masuk else
			// maka otomatis saat user baru buka modul nota beli, maka terfilter transaksi seminggu terakhir
			if (dateTimePickerTanggalAkhirLaporanPenjualan.Value < dateTimePickerTanggalAwalLaporanPenjualan.Value)
			{
				MessageBox.Show("Mohon input tanggal awal dan akhir dengan benar");

				dataGridViewLaporanPenjualan.Rows.Clear();
			}
			else
			{
				PopulateLaporanPenjualanTable(dateTimePickerTanggalAwalLaporanPenjualan.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirLaporanPenjualan.Value.ToString("yyyy-MM-dd"));
			}
		}

		private void dateTimePickerTanggalAwalLaporanPembelian_ValueChanged(object sender, EventArgs e)
		{
			// secara default karena datetimepicker sudah di ubah di code di dalam formmaster_load maka pasti masuk else
			// maka otomatis saat user baru buka modul nota beli, maka terfilter transaksi seminggu terakhir
			if (dateTimePickerTanggalAwalLaporanPembelian.Value > dateTimePickerTanggalAkhirLaporanPembelian.Value)
			{
				MessageBox.Show("Mohon input tanggal awal dan akhir dengan benar");

				dataGridViewLaporanPembelian.Rows.Clear();
			}
			else
			{
				PopulateLaporanPembelianTable(dateTimePickerTanggalAwalLaporanPembelian.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirLaporanPembelian.Value.ToString("yyyy-MM-dd"));
			}
		}

		private void dateTimePickerTanggalAkhirLaporanPembelian_ValueChanged(object sender, EventArgs e)
		{
			// secara default karena datetimepicker sudah di ubah di code di dalam formmaster_load maka pasti masuk else
			// maka otomatis saat user baru buka modul nota beli, maka terfilter transaksi seminggu terakhir
			if (dateTimePickerTanggalAkhirLaporanPembelian.Value < dateTimePickerTanggalAwalLaporanPembelian.Value)
			{
				MessageBox.Show("Mohon input tanggal awal dan akhir dengan benar");

				dataGridViewLaporanPembelian.Rows.Clear();
			}
			else
			{
				PopulateLaporanPembelianTable(dateTimePickerTanggalAwalLaporanPembelian.Value.ToString("yyyy-MM-dd"), dateTimePickerTanggalAkhirLaporanPembelian.Value.ToString("yyyy-MM-dd"));
			}
		}

		private void comboBoxKriteriaLaporanStokBarang_SelectionChangeCommitted(object sender, EventArgs e)
		{
			PopulateLaporanStokTable("", "");

			textBoxSearchBrgInventaris.Clear();
		}
	}
}

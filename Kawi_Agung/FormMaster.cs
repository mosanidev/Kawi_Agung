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
		bool buttonMerekClicked = false;

		public List<KategoriBarang> listKategori = new List<KategoriBarang>();
		public static List<KategoriBarang> listSelectedKategori = new List<KategoriBarang>();
		bool buttonKategoriClicked = false;

		public List<JenisBarang> listJenis = new List<JenisBarang>();
		public static List<JenisBarang> listSelectedJenis = new List<JenisBarang>();
		bool buttonJenisClicked = false;

		public List<Barang> listBarang = new List<Barang>();
		public static List<Barang> listSelectedBarang = new List<Barang>();
		bool buttonBarangClicked = false;

		public List<Supplier> listSupplier = new List<Supplier>();
		public static List<Supplier> listSelectedSupplier = new List<Supplier>();
		bool buttonSupplierClicked = false;

		public List<Pelanggan> listPelanggan = new List<Pelanggan>();
		public static List<Pelanggan> listSelectedPelanggan = new List<Pelanggan>();
		bool buttonPelangganClicked = false;

		public List<User> listUserInfo = new List<User>();
		string pathFoto;

		#endregion

		public FormMaster()
        {
            InitializeComponent();
		}

        public void FormMaster_Load(object sender, EventArgs e)
        {
			timerRunningText.Start();
			addPanels();
			addFlowPanels();
			addButtons();
			CekJabatanUser();

			comboBoxKriteriaBarang.Items.Add("Kode Barang");
			comboBoxKriteriaBarang.Items.Add("Nama Barang");

			comboBoxKriteriaBarangKeluar.Items.Add("No Faktur");
			comboBoxKriteriaBarangKeluar.Items.Add("Pelanggan");
			comboBoxKriteriaBarangKeluar.Items.Add("Tanggal");

			comboBoxKriteriaBarangMasuk.Items.Add("No Faktur");
			comboBoxKriteriaBarangMasuk.Items.Add("Supplier");
			comboBoxKriteriaBarangMasuk.Items.Add("Tanggal");

			comboBoxKriteriaBarang.SelectedIndex = 0;
			comboBoxKriteriaBarangKeluar.SelectedIndex = 0;
			comboBoxKriteriaBarangMasuk.SelectedIndex = 0;

			ClearAllList();

			PopulateMerekTable("","");
			PopulateJenisTable("", "");
			PopulateKategoriTable("", "");
			PopulateSupplierTable("", "");
			PopulatePelangganTable("", "");
			PopulateBarangTable("", "");
			AddUserInfo();
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
		}

		private void PopulateBarangTable(string kriteria, string nilaiKriteria)
		{
			dataGridViewDaftarBarang.DataSource = null;

			string hasilBaca = Barang.BacaDataBarang(kriteria, nilaiKriteria, listBarang);

			if (listBarang.Count > 0)
			{
				dataGridViewDaftarBarang.Rows.Clear();

				for (int i = 0; i < listBarang.Count; i++)
				{
					dataGridViewDaftarBarang.Rows.Add(listBarang[i].No, listBarang[i].IdBarang, listBarang[i].KodeBarang, listBarang[i].Nama, listBarang[i].Jenis.IdJenisBarang, listBarang[i].Jenis.Nama, listBarang[i].Kategori.IdKategoriBarang, listBarang[i].Kategori.Nama, listBarang[i].Merek.IdMerekBarang, listBarang[i].Merek.NamaMerekBarang, ConvertToRupiah(listBarang[i].HargaJual), listBarang[i].DiskonPersenJual);
				}
			}
			else if (buttonBarangClicked && textBoxSearchBarang.Text != "")
			{
				dataGridViewDaftarBarang.Rows.Clear();

				MessageBox.Show("Data tidak ditemukan");

				string barang = textBoxSearchBarang.Text.ToString();
				textBoxSearchBarang.Text = barang.Remove(barang.Length - 1);

				// set focus
				textBoxSearchBarang.SelectionStart = textBoxSearchBarang.Text.Length;
				textBoxSearchBarang.SelectionLength = 0;
			}
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
				labelProfilNama.Text = listUserInfo[0].Nama;
				textBoxNoTelpProfilUser.Text = listUserInfo[0].NoTelp;
				labelProfilTglLahir.Text = listUserInfo[0].TanggalLahir.ToString();
				labelProfilUser.Text = listUserInfo[0].Username;
				labelProfilJabatan.Text = listUserInfo[0].Jabatan.Nama;
				richTextBoxAlamatProfilUser.Text = listUserInfo[0].Alamat;
				textBoxNoRekeningProfilUser.Text = listUserInfo[0].NoRekening;
				textBoxNamaRekeningProfilUser.Text = listUserInfo[0].NamaRekening;
				textBoxRekeningBankProfilUser.Text = listUserInfo[0].NamaBank;
				textBoxPasswordProfilUser.Text = frm.DecryptPassword(listUserInfo[0].Password);
				labelJenisKelaminProfilUser.Text = listUserInfo[0].JenisKelamin;
			}

			if (ConvertBinaryToImage(listUserInfo[0].Foto) != null)
			{
				iconPictureBoxUser.Image = ConvertBinaryToImage(listUserInfo[0].Foto);
				iconPictureBoxFotoProfil.Image = ConvertBinaryToImage(listUserInfo[0].Foto);
			}
			
		}

		private void PopulatePelangganTable(string kriteria, string nilaiKriteria)
		{
			string hasilBaca = Pelanggan.BacaData(kriteria, nilaiKriteria, listPelanggan);

			if (listPelanggan.Count > 0)
			{
				dataGridViewDaftarPelanggan.DataSource = null;
				dataGridViewDaftarPelanggan.DataSource = listPelanggan;
				dataGridViewDaftarPelanggan.Columns[0].HeaderText = "No";

				dataGridViewDaftarPelanggan.Columns[1].Visible = false;

				dataGridViewDaftarPelanggan.Columns[2].HeaderText = "Nama";
				dataGridViewDaftarPelanggan.Columns[3].HeaderText = "Alamat";
				dataGridViewDaftarPelanggan.Columns[4].HeaderText = "No Telp";

			}
			else if (buttonPelangganClicked && textBoxSearchNamaPelanggan.Text != "")
			{
				dataGridViewDaftarPelanggan.DataSource = null;
				MessageBox.Show("Data tidak ditemukan");

				string pelanggan = textBoxSearchNamaPelanggan.Text.ToString();
				textBoxSearchNamaPelanggan.Text = pelanggan.Remove(pelanggan.Length - 1);

				//set focus
				textBoxSearchNamaPelanggan.SelectionStart = textBoxSearchNamaPelanggan.Text.Length;
				textBoxSearchNamaPelanggan.SelectionLength = 0;
			}
		}

		private void PopulateSupplierTable(string kriteria, string nilaiKriteria)
		{
			string hasilBaca = Supplier.BacaData(kriteria, nilaiKriteria, listSupplier);

			if (listSupplier.Count > 0)
			{
				dataGridViewDaftarSupplier.DataSource = null;
				dataGridViewDaftarSupplier.DataSource = listSupplier;
				dataGridViewDaftarSupplier.Columns[0].HeaderText = "No";

				dataGridViewDaftarSupplier.Columns[1].Visible = false;

				dataGridViewDaftarSupplier.Columns[2].HeaderText = "Nama";
				dataGridViewDaftarSupplier.Columns[3].HeaderText = "Alamat";
				dataGridViewDaftarSupplier.Columns[4].HeaderText = "No Telp";

			}
			else if (buttonSupplierClicked && textBoxSearchNamaSupplier.Text != "")
			{
				dataGridViewDaftarSupplier.DataSource = null;
				MessageBox.Show("Data tidak ditemukan");

				string supplier = textBoxSearchNamaSupplier.Text.ToString();
				textBoxSearchNamaSupplier.Text = supplier.Remove(supplier.Length - 1);

				// set focus
				textBoxSearchNamaSupplier.SelectionStart = textBoxSearchNamaSupplier.Text.Length;
				textBoxSearchNamaSupplier.SelectionLength = 0;
			}

		}

		private void PopulateJenisTable(string kriteria, string nilaiKriteria)
		{
			string hasilBaca = JenisBarang.BacaData(kriteria, nilaiKriteria, listJenis);

			if (listJenis.Count > 0)
			{
				dataGridViewDaftarJenisBrg.DataSource = null;
				dataGridViewDaftarJenisBrg.DataSource = listJenis;
				dataGridViewDaftarJenisBrg.Columns[0].HeaderText = "No";

				//dataGridViewDaftarJenisBrg.Columns[1] = "Id Jenis Barang"
				dataGridViewDaftarJenisBrg.Columns[1].Visible = false;

				dataGridViewDaftarJenisBrg.Columns[2].HeaderText = "Jenis Barang";

			}
			else if (buttonJenisClicked && textBoxSearchJenisBrg.Text != "")
			{
				dataGridViewDaftarJenisBrg.DataSource = null;
				MessageBox.Show("Data tidak ditemukan");

				string jenis = textBoxSearchJenisBrg.Text.ToString();
				textBoxSearchJenisBrg.Text = jenis.Remove(jenis.Length - 1);

				// set focus
				textBoxSearchJenisBrg.SelectionStart = textBoxSearchJenisBrg.Text.Length;
				textBoxSearchJenisBrg.SelectionLength = 0;
			}

		}

		private void PopulateMerekTable(string kriteria, string nilaiKriteria)
		{
			string hasilBaca =  MerekBarang.BacaData(kriteria, nilaiKriteria, listMerek);

			if (listMerek.Count > 0)
			{
				dataGridViewSubMenuMerekBrg.DataSource = null;
				dataGridViewSubMenuMerekBrg.DataSource = listMerek;
				dataGridViewSubMenuMerekBrg.Columns[0].HeaderText = "No";

				//dataGridViewSubMenuMerekBrg.Columns[1] = "Id Merek Barang"
				dataGridViewSubMenuMerekBrg.Columns[1].Visible = false;

				dataGridViewSubMenuMerekBrg.Columns[2].HeaderText = "Merek Barang";

			}
			else if (buttonMerekClicked && textBoxSearchMerekBrg.Text != "")
			{
				dataGridViewSubMenuMerekBrg.DataSource = null;
				MessageBox.Show("Data tidak ditemukan");
				string merek = textBoxSearchMerekBrg.Text.ToString();
				textBoxSearchMerekBrg.Text = merek.Remove(merek.Length - 1);

				// set focus
				textBoxSearchMerekBrg.SelectionStart = textBoxSearchMerekBrg.Text.Length;
				textBoxSearchMerekBrg.SelectionLength = 0;
			}
		}

		private void PopulateKategoriTable(string kriteria, string nilaiKriteria)
		{
			string hasilBaca = KategoriBarang.BacaData(kriteria, nilaiKriteria, listKategori);

			if (listKategori.Count > 0)
			{
				dataGridViewSubMenuKategoriBrg.DataSource = null;
				dataGridViewSubMenuKategoriBrg.DataSource = listKategori;
				dataGridViewSubMenuKategoriBrg.Columns[0].HeaderText = "No";

				//dataGridViewSubMenuKategoriBrg.Columns[1] =  "Id Kategori Barang"
				dataGridViewSubMenuKategoriBrg.Columns[1].Visible = false;

				dataGridViewSubMenuKategoriBrg.Columns[2].HeaderText = "Kategori Barang";
			}
			else if (buttonKategoriClicked && textBoxSearchKategoriBrg.Text != "")
			{
				dataGridViewSubMenuKategoriBrg.DataSource = null;
				MessageBox.Show("Data tidak ditemukan");

				string kategori = textBoxSearchKategoriBrg.Text.ToString();
				textBoxSearchKategoriBrg.Text = kategori.Remove(kategori.Length - 1);

				// set focus
				textBoxSearchKategoriBrg.SelectionStart = textBoxSearchKategoriBrg.Text.Length;
				textBoxSearchKategoriBrg.SelectionLength = 0;
			}
		}
		private void CekJabatanUser()
		{
			string jabatan = labelJabatanUser.Text.ToString();

			if (jabatan != "Pemilik")
			{
				label16.Visible = false;
				label17.Visible = false;
				label21.Visible = false;
				textBoxNamaRekeningProfilUser.Visible = false;
				textBoxNoRekeningProfilUser.Visible = false;
				textBoxRekeningBankProfilUser.Visible = false;

				buttonSimpanInfoUser.Location = new Point(27, 351);
			}

			List<HakAkses> listHakAkses = new List<HakAkses>();

			string hasilCek = HakAkses.BacaData("j.nama", jabatan, listHakAkses);

			if (hasilCek == "1")
			{
				for (int i = 0; i < listHakAkses.Count; i++)
				{
					if (listHakAkses[i].Submenu.Nama == "")
					{
						if (listHakAkses[i].BlokirAkses == "On")
						{
							deactiveMenuButtons(allMainButtons, listHakAkses[i].Menu.Nama.ToString());
						}
					}
					else if (listHakAkses[i].Submenu.Nama != "")
					{
						if (listHakAkses[i].BlokirAkses == "On")
						{
							deactiveSubMenus(allFlowPanels, listHakAkses[i].Menu.Nama.ToString(), listHakAkses[i].Submenu.Nama.ToString());
						}
					}
				}
			}
		}

		private void deactiveMenuButtons(ArrayList arr, string text)
		{
			foreach (IconButton value in arr)
			{
				if (value.Text.ToLower().Contains(text))
				{
					value.Visible = false;
				}
			}
		}

		private void deactiveSubMenus(ArrayList arrSubMenu, string menuName, string subMenuName)
		{
			foreach (FlowLayoutPanel value in arrSubMenu)
			{
				if (value.Name.ToLower().Contains(menuName))
				{
					var buttons = value.Controls.OfType<Button>().ToList();

					foreach (Button btn in buttons)
					{
						if (btn.Text.ToLower().Contains(subMenuName))
						{
							btn.Visible = false;
						}
					}
				}
			}
		}

		private void addButtons()
		{
			allMainButtons.Add(iconButtonBarang);
			allMainButtons.Add(iconButtonTransaksi);
			allMainButtons.Add(iconButtonJabatan);
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
			allPanels.Add(panelMenuJabatan);
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

		private void iconButtonBackPanel_MouseHover(object sender, EventArgs e)
		{
			iconButtonBackPanelBarang.ForeColor = Color.FromArgb(7, 104, 159);
		}

		private void iconButtonBackPanel_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBackPanelBarang.ForeColor = Color.FromArgb(12, 26, 46);
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
			//activeButtonIndicator(panelButtonIndicator, iconButtonTransaksi, Color.FromArgb(227, 139, 50));
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
			navigateTo(panelMenuPegawai, "Menu Pegawai"); 

		}

		private void iconButtonJabatan_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonJabatan);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonJabatan, Color.FromArgb(227, 139, 50));
			navigateTo(panelMenuJabatan, "Menu Jabatan");
		}

		private void iconButtonSupplier_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonSupplier);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonSupplier, Color.FromArgb(227, 139, 50));
			navigateTo(panelMenuSupplier, "Menu Supplier");

			buttonSupplierClicked = true;
		}

		private void iconButtonPelanggan_Click(object sender, EventArgs e)
		{
			var unActiveButton = allMainButtons;
			unActiveButton.Remove(iconButtonPelanggan);

			activeButtonIndicator(panelButtonIndicator, unActiveButton, iconButtonPelanggan, Color.FromArgb(227, 139, 50));
			navigateTo(panelMenuPelanggan, "Menu Pelanggan");

			buttonPelangganClicked = true;
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
			this.Hide();
			FormLogin frm = new FormLogin();
			frm.ShowDialog();

			this.Close();
		}

		#endregion

		private void textBoxSearchMenuBarang_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchBarang.Text == "")
			{
				panelUnderlineSearchBarang.BackColor = Color.FromArgb(12, 26, 46);
			}
			else
			{
				panelUnderlineSearchBarang.BackColor = Color.FromArgb(7, 104, 159);
			}

			listBarang.Clear();
			dataGridViewDaftarBarang.DataSource = null;

			if (comboBoxKriteriaBarang.SelectedIndex == 0)
			{
				PopulateBarangTable("b.kode_barang", textBoxSearchBarang.Text);
			}
			else if (comboBoxKriteriaBarang.SelectedIndex == 1)
			{
				PopulateBarangTable("b.nama", textBoxSearchBarang.Text);
			}
			
		}

		private void iconButtonBarangBarang_Click(object sender, EventArgs e)
		{
			navigateTo(panelSubMenuDaftarBarang, "Daftar Barang");
			buttonBarangClicked = true;
		}

		private void iconButtonJenisBarang_Click(object sender, EventArgs e)
		{
			navigateTo(panelSubMenuJenisBarang, "Daftar Jenis Barang");
			buttonJenisClicked = true;
		}

		private void iconButtonKategoriBarang_Click(object sender, EventArgs e)
		{
			navigateTo(panelSubMenuKategoriBarang, "Daftar Kategori Barang");
			buttonKategoriClicked = true;
		}

		private void iconButtonMerekBarang_Click(object sender, EventArgs e)
		{
			navigateTo(panelSubMenuMerekBarang, "Daftar Merek Barang");
			buttonMerekClicked = true;
		}

		private void iconButtonBarangMasuk_Click(object sender, EventArgs e)
		{
			navigateTo(panelSubMenuTransaksiBarangMasuk, "Barang Masuk");
		}

		private void iconButtonBarangKeluar_Click(object sender, EventArgs e)
		{
			navigateTo(panelSubMenuTransaksiBarangKeluar, "Barang Keluar");
		}

		private void textBoxSearchJenisBrg_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchJenisBrg.Text == "")
			{
				panelUnderlineSearchJenisBrg.BackColor = Color.FromArgb(12, 26, 46);
			}
			else
			{
				panelUnderlineSearchJenisBrg.BackColor = Color.FromArgb(7, 104, 159);
			}

			listJenis.Clear();
			dataGridViewDaftarJenisBrg.DataSource = null;
			PopulateJenisTable("nama", textBoxSearchJenisBrg.Text);
		}

		private void textBoxSearchKategoriBrg_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchKategoriBrg.Text == "")
			{
				panelUnderlineSearchKategori.BackColor = Color.FromArgb(12, 26, 46);
			}
			else
			{
				panelUnderlineSearchKategori.BackColor = Color.FromArgb(7, 104, 159);
			}

			listKategori.Clear();
			dataGridViewSubMenuKategoriBrg.DataSource = null;
			PopulateKategoriTable("nama", textBoxSearchKategoriBrg.Text);
		}

		private void textBoxSearchMerekBrg_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchMerekBrg.Text == "")
			{
				panelUnderlineSearchMerek.BackColor = Color.FromArgb(12, 26, 46);

			}
			else
			{
				panelUnderlineSearchMerek.BackColor = Color.FromArgb(7, 104, 159);

			}

			listMerek.Clear();
			dataGridViewSubMenuMerekBrg.DataSource = null;
			PopulateMerekTable("nama", textBoxSearchMerekBrg.Text);

			
		}

		private void textBoxSearchNamaBrgInventaris_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchNamaBrgInventaris.Text == "")
			{
				panelUnderlineSearchBrgInventaris.BackColor = Color.FromArgb(12, 26, 46);
			}
			else
			{
				panelUnderlineSearchBrgInventaris.BackColor = Color.FromArgb(7, 104, 159);
			}
		}

		private void textBoxSearchNamaSupplier_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchNamaSupplier.Text == "")
			{
				panelUnderlineSearchSupplier.BackColor = Color.FromArgb(12, 26, 46);
			}
			else
			{
				panelUnderlineSearchSupplier.BackColor = Color.FromArgb(7, 104, 159);
			}

			listSupplier.Clear();
			dataGridViewDaftarSupplier.DataSource = null;
			PopulateSupplierTable("nama", textBoxSearchNamaSupplier.Text);
		}

		private void iconButtonLaporanInventarisBarang_Click(object sender, EventArgs e)
		{
			navigateTo(panelSubMenuLaporanStokBarang, "Laporan Stok Barang");
		}

		private void iconButtonLaporanPembelian_Click(object sender, EventArgs e)
		{
			navigateTo(panelSubMenuLaporanPembelian, "Laporan Pembelian");
		}

		private void iconButtonLaporanPenjualan_Click(object sender, EventArgs e)
		{
			navigateTo(panelSubMenuLaporanPenjualan, "Laporan Penjualan");
		}

		private void textBoxSearchPegawai_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchPegawai.Text == "")
			{
				panelUnderlineSearchPegawai.BackColor = Color.FromArgb(12, 26, 46);
			}
			else
			{
				panelUnderlineSearchPegawai.BackColor = Color.FromArgb(7, 104, 159);
			}
		}

		private void textBoxSearchJabatan_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchJabatan.Text == "")
			{
				panelUnderlineSearchJbtn.BackColor = Color.FromArgb(12, 26, 46);
			}
			else
			{
				panelUnderlineSearchJbtn.BackColor = Color.FromArgb(7, 104, 159);
			}
		}

		private void iconButtonBackPanelJenisBrg_MouseHover(object sender, EventArgs e)
		{
			iconButtonBackPanelJenisBrg.ForeColor = Color.FromArgb(7, 104, 159);
		}

		private void iconButtonBackPanelJenisBrg_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBackPanelJenisBrg.ForeColor = Color.FromArgb(12, 26, 46);
		}

		private void iconButtonBackPanelKategoriBrg_MouseHover(object sender, EventArgs e)
		{
			iconButtonBackPanelKategoriBrg.ForeColor = Color.FromArgb(7, 104, 159);
		}

		private void iconButtonBackPanelKategoriBrg_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBackPanelKategoriBrg.ForeColor = Color.FromArgb(12, 26, 46);
		}

		private void iconButtonBackPanelMerekBrg_MouseHover(object sender, EventArgs e)
		{
			iconButtonBackPanelMerekBrg.ForeColor = Color.FromArgb(7, 104, 159);
		}

		private void iconButtonBackPanelMerekBrg_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBackPanelMerekBrg.ForeColor = Color.FromArgb(12, 26, 46);
		}

		private void iconButtonBackPanelBarangMasuk_MouseHover(object sender, EventArgs e)
		{
			iconButtonBackPanelBarangMasuk.ForeColor = Color.FromArgb(7, 104, 159);
		}

		private void iconButtonBackPanelBarangMasuk_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBackPanelBarangMasuk.ForeColor = Color.FromArgb(12, 26, 46);
		}

		private void iconButtonBackLaporanInventaris_MouseHover(object sender, EventArgs e)
		{
			iconButtonBackLaporanInventaris.ForeColor = Color.FromArgb(7, 104, 159);
		}

		private void iconButtonBackLaporanInventaris_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBackLaporanInventaris.ForeColor = Color.FromArgb(12, 26, 46);
		}

		private void iconButtonBackBarangKeluar_MouseHover(object sender, EventArgs e)
		{
			iconButtonBackBarangKeluar.ForeColor = Color.FromArgb(7, 104, 159);
		}

		private void iconButtonBackBarangKeluar_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBackBarangKeluar.ForeColor = Color.FromArgb(12, 26, 46);
		}

		private void iconButtonBackLaporanPembelian_MouseHover(object sender, EventArgs e)
		{
			iconButtonBackLaporanPembelian.ForeColor = Color.FromArgb(7, 104, 159);
		}

		private void iconButtonBackLaporanPembelian_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBackLaporanPembelian.ForeColor = Color.FromArgb(12, 26, 46);
		}

		private void iconButtonBackLaporanPenjualan_MouseHover(object sender, EventArgs e)
		{
			iconButtonBackLaporanPenjualan.ForeColor = Color.FromArgb(7, 104, 159);
		}

		private void iconButtonBackLaporanPenjualan_MouseLeave(object sender, EventArgs e)
		{
			iconButtonBackLaporanPenjualan.ForeColor = Color.FromArgb(12, 26, 46);
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
			if (comboBoxKriteriaBarangKeluar.SelectedIndex == 2)
			{
				dateTimePickerTglAwalNotaJual.Visible = true;
				dateTimePickerTanggalAkhirNotaJual.Visible = true;
				labelSDNotaJual.Visible = true;
				textBoxSearchBarangKeluar.Visible = false;
				panelUnderlineSearchBarangKeluar.Visible = false;
			}
			else
			{
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
			}
			else
			{
				panelUnderlineSearchBarangKeluar.BackColor = Color.FromArgb(7, 104, 159);
			}
		}

		private void textBoxSearchBarangMasuk_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchBarangMasuk.Text == "")
			{
				panelUnderlineSearchBarangMasuk.BackColor = Color.FromArgb(12, 26, 46);
			}
			else
			{
				panelUnderlineSearchBarangMasuk.BackColor = Color.FromArgb(7, 104, 159);
			}
		}

		private void iconButtonBackPanelBarangMasuk_Click_1(object sender, EventArgs e)
		{
			navigateTo(panelMenuTransaksi, "Menu Transaksi");
		}

		private void comboBoxKriteriaBarangMasuk_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxKriteriaBarangMasuk.SelectedIndex == 2)
			{
				dateTimePickerTanggalAwalNotaBeli.Visible = true;
				dateTimePickerTanggalAkhirNotaBeli.Visible = true;
				labelSDNotaBeli.Visible = true;
				textBoxSearchBarangMasuk.Visible = false;
				panelUnderlineSearchBarangMasuk.Visible = false;
			}
			else
			{
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
					merek.NamaMerekBarang = row.Cells[2].Value.ToString();

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
					merek.NamaMerekBarang = row.Cells[2].Value.ToString();

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

					FormMaster_Load(sender, e);
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

			foreach (DataGridViewRow r in dataGridViewDaftarBarang.SelectedRows)
			{
				MessageBox.Show(r.Cells[1].Value.ToString());
				hasilBaca = Barang.BacaDetailBarang("b.idbarang", r.Cells[1].Value.ToString(), listSelectedBarang);
			}

			FormDetailBarang frm = new FormDetailBarang();
			frm.Show();
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

					FormMaster_Load(sender, e);
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
					supplier.Alamat = row.Cells[3].Value.ToString();
					supplier.NoTelp = row.Cells[4].Value.ToString();

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

					FormMaster_Load(sender, e);
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}

		private void textBoxSearchNamaPelanggan_TextChanged(object sender, EventArgs e)
		{
			if (textBoxSearchNamaPelanggan.Text == "")
			{
				panelUnderlineSearchPelanggan.BackColor = Color.FromArgb(12, 26, 46);
			}
			else
			{
				panelUnderlineSearchPelanggan.BackColor = Color.FromArgb(7, 104, 159);
			}

			listPelanggan.Clear();
			dataGridViewDaftarPelanggan.DataSource = null;
			PopulatePelangganTable("nama", textBoxSearchNamaPelanggan.Text);
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
					pelanggan.Alamat = row.Cells[3].Value.ToString();
					pelanggan.NoTelp = row.Cells[4].Value.ToString();

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

					FormMaster_Load(sender, e);
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
			user.IdUser = listUserInfo[0].IdUser;
			user.Password = textBoxPasswordProfilUser.Text;
			user.Alamat = richTextBoxAlamatProfilUser.Text;
			user.NoTelp = textBoxNoTelpProfilUser.Text;
			if (listUserInfo[0].Jabatan.Nama == "Pemilik")
			{
				user.NamaRekening = textBoxNamaRekeningProfilUser.Text;
				user.NoRekening = textBoxNoRekeningProfilUser.Text;
				user.NamaBank = textBoxRekeningBankProfilUser.Text;
			}
			else
			{
				user.NamaRekening = "";
				user.NoRekening = "";
				user.NamaBank = "";
			}
			if (pathFoto != null)
			{
				user.Foto = ConvertImageToBinary(Image.FromFile(pathFoto));
			}

			if (textBoxPasswordProfilUser.Text.Length >= 8)
			{
				if (User.UbahData(user) == "1")
				{
					iconPictureBoxUser.ImageLocation = pathFoto;
					iconPictureBoxFotoProfil.ImageLocation = pathFoto;

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
				if (new FileInfo(openFileDialog.FileName).Length > (64 * 1024))
				{
					MessageBox.Show("Ukuran file tidak boleh lebih dari 64 kb");
				}
				else
				{

					iconPictureBoxFotoProfil.Image = new Bitmap(openFileDialog.FileName);
					pathFoto = openFileDialog.FileName;
				}
			}
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

		Image ConvertBinaryToImage(byte[] data)
		{
			var img = Resources.profile_picture;

			if (data == null)
				return img; ;
			using (MemoryStream ms = new MemoryStream(data))
			{
				try
				{
					return Image.FromStream(ms);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message.ToString());

					return img;
				}
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
					hasilBaca = Barang.BacaDetailBarang("b.idbarang", row.Cells[1].Value.ToString(), listSelectedBarang);
				}

				FormUbahBarang frm = new FormUbahBarang(this);
				frm.Show();
			}
		}

		private void buttonHapusDaftarBarang_Click(object sender, EventArgs e)
		{

		}
	}
}

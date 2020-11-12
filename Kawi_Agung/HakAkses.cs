using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kawi_Agung
{
	public class HakAkses
	{
		#region DATAMEMBERS

		private int idHakAkses;
		private Menu menu;
		private SubMenu submenu;
		private string blokirAkses;
		private Jabatan jabatan;

		#endregion

		#region PROPERTIES
		public int IdHakAkses { get => idHakAkses; set => idHakAkses = value; }
		public Menu Menu { get => menu; set => menu = value; }
		public SubMenu Submenu { get => submenu; set => submenu = value; }
		public string BlokirAkses { get => blokirAkses; set => blokirAkses = value; }
		public Jabatan Jabatan { get => jabatan; set => jabatan = value; }

		#endregion

		#region CONSTRUCTORS

		public HakAkses()
		{
			IdHakAkses = Interlocked.Increment(ref idHakAkses); ;
			BlokirAkses = "Off";
		}

		public HakAkses(int pIdHakAkses, Menu pMenu, SubMenu pSubmenu, string pBlokirAkses, Jabatan pJabatan)
		{
			IdHakAkses = pIdHakAkses;
			Menu = pMenu;
			Submenu = pSubmenu;
			BlokirAkses = pBlokirAkses;
			Jabatan = pJabatan;
		}

		#endregion

		#region METHODS
		public int? ToNullableInt(string s)
		{
			int i;
			if (int.TryParse(s, out i)) return i;
			return null;
		}

		public static string BacaData(string kriteria, string nilaiKriteria, List<HakAkses> listHakAkses)
		{
			string sql = "";

			if (kriteria == "")
			{
				sql = "SELECT h.idhak_akses, h.idmenu, m.nama, h.idsub_menu, sm.nama, h.blokir_akses, h.idjabatan, j.nama FROM hak_akses h INNER JOIN  menu m ON h.idmenu=m.idmenu LEFT JOIN sub_menu sm ON h.idsub_menu=sm.idsub_menu INNER JOIN jabatan j ON h.idjabatan=j.idjabatan";
			}
			else
			{
				sql = "SELECT h.idhak_akses, h.idmenu, m.nama, h.idsub_menu, sm.nama, h.blokir_akses, h.idjabatan, j.nama FROM hak_akses h INNER JOIN  menu m ON h.idmenu=m.idmenu LEFT JOIN sub_menu sm ON h.idsub_menu=sm.idsub_menu INNER JOIN jabatan j ON h.idjabatan=j.idjabatan WHERE " + kriteria + "= '" + nilaiKriteria + "'";
			}
			try
			{
				MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

				while (hasilData.Read())
				{
					HakAkses hakAkses = new HakAkses();

					hakAkses.IdHakAkses = int.Parse(hasilData.GetValue(0).ToString());

					Menu hMenu = new Menu(int.Parse(hasilData.GetValue(1).ToString()), hasilData.GetValue(2).ToString());

					hakAkses.Menu = hMenu;

					SubMenu hSubmenu = new SubMenu(hakAkses.ToNullableInt(hasilData.GetValue(3).ToString()), hasilData.GetValue(4).ToString(), null);

					hakAkses.Submenu = hSubmenu;

					hakAkses.BlokirAkses = hasilData.GetValue(5).ToString();

					Jabatan hJabatan = new Jabatan(int.Parse(hasilData.GetValue(6).ToString()), hasilData.GetValue(7).ToString());

					hakAkses.Jabatan = hJabatan;

					listHakAkses.Add(hakAkses);
				}
				return "1";
			}
			catch (Exception ex)
			{
				return "Terjadi Kesalahan. Pesan Kesalahan : " + ex.ToString();
			}
		}

		#endregion


	}
}

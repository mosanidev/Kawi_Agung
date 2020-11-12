using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class SubMenu
	{
		#region DATAMEMBERS

		private int? idSubmenu;
		private string nama;
		private Menu idMenu;

		#endregion

		#region PROPERTIES

		public int? IdSubmenu { get => idSubmenu; set => idSubmenu = value; }
		public string Nama { get => nama; set => nama = value; }
		public Menu IdMenu { get => idMenu; set => idMenu = value; }

		#endregion

		#region CONSTRUCTORS
		public SubMenu(int? a, string v)
		{
			IdSubmenu = 0;
			Nama = "";
		}

		public SubMenu(int? pIdSubmenu, string pNama, Menu pIdMenu)
		{
			IdSubmenu = pIdSubmenu;
			Nama = pNama;
			IdMenu = pIdMenu;
		}
		#endregion
	}
}

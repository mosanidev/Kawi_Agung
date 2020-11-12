using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawi_Agung
{
	public class Menu
	{
		#region DATAMEMBERS 

		private int idMenu;
		private string nama;

		#endregion

		#region PROPERTIES
		public int IdMenu { get => idMenu; set => idMenu = value; }
		public string Nama { get => nama; set => nama = value; }

		#endregion

		#region CONSTRUCTORS

		public Menu()
		{
			IdMenu = 0;
			Nama = "";
		}

		public Menu(int pIdMenu, string pNama)
		{
			IdMenu = pIdMenu;
			Nama = pNama;
		}

		#endregion
	}
}

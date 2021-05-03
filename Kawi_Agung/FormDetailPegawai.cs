using Kawi_Agung.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kawi_Agung
{
	public partial class FormDetailPegawai : Form
	{
		public FormDetailPegawai()
		{
			InitializeComponent();
		}

		private void FormDetailPegawai_Load(object sender, EventArgs e)
		{
			labelNama.Text = FormMaster.listSelectedPegawai[0].Nama;
			labelJenisKelamin.Text = FormMaster.listSelectedPegawai[0].JenisKelamin;
			labelTanggalLahir.Text = FormMaster.listSelectedPegawai[0].TanggalLahir.ToString("dd MMMM yyyy");
			labelNoTelp.Text = FormMaster.listSelectedPegawai[0].NoTelp;
			labelStatus.Text = FormMaster.listSelectedPegawai[0].Status;
			labelUsername.Text = FormMaster.listSelectedPegawai[0].Username;
			labelJabatan.Text = FormMaster.listSelectedPegawai[0].Jabatan.Nama;
			labelAlamat.Text = FormMaster.listSelectedPegawai[0].Alamat;

			if (FormMaster.listSelectedPegawai[0].Foto != "")
			{
				string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\users";
				string folderName = Path.Combine(projectPath, FormMaster.listSelectedPegawai[0].Username);
				pictureBoxGambarPegawai.ImageLocation = folderName + "\\" + "foto" + FormMaster.listSelectedPegawai[0].Foto.ToString();
			}
			else
			{
				pictureBoxGambarPegawai.Image = Resources.profile_picture;
			}
		}

	}
}

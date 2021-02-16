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

			if (FormMaster.listSelectedPegawai[0].Foto != null)
			{
				pictureBoxGambarPegawai.Image = ConvertBinaryToImage(FormMaster.listSelectedPegawai[0].Foto);
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

	}
}

using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Kawi_Agung
{
    public partial class FormLogin : Form
    {
        public List<User> listUser = new List<User>();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonNext_Click(object sender, EventArgs e)
		{
			listUser.Clear();

			if (textBoxUsername.Text == "")
			{
                MessageBox.Show("Harap diisi terlebih dahulu");
				textBoxUsername.Clear();
				textBoxUsername.Focus();
			}
			else if (textBoxUsername.Text != "")
			{
				string hasilBaca = User.BacaData("u.username", textBoxUsername.Text, listUser);

				if (listUser.Count == 1)
				{
                    if (listUser[0].Status == "Aktif")
                    {
                        switchPanel(panelUsername, panelPassword, new Point(12, 54), new Point(999, 999));
                        textBoxPassword.Focus();
                    }
                    else if (listUser[0].Status == "Belum Aktif")
                    {
                        MessageBox.Show("Untuk mengaktifkan akun, silahkan buat password terlebih dahulu");
                        switchPanel(panelUsername, panelBuatPassword, new Point(12, 54), new Point(999, 999));
                        textBoxNewPassword.Focus();
                    }
                    else if (listUser[0].Status == "Blokir")
                    {
                        MessageBox.Show("Mohon maaf anda tidak dapat mengakses sistem. Akun anda terblokir");
                        textBoxUsername.Clear();
                        textBoxUsername.Focus();
                    }
          
                }
                else if (listUser.Count == 0)
                {
                    MessageBox.Show("Username tidak terdaftar di dalam sistem");
                    textBoxUsername.Clear();
                    textBoxUsername.Focus();
                }
                textBoxUsername.Clear();
            }
        }

        private void btnNextPassword_Click(object sender, EventArgs e)
		{
            if (textBoxPassword.Text == "")
            {
                MessageBox.Show("Harap diisi terlebih dahulu");
                textBoxPassword.Clear();
                textBoxPassword.Focus();
            }
            else if (DecryptPassword(listUser[0].Password) == textBoxPassword.Text)
            {
                // berhasil masuk
                MessageBox.Show("Selamat datang, " + listUser[0].Nama);

                this.Hide();
                FormMaster frmMaster = new FormMaster();

                //frmMaster.iconPictureBoxUser
                frmMaster.labelNamaUser.Text = listUser[0].Username;
				frmMaster.labelJabatanUser.Text = listUser[0].Jabatan.Nama;

				frmMaster.ShowDialog();
                this.Close();

            }
            else if (DecryptPassword(listUser[0].Password) != textBoxPassword.Text)
            {
                // salah password
                MessageBox.Show("Password yang anda masukkan salah");
                textBoxPassword.Clear();
                textBoxPassword.Focus();
            }
        }

		private void btnNextBuatPassword_Click(object sender, EventArgs e)
		{
            if (textBoxNewPassword.Text == "" || textBoxRePassword.Text == "")
            {
                MessageBox.Show("Harap di isi terlebih dahulu");
                textBoxNewPassword.Clear();
                textBoxRePassword.Clear();
                textBoxNewPassword.Focus();
            }
            else if (textBoxNewPassword.Text.Length < 8)
            {
                MessageBox.Show("Password minimal 8 karakter");
            }
            else if (textBoxRePassword.Text != textBoxNewPassword.Text)
            {
                MessageBox.Show("Harap ketikkan ulang password dengan benar");
                textBoxNewPassword.Clear();
                textBoxRePassword.Clear();
                textBoxNewPassword.Focus();
            }
            else if (textBoxRePassword.Text == textBoxNewPassword.Text)
            {
                string hasil = User.AktifkanUser(listUser[0].Username.ToString(), textBoxNewPassword.Text);

                if (hasil == "1")
                {
                    MessageBox.Show("Selamat datang, " + listUser[0].Nama);

                    this.Hide();
                    FormMaster frmMaster = new FormMaster();

                    //frmMaster.iconPictureBoxUser
                    frmMaster.labelNamaUser.Text = listUser[0].Username;
                    frmMaster.labelJabatanUser.Text = listUser[0].Jabatan.Nama;

                    frmMaster.ShowDialog();
                    this.Close();
                }
            }
        }

        private void switchPanel(Panel panelBefore, Panel panelAfter, Point destination, Point alienation)
        {
            panelBefore.Dock = DockStyle.None;
            panelBefore.Location = alienation;
            panelAfter.Location = destination;
            panelAfter.Dock = DockStyle.Fill;

        }

        public string DecryptPassword(string password)
        {
            byte[] data = Convert.FromBase64String(password);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] keys = md5.ComputeHash(utf8.GetBytes("EXtr@_S@Lt"));
            TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            ICryptoTransform transform = tripDES.CreateDecryptor();
            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
            return utf8.GetString(results);
        }

		private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.Enter)
            {
                buttonNext_Click(sender, e);
            }
		}

		private void textBoxRePassword_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.Enter)
            {
                btnNextBuatPassword_Click(sender, e);
            }
        }

		private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.Enter)
            {
                btnNextPassword_Click(sender, e);
            }
		}

		private void btnMinimize_Click(object sender, EventArgs e)
		{
            this.WindowState = FormWindowState.Minimized;
        }

		private void iconButtonBackPanelPassword_Click(object sender, EventArgs e)
		{
            textBoxUsername.Clear();
            textBoxPassword.Clear();

            switchPanel(panelPassword, panelUsername, new Point(12, 54), new Point(999, 999));

        }

		private void iconButtonBackPanelNewPassword_Click(object sender, EventArgs e)
		{
            textBoxUsername.Clear();
            textBoxNewPassword.Clear();
            textBoxRePassword.Clear();

            switchPanel(panelBuatPassword, panelUsername, new Point(12, 54), new Point(999, 999));
        }
	}
}

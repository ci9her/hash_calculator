

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace HashCalculator
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Check();
		}

		private void CmdCompute_Click(object sender, EventArgs e)
		{
			Check();
		}

		private void CmdBrowsePath_Click(object sender, EventArgs e)
		{
			var FD = new System.Windows.Forms.OpenFileDialog();
			if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string fileToOpen = FD.FileName;
				TxtPath.Text = FD.FileName;
				Check();
			}
		}

		private void txtPath_TextChanged(object sender, EventArgs e)
		{
			Clear();
		}

		


		private void ChkMD5_CheckedChanged(object sender, EventArgs e)
		{
			Check();
		}

		private void ChkSHA1_CheckedChanged(object sender, EventArgs e)
		{
			Check();
		}

		private void ChkSHA256_CheckedChanged(object sender, EventArgs e)
		{
			Check();
		}

		private void ChkSHA512_CheckedChanged(object sender, EventArgs e)
		{
			Check();
		}

		public void Clear()
		{
			TxtMD5.Text = "";
			TxtSHA1.Text = "";
			TxtSHA256.Text = "";
			TxtSHA512.Text = "";
		}

		

		private static string GetHash(string filePath, HashAlgorithm hasher)
		{
			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				return GetHash(fs, hasher);
		}

		private static string GetHash(Stream s, HashAlgorithm hasher)
		{
			var hash = hasher.ComputeHash(s);
			return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();			
		}

		public void Compute(CheckBox c, TextBox t, string a)
		{
			if (c.Checked)
			{
				if ((t.Text == "") && (TxtPath.Text != ""))
				{
					HashAlgorithm algo = null;
					if (a == "md5")
						algo = new MD5CryptoServiceProvider();
					else if (a == "sha1")
						algo = new SHA1CryptoServiceProvider();
					else if (a == "sha256")
						algo = new SHA256CryptoServiceProvider();
					else if (a == "sha512")
						algo = new SHA512CryptoServiceProvider();

					if (algo != null)
					{
						try
						{
							t.BackColor = Color.LightYellow;
							t.Text = "Calculation...";							
							t.Text = GetHash(TxtPath.Text, algo);							
						}
						catch (Exception e)
						{
							t.Text = e.Message;
						}
						t.Refresh();
					}
					else
						t.Text = "Unknown algorithm.";
				}
			}
			else
			{
				t.Text = "";
			}
						
			
			t.Refresh();

			Application.DoEvents();
		}

		public void Check()
		{
			Application.DoEvents();

			Compute(ChkMD5, TxtMD5, "md5");
			Compute(ChkSHA1, TxtSHA1, "sha1");
			Compute(ChkSHA256, TxtSHA256, "sha256");
			Compute(ChkSHA512, TxtSHA512, "sha512");
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}

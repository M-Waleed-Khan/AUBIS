using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AUBIS
{
   sealed public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        string[] username = { "username1", "username2" };
        string[] password = { "password1", "password2" };
        private  void button1_Click(object sender, EventArgs e)
        {
            
            
                if (username.Contains(txtUserName.Text) && password.Contains(txtPassword.Text) && Array.IndexOf(username, txtUserName.Text) == Array.IndexOf(password, txtPassword.Text))
                {
                    AUBISf aubis = new AUBISf();
                    aubis.Owner = this;
                    this.Hide();
                    aubis.ShowDialog();
                    this.Close();
                    lblIncorrect.Hide();
                   
                }
            
            else
            {
                lblIncorrect.Show();
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtUserName.Text == "Username:")
            {
                txtUserName.ForeColor = Color.Black;
                txtUserName.Text = "";
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            if (txtPassword.Text == "Password:")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                
                
            }
        }

       
    }
}

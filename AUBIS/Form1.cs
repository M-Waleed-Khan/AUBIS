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

namespace AUBIS
{
   sealed public partial class AUBISf : Form
    {
        public AUBISf()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = true;
                App.AUBIS.AddAUBISRow(App.AUBIS.NewAUBISRow());
                aUBISBindingSource.MoveLast();
                txtFullName.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                App.AUBIS.RejectChanges();
            }


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txtBloodGroup.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedRow;
            selectedRow = dataGridView.CurrentCell.RowIndex;
            dataGridView.Rows.RemoveAt(selectedRow);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                aUBISBindingSource.EndEdit();
                App.AUBIS.AcceptChanges();
                App.AUBIS.WriteXml(string.Format("{0}//data.dat", Application.StartupPath));
                panel1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                App.AUBIS.RejectChanges();
            }
        }

        static AubisData db;
        protected static AubisData App
        {
            get
            {
                if (db == null)
                    db = new AubisData();
                return db;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string filename = string.Format("{0}//data.dat", Application.StartupPath);
            if (File.Exists(filename))
                App.AUBIS.ReadXml(filename);
            aUBISBindingSource.DataSource = App.AUBIS;
            panel1.Enabled = false;
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    aUBISBindingSource.RemoveCurrent();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    var query = from o in App.AUBIS
                                where o.Contact == txtSearch.Text || o.FullName.Contains(txtSearch.Text) || o.BloodGroup == txtSearch.Text
                                select o;
                    
                    dataGridView.DataSource = query.ToList();
                }
                else
                    dataGridView.DataSource = aUBISBindingSource;
            
        }

        private void AUBISf_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialoge = MessageBox.Show("Make sure to save data, Any unsaved data will be removed, Exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialoge == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else if (dialoge == DialogResult.No)
            {
                e.Cancel = true;
            }
        }


    }
}

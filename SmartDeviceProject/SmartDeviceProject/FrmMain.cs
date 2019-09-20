using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
namespace SmartDeviceProject
{
    public partial class FrmMain : Form
    {
        string qrcode;
        public FrmMain()
        {
            InitializeComponent();
            user.PasswordChar = '*';
            user.Focus();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }

        private void checklogin()//update_data_to_db();
        {
           
                qrcode = user.Text;
                //password_s = pass.Text;
                WebReference.WebService_kanban mytagdata = new WebReference.WebService_kanban();
                DataTable dt = mytagdata.login(this.qrcode);
                string qr_db = (string)dt.Rows[0]["QRCode"];
                string name = (string)dt.Rows[0]["Username"];
                FrmHome f1 = new FrmHome(qrcode); //ตั้งชื่อเพื่อเรียกใช้ฟอร์ม1 พร้อมส่งข้อความ (data)
                //MessageBox.Show(qrcode);
                if (dt.Rows.Count > 0)
                {
                    if (qrcode == qr_db)
                    {
                         MessageBox.Show("เข้าสู่ระบบสำเร็จ ยินดีต้อนรับ : '" + name + "' ");
                        user.Text = name;
                        this.Hide();
                        f1.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามรถเข้าระบบได้");
                    }
                    //MessageBox.Show(get_username);
                }
            
 
        }

        private void home_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("click home");
            checklogin();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจที่จะออกจากโปรแกรมหรือไม่?", "หน้าต่างยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void delete_rows_mkanban()//Delete same rows
        {
            SqlCeConnection connection = new SqlCeConnection("Data Source=deploy\\m_kanban_local.sdf");
            connection.Open();
            SqlCeCommand sCommand = new SqlCeCommand(@"DELETE FROM m_kanban_db", connection);
            sCommand.ExecuteNonQuery();
            //MessageBox.Show("Kanban Update Sucessfuly");
            connection.Close();
        }

        private void update_Click(object sender, EventArgs e)
        {
            delete_rows_mkanban();
        }
    }
}
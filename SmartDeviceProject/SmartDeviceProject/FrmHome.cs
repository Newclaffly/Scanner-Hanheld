using System;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Collections;

namespace SmartDeviceProject
{
    public partial class FrmHome : Form
    {
        //string connecting = @"Data Source=DESKTOP-IUTHPCG\SQLEXPRESS;Initial Catalog=bds_pp_srct;Integrated Security=true;"; //sql connection
      
        //
        string partnumber_partpi;
        string srctr_partpi;
        string srctr_partpi_mitsu;
        string kanbanid_partpi;
        int pi_qty;
        string dockcode_pi;
        string location_pi;

        string rs_split_tag;
        string username;

        int rows_db = 0;
        int rows_local = 0;
        int rows_update = 0;

        string part_srct_kanban;
        string srct_code_local_kanban;
        string srct_code_local_kanban_mitsu;

        //m_kanban
        string srct_code_kanban;
        string customer_Code_kanban;
        string attension_point_kanban;
        string part_Number_kanban;
        string part_Name_kanban;
        string model_kanban;
        int package_kanban;
        string customer_kanban;
        string line_kanban;
        string store_Address_kanban;
        string part_Ilustration_kanban;
        string part_Ilustration_Path_kanban;
        string dock_Code_kanban;
        string location_kanban;
        int kb_running_kanban;

        //LogHanheld
        //int id_local;
        string part_local;
        string srct_local;
        string dock_local;
        int pack_local = 0;
        string error_local;
        string chk_local;
        //Boolean match_local;
        string user_local;
        DateTime date_local;
        string ekb_local;
        string kanban_local;

        string data_kb;
        //string temp_2;
        ArrayList al = new ArrayList();

        string digit_customercode;
        string digit_dockcode;
        string digit_plane;
        string digit_kanban_export;
        string digit_plane_export;
        int countCharacter = 0;
        string srct_code_local_kanban_export;
        public FrmHome(string data)
        {
            InitializeComponent();
            this.txtkb.Focus();
            username = data;
        }
        
        private void split_kanban()
        {
            data_kb = txtkb.Text;
            digit_customercode = data_kb.Substring(3,4);
            digit_dockcode = data_kb.Substring(1,2);
            digit_plane = data_kb.Substring(11, 2);
          
        }

        private void split_kanban_export()
        {
            data_kb = txtkb.Text;
            digit_kanban_export = data_kb.Substring(39, 4);
            digit_plane_export = data_kb.Substring(102, 2);
          //  MessageBox.Show("digit_kanban_export: " + digit_kanban_export);
        }


        private void split_pi()
        {
            string data_pi = txtpi.Text;
            string[] pi_split = data_pi.Split('|');
            partnumber_partpi = pi_split[0];
            srctr_partpi = pi_split[1];
            srctr_partpi_mitsu = pi_split[1];
            pi_qty = System.Convert.ToInt32(pi_split[3]);
            dockcode_pi = pi_split[2];
            location_pi = pi_split[4];
            kanbanid_partpi = pi_split[5];
            //int taglot = Int32.Parse(dev);
            //MessageBox.Show(partnumber_partpi.ToString());
        }

        private void split_tag()
        {
            string data_tag = txttag.Text;
            string[] rs_tag = data_tag.Split('P');
            rs_split_tag = rs_tag[0];
            //MessageBox.Show(rs_split_tag.ToString());
        }

        private bool check_dupicate(string text)//Check value Kanban Dupicate ?
        {
            if (al.Contains(text)) //if search al(arraylist) == Parameter(input)
            {
                return true;
            }
            else
            {
                return false;
            }
        }// end method check_dupicate(Parameter)

        // ========== Count Rows From DB ==========//
        private void count_rows_db_m_kanban()
        {
                WebReference.WebService_kanban mytagdata = new WebReference.WebService_kanban();
                DataTable dt = mytagdata.get_m_kanban();
                rows_db = dt.Rows.Count;
               // MessageBox.Show("rows DB :" + rows_db.ToString());
        }

        private void count_rows_local_kanban()
        {
            string connection = ("Data Source=deploy\\m_kanban_local.sdf");
            using (SqlCeConnection conn = new SqlCeConnection(connection))
            {
                //** Data Table **//
                SqlCeDataAdapter CountAdapter;
                DataTable ds = new DataTable();
                string count_rows = "SELECT * FROM m_kanban_db";
                CountAdapter = new SqlCeDataAdapter(count_rows, conn);
                CountAdapter.Fill(ds);
                conn.Close();
                rows_local = ds.Rows.Count;
               // MessageBox.Show("ROWS LOCAL: " + rows_local.ToString());
            }
        }

        private void insert_data_hanheld()//insert new data
        {
            SqlCeConnection connection = new SqlCeConnection("Data Source=deploy\\localdata.sdf");
            connection.Open();
            SqlCeCommand sCommand = new SqlCeCommand(@"INSERT INTO LogHendheld (Part_Number,SRCT_Code,Dock_Code,Package,Error_Code,Chk_Type,IsMatch,LogUser,LogDate,ekb_order_no,Kanban_ID) VALUES ('" + partnumber_partpi + "','" + srctr_partpi + "','" + dockcode_pi + "','" + pi_qty + "','" + error_local + "','"+digit_plane+"','','" + username + "','" + DateTime.Now + "','','" + kanbanid_partpi + "')", connection);
            sCommand.ExecuteNonQuery();
            //MessageBox.Show("Row inserted !! ");
            connection.Close();
        }

        private void insert_data_hanheld_mitsu()//insert new data
        {
            SqlCeConnection connection = new SqlCeConnection("Data Source=deploy\\localdata.sdf");
            connection.Open();
            SqlCeCommand sCommand = new SqlCeCommand(@"INSERT INTO LogHendheld (Part_Number,SRCT_Code,Dock_Code,Package,Error_Code,Chk_Type,IsMatch,LogUser,LogDate,ekb_order_no,Kanban_ID) VALUES ('" + partnumber_partpi + "','" + srctr_partpi + "','" + dockcode_pi + "','" + pi_qty + "','" + error_local + "','','','" + username + "','" + DateTime.Now + "','','" + kanbanid_partpi + "')", connection);
            sCommand.ExecuteNonQuery();
            //MessageBox.Show("Row inserted !! ");
            connection.Close();
        }


        private void delete_rows()//Delete same rows
        {
            SqlCeConnection connection = new SqlCeConnection("Data Source=deploy\\localdata.sdf");
            connection.Open();
            SqlCeCommand sCommand = new SqlCeCommand(@"DELETE FROM LogHendheld", connection);
            sCommand.ExecuteNonQuery();
            //MessageBox.Show("Row SDF Local Delete !! ");
            connection.Close();
        }

        private void delete_rows_mkanban()//Delete same rows
        {
            SqlCeConnection connection = new SqlCeConnection("Data Source=deploy\\m_kanban_local.sdf");
            connection.Open();
            SqlCeCommand sCommand = new SqlCeCommand(@"DELETE FROM m_kanban_db", connection);
            sCommand.ExecuteNonQuery();
            //MessageBox.Show("Row SDF Local Delete !! ");
            connection.Close();
        }

        
   private void update_data_local_to_db()
   {
       string connection = ("Data Source=deploy\\localdata.sdf");
       using (SqlCeConnection conn = new SqlCeConnection(connection))
       {
           SqlCeDataAdapter CountAdapter;
           DataTable ds = new DataTable(); //
           //string count_rows = "SELECT TOP (1) Part_Number, SRCT_Code, Dock_Code, Package, Error_Code, Chk_Type, LogUser, LogDate, ekb_order_no, Kanban_ID FROM LogHendheld ORDER BY id DESC";
           string count_rows = "SELECT Part_Number, SRCT_Code, Dock_Code, Package, Error_Code, Chk_Type, LogUser, LogDate, ekb_order_no, Kanban_ID FROM LogHendheld ";
           CountAdapter = new SqlCeDataAdapter(count_rows, conn);
           CountAdapter.Fill(ds);
           conn.Close();
           rows_update = ds.Rows.Count;
           //MessageBox.Show("rows_update : " + rows_update);
           if (ds.Rows.Count > 0)
           {
            //   MessageBox.Show("rows_update_in : " + rows_update);
               for (int i = 0; i < ds.Rows.Count; i++)
               {
                   part_local = (string)ds.Rows[i]["Part_Number"];
                   srct_local = (string)ds.Rows[i]["SRCT_Code"];
                   dock_local = (string)ds.Rows[i]["Dock_Code"];
                   pack_local = (int)ds.Rows[i]["Package"];
                   error_local = (string)ds.Rows[i]["Error_Code"];
                   chk_local = (string)ds.Rows[i]["Chk_Type"];
                   // match_local = (Boolean)ds.Rows[i]["IsMatch"];
                   user_local = (string)ds.Rows[i]["LogUser"];
                   date_local = (DateTime)ds.Rows[i]["LogDate"];
                   //pds_local = (string)ds.Rows[i]["PDS_number"];
                   ekb_local = (string)ds.Rows[i]["ekb_order_no"];
                   kanban_local = (string)ds.Rows[i]["Kanban_ID"];
                   update_db_from_local();
               }
           }
       }
   }
  
   private void update_db_from_local()//update_data_to_db();
   {
           WebReference.WebService_kanban mytagdata = new WebReference.WebService_kanban();
           mytagdata.insert_data_to_db_from_local(this.part_local, this.srct_local, this.dock_local, this.pack_local, this.error_local, this.chk_local, this.user_local, this.date_local, this.ekb_local, this.kanban_local);
          // rows_db = dt.Rows.Count;
   }
   
        // ============= KANBAN ==================//
        private void get_db_insert_kanban_local()
        {
            WebReference.WebService_kanban mytagdata = new WebReference.WebService_kanban();
            DataTable dt = mytagdata.get_m_kanban();
            rows_db = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    srct_code_kanban = (string)dt.Rows[i]["SRCT_Code"];
                    customer_Code_kanban = (string)dt.Rows[i]["Customer_Code"];
                    attension_point_kanban = (string)dt.Rows[i]["Attension_point"];
                    part_Number_kanban = (string)dt.Rows[i]["Part_Number"];
                    part_Name_kanban = (string)dt.Rows[i]["Part_Name"];
                    model_kanban = (string)dt.Rows[i]["Model"];
                    package_kanban = (int)dt.Rows[i]["Package"];
                    customer_kanban = (string)dt.Rows[i]["Customer"];
                    line_kanban = (string)dt.Rows[i]["Line"];
                    store_Address_kanban = (string)dt.Rows[i]["Store_Address"];
                    part_Ilustration_kanban = (string)dt.Rows[i]["Part_Ilustration"];
                    part_Ilustration_Path_kanban = (string)dt.Rows[i]["Part_Ilustration_Path"];
                    dock_Code_kanban = (string)dt.Rows[i]["Dock_Code"];
                    location_kanban = (string)dt.Rows[i]["Location"];
                    kb_running_kanban = (int)dt.Rows[i]["KB_running"];
                    insert_data_kanban_to_local();
                }
                MessageBox.Show("Kanban Update Sucessfuly");
            }
        }

        private void insert_data_kanban_to_local()//include get_db_insert_kanban_local();
        {
                //SqlCeConnection connection = new SqlCeConnection("Data Source=C:\\Users\\SRCT\\Desktop\\job\\smartdevice\\smart_device_winform\\smart_device_winform\\m_kanban_local.sdf");
                 SqlCeConnection connection = new SqlCeConnection("Data Source=deploy\\m_kanban_local.sdf");
                connection.Open(); //Error_Code LogDate
                SqlCeCommand sCommand = new SqlCeCommand(@"INSERT INTO m_kanban_db (SRCT_Code,Customer_Code,Attension_point,Part_Number,Part_Name,Model,Package,Customer,Line,Store_Address,Part_Ilustration,Part_Ilustration_Path,Dock_Code,Location,KB_running) VALUES ('" + srct_code_kanban + "','" + customer_Code_kanban + "','" + attension_point_kanban + "','" + part_Number_kanban + "','" + part_Name_kanban + "','" + model_kanban + "','" + package_kanban + "','" + customer_kanban + "','" + line_kanban + "','" + store_Address_kanban + "','" + part_Ilustration_kanban + "','" + part_Ilustration_Path_kanban + "','" + dock_Code_kanban + "','" + location_kanban + "','" + kb_running_kanban + "')", connection);
                sCommand.ExecuteNonQuery();
                connection.Close();  
        }
        // ============ END ===========//

        // ============ Service t_part_tag =============//
        private void get_srct_kanban_db()
        {
           
                WebReference.WebService_kanban mytagdata = new WebReference.WebService_kanban();
                DataTable dt = mytagdata.get_srct_kanban(this.digit_customercode, this.location_pi,this.digit_dockcode);
                if (dt.Rows.Count > 0)
                {
                    part_srct_kanban = (string)dt.Rows[0]["SRCT_Code"];
                    //MessageBox.Show("part_srct_kanban: " +part_srct_kanban);
                }  
        }//end method

        private void get_m_kanban_from_local()
        {
                string connection = ("Data Source=deploy\\m_kanban_local.sdf");
                using (SqlCeConnection conn = new SqlCeConnection(connection))
                {
                    //** Data Table **//
                    SqlCeDataAdapter CountAdapter;
                    DataTable ds = new DataTable();
                    string count_rows = "SELECT * FROM m_kanban_db WHERE Customer_Code='" + digit_customercode + "'";
                    CountAdapter = new SqlCeDataAdapter(count_rows, conn);
                    CountAdapter.Fill(ds);
                    conn.Close();
                    if (ds.Rows.Count > 0)
                    {
                        srct_code_local_kanban = (string)ds.Rows[0]["SRCT_Code"];
                        //MessageBox.Show("srct_code_local_kanban: " + srct_code_local_kanban);
                    }
                        
                }
        }//end method

        private void get_m_kanban_from_local_export()
        {
            string connection = ("Data Source=deploy\\m_kanban_local.sdf");
            using (SqlCeConnection conn = new SqlCeConnection(connection))
            {
                //** Data Table **//
                SqlCeDataAdapter CountAdapter;
                DataTable ds = new DataTable();
                string count_rows = "SELECT * FROM m_kanban_db WHERE Customer_Code='" + digit_kanban_export + "'";
                CountAdapter = new SqlCeDataAdapter(count_rows, conn);
                CountAdapter.Fill(ds);
                conn.Close();
                if (ds.Rows.Count > 0)
                {
                    srct_code_local_kanban_export = (string)ds.Rows[0]["SRCT_Code"];
                    //MessageBox.Show("srct_code_local_kanban: " + srct_code_local_kanban);
                }

            }
        }//end method

        private void get_m_kanban_from_local_mitsu()
        {
            string connection = ("Data Source=deploy\\m_kanban_local.sdf");
            using (SqlCeConnection conn = new SqlCeConnection(connection))
            {
                //** Data Table **//
                SqlCeDataAdapter CountAdapter;
                DataTable ds = new DataTable();
                string count_rows = "SELECT * FROM m_kanban_db WHERE Part_Number='" + data_kb + "'";
                CountAdapter = new SqlCeDataAdapter(count_rows, conn);
                CountAdapter.Fill(ds);
                conn.Close();
                if (ds.Rows.Count > 0)
                {
                    srct_code_local_kanban_mitsu = (string)ds.Rows[0]["SRCT_Code"];
                    //MessageBox.Show("srct_code_local_kanban: " + srct_code_local_kanban);
                }
            }
        }//end method
        
        private void clear_Click_1(object sender, EventArgs e)
        {
            Color Warn1Color = Color.FromArgb(255, 255, 255);//default colour
            this.BackColor = Warn1Color; 
            txtkb.Text = String.Empty;
            txtpi.Text = String.Empty;
            txttag.Text = String.Empty;
            error_local = String.Empty;
            this.txtkb.Focus();
        }//end method

        private void exit_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจที่จะออกจากโปรแกรมหรือไม่?", "หน้าต่างยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                    update_data_local_to_db();
                    delete_rows();
                   // delete_rows_mkanban();
                     Application.Exit();
            }
        }//end method

        private void txtkb_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    countCharacter = txtkb.Text.Length;  
                    if (countCharacter > 40)
                    {
                     //   MessageBox.Show("Export");
                        split_kanban_export();
                        txtpi.Focus();
                    }
                    else
                    {
                      //  MessageBox.Show("Domestic");
                        split_kanban();
                        txtpi.Focus();
                    }
                   
                    
                  //  txtpi.Focus();
                }
                catch { }
            }
        }//end method

        private void txtpi_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)13)
            {
                try
                {
                    split_pi();
                    get_m_kanban_from_local_export();
                    get_m_kanban_from_local();
                    get_m_kanban_from_local_mitsu();
                    if (srctr_partpi == srct_code_local_kanban)
                    {
                        txttag.Focus();
                    }
                    else if (srctr_partpi_mitsu == srct_code_local_kanban_mitsu)
                        {
                          
                            error_local = "Complete"; 
                            //insert_data_hanheld();
                            insert_data_hanheld_mitsu();
                            error_local = String.Empty;
                            txtkb.Text = String.Empty;
                            txtpi.Text = String.Empty;
                            txtkb.Focus();
                        }
                    else if (srctr_partpi == srct_code_local_kanban_export)
                    {
                        txttag.Focus();
                    }
                    else
                    {

                        Color Warn1Color1 = Color.FromArgb(255, 0, 0);//default colour
                        this.BackColor = Warn1Color1; ;
                        error_local = data_kb + "|" + partnumber_partpi + "|" + srctr_partpi + "| Mitsu";
                        insert_data_hanheld();
                        error_local = String.Empty;
                    }
               
                }//end try
                catch { }
            }// end if == enter
        }//end method

        private void txttag_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    split_tag();
                   // get_m_kanban_from_local_export();
                    int countlength = countCharacter;
                    if (countCharacter >= 40)
                    {
                        if (srct_code_local_kanban_export == srctr_partpi && srctr_partpi == rs_split_tag)
                        {
                            error_local = "Complete";
                            insert_data_hanheld();
                            txtkb.Focus();
                            error_local = String.Empty;
                            txtkb.Text = String.Empty;
                            txtpi.Text = String.Empty;
                            txttag.Text = String.Empty;
                        }
                        else
                        {
                            Color Warn1Color = Color.FromArgb(255, 0, 0);//default colour
                            this.BackColor = Warn1Color; ;
                            error_local = data_kb + "|" + partnumber_partpi + "|" + srctr_partpi+"|"+"Export";
                            insert_data_hanheld();
                            txtkb.Focus();
                            error_local = String.Empty;
                            txtkb.Text = String.Empty;
                            txtpi.Text = String.Empty;
                            txttag.Text = String.Empty;
                        }
                    }
                    if (countCharacter < 40)
                    {
                        if (srct_code_local_kanban == srctr_partpi && srctr_partpi == rs_split_tag)
                        {
                            error_local = "Complete";
                            insert_data_hanheld();
                            txtkb.Focus();
                            error_local = String.Empty;
                            txtkb.Text = String.Empty;
                            txtpi.Text = String.Empty;
                            txttag.Text = String.Empty;
                        }
                        else
                        {
                            Color Warn1Color = Color.FromArgb(255, 0, 0);//default colour
                            this.BackColor = Warn1Color; ;
                            error_local = data_kb + "|" + partnumber_partpi + "|" + srctr_partpi+"|"+"Domestic";
                            insert_data_hanheld();
                            txtkb.Focus();
                            error_local = String.Empty;
                            txtkb.Text = String.Empty;
                            txtpi.Text = String.Empty;
                            txttag.Text = String.Empty;
                        }
                    }
                }
                catch { }
            }
        }//end method

        private void FrmHome_Load_1(object sender, EventArgs e)
        {
            //MessageBox.Show("Waitting For Update Device");
            count_rows_local_kanban();
            count_rows_db_m_kanban();
            if (rows_db > rows_local)
            {
                get_db_insert_kanban_local();
            }
            else if (rows_db == rows_local)
            {
                //get_db_update_kanban_local();
            }
        }//end method
    }
}
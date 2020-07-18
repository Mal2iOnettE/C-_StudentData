using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DBStudent
{
    public partial class Form1 : Form

    { 
        public Form1()
        {
            InitializeComponent();
           /* OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sql, conn);
            // creating a DataSet object
            DataSet ds = new DataSet();
            // filling table Order  
            dataAdapter.Fill(ds, "DataSudent");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;*/
        }
    

        OleDbDataAdapter DA = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        OleDbCommandBuilder comb = new OleDbCommandBuilder();
        OleDbCommand comm = new OleDbCommand();

        
        //String sql = "select * from DataSudent";
        OleDbConnection conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\\Users\\Attachai khum-ai\\Desktop\\WORK CODE\\C#\\DBStudent\\DBStudent\\DBStudents.mdb");
       
         private void Form1_Load(object sender, EventArgs e)
         {
            String sql = "select * from DataSudent";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sql, conn);

            //DataSet ds = new DataSet();
            // filling table Order  
            dataAdapter.Fill(ds, "DataSudent");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            
            this.majorTableAdapter.Fill(this.dBStudentsDataSet.Major);
            try
            {  
                     conn.Open();
                    // ShowData();


                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("Error: Failed to retrieve the required data from the Database.\n{0}", ex.Message);
                     return;
                 }
             }  
        

         private void button1_Click(object sender, EventArgs e)
         {

             string sql, s;

             sql = "Select MID FROM Major WHERE MNAME = '" + comboBox1.Text + "'";
             comm = new OleDbCommand(sql, conn);
             s = comm.ExecuteScalar().ToString();

             sql = "insert into DataSudent(SID, SNAME, MID, GPA)";
             sql += "VALUES(";
             sql += stuID.Text + ",'";
             sql += stuName.Text + "','";
             sql += s + "',";
             sql += Gpa.Text + ")";
             comm = new OleDbCommand(sql, conn);

             if (comm.ExecuteNonQuery() > 0)
             {
                 MessageBox.Show("New Book has been saved", "", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
             }
             sql = "Select * from DataSudent";
             DA = new OleDbDataAdapter(sql, conn);
             ds.Tables.Clear();

             DA.Fill(ds, "DataSudent");
             dataGridView1.DataSource = ds.Tables[0].DefaultView;
         }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "Select MAX(SID)+1 from DataSudent";
            comm = new OleDbCommand(sql, conn);
            stuID.Text = comm.ExecuteScalar().ToString();
            stuName.Text = "";
            comboBox1.Text = "";
            Gpa.Text = "";
        }
    }
    
}

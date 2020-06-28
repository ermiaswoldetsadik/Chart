using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Chart
{
    public partial class Chart : Form
    {
        public Chart()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Chart_Load(object sender, EventArgs e)
        {
            FromComboBox.Text = "2012";
            ToComboBox.Text = "2015";

            SalesByYearChart.DataSource = GetData();
            SalesByYearChart.Series["Series1"].Points.Clear();
            SalesByYearChart.Series["Series1"].XValueMember = "Year";
            SalesByYearChart.Series["Series1"].YValueMembers = "Total";
        }



        // dataset,object,array,collection,generic,datatable
        private DataTable GetData()
        {
            DataTable dtChartData = new DataTable();

            using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("Orders1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    cmd.Parameters.AddWithValue("@FromYear", FromComboBox.Text);
                    cmd.Parameters.AddWithValue("@ToYear", ToComboBox.Text);

                   //SqlDataReader reader = cmd.ExecuteReader();

                    //dtChartData.Load(reader);
                }
            }
            return dtChartData;
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            SalesByYearChart.DataSource = GetData();
            SalesByYearChart.Series["Series1"].Points.Clear();
            SalesByYearChart.Series["Series1"].XValueMember = "Year";
            SalesByYearChart.Series["Series1"].YValueMembers = "Total";
        }
    }
}

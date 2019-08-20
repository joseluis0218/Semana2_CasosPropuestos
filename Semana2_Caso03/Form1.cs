using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace Semana2_Caso03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

        private void ListaAños()
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaAnios", cn))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable df = new DataTable();
                    Da.Fill(df);
                    cmbAño.DataSource = df;
                    cmbAño.DisplayMember = "Anios";
                    cmbAño.ValueMember = "Anios";
                }
            }
        }
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaAños();
        }

        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaMeses", cn))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@anio", cmbAño.SelectedValue);

                    DataTable df = new DataTable();
                    Da.Fill(df);
                    cmbMes.DataSource = df;
                    cmbMes.DisplayMember = "Mes";
                    cmbMes.ValueMember = "NumeroMes";
                }
            }
        }

        private void ComboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaPedidos_FechaPedido", cn))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@anio", cmbAño.SelectedValue);
                    Da.SelectCommand.Parameters.AddWithValue("@mes", cmbMes.SelectedValue);

                    using (DataSet df = new DataSet())
                    {
                        Da.Fill(df, "Pedidos");
                        dgPedidos.DataSource = df.Tables["Pedidos"];
                        txtPedidos.Text = df.Tables["Pedidos"].Rows.Count.ToString();
                    }
                }
            }
        }
    }
}

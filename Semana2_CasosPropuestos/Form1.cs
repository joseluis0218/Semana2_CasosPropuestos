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
namespace Semana2_Caso01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

        private void ListaEmpleados()
        {
            using (SqlCommand cmd = new SqlCommand("Usp_Lista_Empleados", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable df = new DataTable();
                    
                     da.Fill(df);
                     cmbEmpleados.DataSource = df;
                     cmbEmpleados.DisplayMember = "Empleado";
                     cmbEmpleados.ValueMember = "Empleado";
                    
                }
            }
        }
        private void CmbEmpleados_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaPedidosPorEmpelado", cn))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@empleado", cmbEmpleados.SelectedValue);

                    using (DataSet df = new DataSet())
                    {
                        Da.Fill(df, "Pedidos");
                        dgPedidos.DataSource = df.Tables["Pedidos"];
                        txtNumero.Text = df.Tables["Pedidos"].Rows.Count.ToString();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaEmpleados();
        }
    }
}

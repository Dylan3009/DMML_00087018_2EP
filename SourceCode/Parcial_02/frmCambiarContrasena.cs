using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class frmCambiarContrasena : Form
    {
        public frmCambiarContrasena()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCambiarContrasena_Load(object sender, EventArgs e)
        {
            string query2 = "select username from appuser";
            var users = ConnectionDB.ExecuteQuery(query2);
            var usersCombo = new List<string>();

            foreach (DataRow dr in users.Rows)
            {
                usersCombo.Add(dr[0].ToString());
            }

            comboBox1.DataSource = usersCombo;
            
        }

        private void btnCambiarContra_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"SELECT password from appuser WHERE username ='{comboBox1.Text}'";
                var dt = ConnectionDB.ExecuteQuery(query);
                var dr = dt.Rows[0];
                var iduser = Convert.ToString(dr[0].ToString());

                if (txtActual.Text.Equals(iduser) && txtNueva.Text.Equals(txtRepetir.Text))
                {
                    string actualizar =
                        $"UPDATE appuser SET password = '{txtNueva.Text}' WHERE password = '{txtActual.Text}'";
                    ConnectionDB.ExecuteNonQuery(actualizar);
                    MessageBox.Show("Se ha actualizado la contra");
                }
                else
                {
                    MessageBox.Show("Datos incorrectos");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un Error");
            }
        }
    }
}

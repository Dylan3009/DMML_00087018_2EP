using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class frmInicioDeSesion : Form
    {
        TipoUsuario u = new TipoUsuario();
        public frmInicioDeSesion()
        {
            InitializeComponent();
        }

        private void btnInicioSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"SELECT password FROM appuser WHERE username ='{cboUsuarios.Text}'";
                var dt = ConnectionDB.ExecuteQuery(query);
                var dr = dt.Rows[0];
                var contrasena = Convert.ToString(dr[0].ToString());

                if (txtPassword.Text.Equals(contrasena))
                {
                    u.admin =  cboUsuarios.Text;
                    frmPrincipal ventana = new frmPrincipal(u);
                    ventana.Show();
                    //this.Hide();
                }
                else
                {
                    MessageBox.Show("Contraseña Incorrecta");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
            
        }

        private void frmInicioDeSesion_Load(object sender, EventArgs e)
        {
            string query = "select username from appuser";
            var users = ConnectionDB.ExecuteQuery(query);
            var usersCombo = new List<string>();

            foreach (DataRow dr in users.Rows)
            {
                usersCombo.Add(dr[0].ToString());
            }

            cboUsuarios.DataSource = usersCombo;
        }
        
        private void btnCambiarContra_Click(object sender, EventArgs e)
        {
            frmCambiarContrasena unaVentana = new frmCambiarContrasena();
            unaVentana.ShowDialog();
        }
    }
}
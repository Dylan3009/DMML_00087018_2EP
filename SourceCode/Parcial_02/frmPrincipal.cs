using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class frmPrincipal : Form
    {
        private TipoUsuario type;

        public frmPrincipal(TipoUsuario pUsuario)
        {
            InitializeComponent();
            type = pUsuario;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bienvenido/a");
            try
            {
                string query = $"SELECT usertype FROM appuser WHERE username ='{type.admin}'";
                var dt = ConnectionDB.ExecuteQuery(query);
                var dr = dt.Rows[0];
                var tipodeusuario = Convert.ToString(dr[0].ToString());

                if (tipodeusuario.Equals("admin"))
                {
                    tabContenedor.TabPages[1].Parent = null;
                    tabContenedor.TabPages[4].Parent = null;
                }
                else
                {
                    tabContenedor.TabPages[0].Parent = null;
                    tabContenedor.TabPages[1].Parent = null;
                    tabContenedor.TabPages[1].Parent = null;
                    tabContenedor.TabPages[1].Parent = null;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error");
            }

            DatosAppUser();
            DatosAddress();
            DatosBusiness();
            DatosProduct();
            DatosOrden();
        }

        public void DatosAppUser()
        {
            string query2 = "select username from appuser";
            var users = ConnectionDB.ExecuteQuery(query2);
            var usersCombo = new List<string>();

            foreach (DataRow dr in users.Rows)
            {
                usersCombo.Add(dr[0].ToString());
            }

            comboBox3.DataSource = usersCombo;
        }
        
        public void DatosOrden()
        {
            string query2 = $"SELECT ord.id_order FROM apporder ord, address ress, appuser us, business bus, " +
                            "product pro WHERE pro.id_product = ord.id_product " +
                            "AND ress.id_address = ord.id_address " +
                            "AND us.id_user = ress.id_user " +
                            "AND bus.id_business = pro.id_business " +
                            $"AND us.username = '{type.admin}'";
            var users = ConnectionDB.ExecuteQuery(query2);
            var usersCombo = new List<string>();

            foreach (DataRow dr in users.Rows)
            {
                usersCombo.Add(dr[0].ToString());
            }

            comboBox7.DataSource = usersCombo;
        }
        
        public void DatosProduct()
        {
            string query2 = "select product.name from product";
            var users = ConnectionDB.ExecuteQuery(query2);
            var usersCombo = new List<string>();

            foreach (DataRow dr in users.Rows)
            {
                usersCombo.Add(dr[0].ToString());
            }

            comboBox5.DataSource = usersCombo;
            comboBox6.DataSource = usersCombo;
        }

        public void DatosBusiness()
        {
            string query2 = "select business.name from business";
            var users = ConnectionDB.ExecuteQuery(query2);
            var usersCombo = new List<string>();

            foreach (DataRow dr in users.Rows)
            {
                usersCombo.Add(dr[0].ToString());
            }

            comboBox4.DataSource = usersCombo;
            comboBox1.DataSource = usersCombo;
        }
        
        public void DatosAddress()
        {
            string query2 = "select address from address";
            var users = ConnectionDB.ExecuteQuery(query2);
            var usersCombo = new List<string>();

            foreach (DataRow dr in users.Rows)
            {
                usersCombo.Add(dr[0].ToString());
            }

            comboBox2.DataSource = usersCombo;
            comboBox8.DataSource = usersCombo;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") ||
                textBox2.Text.Equals("") ||
                textBox4.Text.Equals("") ||
                radioButton2.Checked.Equals(false) &&
                radioButton3.Checked.Equals(false))
            {
                MessageBox.Show("Error hay campos vacios");
            }
            else
            {
                try
                {
                    string au;
                    if (radioButton2.Checked.Equals(true))
                    {
                        au = "usuario";
                    }
                    else
                    {
                        au = "admin";
                    }

                    ConnectionDB.ExecuteNonQuery($"INSERT INTO appuser(fullname, username," +
                                                 $" password, usertype) VALUES(" +
                                                 $"'{textBox4.Text}'," +
                                                 $"'{textBox1.Text}'," +
                                                 $"'{textBox2.Text}'," +
                                                 $"'{au}');");
                    MessageBox.Show("Usuario Registrado");
                    DatosAppUser();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Seguro que quieres eliminar al usuario?",
                "Parcial_02", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConnectionDB.ExecuteNonQuery($"DELETE FROM appuser WHERE username = '{comboBox3.Text}'");
                MessageBox.Show("Usuario Eliminado");
                DatosAppUser();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = ConnectionDB.ExecuteQuery("SELECT * FROM appuser");

                dataGridView1.DataSource = dt;
                MessageBox.Show("Datos obtenidos correctamente");
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Equals(""))
            {
                MessageBox.Show("Error no se puede dejar vacio");
            }
            else
            {
                try
                {
                    string query = $"SELECT id_user from appuser WHERE username ='{type.admin}'";
                    var dt = ConnectionDB.ExecuteQuery(query);
                    var dr = dt.Rows[0];
                    var iduser = Convert.ToInt32(dr[0].ToString());
                    
                    ConnectionDB.ExecuteNonQuery($"INSERT INTO address(id_user, address) VALUES(" +
                                                 $"{iduser}," +
                                                 $"'{textBox3.Text}')");
                    MessageBox.Show("Dirección Registrada");
                    DatosAddress();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quieres eliminar la dirección?",
                "Parcial_02", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConnectionDB.ExecuteNonQuery($"DELETE FROM address WHERE address = '{comboBox2.Text}'");
                MessageBox.Show("Dirección Eliminada");
                DatosAddress();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Equals("")||
                textBox6.Text.Equals(""))
            {
                MessageBox.Show("Error no se puede dejar campos vacios");
            }
            else
            {
                try
                {
                    ConnectionDB.ExecuteNonQuery($"INSERT INTO business(name, description) VALUES(" +
                                                 $"'{textBox5.Text}'," +
                                                 $"'{textBox6.Text}')");
                    MessageBox.Show("Negocio Registrado");
                    DatosBusiness();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quieres eliminar el negocio?",
                "Parcial_02", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConnectionDB.ExecuteNonQuery($"DELETE FROM business WHERE name = '{comboBox4.Text}'");
                MessageBox.Show("Negocio Eliminado");
                DatosBusiness();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox7.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }
            else
            {
                try
                {
                    string query = $"SELECT id_business from business WHERE name ='{comboBox1.Text}'";
                    var dt = ConnectionDB.ExecuteQuery(query);
                    var dr = dt.Rows[0];
                    var iduser = Convert.ToInt32(dr[0].ToString());
                    
                    ConnectionDB.ExecuteNonQuery($"INSERT INTO product(id_business, name) VALUES(" +
                                                 $"{iduser}," +
                                                 $"'{textBox7.Text}')");
                    MessageBox.Show("Producto Registrado");
                    DatosProduct();
                    DatosBusiness();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quieres eliminar el producto?",
                "Parcial_02", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConnectionDB.ExecuteNonQuery($"DELETE FROM product WHERE name = '{comboBox5.Text}'");
                MessageBox.Show("Producto Eliminado");
                DatosProduct();
                DatosBusiness();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                var dgv = ConnectionDB.ExecuteQuery("SELECT ord.id_order, ress.address, us.fullname, " +
                                                    "bus.name,pro.name, ord.createdate " + 
                                                    "FROM apporder ord, address ress, appuser us, business bus, " +
                                                    "product pro WHERE pro.id_product = ord.id_product " +
                                                    "AND ress.id_address = ord.id_address " +
                                                    "AND us.id_user = ress.id_user " +
                                                    "AND bus.id_business = pro.id_business");

                dataGridView2.DataSource = dgv;
                MessageBox.Show("Datos obtenidos correctamente");
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"SELECT id_product FROM product WHERE name ='{comboBox6.Text}'";
                var dt = ConnectionDB.ExecuteQuery(query);
                var dr = dt.Rows[0];
                var iduser = Convert.ToInt32(dr[0].ToString());
                
                string query2 = $"SELECT id_address FROM address WHERE address ='{comboBox8.Text}'";
                var dt2 = ConnectionDB.ExecuteQuery(query2);
                var dr2 = dt2.Rows[0];
                var idaddress = Convert.ToInt32(dr2[0].ToString());
                    
                ConnectionDB.ExecuteNonQuery($"INSERT INTO apporder(createdate, id_product, id_address) VALUES(" +
                                             $"'{type.dia}',"+
                                             $"{iduser}," +
                                             $"{idaddress})");
                MessageBox.Show("Orden Registrada");
                DatosProduct();
                DatosOrden();
                DatosAddress();
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = ConnectionDB.ExecuteQuery("SELECT ord.id_order, ress.address, us.fullname, " +
                                                   "bus.name,pro.name, ord.createdate " + 
                                                   "FROM apporder ord, address ress, appuser us, business bus, " +
                                                   "product pro WHERE pro.id_product = ord.id_product " +
                                                   "AND ress.id_address = ord.id_address " +
                                                   "AND us.id_user = ress.id_user " +
                                                   "AND bus.id_business = pro.id_business " +
                                                   $"AND us.username = '{type.admin}'");

                dataGridView3.DataSource = dt;
                MessageBox.Show("Datos obtenidos correctamente");
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quieres eliminar la orden?",
                "Parcial_02", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConnectionDB.ExecuteNonQuery($"DELETE FROM apporder WHERE id_order = '{comboBox7.Text}'");
                MessageBox.Show("Orden Eliminada");
                DatosProduct();
                DatosOrden();
                DatosAddress();
            }
        }
    }
}
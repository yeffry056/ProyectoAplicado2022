using ProyectoFinalAplicada1.BLL;
using ProyectoFinalAplicada1.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoAplicadoColegas.UI.Registro
{
    /// <summary>
    /// Interaction logic for rBuscarUsuario.xaml
    /// </summary>
    public partial class rBuscarUsuario : Window
    {
        private Usuarios usuario = new Usuarios();
        public rBuscarUsuario()
        {
            InitializeComponent();
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = UsuariosBLL.GetListado();
        }

        private void obtener(object sender, SelectionChangedEventArgs e)
        {
            usuario.UsuarioId = ((Usuarios)DatosDataGrid.SelectedItem).UsuarioId;
            this.Close();
        }

        public int Extraer()
        {
            return usuario.UsuarioId;
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {
            var listado = new List<Usuarios>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: //ProductoId
                        listado = UsuariosBLL.GetList(p => p.UsuarioId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;

                    case 1: //Nombres                       
                        listado = UsuariosBLL.GetList(p => p.Nombres.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;

                    case 2: //Apellidos                       
                        listado = UsuariosBLL.GetList(p => p.Apellidos.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;

                    case 3: //Nombre Usuario                       
                        listado = UsuariosBLL.GetList(p => p.NombreUsuario.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;
                }
            }
            else
            {
                listado = UsuariosBLL.GetList(c => true);
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}

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
    /// Interaction logic for rBuscarVenta.xaml
    /// </summary>
    public partial class rBuscarVenta : Window
    {
        private Ventas venta = new Ventas();
        public rBuscarVenta()
        {
            InitializeComponent();
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = VentasBLL.GetListado();
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {
            var listado = new List<Ventas>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: //ProductoId
                        listado = VentasBLL.GetList(p => p.VentaId == Utilidades.ToInt(CriterioTextBox.Text));
                        DesdeDataPicker.SelectedDate = null;
                        HastaDatePicker.SelectedDate = null;
                        break;

                   /* case 1: //Nombres                       
                        listado = VentasBLL.GetList(p => p.Nombres.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;

                    case 2: //Apellidos                       
                        listado = VentasBLL.GetList(p => p.Apellidos.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;

                    case 3: //Nombre Usuario                       
                        listado = VentasBLL.GetList(p => p.NombreUsuario.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        break;*/
                }
            }
            else
            {
                listado = VentasBLL.GetList(c => true);
            }
            if (listado == null)
            {
                if (DesdeDataPicker.SelectedDate != null)
                    listado = VentasBLL.GetList(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate);

                if (HastaDatePicker.SelectedDate != null)
                    listado = VentasBLL.GetList(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
        private void enter(object sender, MouseEventArgs e)
        {
            CriterioTextBox.Text = null;
            FiltroComboBox.SelectedItem = null;
        }

        private void limpiar(object sender, MouseEventArgs e)
        {
            CriterioTextBox.Text = null;
            FiltroComboBox.SelectedItem = null;
        }

        private void obtener(object sender, SelectionChangedEventArgs e)
        {
            venta.VentaId = ((Ventas)DatosDataGrid.SelectedItem).VentaId;
            this.Close();
        }
        public int Extraer()
        {
            return venta.VentaId;
        }
    }
}

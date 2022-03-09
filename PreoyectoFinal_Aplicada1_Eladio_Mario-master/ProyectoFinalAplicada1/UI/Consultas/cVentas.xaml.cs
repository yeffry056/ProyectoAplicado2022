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

namespace ProyectoFinalAplicada1.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cVentas.xaml
    /// </summary>
    public partial class cVentas : Window
    {
        public cVentas()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Ventas>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = VentasBLL.GetList(e => e.VentaId == Utilidades.ToInt(CriterioTextBox.Text));
                        DesdeDataPicker.SelectedDate = null;
                        HastaDatePicker.SelectedDate = null;
                        break;

                    case 1:
                        /*listado = VentasBLL.GetList(e => e.descripcion.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        DesdeDataPicker.SelectedDate = null;
                        HastaDatePicker.SelectedDate = null;*/
                        break;
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


            // listado = UsuariosBLL.GetList();
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}

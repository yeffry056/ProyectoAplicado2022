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
                    case 0: //ProductoId
                        listado = VentasBLL.GetList(p => p.VentaId == Utilidades.ToInt(CriterioTextBox.Text));
                        DesdeDataPicker.SelectedDate = null;
                        HastaDatePicker.SelectedDate = null;

                        if (listado == null)
                        {
                            MessageBox.Show("no fue encontrado ", "fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        break;

                   /* case 1: //Descripcion                       
                        listado = VentasBLL.GetList(p => p..ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        DesdeDataPicker.SelectedDate = null;
                        HastaDatePicker.SelectedDate = null;
                        break;*/

                        /*case 2: //Categoria                       
                                listado = ProductosBLL.GetList(p => p.CategoriaId.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                            break;*/
                }
            }
            else if (DesdeDataPicker.SelectedDate == null || HastaDatePicker.SelectedDate == null)
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
            // var x = ((Productos)DatosDataGrid.ItemsSource).ProductoId;
            /*if (x == 0)
            {
                MessageBox.Show("no fue encontrado ", "fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }*/
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

        private void verificar(List<Productos> lista)
        {
            if (lista == null)
            {
                MessageBox.Show("no fue encontrado ", "fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}

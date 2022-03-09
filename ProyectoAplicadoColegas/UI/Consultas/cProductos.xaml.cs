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
    /// Interaction logic for cProductos.xaml
    /// </summary>
    public partial class cProductos : Window
    {
        public cProductos()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            List<Productos> listado = new List<Productos>();
            //Productos productos = new Productos();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: //ProductoId
                        listado = ProductosBLL.GetList(p => p.ProductoId == Utilidades.ToInt(CriterioTextBox.Text));
                        DesdeDataPicker.SelectedDate = null;
                        HastaDatePicker.SelectedDate = null;
                        
                        if(listado == null)
                        {
                            MessageBox.Show("no fue encontrado ", "fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        break;

                    case 1: //Descripcion                       
                        listado = ProductosBLL.GetList(p => p.Descripcion.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        DesdeDataPicker.SelectedDate = null;
                        HastaDatePicker.SelectedDate = null;
                        break;

                    /*case 2: //Categoria                       
                            listado = ProductosBLL.GetList(p => p.CategoriaId.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;*/
                }
            }
            else if(DesdeDataPicker.SelectedDate == null || HastaDatePicker.SelectedDate == null){
                listado = ProductosBLL.GetList(c => true);
            }else
            {
                listado = null;
            }

            if (listado == null)
            {
                if (DesdeDataPicker.SelectedDate != null)
                    listado = ProductosBLL.GetList(c => c.FechaEntrada.Date >= DesdeDataPicker.SelectedDate && c.FechaEntrada <= HastaDatePicker.SelectedDate);
                    /*DatosDataGrid.ItemsSource = null;
                    DatosDataGrid.ItemsSource = listado;
                    return;*/
                
                    
                   
                    //productos = ((Productos)ProductosBLL.GetList(c => c.FechaEntrada.Date >= DesdeDataPicker.SelectedDate);
                
                    

                if (HastaDatePicker.SelectedDate != null)
                    listado = ProductosBLL.GetList(c => c.FechaEntrada.Date <= HastaDatePicker.SelectedDate && c.FechaEntrada.Date >= DesdeDataPicker.SelectedDate);
                
                    
               
               
            }

            if (listado.Count == 0)
            {
                MessageBox.Show("no fue encontrado ", "fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                DatosDataGrid.ItemsSource = null;
                return;
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

using ProyectoFinalAplicada1.BLL;
using ProyectoFinalAplicada1.Entidades;
using ProyectoFinalAplicada1.UI.Registro;
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
    /// Interaction logic for rBuscarCliente.xaml
    /// </summary>
    public partial class rBuscarCliente : Window
    {
        public rBuscarCliente()
        {
            InitializeComponent();
            
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = ClientesBLL.GetListado();
            
        }
      
        Clientes s = new Clientes();
     
        private void sele(object sender, SelectionChangedEventArgs e)
        {
            s = new Clientes();
            s.ClienteId = ((Clientes)DatosDataGrid.SelectedItem).ClienteId;
            this.Close();

        }
        public int retornar()
        {
            return s.ClienteId;
        }
       
    }
}

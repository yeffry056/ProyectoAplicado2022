using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoFinalAplicada1.Entidades
{
    public class VentasDetalles
    {
        [Key]
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public double Costo { get; set; }

        
        
        [ForeignKey("ProductoId")]
        public Productos productos { get; set; }
    }
}
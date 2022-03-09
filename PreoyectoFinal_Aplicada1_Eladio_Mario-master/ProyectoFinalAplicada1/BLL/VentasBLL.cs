using Microsoft.EntityFrameworkCore;
using ProyectoFinalAplicada1.DAL;
using ProyectoFinalAplicada1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProyectoFinalAplicada1.BLL
{
    public class VentasBLL
    {

        public static bool Guardar(Ventas ventas)
        {
            if (!Existe(ventas.VentaId))//si no existe insertamos
                return Insertar(ventas);
            else
                return Modificar(ventas);
        }
        private static bool Insertar(Ventas ventas)
        {
            bool paso = false, validar = true;
            Contexto contexto = new Contexto();

            try
            {
                //Agregar la entidad que se desea insertar al contexto
                contexto.Ventas.Add(ventas);

                foreach (var detalle in ventas.VentaDetalle)
                {
                    contexto.Entry(detalle.productos).State = EntityState.Modified;
                    
                }
                if (validar)
                {
                    if (contexto.Ventas.Add(ventas) != null)
                        paso = contexto.SaveChanges() > 0;
                }


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        private static bool Modificar(Ventas ventas)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var VentaAnterior = contexto.Ventas
                     .Where(x => x.VentaId == ventas.VentaId)
                     .Include(x => x.VentaDetalle)
                     .AsNoTracking()
                     .SingleOrDefault();

                Productos productos;
                // foreach (var item in ventas.Detalle)
                // {



                //busca la entidad en la base de datos y la elimina
                foreach (var detalle in VentaAnterior.VentaDetalle)
                {
                    productos = contexto.Productos.Find(detalle.ProductoId);
                   // productos.Inventario += detalle.CantidadArticulo;

                  //  ventas.Total -= detalle.Precio * detalle.CantidadArticulo;

                }
                //  }

                contexto.Database.ExecuteSqlRaw($"Delete FROM VentasDetalles Where VentaId={ventas.VentaId}");


                foreach (var item in ventas.VentaDetalle)
                {




                    /*ventas.Total += item.Precio * item.CantidadArticulo;

                    articulo = contexto.Articulos.Find(item.ArticuloId);
                    articulo.Inventario -= item.CantidadArticulo;

                    contexto.Entry(articulo).State = EntityState.Modified;*/
                    contexto.Entry(item).State = EntityState.Added;





                }

                //marcar la entidad como modificada para que el contexto sepa como proceder
                contexto.Entry(ventas).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Ventas.Any(e => e.VentaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
        public static Ventas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Ventas ventas = new Ventas();

            try
            {
                ventas = contexto.Ventas.Include(x => x.VentaDetalle)
                    .Where(p => p.VentaId == id)
                    .Include(x => x.VentaDetalle)
                    .ThenInclude(x => x.productos)
                    .SingleOrDefault();

              /*  ventas = contexto.Ventas.Include(x => x.Detalle)
                    .Where(x => x.VentaId == id)
                     .Include(x => x.Detalle)
                    .ThenInclude(x => x.Articulo)
                    .Include(x => x.Detalle)
                    .ThenInclude(x => x.Empleado)
                    .SingleOrDefault();*/

            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ventas;
        }
        /*public static bool Modificar(Ventas ventas)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"DELETE FROM VentasDetalles Where VentaId = {ventas.VentaId}");
                foreach (var anterior in ventas.VentaDetalle)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }

                contexto.Entry(ventas).State = EntityState.Modified;
                paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;

        }*/

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var eliminar = contexto.Ventas.Find(id);
                contexto.Entry(eliminar).State = EntityState.Deleted;

                paso = (contexto.SaveChanges() > 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        //
        public static List<Ventas> GetList(Expression<Func<Ventas, bool>> ventas)
        {
            List<Ventas> lista = new List<Ventas>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Ventas.Where(ventas).ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

    }
}

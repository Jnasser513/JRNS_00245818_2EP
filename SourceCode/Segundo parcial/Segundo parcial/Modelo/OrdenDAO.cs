using System;
using System.Collections.Generic;
using System.Data;

namespace Segundo_parcial.Modelo
{
    public class OrdenDAO
    {
        public static List<Orden> GetLista()
        {
            string sql = "select * from APPORDER";
            var dt = ConnectionDB.realizarConsulta(sql);
            List<Orden> lista = new List<Orden>();

            foreach (DataRow fila in dt.Rows)
            {
                Orden orden = new Orden();
                orden.idOrder = Convert.ToInt32(fila[0].ToString());
                orden.createOrder = Convert.ToDateTime(fila[1].ToString());
                orden.idProduct = Convert.ToInt32(fila[2].ToString());
                orden.idAdress = Convert.ToInt32(fila[3].ToString());
                lista.Add(orden);
            }

            return lista;
        }
        
        public static List<Orden> getOListP(int pidUser)
        {
            string sql = $"SELECT ao.idOrder, ao.createDate, pr.idProduct, ad.idAddress "+
            "FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au "+
                "WHERE ao.idProduct = pr.idProduct "+
                "AND ao.idAddress = ad.idAddress "+
                "AND ad.idUser = au.idUser "+
                $"AND au.idUser = {pidUser};";
                    
            var dt = ConnectionDB.realizarConsulta(sql);
            List<Orden> lista = new List<Orden>();

            foreach (DataRow fila in dt.Rows)
            {
                Orden orden = new Orden();
                orden.idOrder = Convert.ToInt32(fila[0].ToString());
                orden.createOrder = Convert.ToDateTime(fila[1]);
                orden.idProduct = Convert.ToInt32(fila[2].ToString());
                orden.idAdress = Convert.ToInt32(fila[3].ToString());
                lista.Add(orden);
            }

            return lista;
        }
        
        public static void crearOrden(DateTime ptime, int pidProduct, int pidAdress)
        {
            string sql = String.Format(
                "insert into apporder(createDate, idProduct, idAddress) " +
                "values('{0}', {1}, {2});",
                ptime, pidProduct, pidAdress);
            
            ConnectionDB.realizarAccion(sql);
        }
        
        public static void eliminarOrden(int pIdOrder)
        {
            string sql = String.Format(
                "delete from apporder where idOrder ='{0}'; ",
                pIdOrder);
        }
    }
}
using System;

namespace Segundo_parcial.Modelo
{
    public class Orden
    {
        public int idOrder { get; set; }
                public DateTime createOrder { get; set; }
                public int idProduct { get; set; }
                public int idAdress { get; set; }
        
                public Orden()
                {
                    idOrder = 0;
                    createOrder = DateTime.Now;
                    idProduct = 0;
                    idAdress = 0;
                }
    }
}
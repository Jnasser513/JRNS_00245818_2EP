namespace Segundo_parcial.Modelo
{
    public class Direccion
    {
        public int idAddres { get; set; }
        public int idUser { get; set; }
        public string address { get; set; }

        public Direccion()
        {
            idAddres = 0;
            idUser = 0;
            address = "";
        }
    }
}
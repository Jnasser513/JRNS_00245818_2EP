namespace Segundo_parcial.Modelo
{
    public class APPUSER
    {
        public int id { get; set; }
        public string name { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public bool admin { get; set; }

        public APPUSER()
        {
            id = 0;
            name = "";
            user = "";
            password = "";
            admin = false;
        }
    }
}
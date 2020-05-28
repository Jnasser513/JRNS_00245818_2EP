namespace Segundo_parcial.Modelo
{
    public class APPUSER
    {
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool type { get; set; }

        public APPUSER()
        {
            fullname = "";
            username = "";
            password = "";
            type = true;
        }
    }
}
using CV.Models;

namespace CV.Data
{
    public class Seeder
    {
        private List<Ibbi> _ibbis = new List<Ibbi> ();
        public Seeder() {

            Ibbi ibbi = new Ibbi
            {
                Id = 1,
                FirstName = "Ibrahima Secka",
                DOB= "18.04.2001",
                Img = "",
                Description = "This is me"



            };
            _ibbis.Add (ibbi);
        }
        public List<Ibbi> Ibbis { get { return _ibbis; } }
    }
}

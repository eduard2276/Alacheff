using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALACHEFF.Clase
{
    class MateriiPrime
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public string UM { get; set; }
        public float Cantitate { get; set; }
        public float Pret { get; set; }

        public MateriiPrime()
        {
            Id = 0;
            Denumire = "";
            UM = "";
            Cantitate = 0;
            Pret = 0;
        }

        public MateriiPrime(string Denumire, string UM, float Cantitate, float Pret)
        {
            this.Denumire = Denumire;
            this.UM = UM;
            this.Cantitate = Cantitate;
            this.Pret = Pret;
        }

        public MateriiPrime(int Id, string Denumire, string UM, float Cantitate, float Pret)
        {
            this.Id = Id;
            this.Denumire = Denumire;
            this.UM = UM;
            this.Cantitate = Cantitate;
            this.Pret = Pret;
        }






    }
}

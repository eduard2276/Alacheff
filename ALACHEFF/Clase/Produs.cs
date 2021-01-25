using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALACHEFF
{
    class Produs
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public float Pret { get; set; }
        public int Gramaj { get; set; }
        public string Retetar { get; set; }
        //public int Portii { get; set; }

        public Produs()
        {
            this.Id = 0;
            this.Denumire = "";
            this.Pret = 0;
            this.Gramaj = 0;
            this.Retetar = "";
        }


        public Produs(int Id, string denumire, float pret, int gramaj, string retetar)
        {
            this.Id = Id;
            this.Denumire = denumire;
            this.Pret = pret;
            this.Gramaj = gramaj;
            this.Retetar = retetar;
            //this.Portii = portii;
        }

        public Produs(string denumire, float pret, int gramaj, string retetar)
        {
            this.Id = 0;
            this.Denumire = denumire;
            this.Pret = pret;
            this.Gramaj = gramaj;
            this.Retetar = retetar;
            //this.Portii = portii;
        }


    }
}

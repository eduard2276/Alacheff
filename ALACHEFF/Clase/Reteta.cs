using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALACHEFF.Clase
{
    class Reteta
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public int Gramaj { get; set; }
        public float Pret { get; set; }



        public IDictionary<string, float> Materii_Prime;


        public Reteta()
        {
            this.Id = 0;
            this.Denumire = "";
            this.Gramaj = 0;
            //this.MateriiPrime = "";
            this.Pret = 0;
            this.Materii_Prime = new Dictionary<string, float>();
        }
        public Reteta(int Id, string Denumire, int Gramaj, string MateriiPrime, float Pret)
        {
            this.Id = Id;
            this.Denumire = Denumire;
            this.Gramaj = Gramaj;
            //this.MateriiPrime = MateriiPrime;
            this.Pret = Pret;
        }

        public Reteta(int Id, string Denumire, int Gramaj, ListBox listBox, DataTable dt)
        {
            this.Id = Id;
            this.Denumire = Denumire;
            this.Gramaj = Gramaj;
            extrageMateriiPrimeListBox(listBox,dt);
        }

        public Reteta(int Id, string Denumire, int Gramaj, string materiiPrime, DataTable dt)
        {
            this.Id = Id;
            this.Denumire = Denumire;
            this.Gramaj = Gramaj;
            extrageMateriiPrimeString(materiiPrime, dt);
        }

        private void extrageMateriiPrimeString(string materiiPrime, DataTable dt)
        {
            this.Pret = 0;
            this.Materii_Prime = new Dictionary<string, float>();
            foreach(string materiePrima in materiiPrime.Split(';'))
            {
                if(materiePrima!="")
                {
                    string numeMateriePrima = materiePrima.Split(':')[0];
                    float gramaj = float.Parse(materiePrima.Split(':')[1]);
                    this.Materii_Prime.Add(numeMateriePrima, gramaj);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[1].ToString() == numeMateriePrima)
                        {
                            this.Pret += (float.Parse(dr[4].ToString()) * gramaj) / float.Parse(dr[3].ToString());
                        }
                    }
                }
            }
        }
        private void extrageMateriiPrimeListBox(ListBox listbox, DataTable dt)
        {
            this.Pret = 0;
            //this.MateriiPrime = "";
            this.Materii_Prime = new Dictionary<string, float>();
            foreach (string materiePrima in listbox.Items)
            {
                //this.MateriiPrime += materiePrima + ';';
                string numeMateriePrima = materiePrima.Split('-')[0].Remove(materiePrima.Split('-')[0].Length - 1, 1);
                float gramaj = float.Parse(materiePrima.Split('-')[1].Replace(" ", "").Remove(materiePrima.Split('-')[1].Replace(" ", "").Length - 1, 1));
                this.Materii_Prime.Add(numeMateriePrima, gramaj);
                
                foreach(DataRow dr in dt.Rows)
                {
                    if(dr[1].ToString() == numeMateriePrima)
                    {
                        this.Pret += (float.Parse(dr[4].ToString()) * gramaj) / float.Parse(dr[3].ToString());
                    }
                }
            }
        }
    }
}

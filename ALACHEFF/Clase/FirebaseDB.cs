using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace ALACHEFF.Clase
{
    class FirebaseDB
    {
        //Listele pentru datele extrase din baza de date (o lista pentru fiecare tabel)
        private List<Data> lista = new List<Data>();
        private List<DateComanda> listComenzi = new List<DateComanda>();
        private List<MateriiPrime> listMateriiPrime = new List<MateriiPrime>();
        private List<Reteta> listRetete = new List<Reteta>();

        //Numarul de elemente din fiecare tabel
        private int nrComenzi, nrProduse, nrMateriiPrime, nrRetete;


        //Configurarea bazei de date
        IFirebaseConfig config = new FirebaseConfig
        {
            //PENTRU TESTARE
            
            AuthSecret = "qLrP8FRw5J1oBpzYCeVqhkbgJ6lzUD2dwJoq5qQL",
            BasePath = "https://alacheff-deploy-default-rtdb.firebaseio.com/"


            //PENTRU DEVELOPMENT
            
            //AuthSecret = "PWt4EKSWjR5xdFVNaTZH7UagAATnAjsyULZAqhZu",
            //BasePath = "https://alacheff-df964.firebaseio.com/"
            
        };
        IFirebaseClient client;



        public bool l1, l2, l3, l4;


        public FirebaseDB()
        {
            l1 = l2 = l3 = l4 = false;
            client = new FireSharp.FirebaseClient(config);

            listaProduse();
            listaComenzi();
            listaMateriiprime();
            listaRetete();
        }
        



        //Get-ere pentru lista de elemente de la fiecare element in parte
        public List<Data> GetListaProduse()
        {
            return this.lista;
        }
        public List<DateComanda> GetListaComenzi()
        {
            return this.listComenzi;
        }
        public List<MateriiPrime> GetListaMateriiPrime()
        {
            return this.listMateriiPrime;
        }
        public List<Reteta> GetListaRetete()
        {
            return this.listRetete;
        }




        //Get-ere pentru numarul de comenzi din fiecare tabel
        public int getNrComenzi()
        {
            return this.nrComenzi;
        }
        public int getNrProduse()
        {
            return this.nrProduse;
        }
        public int getNrMateriiPrime()
        {
            return this.nrMateriiPrime;
        }
        public int getNrRetete()
        {
            return this.nrRetete;
        }




        //Funtii de introducere in lista de elemente pentru fiecare tabel
        public async void listaProduse()
        {

            this.lista.Clear();
            FirebaseResponse idResponse = await client.GetAsync("Counter/ProdCNT");

            Counter get = idResponse.ResultAs<Counter>();
            nrProduse = get.nr;

            for (int i = 1; i <= get.nr; i++)
            {
                FirebaseResponse prodResp = await client.GetAsync("Produse/"+i.ToString());
                Data dt = prodResp.ResultAs<Data>();
                if(dt!=null)
                    this.lista.Add(dt);
            }
            l1 = true;
            
        }
        public async void listaComenzi()
        {
            this.listComenzi.Clear();
            FirebaseResponse idResponse = await client.GetAsync("Counter/ComCNT");

            Counter get = idResponse.ResultAs<Counter>();
            nrComenzi = get.nr;
            for (int i = 1; i <= get.nr; i++)
            {
                FirebaseResponse prodResp = await client.GetAsync("Comenzi/" + i.ToString());
                DateComanda dt = prodResp.ResultAs<DateComanda>();
                if(dt!=null)
                    this.listComenzi.Add(dt);
            }
            this.l2 = true;
        }
        public async void listaMateriiprime()
        {
            this.listMateriiPrime.Clear();
            FirebaseResponse idResponse = await client.GetAsync("Counter/MateriiPrimeCNT");

            Counter get = idResponse.ResultAs<Counter>();
            nrMateriiPrime = get.nr;

            for (int i = 1; i <= get.nr; i++)
            {
                FirebaseResponse MateriiPrimeResp = await client.GetAsync("Materii_Prime/" + i.ToString());
                MateriiPrime mp = MateriiPrimeResp.ResultAs<MateriiPrime>();
                if (mp != null)
                    this.listMateriiPrime.Add(mp);
            }
            this.l3 = true;
        }
        public async void listaRetete()
        {
            this.listRetete.Clear();
            FirebaseResponse idResponse = await client.GetAsync("Counter/RetCNT");

            Counter get = idResponse.ResultAs<Counter>();
            nrRetete = get.nr;

            for (int i = 1; i <= get.nr; i++)
            {
                FirebaseResponse ReteteResp = await client.GetAsync("Retete/" + i.ToString());
                Reteta ret = ReteteResp.ResultAs<Reteta>();
                if (ret != null)
                    this.listRetete.Add(ret);
            }
            this.l4 = true;
        }



        //Funtiile de adaugare in baza de date pentru fiecare tabel in parte
        public async Task addProdusAsync(Produs produs)
        {
            //Se ia numarul de id-uri din baza de date
            FirebaseResponse idResponse = await client.GetAsync("Counter/ProdCNT");
            Counter get = idResponse.ResultAs<Counter>();
            get.nr = get.nr + 1;
            nrProduse = get.nr;
            //Se face update in baza de date la numarul de produse adaugate
            FirebaseResponse updateId = await client.UpdateAsync("Counter/ProdCNT", get);
           

            //Se introduce produsul cu id-ul sau
            var data = new Data
            {
                Id = get.nr,
                Denumire = produs.Denumire,
                Pret = produs.Pret,
                Gramaj = produs.Gramaj,
                Retetar = produs.Retetar

            };
            SetResponse response = await client.SetAsync("Produse/" + get.nr.ToString(), data);
            Data result = response.ResultAs<Data>();

            //listaProduse();
            


        }
        public async Task addComandaAsync(Comanda comanda)
        {
            //Se ia numarul de id-uri din baza de date
            FirebaseResponse idResponse = await client.GetAsync("Counter/ComCNT");
            Counter get = idResponse.ResultAs<Counter>();
            get.nr = get.nr + 1;
            nrComenzi = get.nr;
            //Se face update in baza de date la numarul de comenzi adaugate
            FirebaseResponse updateId = await client.UpdateAsync("Counter/ComCNT", get);

            var data = new DateComanda
            {
                Id = get.nr,
                Sursa = comanda.sursa,
                Beneficiar = comanda.beneficiar,
                DataComanda = comanda.dataComanda.ToString("dd/MM/yyyy"),
                DataLivrare = comanda.dataLivrare.ToString("dd/MM/yyyy"),
                ZiuaLivrare = comanda.ziuaLivrare,
                OraLivrare = comanda.oraLivrare,
                Adresa = comanda.adresa,
                Telefon = comanda.telefon,
                TelefonRezerva = comanda.telefonRezerva,
                Servicii = comanda.servicii,
                PretTotal = comanda.pretTotal,
                Avans = comanda.avans,
                Rest = comanda.rest,
                BCF = comanda.bcf
            };
            SetResponse response = await client.SetAsync("Comenzi/" + get.nr, data);
            DateComanda result = response.ResultAs<DateComanda>();
            
            //listaComenzi();
        }
        public async Task addMateriePrimaAsync(MateriiPrime materiePrima)
        {
            FirebaseResponse idResponse = await client.GetAsync("Counter/MateriiPrimeCNT");
            Counter get = idResponse.ResultAs<Counter>();
            get.nr = get.nr + 1;
            nrMateriiPrime = get.nr;
            //Se face update in baza de date la numarul de comenzi adaugate
            FirebaseResponse updateId = await client.UpdateAsync("Counter/MateriiPrimeCNT", get);

            var data = new MateriiPrime
            {
                Id = get.nr,
                Denumire = materiePrima.Denumire,
                UM = materiePrima.UM,
                Cantitate = materiePrima.Cantitate,
                Pret = materiePrima.Pret
            };
            SetResponse response = await client.SetAsync("Materii_Prime/" + get.nr, data);
            DateComanda result = response.ResultAs<DateComanda>();
        }
        public async Task addRetetaAsync(Reteta reteta)
        {
            FirebaseResponse idResponse = await client.GetAsync("Counter/RetCNT");
            Counter get = idResponse.ResultAs<Counter>();
            get.nr = get.nr + 1;
            nrRetete = reteta.Id = get.nr; 
            //Se face update in baza de date la numarul de comenzi adaugate
            FirebaseResponse updateId = await client.UpdateAsync("Counter/RetCNT", get);
            SetResponse response = await client.SetAsync("Retete/" + get.nr, reteta);
            Reteta result = response.ResultAs<Reteta>();
        }





        //Funtiile de update pentru fiecare tabel in parte
        public async Task updateProdusAsync(Produs produs)
        {
            var data = new Data
            {
                Id=produs.Id,
                Denumire = produs.Denumire,
                Pret = produs.Pret,
                Gramaj = produs.Gramaj,
                Retetar = produs.Retetar

            };
            FirebaseResponse response = await client.UpdateAsync("Produse/" + data.Id, data);
            Data result = response.ResultAs<Data>();
            //listaProduse();
            
        }
        public async Task updateComandaAsync(Comanda comanda)
        {
            var data = new DateComanda
            {
                Id = comanda.id,
                Sursa = comanda.sursa,
                Beneficiar = comanda.beneficiar,
                DataComanda = comanda.dataComanda.ToString("dd/MM/yyyy"),
                DataLivrare = comanda.dataLivrare.ToString("dd/MM/yyyy"),
                ZiuaLivrare = comanda.ziuaLivrare,
                OraLivrare = comanda.oraLivrare,
                Adresa = comanda.adresa,
                Telefon = comanda.telefon,
                TelefonRezerva = comanda.telefonRezerva,
                Servicii = comanda.servicii,
                PretTotal = comanda.pretTotal,
                Avans = comanda.avans,
                Rest = comanda.rest,
                BCF = comanda.bcf
            };

            FirebaseResponse response = await client.UpdateAsync("Comenzi/" + data.Id, data);
            DateComanda result = response.ResultAs<DateComanda>();
            //listaComenzi();
        }
        public async Task UpdateMateriePrimaAsync(MateriiPrime materiePrima)
        {
            FirebaseResponse response = await client.UpdateAsync("Materii_Prime/" + materiePrima.Id, materiePrima);

        }
        public async Task UpdateRetetaAsync(Reteta reteta)
        {
            FirebaseResponse response = await client.UpdateAsync("Retete/" + reteta.Id, reteta);
        }



        //Functiile de stergere din baza de date de la fiecare tabel in parte
        public async Task deleteProdusAsync(int id)
        {
            FirebaseResponse response = await client.DeleteAsync("Produse/" + id);
            //listaProduse();
            
        }
        public async Task deleteComandaAsync(int id)
        {
            FirebaseResponse response = await client.DeleteAsync("Comenzi/" + id);
            
            //listaComenzi();
        }
        public async Task deleteMateriePrimaAsync(int id)
        {
            FirebaseResponse response = await client.DeleteAsync("Materii_Prime/" + id);
        }
        public async Task deleteRetetaAsync(int id)
        {
            FirebaseResponse response = await client.DeleteAsync("Retete/" + id);
        }

    }
}

using Bunifu.Framework.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALACHEFF
{
   public class Comanda
    {
        public int id { get; set; }
        public string sursa { get; set; }
        public string beneficiar { get; set; }
        public DateTime dataComanda { get; set; }
        public DateTime dataLivrare { get; set; }
        public string ziuaLivrare { get; set; }
        public string oraLivrare { get; set; }
        public string adresa { get; set; }
        public string telefon { get; set; }
        public string telefonRezerva { get; set; }
        public string servicii { get; set; }
        public float pretTotal { get; set; }
        public float avans { get; set; }
        public float rest { get; set; }
        public string bcf { get; set; }

        public string serviciiOriginal { get; set; }
        public float pretOriginal { get; set; }

        public Comanda()
        {
            this.id = 0;
            this.sursa = "";
            this.beneficiar = "";
            this.dataComanda = new DateTime(1, 1, 1);
            this.dataLivrare = new DateTime(1, 1, 1);
            this.ziuaLivrare = "";
            this.oraLivrare = "";
            this.adresa = "";
            this.telefon = "";
            this.telefonRezerva = "";
            this.servicii = "";
            this.pretTotal = 0;
            this.avans = 0;
            this.rest = 0;
            this.bcf = "";
        }
        public Comanda(int id, string sursa, string beneficiar, BunifuDatepicker dataComanda, string oraLivrare, string adresa, string telefon, string telefonRezerva,
            string servicii, float pretTotal, float avans, float rest, string bcf)
        {
            this.id = id;
            this.sursa = sursa;
            this.beneficiar = beneficiar;
            this.dataComanda = new DateTime(dataComanda.Value.Year, dataComanda.Value.Month, dataComanda.Value.Day);
            this.oraLivrare = oraLivrare;
            this.adresa = adresa;
            this.telefon = telefon;
            this.telefonRezerva = telefonRezerva;
            this.servicii = servicii;
            this.pretTotal = pretTotal;
            this.avans = avans;
            this.rest = pretTotal-avans;
            this.bcf = bcf;
        }
        public Comanda(int id, string sursa, string beneficiar, BunifuDatepicker dataComanda, BunifuDatepicker dataLivrare, string oraLivrare, string adresa,
            string telefon, string telefonRezerva, string servicii, float pretTotal, float avans, float rest, string bcf)
        {
            this.id = id;
            this.sursa = sursa;
            this.beneficiar = beneficiar;
            this.dataComanda = new DateTime(dataComanda.Value.Year, dataComanda.Value.Month, dataComanda.Value.Day);
            this.dataLivrare = new DateTime(dataLivrare.Value.Year, dataLivrare.Value.Month, dataLivrare.Value.Day);
            this.ziuaLivrare = convertireData();
            this.oraLivrare = oraLivrare;
            this.adresa = adresa;
            this.telefon = telefon;
            this.telefonRezerva = telefonRezerva;
            this.servicii = servicii;
            this.pretTotal = pretTotal;
            this.avans = avans;
            this.rest = pretTotal - avans;
            this.bcf = bcf;
        }
        public Comanda(int id, string sursa, string beneficiar, string dataComanda, string dataLivrare, string oraLivrare, string adresa, string telefon,
            string telefonRezerva, string servicii, float pretTotal, float avans, float rest, string bcf)
        {
            this.id = id;
            this.sursa = sursa;
            this.beneficiar = beneficiar;
            string[] data = dataComanda.Split('/');
            this.dataComanda = new DateTime(int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]));
            data = dataLivrare.Split('/');
            this.dataLivrare = new DateTime(int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]));
            this.ziuaLivrare = convertireData();
            this.oraLivrare = oraLivrare;
            this.adresa = adresa;
            this.telefon = telefon;
            this.telefonRezerva = telefonRezerva;
            this.servicii = servicii;
            this.pretTotal = pretTotal;
            this.avans = avans;
            this.rest = pretTotal - avans;
            this.bcf = bcf;

        }

        public void setDataLivrare(BunifuDatepicker dtl)
        {
            this.dataLivrare= new DateTime(dtl.Value.Year,dtl.Value.Month,dtl.Value.Day);
            ziuaLivrare = convertireData();
            
        }
        public string convertireData()
        {
            switch(dataLivrare.DayOfWeek.ToString())
            {
                case "Monday":
                    return "Luni";
                case "Tuesday":
                    return "Marti";
                case "Wednesday":
                    return "Miercuri";
                case "Thursday":
                    return "Joi";
                case "Friday":
                    return "Vineri";
                case "Saturday":
                    return "Sambata";
                case "Sunday":
                    return "Duminica";
            }
            return "";
        }
        public void setServicii(string serviciu)
        {

        }
        public void emptyData()
        {
            this.id = 0;
            this.sursa = "";
            this.beneficiar = "";
            this.dataComanda = new DateTime(1, 1, 1);
            this.dataLivrare = new DateTime(1, 1, 1);
            this.ziuaLivrare = "";
            this.oraLivrare = "";
            this.adresa = "";
            this.telefon = "";
            this.telefonRezerva = "";
            this.servicii = "";
            this.pretTotal = 0;
            this.avans = 0;
            this.rest = 0;
            this.bcf = "";
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public void setDate(string sursa, string beneficiar, BunifuDatepicker dataComanda, string oraLivrare, string adresa, string telefon, string telefonRezerva, string servicii, float pretTotal, float avans, float rest, string bcf)
        {
            this.sursa = sursa;
            this.beneficiar = beneficiar;
            this.dataComanda = new DateTime(dataComanda.Value.Year, dataComanda.Value.Month, dataComanda.Value.Day);
            this.oraLivrare = oraLivrare;
            this.adresa = adresa;
            this.telefon = telefon;
            this.telefonRezerva = telefonRezerva;
            this.servicii = servicii;
            this.pretTotal = pretTotal;
            this.avans = avans;
            this.rest = pretTotal-avans;
            this.bcf = bcf;
        }
        public void setDate(int id, string sursa, string beneficiar, BunifuDatepicker dataComanda, string oraLivrare, string adresa, string telefon, string telefonRezerva, string servicii, float pretTotal, float avans, float rest, string bcf)
        {
            this.id = id;
            this.sursa = sursa;
            this.beneficiar = beneficiar;
            this.dataComanda = new DateTime(dataComanda.Value.Year, dataComanda.Value.Month, dataComanda.Value.Day);
            this.oraLivrare = oraLivrare;
            this.adresa = adresa;
            this.telefon = telefon;
            this.telefonRezerva = telefonRezerva;
            this.servicii = servicii;
            this.pretTotal = pretTotal;
            this.avans = avans;
            this.rest = pretTotal - avans;
            this.bcf = bcf;
        }
        public void setDate(int id, string sursa, string beneficiar, BunifuDatepicker dataComanda, BunifuDatepicker dataLivrare, string oraLivrare, string adresa, string telefon, string telefonRezerva, string servicii, float pretTotal, float avans, float rest, string bcf)
        {
            this.id = id;
            this.sursa = sursa;
            this.beneficiar = beneficiar;
            this.dataComanda = new DateTime(dataComanda.Value.Year, dataComanda.Value.Month, dataComanda.Value.Day);
            this.dataLivrare = new DateTime(dataLivrare.Value.Year, dataLivrare.Value.Month, dataLivrare.Value.Day);
            this.ziuaLivrare = convertireData();
            this.oraLivrare = oraLivrare;
            this.adresa = adresa;
            this.telefon = telefon;
            this.telefonRezerva = telefonRezerva;
            this.servicii = servicii;
            this.pretTotal = pretTotal;
            this.avans = avans;
            this.rest = pretTotal - avans;
            this.bcf = bcf;
        }
        public void setDate(string sursa, string beneficiar, BunifuDatepicker dataComanda, BunifuDatepicker dataLivrare, string oraLivrare, string adresa, string telefon, string telefonRezerva, float pretTotal, float avans, float rest, string bcf)
        {
            this.sursa = sursa;
            this.beneficiar = beneficiar;
            this.dataComanda = new DateTime(dataComanda.Value.Year, dataComanda.Value.Month, dataComanda.Value.Day);
            this.dataLivrare = new DateTime(dataLivrare.Value.Year, dataLivrare.Value.Month, dataLivrare.Value.Day);
            this.ziuaLivrare = convertireData();
            this.oraLivrare = oraLivrare;
            this.adresa = adresa;
            this.telefon = telefon;
            this.telefonRezerva = telefonRezerva;
            this.pretTotal = pretTotal;
            this.avans = avans;
            this.rest = pretTotal - avans;
            this.bcf = bcf;
        }
        
       
        
    }
}

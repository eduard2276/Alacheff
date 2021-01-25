using ALACHEFF.Clase;
using Bunifu.Framework.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace ALACHEFF
{
    public partial class Form1 : Form
    {

        FirebaseDB firebase;
     

        public Form1()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            //reteteBunifuCustomDataGrid.Columns[4].Visible = false;
            //comenziBunifuCustomDataGrid.Columns[0].Visible = false;

            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Text = "";
            }

            dtCom = new DataTable();
            dtCom.Columns.Add("Id");
            dtCom.Columns.Add("Sursa");
            dtCom.Columns.Add("Beneficiar");
            dtCom.Columns.Add("Data_Comanda");
            dtCom.Columns.Add("Data_Livrare");
            dtCom.Columns.Add("Ziua Livrare");
            dtCom.Columns.Add("Ora Livrare");
            dtCom.Columns.Add("Adresa");
            dtCom.Columns.Add("Telefon");
            dtCom.Columns.Add("Telefon Rezerva");
            dtCom.Columns.Add("Servicii");
            dtCom.Columns.Add("Pret Total");
            dtCom.Columns.Add("Avans");
            dtCom.Columns.Add("Rest");
            dtCom.Columns.Add("BCF");
            tabelComenzi.DataSource = dtCom;
            tabelComenzi.Columns[0].Visible = false;
            for (int i = 1; i <= 14; i++)
                tabelComenzi.Columns[i].ReadOnly = true;
            firebase = new FirebaseDB();

            
        }


        DataTable dtPlat;
        DataTable dtCom;
        DataTable dtMateriiPrime;
        DataTable dtRetete;


        private void populeazaPlatouri()
        {
            List<Data> list = firebase.GetListaProduse();

            dtPlat = new DataTable();

            dtPlat.Columns.Add("Id");
            dtPlat.Columns.Add("Denumire");
            dtPlat.Columns.Add("Pret");
            dtPlat.Columns.Add("Gramaj");
            dtPlat.Columns.Add("Retetar");

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    dtPlat.Rows.Add(list[i].Id, list[i].Denumire, list[i].Pret, list[i].Gramaj, list[i].Retetar);
                    //tabelRetete.Rows.Add();
                    //tabelRetete.Rows[i].Cells[0].Value = list[i].Id;
                    //tabelRetete.Rows[i].Cells[1].Value = list[i].Denumire;
                    //tabelRetete.Rows[i].Cells[2].Value = list[i].Pret;
                    //tabelRetete.Rows[i].Cells[3].Value = list[i].Gramaj;
                    //tabelRetete.Rows[i].Cells[4].Value = list[i].Retetar;
                }

            }
            tabelRetete.DataSource = dtPlat;
        }
        private void populeazaComenzi()
        {
            List<DateComanda> list = firebase.GetListaComenzi();
            dtCom = new DataTable();
            dtCom.Columns.Add("Id");
            dtCom.Columns.Add("Sursa");
            dtCom.Columns.Add("Beneficiar");
            dtCom.Columns.Add("Data_Comanda");
            dtCom.Columns.Add("Data_Livrare");
            dtCom.Columns.Add("Ziua Livrare");
            dtCom.Columns.Add("Ora Livrare");
            dtCom.Columns.Add("Adresa");
            dtCom.Columns.Add("Telefon");
            dtCom.Columns.Add("Telefon Rezerva");
            dtCom.Columns.Add("Servicii");
            dtCom.Columns.Add("Pret Total");
            dtCom.Columns.Add("Avans");
            dtCom.Columns.Add("Rest");
            dtCom.Columns.Add("BCF");

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    dtCom.Rows.Add(list[i].Id, list[i].Sursa, list[i].Beneficiar, list[i].DataComanda, list[i].DataLivrare, list[i].ZiuaLivrare, list[i].OraLivrare, list[i].Adresa, list[i].Telefon,
                        list[i].TelefonRezerva, list[i].Servicii, list[i].PretTotal, list[i].Avans, list[i].Rest, list[i].BCF);
                }
            }
            tabelComenzi.DataSource = dtCom;

        }
        private void populeazaMateriiPrime()
        {

            List<MateriiPrime> list = firebase.GetListaMateriiPrime();

            dtMateriiPrime = new DataTable();

            dtMateriiPrime.Columns.Add("Id");
            dtMateriiPrime.Columns.Add("Denumire");
            dtMateriiPrime.Columns.Add("UM");
            dtMateriiPrime.Columns.Add("Cantitate_Referinta");
            
            dtMateriiPrime.Columns.Add("Pret");

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    dtMateriiPrime.Rows.Add(list[i].Id, list[i].Denumire, list[i].UM, list[i].Cantitate, list[i].Pret);
                }
            }

            tabelMateriiPrime.DataSource = dtMateriiPrime;
        }
        private void populeazaRetete()
        {
            List<Reteta> list = firebase.GetListaRetete();

            dtRetete = new DataTable();

            dtRetete.Columns.Add("Id");
            dtRetete.Columns.Add("Denumire");
            dtRetete.Columns.Add("Gramaj");
            dtRetete.Columns.Add("Pret");
            dtRetete.Columns.Add("Materii_Prime");

            for(int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    dtRetete.Rows.Add(list[i].Id, list[i].Denumire, list[i].Gramaj, list[i].Pret, dicToString(list[i].Materii_Prime));
                }
            }

            tabelPtRetete.DataSource = dtRetete;
        }
        private string dicToString(IDictionary<string, float> Materii_Prime)
        {
            string rezultat = "";
            foreach (KeyValuePair<string, float> kvp in Materii_Prime)
                rezultat += kvp.Key + ':' + kvp.Value + ';';

            return rezultat;
        }



        //TIMER SI FUNCTIILE PENTRU INCARCAREA TABELELOR LA PORNIREA APLICATIEI
        private Timer timer1;
        public void InitTimer()
        {
     
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 100; // in miliseconds
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (firebase.l1 == true && firebase.l2 == true && firebase.l3 == true && firebase.l4 == true)
            {
                
                timer1.Stop();
                MessageBox.Show("Aplicatia se poate folosi");
                tabelComenzi.ClearSelection();
            }
            populeazaComenzi();
            populeazaPlatouri();
            populeazaMateriiPrime();
            populeazaRetete();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitTimer();
            toolTipInitianalizer();
            
            
        }

        private void toolTipInitianalizer()
        {
            /*-------------------COMENZI---------------------*/
            toolTip.SetToolTip(sursa, "Sursa");

            toolTip.SetToolTip(adaugaPlatouComandaBTN, "Adauga platou");
            toolTip.SetToolTip(stergePlatouComandaBTN, "Sterge platou");

            toolTip.SetToolTip(printeazaComandaBTN, "Printeaza comanda");
            toolTip.SetToolTip(adaugaComandaBTN, "Adauga comanda");
            toolTip.SetToolTip(stergeComandaBTN, "Sterge comanda");
            toolTip.SetToolTip(actualizeazaComandaBTN, "Actualizare comanda");
            toolTip.SetToolTip(cautaComandaTxt, "Cauta comanda");

            /*--------------------PLATOURI------------------*/
            toolTip.SetToolTip(adaugaRetetaPlatouBTN, "Adauga Reteta");
            toolTip.SetToolTip(stergeRetetaPlatouBTN, "Sterge Reteta");

            toolTip.SetToolTip(adaugaPlatouBTN, "Adauga platou");
            toolTip.SetToolTip(stergePlatouBTN, "Sterge platou");
            toolTip.SetToolTip(actualizarePlatouBTN, "Actualizare platou");

            /*---------------------RETETE-------------------*/

            toolTip.SetToolTip(adaugaListaMateriiPrimeBTN, "Adauga materie prima");
            toolTip.SetToolTip(stergeMateriePrimareRetetaBTN, "Sterge materie prima");

            toolTip.SetToolTip(adaugaRetetaBTN, "Adauga reteta");
            toolTip.SetToolTip(stergeRetetaBTN, "Sterge reteta");
            toolTip.SetToolTip(updateRetetaBTN, "Actualizare reteta");
            toolTip.SetToolTip(printeazaRetetaBTN, "Printeaza reteta");

            /*---------------------Materie prima-------------------*/

            toolTip.SetToolTip(adaugaMaterii, "Adauga materie prima");
            toolTip.SetToolTip(StergeMateriePrima, "Sterge materie prima");
            toolTip.SetToolTip(actualizareMateriePrima, "Actualizare materie prima");

        }


        /*-----------------------------BUTOANE DE NAVIGARE-------------------------------*/
        
        private void comandaNouaBtn_Click(object sender, EventArgs e)
        {
            tabelComenzi.ClearSelection();
            tabControl1.SelectTab(0);
            clc("comenzi");

        }
        private void cautaComandaBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            clc("platouri");

        }
        private void reteteBtn_Click(object sender, EventArgs e)
        {
            //if(isLoaded == false)
            //{
            //    isLoaded = true;
            //    populeazaRetete();
            //}
            tabelRetete.ClearSelection();
            tabControl1.SelectTab(1);
            clc("platouri");

        }
        private void listaLucruBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }
        private void necesarBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(5);
        }
        private void statisticiBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
        }
        private void export_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Text file(*.txt)|*.txt";
            sfd.FilterIndex = 1;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }
            string dirLocationString = sfd.FileName;
            StreamWriter sW = new StreamWriter(dirLocationString);
            sW.WriteLine("Retete:\t" + (tabelRetete.Rows.Count - 1));
            for (int i = 0; i < tabelRetete.Rows.Count - 1; i++)
            {
                sW.WriteLine(tabelRetete.Rows[i].Cells[0].Value.ToString().Split('\0')[0] + '\t' + tabelRetete.Rows[i].Cells[1].Value.ToString().Split('\0')[0] + '\t'
                    + tabelRetete.Rows[i].Cells[2].Value.ToString().Split('\0')[0] + '\t' + tabelRetete.Rows[i].Cells[3].Value.ToString().Split('\0')[0] + '\t'
                    + tabelRetete.Rows[i].Cells[4].Value.ToString().Split('\0')[0] + '\t');
            }
            sW.WriteLine("Comenzi:\t" + (tabelComenzi.Rows.Count - 1));

            for (int i = 0; i < tabelComenzi.Rows.Count - 1; i++)
            {
                sW.WriteLine(tabelComenzi.Rows[i].Cells[0].Value.ToString().Split('\0')[0] + '\t' + tabelComenzi.Rows[i].Cells[1].Value.ToString().Split('\0')[0] + '\t'
                    + tabelComenzi.Rows[i].Cells[2].Value.ToString().Split('\0')[0] + '\t' + tabelComenzi.Rows[i].Cells[3].Value.ToString().Split('\0')[0] + '\t'
                    + tabelComenzi.Rows[i].Cells[4].Value.ToString().Split('\0')[0] + '\t' + tabelComenzi.Rows[i].Cells[5].Value.ToString().Split('\0')[0] + '\t'
                    + tabelComenzi.Rows[i].Cells[6].Value.ToString().Split('\0')[0] + '\t' + tabelComenzi.Rows[i].Cells[7].Value.ToString().Split('\0')[0] + '\t'
                    + tabelComenzi.Rows[i].Cells[8].Value.ToString().Split('\0')[0] + '\t' + tabelComenzi.Rows[i].Cells[9].Value.ToString().Split('\0')[0] + '\t'
                    + tabelComenzi.Rows[i].Cells[10].Value.ToString().Split('\0')[0] + '\t' + tabelComenzi.Rows[i].Cells[11].Value.ToString().Split('\0')[0] + '\t'
                    + tabelComenzi.Rows[i].Cells[12].Value.ToString().Split('\0')[0] + '\t' + tabelComenzi.Rows[i].Cells[13].Value.ToString().Split('\0')[0] + '\t'
                    + tabelComenzi.Rows[i].Cells[14].Value.ToString().Split('\0')[0] + '\t');
            }
            sW.Close();
        }
        private void import_Click(object sender, EventArgs e)
        {
            string line = "";
            string path = "";
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    path = theDialog.FileName;
                    StreamReader f = new StreamReader(path);

                    int nr = int.Parse(f.ReadLine().Split('\t')[1]);
                    for (int i = 0; i < nr; i++)
                    {
                        line = f.ReadLine();
                        string[] date = line.Split('\t');
                        //insereazaReteta(date[0], float.Parse(date[1]), int.Parse(date[2]), date[3]);
                    }
                    nr = int.Parse(f.ReadLine().Split('\t')[1]);
                    for (int i = 0; i < nr; i++)
                    {
                        line = f.ReadLine();
                        string[] date = line.Split('\t');
                        Comanda com = new Comanda(int.Parse(date[0]), date[1], date[2], date[3], date[4], date[6], date[7], date[8], date[9], date[10], float.Parse(date[11]), float.Parse(date[12]), float.Parse(date[13]), date[14]);

                    }


                    f.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }
        private void ReteteTrz_Click(object sender, EventArgs e)
        {
            tabelPtRetete.ClearSelection();
            tabControl1.SelectTab(2);
            clc("retete");
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            tabelMateriiPrime.ClearSelection();
            tabControl1.SelectTab(6);
            clc("materii prime");
        }
    



        /*-----------------------------PLATOURI SI PREPARATE-------------------------------*/
        Produs produs;
        string retetar = "";
        string originalDenumire;
        int nrPreparate = 0;
        float originalPret;
        int originalGramaj;
        string originalPreparate;
        int id;

        void adaugareRetetaTabel(Produs prod)
        {
            dtPlat.Rows.Add(firebase.getNrProduse() + 1, prod.Denumire, prod.Pret, prod.Gramaj, prod.Retetar);
        }
        // BUTTON DE ADAUGARE IN BAZA DE DATE
        private void adauga_Click(object sender, EventArgs e)
        {
            //Produs prd = new Produs(denumire.Text, 1, float.Parse(pret.Text), int.Parse(gramaj.Text), retetar.Text.Split(','));
            try
            {
                verificaCampuri("platou");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                pret.Text = "";
                gramaj.Text = "";
                return;
            }
            try
            {

                Produs prod = new Produs(denumire.Text, float.Parse(pret.Text), int.Parse(gramaj.Text), retetar);

                DataRow[] dr = dtPlat.Select("Denumire = '" + prod.Denumire + "'");
                if (dr.Length != 0)
                    throw new Exception(prod.Denumire + " deja exista in baza de date");

                firebase.addProdusAsync(prod);
                adaugareRetetaTabel(prod);

                clc("platouri");
                tabelRetete.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);

            }
        }

        //BUTTON DE STERGERE DIN BAZA DE DATE
        private void sterge_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esti sigur ca vrei sa stergi aceast preparat?", "Confirmare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Produs prod = new Produs(id, denumire.Text, float.Parse(pret.Text), int.Parse(gramaj.Text), retetar);

                    clc("platouri");
                    firebase.deleteProdusAsync(id);

                    foreach(DataRow dr in dtPlat.Rows)
                    {
                        if(int.Parse(dr[0].ToString()) == id)
                        {
                            dr.Delete();
                            break;
                        }
                    }

                    actualizareComenzi(prod,new Produs());
                    tabelRetete.ClearSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare: " + ex.Message);

                }
            }
            
        }



        void actualizareComenzi(Produs prepVechi, Produs prepNou)
        {
            DateTime currDate = DateTime.Now;
            for(int i=0;i<tabelComenzi.Rows.Count-1; i++)
            {
                string[] dt = tabelComenzi.Rows[i].Cells[4].Value.ToString().Split('/');
                DateTime date = new DateTime(int.Parse(dt[2]), int.Parse(dt[1]), int.Parse(dt[0]));
                if((date - currDate).TotalDays > 0)
                {
                    Comanda com = new Comanda(int.Parse(tabelComenzi.Rows[i].Cells[0].Value.ToString()), tabelComenzi.Rows[i].Cells[1].Value.ToString(), tabelComenzi.Rows[i].Cells[2].Value.ToString(),
                                            tabelComenzi.Rows[i].Cells[3].Value.ToString(), tabelComenzi.Rows[i].Cells[4].Value.ToString(), tabelComenzi.Rows[i].Cells[6].Value.ToString(),
                                            tabelComenzi.Rows[i].Cells[7].Value.ToString(), tabelComenzi.Rows[i].Cells[8].Value.ToString(), tabelComenzi.Rows[i].Cells[9].Value.ToString(),
                                            tabelComenzi.Rows[i].Cells[10].Value.ToString(), float.Parse(tabelComenzi.Rows[i].Cells[11].Value.ToString()), float.Parse(tabelComenzi.Rows[i].Cells[12].Value.ToString()),
                                            float.Parse(tabelComenzi.Rows[i].Cells[13].Value.ToString()), tabelComenzi.Rows[i].Cells[14].Value.ToString());
                    string[] serv = com.servicii.Split(';');
                    float nrPortii = 0;
                    string serviciu = "";
                    for(int j = 0; j< serv.Length-1;j++)
                    {
                        
                        if(serv[j].Split('-')[0].Remove(serv[j].Split('-')[0].Length-1,1) == prepVechi.Denumire)
                        {
                            if(prepNou.Denumire=="")
                            {
                                
                            }
                            else
                            {
                                serviciu += serv[j].Replace(prepVechi.Denumire, prepNou.Denumire)+";";
                                //com.servicii = com.servicii.Replace(prepVechi.Denumire, prepNou.Denumire);
                            }

                            //com.servicii = com.servicii.Replace(prepVechi.Denumire, prepNou.Denumire);
                            nrPortii = float.Parse(serv[j].Split('-')[1].Replace(" ", "").Remove(serv[j].Split('-')[1].Replace(" ", "").Length - 1, 1));
                            com.pretTotal = com.pretTotal - (nrPortii * prepVechi.Pret) + (nrPortii * prepNou.Pret);
                            com.rest = com.pretTotal - com.avans;
                            
                            firebase.updateComandaAsync(com);
                        }
                        else
                        {
                            serviciu += serv[j] + ";";
                        }
                    }
                    dtCom.Rows[i]["Servicii"] = serviciu;
                    dtCom.Rows[i]["Pret Total"] = com.pretTotal;
                    dtCom.Rows[i]["Rest"] = com.rest;

                    // float.Parse(serviciu.Split('-')[1].Replace(" ", "").Remove(serviciu.Split('-')[1].Replace(" ", "").Length - 1, 1))
                }
            }
            clc("comenzi");
        }
       
        //BUTTON DE ACTUALIZARE A ELEMENTEOR DIN BAZA DE DATE
        private void actualizeaza_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esti sigur ca vrei sa actualizezi aceast preparat?", "Confirmare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Produs prod = new Produs(id, denumire.Text, float.Parse(pret.Text), int.Parse(gramaj.Text), retetar);

                    DataRow[] dr = dtPlat.Select("Denumire = '" + prod.Denumire + "'");
                    if (dr.Length != 0 && produs.Denumire != prod.Denumire)
                        throw new Exception(prod.Denumire + " deja exista in baza de date");

                    updateListaPreparate();
                    for (int i = 0; i < tabelRetete.Rows.Count - 1; i++)
                    {
                        if (tabelRetete.Rows[i].Cells[0].Value.ToString() == id.ToString())
                        {
                            dtPlat.Rows[i]["Denumire"] = prod.Denumire;
                            dtPlat.Rows[i]["Pret"] = prod.Pret;
                            dtPlat.Rows[i]["Gramaj"] = prod.Gramaj;
                            dtPlat.Rows[i]["Retetar"] = prod.Retetar;
                            break;
                        }
                    }
                    

                    actualizareComenzi(produs, prod);
                    //TO DO: Functie care la apelarea sa va actualiza si datele din tabelul de comenzi, comenzile actualizate dupa data de azi

                    firebase.updateProdusAsync(prod);
                    clc("platouri");
                    tabelRetete.ClearSelection();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Eroare: " + ex.Message);
                }
            }
        }


        //FUNCTIE DE CAUTARE IN BAZA DE DATE
        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = tabelRetete.DataSource;
            bs.Filter = "Denumire like '%" + cauta.text + "%'";
            tabelRetete.DataSource = bs;
            //(tabelRetete.DataSource as DataTable).DefaultView.RowFilter = string.Format("Denumire = '{0}'", cauta.Text);
            //(tabelRetete.DataSource as DataTable).DefaultView.RowFilter =
            // string.Format("Denumire LIKE '%"+cauta.text +"%'");
        }


        //FUNCTIE PENTRU AFLAREA DATELOR A UNUI RAND DIN BAZA DE DATE
        private void reteteBunifuCustomDataGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            listaPreparate.Items.Clear();

            string[] preparate;
            int index = e.RowIndex, nrPreparat = 0;
            id = int.Parse(tabelRetete.Rows[index].Cells[0].Value.ToString());
            originalDenumire = denumire.Text = tabelRetete.Rows[index].Cells[1].Value.ToString();

            pret.Text = tabelRetete.Rows[index].Cells[2].Value.ToString();
            originalPret = float.Parse(pret.Text);

            gramaj.Text = tabelRetete.Rows[index].Cells[3].Value.ToString();
            originalGramaj = int.Parse(gramaj.Text);

            originalPreparate = this.retetar = tabelRetete.Rows[index].Cells[3].Value.ToString();


            preparate = tabelRetete.Rows[index].Cells[4].Value.ToString().Split(';');

            nrPreparate = preparate.Length - 1;
            foreach (string preparat in preparate)
            {
                try
                {
                    string[] prep = preparat.Split(':');
                    listaPreparate.Items.Add("Preparat " + (++nrPreparat).ToString() + ": " + prep[0] + " - " + prep[1] + "g");
                }
                catch { }

            }
            produs = new Produs(id, originalDenumire, originalPret, originalGramaj, originalPreparate);
            updateListaPreparate();

        }

        //FUNCTIE DE CLEAR
        private void clc(string caz)
        {
            switch (caz)
            {
                case "platouri":
                    denumire.Text = "";
                    pret.Text = "";
                    gramaj.Text = "";
                    listaPreparate.Items.Clear();
                    retetar = "";
                    nrPreparate = 0;
                    adaugaNumePreparat.Text = "";
                    adaugaGramajPreparat.Text = "";

                    break;

                case "comenzi":
                    sursa.Text = "";
                    beneficiar.Text = "";
                    dataComanda.Value = DateTime.Now;
                    dataLivrare.Value = DateTime.Now;
                    oraLivrare.Text = "";
                    adresaLivrare.Text = "";
                    telefonContact.Text = "";
                    telefonRezerva.Text = "";
                    // ziuaLivrare.Text = "";
                    // pretTotal.Text = "";
                    avans.Text = "";

                    BCF.Text = "";
                    listaServicii.Items.Clear();
                    //AICI O SA URMEZE O LISTA
                    serviciiExtrase = "";
                    

                    break;

                case "retete":
                    denumireReteta.Text = "";
                    gramajReteta.Text = "";
                    MateriePrimaTxt.Text = "";
                    masuraMateriePrima.Text = "";
                    listaMateriiPrime.Items.Clear();
                    break;

                case "materii prime":
                    denumireMateriePrima.Text = "";
                    cantitateMateriePrima.Text = "";
                    pretMateriePrima.Text = "";
                    break;




            }
        }
        //FUNCTIE DE VERIFICARE DACA CAMPURILE SUNT CORECTE
        void verificaCampuri(string camp)
        {
            if (camp == "platou")
            {

                if (denumire.Text == "")
                    throw new Exception("Nu ai introdus denumirea");
                if (pret.Text == "")
                    throw new Exception("Nu ai introdus pret");
                if (gramaj.Text == "")
                    throw new Exception("Nu ai introdus gramaj");

            }
            if (camp == "preparat")
            {
                if (adaugaGramajPreparat.Text == "")
                    throw new Exception("Nu ai introdus gramaj penntru preparat");
                if (adaugaNumePreparat.Text == "")
                    throw new Exception("Nu ai introdus nume pentru preparat");
            }
        }
        //FUNCTIE CARE VERIFICA DACA IN TEXTBX.UL DE PRET SUNT INTRODUSE VALORI VALIDE
        private void pret_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) != true && (Char.IsDigit(e.KeyChar) != true && e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        //FUNCTIE CARE VERIFICA DACA IN TEXTBX.UL DE GRAMAJ SUNT INTRODUSE VALORI VALIDE
        private void gramaj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar=='\r')
                adaugaPreparat();

            if (Char.IsControl(e.KeyChar) != true && Char.IsDigit(e.KeyChar) != true)
            {
                e.Handled = true;
            }
        }
        private void gramaj_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) != true && Char.IsDigit(e.KeyChar) != true)
            {
                e.Handled = true;
            }
        }
        private void adaugaNumePreparat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                adaugaPreparat();
        }

        private void reloadRetete_Click(object sender, EventArgs e)
        {
            //tabelRetete.Rows.Clear();
            populeazaPlatouri();
        }

        private void adaugaNumePreparat_Enter(object sender, EventArgs e)
        {
            foreach(DataRow dr in dtRetete.Rows)
            {
                adaugaNumePreparat.AutoCompleteCustomSource.Add(dr[1].ToString());
            }
        }


        /*---------------------------------PANOU DE PREPARATE-------------------------------*/
        //FUNCTIE DE ADAUGARE IN LISTA DE PREPARATE
        private void adaugaPreparat()
        {
            try
            {
                verificaCampuri("preparat");


                //retetar += adaugaNumePreparat.Text + ":" + adaugaGramajPreparat.Text + ";";
                DataRow[] dr = dtRetete.Select("Denumire = '" + adaugaNumePreparat.Text + "'");
                if (dr.Length == 1)
                {
                    listaPreparate.Items.Add("Preparat " + (++nrPreparate).ToString() + ": " + dr[0][1].ToString() + " - " + adaugaGramajPreparat.Text + "g");
                    adaugaNumePreparat.Text = "";
                    adaugaGramajPreparat.Text = "";
                }
                else
                    throw new Exception("Reteta nu exista");

                updateListaPreparate();
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROARE: " + ex.Message);
                return;
            }
        }

        //BUTTON DE ADUAGARE IN LISTA DE PREPARATE
        private void adaugaPreparat_Click(object sender, EventArgs e)
        {
            adaugaPreparat();
        }

        //FUNCTIE DE UPDATE A LISTEI DE PREPARATE
        private void updateListaPreparate()
        {
            retetar = "";
            int numarPreparate = 0;
            foreach (string prep in listaPreparate.Items)
            {
                string[] s = prep.Split(':', '-');
                s[1] = s[1].Remove(0, 1);
                s[1] = s[1].Remove(s[1].Length - 1, 1);
                s[2] = s[2].Remove(0, 1);
                s[2] = s[2].Remove(s[2].Length - 1, 1);
                retetar += s[1] + ":" + s[2] + ";";
            }
            listaPreparate.Items.Clear();
            foreach (string prep in retetar.Split(';'))
            {
                try
                {
                    string[] s = prep.Split(':');
                    listaPreparate.Items.Add("Preparat " + (++numarPreparate).ToString() + ": " + s[0] + " - " + s[1] + "g");
                }
                catch { }
            }

        }

        //BUTTON DE STERGERE DIN LISTA DE PREPARATE
        private void stergeListaPreparate_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listaPreparate.SelectedIndex;
                listaPreparate.Items.RemoveAt(index);
                updateListaPreparate();
            }
            catch { }
        }



        




        /*-----------------------------COMENZI-------------------------------*/
        //-----------------DECLARARII------------------//
        Comanda comanda = new Comanda();
        string serviciiExtrase = "";
        List<Produs> produse = new List<Produs>();  


        //------------------------BUTOANE-----------------------------//
        //BUTTON DE SALVARE A UNEI COMENZI

        private void adaugaComandaTabel(Comanda com)
        {
            dtCom.Rows.Add(firebase.getNrComenzi() + 1, com.sursa, com.beneficiar, com.dataComanda.ToString("dd/MM/yyyy"), com.dataLivrare.ToString("dd/MM/yyyy"), com.ziuaLivrare, com.oraLivrare,
                com.adresa, com.telefon, com.telefonRezerva, com.servicii, com.pretTotal, com.avans, com.rest, com.bcf);

        }
        private void Salveaza_Click(object sender, EventArgs e)
        {
            try
            {
                updateListaComenzi();
                comanda.setDate(sursa.Text, beneficiar.Text, dataComanda, dataLivrare, oraLivrare.Text, adresaLivrare.Text, telefonContact.Text,
                    telefonRezerva.Text, comanda.pretTotal, float.Parse(avans.Text), 0, BCF.Text);


                firebase.addComandaAsync(comanda);
                adaugaComandaTabel(comanda);
                clc("comenzi");
                tabelComenzi.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);

            }

        }
        
        //BUTTON DE PRINT
        private void printeaza_Click(object sender, EventArgs e)
        {

            print(comanda);
            clc("comenzi");
        }

        private void print(Comanda comanda)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook sheet = excel.Workbooks.Open(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"resurse\template.xlsx"));
            Excel.Worksheet x = excel.ActiveSheet as Excel.Worksheet;

            x.Cells[2, 2] = comanda.beneficiar;
            x.Cells[13, 2] = comanda.beneficiar;
            x.Cells[26, 2] = comanda.beneficiar;

            x.Cells[3, 2] = comanda.dataComanda.ToString("dd/MM/yyyy");
            x.Cells[14, 2] = comanda.dataComanda.ToString("dd/MM/yyyy");
            x.Cells[27, 2] = comanda.dataComanda.ToString("dd/MM/yyyy");

            x.Cells[3, 4] = comanda.dataLivrare.ToString("dd/MM/yyyy");
            x.Cells[14, 4] = comanda.dataLivrare.ToString("dd/MM/yyyy");
            x.Cells[27, 4] = comanda.dataLivrare.ToString("dd/MM/yyyy");

            x.Cells[3, 6] = comanda.ziuaLivrare;
            x.Cells[14, 6] = comanda.ziuaLivrare;
            x.Cells[27, 6] = comanda.ziuaLivrare;

            x.Cells[3, 8] = comanda.oraLivrare;
            x.Cells[14, 8] = comanda.oraLivrare;
            x.Cells[27, 8] = comanda.oraLivrare;

            x.Cells[4, 2] = comanda.adresa;
            x.Cells[15, 2] = comanda.adresa;
            x.Cells[28, 2] = comanda.adresa;

            x.Cells[5, 2] = comanda.telefon;
            x.Cells[16, 2] = comanda.telefon;
            x.Cells[29, 2] = comanda.telefon;


            string[] serviciu = comanda.servicii.Split(';');
            string servicii = "";
            for (int i = 0; i < serviciu.Length - 1; i++)
            {
                servicii += serviciu[i];
                if (i == serviciu.Length - 2)
                    break;
                else
                    servicii += '\n';
            }


            x.Cells[6, 1] = servicii;
            x.Cells[17, 1] = servicii;
            x.Cells[30, 1] = servicii;

            x.Cells[8, 2] = comanda.pretTotal.ToString();
            x.Cells[19, 2] = comanda.pretTotal.ToString();
            x.Cells[32, 2] = comanda.pretTotal.ToString();

            x.Cells[8, 4] = comanda.avans.ToString();
            x.Cells[19, 4] = comanda.avans.ToString();
            x.Cells[32, 4] = comanda.avans.ToString();

            x.Cells[8, 6] = comanda.rest.ToString();
            x.Cells[19, 6] = comanda.rest.ToString();
            x.Cells[32, 6] = comanda.rest.ToString();

            x.Cells[8, 7] = comanda.bcf;
            x.Cells[19, 7] = comanda.bcf;
            x.Cells[32, 7] = comanda.bcf;

            excel.Visible = true;
        }
        private void comenziBunifuCustomDataGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                clc("comenzi");
                int index = e.RowIndex;

                sursa.Text = tabelComenzi.Rows[index].Cells[1].Value.ToString();
                beneficiar.Text = tabelComenzi.Rows[index].Cells[2].Value.ToString();

                string[] dataCom = tabelComenzi.Rows[index].Cells[3].Value.ToString().Split('/');
                dataComanda.Value = new DateTime(int.Parse(dataCom[2]), int.Parse(dataCom[1]), int.Parse(dataCom[0]));

                dataCom = tabelComenzi.Rows[index].Cells[4].Value.ToString().Split('/'); // Se foloseste aceeasi variabila si pentru a doua caseta
              
                
                dataLivrare.Value = new DateTime(int.Parse(dataCom[2]), int.Parse(dataCom[1]), int.Parse(dataCom[0]));
                
                

                //ziuaLivrare.Text = comenziBunifuCustomDataGrid.Rows[index].Cells[5].Value.ToString();
                oraLivrare.Text = tabelComenzi.Rows[index].Cells[6].Value.ToString();
                adresaLivrare.Text = tabelComenzi.Rows[index].Cells[7].Value.ToString();
                telefonContact.Text = tabelComenzi.Rows[index].Cells[8].Value.ToString();
                telefonRezerva.Text = tabelComenzi.Rows[index].Cells[9].Value.ToString();

                string[] listaExtrasa = tabelComenzi.Rows[index].Cells[10].Value.ToString().Split(';');
                serviciiExtrase = "";
                for (int i = 0; i < listaExtrasa.Length - 1; i++)
                {
                    listaServicii.Items.Add(listaExtrasa[i]);
                    serviciiExtrase += listaExtrasa[i] + ';';

                }

                //pretTotal.Text = comenziBunifuCustomDataGrid.Rows[index].Cells[11].Value.ToString();
                avans.Text = tabelComenzi.Rows[index].Cells[12].Value.ToString();
            
                BCF.Text = tabelComenzi.Rows[index].Cells[14].Value.ToString();
                comanda.setDate(int.Parse(tabelComenzi.Rows[index].Cells[0].Value.ToString()), sursa.Text, beneficiar.Text, dataComanda, dataLivrare, oraLivrare.Text, adresaLivrare.Text, telefonContact.Text, telefonRezerva.Text, serviciiExtrase, int.Parse(tabelComenzi.Rows[index].Cells[11].Value.ToString()), float.Parse(avans.Text), 0, BCF.Text);


                comanda.pretOriginal = float.Parse(tabelComenzi.Rows[index].Cells[11].Value.ToString());
                comanda.serviciiOriginal = serviciiExtrase;
                updateListaComenzi();
            }
            catch { }
        }
        private void serviciiListTxt_Enter(object sender, EventArgs e)
        {
            for (int i = 0; i < tabelRetete.Rows.Count - 1; i++)
            {
                serviciiListTxt.AutoCompleteCustomSource.Add(tabelRetete.Rows[i].Cells[1].Value.ToString());
            }
        }
        private void adaugaListaServicii_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dr = dtPlat.Select("Denumire = '" + serviciiListTxt.Text + "'");

                if (dr.Length == 1)
                {
                    listaServicii.Items.Add(dr[0][1].ToString() + " - " + selectorPortii.Value.ToString() + "p");
                    updateListaComenzi();

                    serviciiListTxt.Text = "";
                    selectorPortii.Value = 0;
                }
                else
                    throw new Exception("Serviciul nu se gaseste");
            }
            catch (Exception ex)
            {
                MessageBox.Show("EROARE: " + ex.Message);
            }



        }
        private void deleteListaServ_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listaServicii.SelectedIndex;
                listaServicii.Items.RemoveAt(index);
                updateListaComenzi();


            }
            catch { }
        }

        private void stergeComanda_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esti sigur ca vrei sa stergi aceasta comanda?", "Confirmare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    foreach(DataRow dr in dtCom.Rows)
                    {
                        if(int.Parse(dr[0].ToString()) == comanda.id)
                        {
                            dr.Delete();
                            break;
                        }
                    }
                
                    clc("comenzi");
                    firebase.deleteComandaAsync(comanda.id);
                    tabelComenzi.ClearSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare: " + ex.Message);

                }
            }
        }
        private void updateListaServicii()
        {
           // for(int i = 0; i<listaServicii.Items.Count;i++)
            //{

           // }
        }
        private void actualizare_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esti sigur ca vrei sa actualizezi aceasta comanda?", "Confirmare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    //MessageBox.Show(retetar.Remove(900, 100).Length);
                    Comanda comandaActualizata = new Comanda(0, sursa.Text, beneficiar.Text, dataComanda, dataLivrare, oraLivrare.Text, adresaLivrare.Text, telefonContact.Text,
                        telefonRezerva.Text, serviciiExtrase, comanda.pretTotal, float.Parse(avans.Text), 0, BCF.Text);




                    comandaActualizata.setId(comanda.id);
                    firebase.updateComandaAsync(comandaActualizata);

                    foreach(DataRow dr in dtCom.Rows)
                    {
                        if(int.Parse(dr[0].ToString()) == comandaActualizata.id)
                        {
                            dr[1] = comandaActualizata.sursa;
                            dr[2] = comandaActualizata.beneficiar;
                            dr[3] = comandaActualizata.dataComanda.ToString("dd/MM/yyyy");
                            dr[4] = comandaActualizata.dataLivrare.ToString("dd/MM/yyyy");
                            dr[5] = comandaActualizata.ziuaLivrare;
                            dr[6] = comandaActualizata.oraLivrare;
                            dr[7] = comandaActualizata.adresa;
                            dr[8] = comandaActualizata.telefon;
                            dr[9] = comandaActualizata.telefonRezerva;
                            dr[10] = comandaActualizata.servicii;
                            dr[11] = comandaActualizata.pretTotal;
                            dr[12] = comandaActualizata.avans;
                            dr[13] = comandaActualizata.rest;
                            dr[14] = comandaActualizata.bcf;
                            break;
                        }
                    }
                    clc("comenzi");
                    tabelComenzi.ClearSelection();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare: " + ex.Message);

                }
            }
            
        }

        private void updateListaComenzi()
        {
            serviciiExtrase = "";
            comanda.pretTotal = 0;
            comanda.servicii = "";
            foreach (string serviciu in listaServicii.Items)
            {
                
                for (int i = 0; i < tabelRetete.Rows.Count - 1; i++)
                {
                    if (tabelRetete.Rows[i].Cells[1].Value.ToString() == (serviciu.Split('-')[0].Remove(serviciu.Split('-')[0].Length - 1, 1)))
                    {
                        comanda.pretTotal += float.Parse(tabelRetete.Rows[i].Cells[2].Value.ToString()) * float.Parse(serviciu.Split('-')[1].Replace(" ","").Remove(serviciu.Split('-')[1].Replace(" ", "").Length-1,1));
                        comanda.servicii += serviciu + ';';
                        serviciiExtrase += serviciu + ';';
                        //pretTotal.Text = comanda.pretTotal.ToString();
                    }
                }
            }
        }
        private void cautaComandaTxt_OnTextChange(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = tabelComenzi.DataSource;
            bs.Filter = "Beneficiar like '%" + cautaComandaTxt.text + "%' OR Data_Comanda like '%" + cautaComandaTxt.text + "%' OR Data_Livrare like '%" + cautaComandaTxt.text + "%'";
            tabelComenzi.DataSource = bs;

        }

        private void reloadComenzi_Click(object sender, EventArgs e)
        {
            // tabelComenzi.Rows.Clear();
           
            populeazaComenzi();
        }



        /*-----------------------------LISTA DE LUCRU-------------------------------*/
        
        int rowLocationPoint;
        int cellLocationPoint;
        
        private void selectDateToday(object sender, EventArgs e)
        {
            BunifuDatepicker bnf = (BunifuDatepicker)sender;
            bnf.Value = DateTime.Today;
        }
        private Panel rowPanelCreate()
        {
            Panel panel = new Panel();
            panel.Location = new Point(0,rowLocationPoint);
            rowLocationPoint += 346;
            panel.Size = new Size(1202, 346);
            //panel.Dock = DockStyle.Top;
            return panel;
        }

        private void printComandaAleasa(object sender, EventArgs e)
        {
            
        }
        private Panel cellPanelCreate(int id, string data, string ora, string benefic, string descriere)
        {
            if (cellLocationPoint > 800)
                cellLocationPoint = 0;
            
            Panel panel = new Panel();
            panel.Location = new Point(cellLocationPoint, 0);
            cellLocationPoint += 400;
            panel.Size = new Size(400, 346);
           // panel.Dock = DockStyle.Right;

            Label dataLivrare = new Label();
            dataLivrare.Location = new Point(30, 25);
            dataLivrare.Size = new Size(90, 17);
            dataLivrare.Font = new Font("Microsoft Sans Serif", 10);
            dataLivrare.ForeColor = System.Drawing.Color.White;
            dataLivrare.Text = "Data Livrare:";
            panel.Controls.Add(dataLivrare);

            Label dataLivrareComp = new Label();
            dataLivrareComp.Location = new Point(126, 25);
            dataLivrareComp.Size = new Size(90, 17);
            dataLivrareComp.Font = new Font("Microsoft Sans Serif", 10);
            dataLivrareComp.ForeColor = System.Drawing.Color.White;
            dataLivrareComp.Text = data;
            panel.Controls.Add(dataLivrareComp);

            Label oraLivrare = new Label();
            oraLivrare.Location = new Point(30, 55);
            oraLivrare.Size = new Size(84, 17);
            oraLivrare.Font = new Font("Microsoft Sans Serif", 10);
            oraLivrare.ForeColor = System.Drawing.Color.White;
            oraLivrare.Text = "Ora Livrare:";
            panel.Controls.Add(oraLivrare);

            Label oraLivrareComp = new Label
            {
                Location = new Point(126, 55),
                Size = new Size(84, 17),
                Font = new Font("Microsoft Sans Serif", 10),
                ForeColor = System.Drawing.Color.White,
                Text = ora
            };
            panel.Controls.Add(oraLivrareComp);

            Label beneficiar = new Label();
            beneficiar.Location = new Point(30, 85);
            beneficiar.Size = new Size(75, 17);
            beneficiar.Font = new Font("Microsoft Sans Serif", 10);
            beneficiar.ForeColor = System.Drawing.Color.White;
            beneficiar.Text = "Beneficiar:";
            panel.Controls.Add(beneficiar);

            Label beneficiarComp = new Label();
            beneficiarComp.Location = new Point(126, 85);
            beneficiarComp.Size = new Size(75, 17);
            beneficiarComp.Font = new Font("Microsoft Sans Serif", 10);
            beneficiarComp.ForeColor = System.Drawing.Color.White;
            beneficiarComp.Text = benefic;
            panel.Controls.Add(beneficiarComp);

            Label descriereComanda = new Label();
            descriereComanda.Location = new Point(30, 115);
            descriereComanda.Size = new Size(137, 17);
            descriereComanda.Font = new Font("Microsoft Sans Serif", 10);
            descriereComanda.ForeColor = System.Drawing.Color.White;
            descriereComanda.Text = "Descriere Comanda:";
            panel.Controls.Add(descriereComanda);

            ListBox listBox = new ListBox();
            listBox.Location = new Point(33, 155);
            listBox.Size = new Size(335, 147);
            listBox.Font = new Font("Microsoft Sans Serif", 10);
            foreach (string item in descriere.Split(';'))
            {
                listBox.Items.Add(item);
            }
            panel.Controls.Add(listBox);

            CheckBox button = new CheckBox();
            button.Location = new Point(340,20);
            button.Font = new Font("Microsoft Sans Serif", 13);
            button.ForeColor = System.Drawing.Color.White;
            button.Size = new Size(83, 40);
            //button.Text = "Printeaza";
            button.Name = id.ToString();
            //button.Click += new EventHandler(printComandaAleasa);
            panel.Controls.Add(button);


            return panel;
        }
        
        
       
        private void genereazaLsita_Click(object sender, EventArgs e)
        {
            panouListaLucru.Controls.Clear();
            rowLocationPoint = 0;
            cellLocationPoint = 0;
            int row = 0;
            Panel panel = new Panel();
            IDictionary<string, int> listaLucru = new Dictionary<string, int>();
            for (int i = 0;i< tabelComenzi.Rows.Count - 1; i++)
            {
                string []dataExtrasa = tabelComenzi.Rows[i].Cells[4].Value.ToString().Split('/');
                DateTime dt = new DateTime(int.Parse(dataExtrasa[2]), int.Parse(dataExtrasa[1]), int.Parse(dataExtrasa[0]));
                if (DateTime.Compare(dt, startDate.Value) >= 0 && DateTime.Compare(dt, stopDate.Value) <= 0)
                {
                   
                    
                    if (row == 0)
                    {
                        panel = rowPanelCreate();
                        panouListaLucru.Controls.Add(panel);
                    }
                    Panel cellPanel = cellPanelCreate(int.Parse(tabelComenzi.Rows[i].Cells[0].Value.ToString()), tabelComenzi.Rows[i].Cells[4].Value.ToString(), tabelComenzi.Rows[i].Cells[6].Value.ToString(), tabelComenzi.Rows[i].Cells[2].Value.ToString(), tabelComenzi.Rows[i].Cells[10].Value.ToString());

                    panel.Controls.Add(cellPanel);
                    row++;
                    if (row == 3)
                        row = 0;

                    string[] platouri = tabelComenzi.Rows[i].Cells[10].Value.ToString().Split(';');
                    for(int j=0;j<platouri.Length - 1;j++)
                    {
                        string denumire = platouri[j].Split('-')[0].Remove(platouri[j].Split('-')[0].Length - 1, 1);
                        int portii = int.Parse(platouri[j].Split('-')[1].Replace(" ", "").Remove(platouri[j].Split('-')[1].Replace(" ", "").Length - 1, 1));
                        

                        if (listaLucru.ContainsKey(denumire))
                            listaLucru[denumire] += portii;
                        else
                            listaLucru.Add(denumire, portii);

                    }
                    
                }
                
            }

            
        }



        private void printeazaComenziSelectate(List<int> listaComenzi)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook sheet = excel.Workbooks.Add(Type.Missing);
            Excel.Worksheet x = excel.ActiveSheet as Excel.Worksheet;
            x.Cells[1, 1].EntireRow.Font.Bold = true;
            x.Cells[1, 1] = "REZUMATUL COMENZILOR";
            IDictionary<string, int> dictionarRetete = new Dictionary<string, int>();
            IDictionary<string, int> dictionarPlatouri = new Dictionary<string, int>();
            int nr = 1;
            int linie = 3;
            int coloana = 2;
            int nrPlatouri = 0;
            foreach(int id in listaComenzi)
            {
                if(nr==4)
                {
                    nr = 1;
                    coloana = 2;
                    linie = linie + 4 + nrPlatouri;
                    nrPlatouri = 0;
                }

                DataRow[]dr = dtCom.Select("Id = " + id);
                x.Columns[coloana].ColumnWidth = 11;
                x.Columns[coloana+1].ColumnWidth = 11;

                x.Cells[linie, coloana].Font.Bold = true;
                x.Cells[linie, coloana] = "Data livrare:";
                x.Cells[linie, coloana + 1] = dr[0][4].ToString();


                x.Cells[linie + 1, coloana].Font.Bold = true;
                x.Cells[linie + 1, coloana] = "Ora livrare:";
                x.Cells[linie + 1, coloana + 1] = dr[0][6].ToString();


                x.Cells[linie + 2, coloana].Font.Bold = true;
                x.Cells[linie + 2, coloana] = "Beneficiar:";
                x.Cells[linie + 2, coloana + 1] = dr[0][2].ToString();

                x.Cells[linie + 3, coloana - 1].Font.Bold = true;
                x.Cells[linie + 3, coloana - 1] = "Descriere comanda:";

                int nrPlatouriMax = 0;
                foreach(string platou in dr[0][10].ToString().Split(';'))
                {
                    if (platou != "")
                    {
                        string numePlatou = platou.Split('-')[0].Remove(platou.Split('-')[0].Length - 1, 1);
                        int numarPortii = int.Parse(platou.Split('-')[1].Replace(" ", "").Remove((platou.Split('-')[1].Replace(" ", "").Length - 1), 1));

                        if (dictionarPlatouri.ContainsKey(numePlatou))
                            dictionarPlatouri[numePlatou] += numarPortii;
                        else
                            dictionarPlatouri.Add(numePlatou, numarPortii);

                        DataRow[] drPlatou = dtPlat.Select("Denumire = '" + numePlatou +"'");

                        foreach (string retetaExtrasa in drPlatou[0][4].ToString().Split(';'))
                        {
                            if (retetaExtrasa != "")
                            {
                                if (dictionarRetete.ContainsKey(retetaExtrasa.Split(':')[0]))
                                    dictionarRetete[retetaExtrasa.Split(':')[0]] += ( numarPortii * int.Parse(retetaExtrasa.Split(':')[1]) );
                                else
                                    dictionarRetete.Add(retetaExtrasa.Split(':')[0], numarPortii * int.Parse(retetaExtrasa.Split(':')[1]));
                            }
                        }
                    }
                    x.Cells[nrPlatouriMax++ + linie + 4, coloana] = platou;

                }
                if (nrPlatouriMax > nrPlatouri)
                    nrPlatouri = nrPlatouriMax;
                coloana = coloana + 4;
                nr++;
            }


            coloana = 2;
            linie = linie + 4 + nrPlatouri;

            x.Cells[linie, coloana - 1].EntireRow.Font.Bold = true;
            x.Cells[linie, coloana - 1] = "REZUMATUL PLATOURILOR";

            x.Cells[linie, coloana + 4] = "REZUMATUL RETETELOR";

            nr = 1;
            foreach(KeyValuePair<string,int> kvp in dictionarPlatouri)
            {
                x.Cells[linie + nr++, coloana] = kvp.Key + " - " + kvp.Value;
                
            }
            nr = 1;
            foreach (KeyValuePair<string, int> kvp in dictionarRetete)
            {
                x.Cells[linie + nr++, coloana+5] = kvp.Key + " - " + kvp.Value;
            }

            excel.Visible = true;
        }
        private void printeazaComenzi_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> comenzi = new List<int>();
                bool gasit = false;
                foreach (Control control in panouListaLucru.Controls)
                    if (control is Panel)
                        foreach (Control ctr in control.Controls)
                            if (ctr is Panel)
                                foreach (Control c in ctr.Controls)
                                    if (c is CheckBox)
                                        if (((CheckBox)c).Checked == true)
                                        {
                                            //MessageBox.Show(c.Name);
                                            comenzi.Add(int.Parse(c.Name));
                                            gasit = true;
                                        }

                if (gasit)
                {
                    printeazaComenziSelectate(comenzi);
                }
                else
                {
                    throw new Exception("Nu a fost selectat nicio comanda");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("EROARE: " + ex.Message);
            }




        }



        /*-----------------------------MATERII PRIME------------------------------*/

        MateriiPrime materiePrima;

       
        //BUTTON DE ADUAGARE IN BAZA DE DATE A UNEI MATERII PRIME
        private void adaugaMaterii_Click(object sender, EventArgs e)
        {
            try
            {
                MateriiPrime materie = new MateriiPrime(denumireMateriePrima.Text, UM.Text, float.Parse(cantitateMateriePrima.Text), float.Parse(pretMateriePrima.Text));

                DataRow[] dr = dtMateriiPrime.Select("Denumire = '" + materie.Denumire + "'");
                if (dr.Length != 0)
                    throw new Exception(materie.Denumire + " deja exista in baza de date");


                adaugareMateriePrimaTabel(materie);
                firebase.addMateriePrimaAsync(materie);
                clc("materii prime");
                tabelMateriiPrime.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("EROARE: " + ex.Message);
            }

        }
        void adaugareMateriePrimaTabel(MateriiPrime materiePrima)
        {
            dtMateriiPrime.Rows.Add(firebase.getNrMateriiPrime() + 1, materiePrima.Denumire, materiePrima.UM, materiePrima.Cantitate, materiePrima.Pret);
        }



        //BUTTON DE STERGERE A UNEI MATERII PRIME DIN BAZA DE DATE
        private void StergeMateriePrima_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esti sigur ca vrei sa stergi aceasta materie prima?", "Confirmare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    firebase.deleteMateriePrimaAsync(materiePrima.Id);

                    foreach(DataRow dr in dtMateriiPrime.Rows)
                    {
                        if(int.Parse(dr[0].ToString()) == materiePrima.Id)
                        {
                            dr.Delete();
                            break;
                        }
                    }

                    clc("materii prime");
                    tabelMateriiPrime.ClearSelection();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("EROARE: " + ex.Message);
                }
            }
        }


        private void actualizareMateriiPrime(MateriiPrime vechi, MateriiPrime nou)
        {
            

            foreach (DataRow dr in dtRetete.Rows)
            {
                string materiiPrime = dr[4].ToString();
                string materiiPrimeNoi = "";
                bool gasit = false;
                foreach(string MateriePrima in materiiPrime.Split(';'))
                {
                    string numeMateriePrima = MateriePrima.Split(':')[0];
                    if (numeMateriePrima == "")
                    {
                        break;
                    }
                    else if(numeMateriePrima == vechi.Denumire)
                    {
                        gasit = true;
                        materiiPrimeNoi += nou.Denumire+":"+ MateriePrima.Split(':')[1]+";"; 
                    }
                    else
                    {
                        materiiPrimeNoi += numeMateriePrima + ":" + MateriePrima.Split(':')[1] + ";";
                    }
                }
                if(gasit)
                {
                    Reteta ret = new Reteta(int.Parse(dr[0].ToString()), dr[1].ToString(), int.Parse(dr[2].ToString()), materiiPrimeNoi, dtMateriiPrime);
                    firebase.UpdateRetetaAsync(ret);
                    dr[3] = ret.Pret;
                    dr[4] = materiiPrimeNoi;
                }

            }
        }
        //BUTTON DE ACTUALIZARE A UNEI MATERII PRIME
        private void actualizareMateriePrima_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esti sigur ca vrei sa actualizezi aceasta materie prima?", "Confirmare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    MateriiPrime materieActualizata = new MateriiPrime(materiePrima.Id, denumireMateriePrima.Text, UM.Text, float.Parse(cantitateMateriePrima.Text), float.Parse(pretMateriePrima.Text));

                    DataRow[] drr = dtMateriiPrime.Select("Denumire = '" + materieActualizata.Denumire + "'");
                    if (drr.Length != 0 && materieActualizata.Denumire != materiePrima.Denumire)
                        throw new Exception(materieActualizata.Denumire + " deja exista in baza de date");


                    firebase.UpdateMateriePrimaAsync(materieActualizata);

                    foreach(DataRow dr in dtMateriiPrime.Rows)
                    {
                        if(int.Parse(dr[0].ToString()) == materieActualizata.Id)
                        {
                            dr[1] = materieActualizata.Denumire;
                            dr[3] = materieActualizata.Cantitate;
                            dr[2] = materieActualizata.UM;
                            dr[4] = materieActualizata.Pret;

                            break;
                        }
                    }
                    actualizareMateriiPrime(materiePrima, materieActualizata);

                    clc("materii prime");
                    tabelMateriiPrime.ClearSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("EROARE: " + ex.Message);
                }
            }
        }



        private void tabelMateriiPrime_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int index = e.RowIndex;

                int id = int.Parse(tabelMateriiPrime.Rows[index].Cells[0].Value.ToString());
                denumireMateriePrima.Text = tabelMateriiPrime.Rows[index].Cells[1].Value.ToString();
                cantitateMateriePrima.Text = tabelMateriiPrime.Rows[index].Cells[3].Value.ToString();
                UM.Text = tabelMateriiPrime.Rows[index].Cells[2].Value.ToString();
                pretMateriePrima.Text = tabelMateriiPrime.Rows[index].Cells[4].Value.ToString();

                materiePrima = new MateriiPrime(id, denumireMateriePrima.Text, UM.Text, float.Parse(cantitateMateriePrima.Text), float.Parse(pretMateriePrima.Text));
            }
            catch { }
        }


        private void bunifuTextbox2_OnTextChange(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = tabelMateriiPrime.DataSource;
            bs.Filter = "Denumire like '%" + bunifuTextbox2.text + "%'";
            tabelRetete.DataSource = bs;
        }


        /*-----------------------------RETETE------------------------------*/


        Reteta reteta = new Reteta();

        private void MateriePrimaTxt_Enter(object sender, EventArgs e)
        {
            for (int i = 0; i < tabelMateriiPrime.Rows.Count - 1; i++)
            {
                MateriePrimaTxt.AutoCompleteCustomSource.Add(tabelMateriiPrime.Rows[i].Cells[1].Value.ToString());
            }
        }

        private void adaugaListaMateriiPrimeBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (MateriePrimaTxt.Text == "" || masuraMateriePrima.Text == "")
                    throw new Exception("Campurile nu au fost completate");
                else
                {
                    DataRow[] dr = dtMateriiPrime.Select("Denumire = '" + MateriePrimaTxt.Text + "'");

                    if(dr.Length == 1)
                    {
                        listaMateriiPrime.Items.Add(dr[0][1].ToString() + " - " + masuraMateriePrima.Text + dr[0][2].ToString());
                    }
                    else
                        throw new Exception("Materia prima nu exista");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROARE: " + ex.Message);
                return;
            }
        }


        void adaugaRetetaTabel(Reteta reteta)
        {
            dtRetete.Rows.Add(firebase.getNrRetete() + 1, reteta.Denumire, reteta.Gramaj,reteta.Pret, dicToString(reteta.Materii_Prime));
        }
        private void adaugaRetetaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                reteta = new Reteta(1, denumireReteta.Text, int.Parse(gramajReteta.Text), listaMateriiPrime, dtMateriiPrime);

                DataRow[] dr = dtRetete.Select("Denumire = '" + reteta.Denumire + "'");
                if (dr.Length != 0)
                    throw new Exception(reteta.Denumire + " deja exista in baza de date");

                adaugaRetetaTabel(reteta);

                firebase.addRetetaAsync(reteta);
                clc("retete");
                tabelPtRetete.ClearSelection();
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROARE: " + ex.Message);
                return;
            }
        }


        private void tabelPtRetete_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {


                int index = e.RowIndex;

                listaMateriiPrime.Items.Clear();

                int id = int.Parse(tabelPtRetete.Rows[index].Cells[0].Value.ToString());
                denumireReteta.Text = tabelPtRetete.Rows[index].Cells[1].Value.ToString();
                gramajReteta.Text = tabelPtRetete.Rows[index].Cells[2].Value.ToString();

                string materiiPrime = tabelPtRetete.Rows[index].Cells[4].Value.ToString();
                foreach (string materiePrima in materiiPrime.Split(';'))
                {
                    if (materiePrima != "")
                    {
                        string denumire = materiePrima.Split(':')[0];
                        float gramaj = float.Parse(materiePrima.Split(':')[1]);
                        string um = "";
                        foreach (DataRow dr in dtMateriiPrime.Rows)
                        {
                            if (dr[1].ToString() == denumire)
                            {
                                um = dr[2].ToString();
                                break;
                            }
                        }
                        if (um != "")
                            listaMateriiPrime.Items.Add(denumire + " - " + gramaj + um);
                    }
                }

                reteta = new Reteta(id, denumireReteta.Text, int.Parse(gramajReteta.Text), listaMateriiPrime, dtMateriiPrime);

            }
            catch { }
        }


        private void actualizarePlatouri(Reteta veche, Reteta noua)
        {
            foreach(DataRow dr in dtPlat.Rows)
            {
                string reteta = dr[4].ToString();
                string retActualizata = "";
                bool gasit = false;
                foreach (string ret in reteta.Split(';'))
                {

                    if (ret != "")
                    {
                        if (reteta.Split(':')[0] == veche.Denumire)
                        {
                            retActualizata += ret.Replace(veche.Denumire, noua.Denumire) + ";";
                            gasit = true;
                        }
                        else
                            retActualizata += ret;
                    }
                    else
                        break;
                }
                if(gasit)
                {
                    Produs prod = new Produs(int.Parse(dr[0].ToString()),dr[1].ToString(),float.Parse(dr[2].ToString()),int.Parse(dr[3].ToString()),retActualizata);
                    dr[4] = retActualizata;
                    firebase.updateProdusAsync(prod);
                }

            }
        }
        private void UpdateReteta_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esti sigur ca vrei sa actualizezi aceasta reteta?", "Confirmare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //Verifica campuri
                    Reteta retActualizata = new Reteta(reteta.Id, denumireReteta.Text, int.Parse(gramajReteta.Text), listaMateriiPrime, dtMateriiPrime);

                    DataRow[] drr = dtRetete.Select("Denumire = '" + retActualizata.Denumire + "'");
                    if (drr.Length != 0 && retActualizata.Denumire != reteta.Denumire)
                        throw new Exception(retActualizata.Denumire + " deja exista in baza de date");

                    firebase.UpdateRetetaAsync(retActualizata);

                    foreach (DataRow dr in dtRetete.Rows)
                    {
                        if (int.Parse(dr[0].ToString()) == reteta.Id)
                        {
                            dr[1] = retActualizata.Denumire;
                            dr[2] = retActualizata.Gramaj;
                            dr[3] = retActualizata.Pret;
                            dr[4] = dicToString(retActualizata.Materii_Prime);
                            break;
                        }
                    }
                    actualizarePlatouri(reteta, retActualizata);
                    clc("retete");
                    tabelPtRetete.ClearSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("EROARE: " + ex.Message);
                }
            }
        }

        private void StergeReteta_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esti sigur ca vrei sa stergi reteta " + reteta.Denumire + "?", "Confirmare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    firebase.deleteRetetaAsync(reteta.Id);

                    foreach (DataRow dr in dtRetete.Rows)
                    {
                        if (int.Parse(dr[0].ToString()) == reteta.Id)
                        {
                            dr.Delete();
                            break;
                        }
                    }
                    clc("retete");
                    tabelPtRetete.ClearSelection();
                }
                 
                catch (Exception ex)
                {
                    MessageBox.Show("EROARE: " + ex.Message);
                }
            }
        }

        private void stergeMateriePrimareRetetaBTN_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listaMateriiPrime.SelectedIndex;
                listaMateriiPrime.Items.RemoveAt(index);
            }
            catch
            {

            }
        }

        private void printeazaRetetaBTN_Click(object sender, EventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook sheet = excel.Workbooks.Add(Type.Missing);
            Excel.Worksheet x = excel.ActiveSheet as Excel.Worksheet;

            x.Columns[1].ColumnWidth = 13;
            x.Cells[1, 1].Font.Bold = true;
            x.Cells[1, 1] = "Denumire:";
            x.Cells[1, 2] = reteta.Denumire;

            x.Cells[2, 1].Font.Bold = true;
            x.Cells[2, 1] = "Gramaj: ";
            x.Cells[2, 2] = reteta.Gramaj;

            x.Cells[3, 1].Font.Bold = true;
            x.Cells[3, 1] = "Pret: ";
            x.Cells[3, 2] = reteta.Pret;

            x.Cells[4, 1].Font.Bold = true;
            x.Cells[4, 1] = "Materii Prime: ";

            int linie = 4;
            foreach(KeyValuePair<string,float> kvp in reteta.Materii_Prime)
            {
                DataRow[] dr = dtMateriiPrime.Select("Denumire = '" + kvp.Key + "'");
                string um = "";
                if (dr.Length == 1)
                    um = dr[0][2].ToString();
                x.Cells[linie++, 2] = kvp.Key + " - " + kvp.Value.ToString() + um;
            }

            excel.Visible = true;
        }

        private void bunifuTextbox1_OnTextChange_1(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = tabelPtRetete.DataSource;
            bs.Filter = "Denumire like '%" + bunifuTextbox1.text + "%'";
            tabelRetete.DataSource = bs;
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace DaneZPlikuOkienko
{
    public partial class DaneZPliku : Form
    {
        private string[][] atrType;
        private string[][] wczytaneDane;

        public DaneZPliku()
        {
            InitializeComponent();
        }

        private void btnWybierzPlik_Click(object sender, EventArgs e)
        {
            DialogResult wynikWyboruPliku = ofd.ShowDialog(); // wybieramy plik
            if (wynikWyboruPliku != DialogResult.OK)
                return;

            tbSciezkaDoPlikuSystemu.Text = ofd.FileName;
            string trescPliku = System.IO.File.ReadAllText(ofd.FileName); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            wczytaneDane = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                wczytaneDane[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    wczytaneDane[i][j] = cyfra;
                }
            }

            tbWynik.Text = TablicaDoString(wczytaneDane);
        }

        private void btnWybierzPlikZTypami_Click(object sender, EventArgs e)
        {
            DialogResult wynikWyboruPliku = ofd.ShowDialog(); // wybieramy plik
            if (wynikWyboruPliku != DialogResult.OK)
                return;

            tbSciezkaDoPlikuZTypami.Text = ofd.FileName;
            string trescPliku = System.IO.File.ReadAllText(ofd.FileName).Trim();

            string[] wiersze = trescPliku.Split(new char[] { '\n' });
            atrType = new string[wiersze.Length][];
            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();
                atrType[i] = wiersz.Split(new char[] { ' ' });
            }

            tbAtrType.Text = TablicaDoString(atrType);
        }

        public string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                {
                    wynik += tab[i][j].ToString() + " ";
                }
                wynik = wynik.Trim() + Environment.NewLine;
            }

            return wynik;
        }


        public double StringToDouble(string liczba)
        {
            double wynik; liczba = liczba.Trim();
            if (!double.TryParse(liczba.Replace(',', '.'), out wynik) && !double.TryParse(liczba.Replace('.', ','), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do double");

            return wynik;
        }


        public int StringToInt(string liczba)
        {
            int wynik;
            if (!int.TryParse(liczba.Trim(), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do int");

            return wynik;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            // Tablica z wczytanymi danymi dostępna poniżej
            // this.wczytaneDane;

            // Tablica z typami atrybutów
            // this.atrType;

            //
            // Przykład konwersji string to double
            string sLiczbaDouble = "1.5";
            double dsLiczbaDouble = StringToDouble(sLiczbaDouble);

            // Przykład konwersji string to int
            string sLiczbaInt = "1";
            int iLiczbaInt = StringToInt(sLiczbaInt);

            /****************** Miejsce na rozwiązanie *********************************/

            //Zadanie 3a
            string[][] dane = this.wczytaneDane;
            List<string> klasy_decyzyjne = new List<string>();
            for (int i = 0; i < dane.Length; i++)
            {
                if (klasy_decyzyjne.Contains(dane[i][dane[i].Length - 1]))
                {
                    continue;
                }
                else
                {
                    klasy_decyzyjne.Add(dane[i][dane[i].Length - 1]);
                }
            }

            string wynik_3a = "Klasy decyzyjne:\n";
            for (int i = 0; i < klasy_decyzyjne.Count; i++)
            {
                wynik_3a += klasy_decyzyjne[i] + "\n";
            }

            MessageBox.Show(wynik_3a, "Zadanie 3a");
            //Zadanie 3b
            List<string> ilosc = new List<string>();


            for (int j = 0; j < klasy_decyzyjne.Count; j++)
            {
                int liczba = 0;
                for (int i = 0; i < dane.Length; i++)
                {
                    if (dane[i][dane[i].Length - 1].Equals(klasy_decyzyjne[j]))
                    {
                        liczba++;
                    }

                }
                ilosc.Add(liczba.ToString());
            }

            string wynik_3b = "Ilość elementów: "+dane.Length+"\n";
            for (int i = 0; i < ilosc.Count; i++)
            {
                wynik_3b+="Ilosc " + klasy_decyzyjne[i] + ": " + ilosc[i]+"\n";
            }

            MessageBox.Show(wynik_3b, "Zadanie 3b");
            //Zadanie 3c
            string[][] atrybuty = this.atrType;
            List<int> indeksy_kolumn = new List<int>();
            List<double> maks = new List<double>();
            List<double> min = new List<double>();
            for (int i = 0; i < atrybuty.Length; i++)
            {
                if (atrybuty[i][atrybuty[i].Length - 1] == "n")
                {
                    indeksy_kolumn.Add(i);
                }
            }
            Double mini = 0, max = 0;
            for (int i = 0; i < indeksy_kolumn.Count; i++)
            {
                mini = StringToDouble(dane[0][indeksy_kolumn[i]]);
                max = StringToDouble(dane[0][indeksy_kolumn[i]]);
                for (int j = 0; j < dane.Length; j++)
                {
                    Double aktualna = StringToDouble(dane[j][indeksy_kolumn[i]]);
                    if (aktualna < mini)
                    {
                        mini = aktualna;
                    }
                    if (aktualna > max)
                    {
                        max = aktualna;
                    }
                }
                min.Add(mini);
                maks.Add(max);
            }

            string wynik_3c = "";
            for (int i = 0; i < indeksy_kolumn.Count; i++)
            {
                int kolumna_ind = indeksy_kolumn[i] + 1;
                wynik_3c += "Kolumna " + kolumna_ind+ ": maks: " + maks[i] + " min: " + min[i]+"\n";

            }

            MessageBox.Show(wynik_3c,"zadanie 3c");

            //Zadanie 3d i 3e
            List<int>wyniki_unikal=new List<int>();
            List<string>unikalne=new List<string>();
            List<List<string>>unik_wartosci=new List<List<string>>();

            //Debug.Print(dane.Length.ToString());
            string wynik_3d = "";
            try
            {
                for (int i = 0; i < dane[0].Length; i++)
                {
                    for (int j = 0; j < dane.Length; j++)
                    {
                        if (unikalne.Contains(dane[j][i]))
                        {
                            continue;
                        }
                        else
                        {
                            unikalne.Add(dane[j][i]);
                        }
                    }

                    wyniki_unikal.Add(unikalne.Count);
                    List<string>c=new List<string>(unikalne);
                    unik_wartosci.Add(c);
                    unikalne.Clear();
                }

                
                for (int i = 0; i < wyniki_unikal.Count - 1; i++)
                {
                    int c = i + 1;
                    wynik_3d += "Ilosc unikalnych dla kolumny: " + c + " : " + wyniki_unikal[i] + "\n";
                }
            }
            catch (Exception f)
            {
                wynik_3d = f.Message + "\n";
            }

            MessageBox.Show(wynik_3d, "zadanie 3d");

            string wynik_3e = "";
            for (int i = 0; i < unik_wartosci.Count-1; i++)
            {
                int kolumna = i + 1;
                wynik_3e += "Unikalne wartości dla kolumny: " + kolumna + ": ";
                for (int j = 0; j < unik_wartosci[i].Count; j++)
                {
                    wynik_3e += unik_wartosci[i][j] + ", ";
                }

                wynik_3e += "\n";
            }

            MessageBox.Show(wynik_3e, "Zadanie 3e");
            //Zadanie 3f
            //Liczenie średniej
            string wynik_3f = "";
            List<double>srednia=new List<double>();
            try
            {
                for (int i = 0; i < indeksy_kolumn.Count; i++)
                {
                    double liczba = 0;
                    int ile = dane.Length;
                    for (int j = 0; j < dane.Length; j++)
                    {
                        liczba += StringToDouble(dane[j][indeksy_kolumn[i]]);
                    }

                    srednia.Add(liczba / ile);
                }


                //liczenie wariancji
                List<double> wariancja = new List<double>();
                for (int i = 0; i < indeksy_kolumn.Count; i++)
                {
                    int ile = dane.Length;
                    double gora = 0;
                    for (int j = 0; j < dane.Length; j++)
                    {
                        gora += Math.Pow((StringToDouble(dane[j][indeksy_kolumn[i]]) - srednia[i]), 2);
                    }

                    double wyn = gora / dane.Length;
                    wariancja.Add(wyn);
                }


                for (int i = 0; i < wariancja.Count; i++)
                {
                    wynik_3f += "Kolumna " + indeksy_kolumn[i] + ": Odchylenie: " + Math.Sqrt(wariancja[i]) + "\n";
                }
            }
            catch (Exception w)
            {
                wynik_3f = "Nie udało się skonwertować atrybutów do double" + "\n";
            }

            //wariancja z klas decyzyjnych
            List<double> decyz_zmiana = new List<double>();
            string wynik_decyzyjne="";
            try
            {
                for (int i = 0; i < dane.Length; i++)
                {
                    double liczba = 0;
                    bool czyUdane = double.TryParse(dane[i][dane[i].Length - 1], out liczba);
                    if (czyUdane)
                    {
                        decyz_zmiana.Add(liczba);
                    }
                    else
                    {
                        wynik_decyzyjne = "Nie udało się skonwertować klas decyzyjnych";
                       
                    }
                }
                
                if (decyz_zmiana.Count != 0)
                {
                    double suma_dec = 0;
                    double sred = 0;
                    for (int i = 0; i < decyz_zmiana.Count; i++)
                    {
                        suma_dec += decyz_zmiana[i];
                    }

                    sred = suma_dec / decyz_zmiana.Count;
                    double gora_wariancja = 0;
                    double warian = 0;
                    for (int i = 0; i < decyz_zmiana.Count; i++)
                    {
                        gora_wariancja += Math.Pow((decyz_zmiana[i] - sred), 2);
                    }

                    warian = gora_wariancja / decyz_zmiana.Count;
                    wynik_decyzyjne = "Odchylenie dla klas decyzyjnych: " + Math.Sqrt(warian) + "\n";

                }
            }
            catch (Exception t)
            {
                wynik_decyzyjne = "Nie udało się skonwertować klas decyzyjnych";
            }
            wynik_3f += wynik_decyzyjne;
            MessageBox.Show(wynik_3f, "Zadanie 3f");

            
            /****************** Koniec miejsca na rozwiązanie ********************************/
        }
    }
}
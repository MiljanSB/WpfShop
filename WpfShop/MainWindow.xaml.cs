using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SortedDictionary<int, Stavka> korpa = new SortedDictionary<int, Stavka>();
        public MainWindow()
        {
            InitializeComponent();
        }

        //PRIKAZI KUPCE
        private void PrikaziKupce()
        {
            ComboBox1.ItemsSource = KupacDal.VratiKupce();
        }

        //PRIKAZI PROIZVODE
        private void PrikaziProizvode()
        {
            ListBox1.ItemsSource = ProizvodDal.VratiProizvode();
        }

        //DOZVOLI KUPOVINU
        private void DozvoliKupovinu(bool dozvola)
        {
            GroupBox1.IsEnabled = dozvola;
            ButtonNova.IsEnabled = !dozvola;
        }

        //RESETUJ
        private void Resetuj()
        {
            korpa.Clear();
            DataGrid1.Items.Clear();
            ComboBox1.SelectedIndex = -1;
            ListBox1.SelectedIndex = -1;
            TextBlockCena.Text = "";
            TextBoxKolicina.Text = "1";
            TextBlockVrednost.Text = "";
        }

        //DODAJ U KORPU
        private void DodajUkorpu(Stavka st)
        {
            int id = st.Proizvod.ProizvodId;
            if (korpa.ContainsKey(id))
            {
                //increment ako se nalazi
                korpa[id].Kolicina += st.Kolicina;
            }
            else
            {
                korpa.Add(id, st);
            }
        }

        //VREDNOST KORPE
        private decimal VrednostKorpe()
        {
            decimal suma = 0;

            foreach (Stavka st in korpa.Values)
            {
                suma += st.Proizvod.Cena * st.Kolicina;
            }

            return suma;
        }

        //STAMPAJ KORPU
        private void StampajKorpu()
        {
            DataGrid1.Items.Clear();
            foreach (Stavka st in korpa.Values)
            {
                DataGrid1.Items.Add(st);
            }

            TextBlockVrednost.Text = "Vrednost: " + VrednostKorpe();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziKupce();
            PrikaziProizvode();
            DozvoliKupovinu(false);
        }

        private void ButtonNova_Click(object sender, RoutedEventArgs e)
        {
            Resetuj();
            DozvoliKupovinu(true);

        }

        private void ButtonOdustani_Click(object sender, RoutedEventArgs e)
        {
            Resetuj();
            DozvoliKupovinu(false);
        }

        private void ButtonUbaciUkorpu_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox1.SelectedIndex >-1)
            {

                if (int.TryParse(TextBoxKolicina.Text,out int kolicina))
                {
                    Proizvod p = ListBox1.SelectedItem as Proizvod;
                    Stavka st = new Stavka { Proizvod =p, Kolicina = kolicina };
                    DodajUkorpu(st);
                    StampajKorpu();
                }
                else
                {
                    MessageBox.Show("Kolicina je ceo broj");
                    TextBoxKolicina.Text = "1";
                    TextBoxKolicina.Focus();
                }
            }
            else
            {
                MessageBox.Show("Odaberi proizvod");
            }
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox1.SelectedIndex > -1)
            {
                Proizvod p = ListBox1.SelectedItem as Proizvod;

                TextBlockCena.Text = p.Cena.ToString();
                TextBoxKolicina.Text = "1";
                TextBoxKolicina.Focus();
            }
        }

        private void ButtonIzbaci_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            int id = (int)b.CommandParameter;

            korpa.Remove(id);
            StampajKorpu();
        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            int id = (int)b.CommandParameter;

            Stavka st = korpa[id];

            Window1 w1 = new Window1();
            w1.Title = "Promeni kolicinu";
            w1.TextBlock1.Text = st.Proizvod.Naziv;
            w1.TextBoxKolicina.Text = st.Kolicina.ToString();

            if (w1.ShowDialog() == true)
            {
                st.Kolicina = int.Parse(w1.TextBoxKolicina.Text);
                StampajKorpu();
            }
        }

        private void ButtonKupi_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox1.SelectedIndex > -1)
            {
                if (korpa.Count >0)
                {
                    Kupac k = ComboBox1.SelectedItem as Kupac;
                    int rezultat = KupovinaDal.UbaciKupovinu(korpa, k);

                    if (rezultat == 0)
                    {
                        DozvoliKupovinu(false);
                        MessageBox.Show("Podaci sacuvani");
                    }
                    else
                    {
                        MessageBox.Show("Greska pri cuvanju podataka");
                    }
                }
                else
                {
                    MessageBox.Show("Korpa je prazna");
                }
            }
            else
            {
                MessageBox.Show("Odaberi kupca");
            }
        }
    }
}

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
using System.Windows.Shapes;

namespace WpfShop
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            TextBlockPoruka.Text = "";
            string ime = TextBoxKorisnickoIme.Text;
            string lozinka = PasswordBox1.Password;

            Zaposleni z = ZaposleniDal.NadjiZaposlenog(ime, lozinka);

            if (z != null)
            {
                MainWindow mw = new MainWindow();
                mw.Title = z.Ime + " " + z.Prezime;
                mw.ShowDialog();
            }
            else
            {
                TextBlockPoruka.Text = "Neispravni podaci";
            }
        }
    }
}

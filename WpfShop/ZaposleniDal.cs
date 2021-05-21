using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfShop
{
    static class ZaposleniDal
    {
        public static Zaposleni NadjiZaposlenog(string korisnickoIme,string lozinka)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnProdavnica))
            {
                using (SqlCommand komanda = new SqlCommand("SELECT * FROM Zaposleni",konekcija))
                {

                    try
                    {
                        konekcija.Open();
                        SqlDataReader dr =komanda.ExecuteReader();
                        while (dr.Read())
                        {
                            Zaposleni z = new Zaposleni {
                                ZaposleniId = dr.GetInt32(0),
                                Ime = dr.GetString(1),
                                Prezime = dr.GetString(2),
                                KorisnickoIme = dr.GetString(3).Trim().ToLower(),
                                Lozinka = dr.GetString(4).Trim().ToLower()
                            };

                            if (z.KorisnickoIme == korisnickoIme.Trim().ToLower() && z.Lozinka==lozinka.Trim().ToLower())
                            {
                                return z;
                            }                           

                        }
                        return null;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }
    }
}

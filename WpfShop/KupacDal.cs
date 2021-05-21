using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfShop
{
    static class KupacDal
    {
        public static List<Kupac> VratiKupce()
        {
            List<Kupac> listaKupaca = new List<Kupac>();


            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnProdavnica))
            {
                using (SqlCommand komanda = new SqlCommand("SELECT * FROM Kupac", konekcija))
                {
                    try
                    {
                        konekcija.Open();
                        SqlDataReader dr = komanda.ExecuteReader();
                        while (dr.Read())
                        {
                            Kupac k = new Kupac
                            {
                                KupacId = dr.GetInt32(0),
                                Ime = dr.GetString(1),
                                Prezime = dr.GetString(2)
                            };
                            listaKupaca.Add(k);
                        }

                        return listaKupaca;
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

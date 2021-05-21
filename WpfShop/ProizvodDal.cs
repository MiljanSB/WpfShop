using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfShop
{
    static class ProizvodDal
    {

        public static List<Proizvod> VratiProizvode()
        {
            List<Proizvod> listaProizvoda = new List<Proizvod>();


            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnProdavnica))
            {
                using (SqlCommand komanda = new SqlCommand("SELECT * FROM Proizvod", konekcija))
                {
                    try
                    {
                        konekcija.Open();
                        SqlDataReader dr = komanda.ExecuteReader();
                        while (dr.Read())
                        {
                            Proizvod p = new Proizvod
                            {
                                ProizvodId = dr.GetInt32(0),
                                Naziv = dr.GetString(1),
                                Cena = dr.GetDecimal(2)
                            };
                            listaProizvoda.Add(p);
                        }

                        return listaProizvoda;
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

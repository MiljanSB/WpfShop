using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfShop
{
    static class KupovinaDal
    {
        public static int UbaciKupovinu(SortedDictionary<int,Stavka> korpa, Kupac k)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnProdavnica))
            {
                SqlTransaction tran = null;
                SqlParameter paramID = new SqlParameter("@KupovinaId", SqlDbType.Int);
                paramID.Direction = ParameterDirection.Output;

                try
                {
                    int KupovinaId = -1;
                    konekcija.Open();
                    tran = konekcija.BeginTransaction();//sve ili nista

                    using (SqlCommand komanda1 = new SqlCommand("UbaciKupovinu", konekcija,tran))
                    {
                        komanda1.CommandType = CommandType.StoredProcedure;
                        komanda1.Parameters.AddWithValue("@KupacId", k.KupacId);
                        komanda1.Parameters.Add(paramID);
                        komanda1.ExecuteNonQuery();
                        KupovinaId = (int)paramID.Value;
                    }

                    using (SqlCommand komanda2 = new SqlCommand("UbaciStavkuKupovine", konekcija,tran))
                    {
                        foreach (Stavka st in korpa.Values)
                        {
                            komanda2.Parameters.Clear(); //da ne ostanu stari par
                            komanda2.CommandType = CommandType.StoredProcedure;
                            komanda2.Parameters.AddWithValue("@KupovinaId", KupovinaId);
                            komanda2.Parameters.AddWithValue("@ProizvodId", st.Proizvod.ProizvodId);
                            komanda2.Parameters.AddWithValue("@Kolicina", st.Kolicina);

                            komanda2.ExecuteNonQuery();
                        }
                    }
                    tran.Commit(); //commit ako prodje sve
                    return 0;
                }
                catch (Exception )
                {
                    tran.Rollback(); //unistavaju se uspesno izvrs komande
                    return -1;
                }
                
            }
        }
    }
}

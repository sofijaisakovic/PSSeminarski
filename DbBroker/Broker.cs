using Domain;
using Konfiguracija;
using Microsoft.Data.SqlClient;

namespace DbBroker
{
    public class Broker
    {
        private SqlConnection konekcija;
        private SqlTransaction transakcija;

        public void OtvoriKonekciju()
        {
            string konekcijaString = Podaci.UcitajPodatke().KonekcijaString;
            konekcija = new SqlConnection(konekcijaString);
            konekcija.Open();
        }
        public void ZatvoriKonekciju()
        {
            if (konekcija != null)
            {
                konekcija.Close();
                konekcija = null;
            }
        }
        public void ZapocniTransakciju()
        {
           transakcija = konekcija.BeginTransaction();
        }
        public void Commit()
        {
            transakcija.Commit();
        }
        public void Rollback()
        {
            transakcija.Rollback();
        }

        public int Add(OpstiDomenskiObjekat odo)
        {
            string upit = $"INSERT INTO {odo.TableName} ({odo.InsertColumns}) VALUES ({odo.InsertValues}); " +
                $"SELECT CAST(SCOPE_IDENTITY() AS int);"; //vrati novi id koji je baza sama dodelila pri unosu
            //SCOPE_IDENTITY() je SQL funkcija koja vrati poslednji dodeljeni id iz unosa.
            SqlCommand komanda = new SqlCommand(upit, konekcija, transakcija);
            object rezultat = komanda.ExecuteScalar();

            int id = 0;
            //SCOPE_IDENTITY() vrati NULL kad tabela nema IDENTITY kolonu (StavkaPrijave, PonudaTreninga). Tada ExecuteScalar() vrati DBNull.Value
            if ( rezultat != null && rezultat!=DBNull.Value)
            {
                id = (int)rezultat;
            }
            return id;
        }

        public List<OpstiDomenskiObjekat> GetAll(OpstiDomenskiObjekat odo)
        {
            string upit = $"SELECT * FROM {odo.TableName} {odo.JoinCondition}";
            SqlCommand komanda = new SqlCommand(upit, konekcija, transakcija);
            SqlDataReader reader = komanda.ExecuteReader();
            List<OpstiDomenskiObjekat> lista = odo.GetObjectsFromReader(reader);
            reader.Close();
            return lista;
        }
        


    }
}

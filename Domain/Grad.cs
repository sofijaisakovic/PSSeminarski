using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Grad : OpstiDomenskiObjekat
    {
        public int IdGrad { get; set; }
        public string Naziv {  get; set; }
        public override string TableName => "Grad";

        public override string InsertColumns => "naziv";
        public override string InsertValues => $"'{Naziv}'";

        //' ' navodnici za SQL parametre
        public override string UpdateValues => $"naziv = '{Naziv}' ";

        //brojevi idu bez navodnika!
        public override string PrimaryKeyCondition => $"idGrad = {IdGrad}";

        //nema spoljnog kljuca
        public override string JoinCondition => "";

        public override List<OpstiDomenskiObjekat> GetObjectsFromReader(SqlDataReader reader)
        {
            List<OpstiDomenskiObjekat> lista = new List<OpstiDomenskiObjekat> ();
            while (reader.Read())
            {
                Grad g = new Grad
                {
                    IdGrad = (int)reader["idGrad"],
                    Naziv = (string)reader["naziv"]
                };
                lista.Add(g);
            }
            return lista;
        }

        public override string? ToString()
        {
            return Naziv;
        }
    }
}

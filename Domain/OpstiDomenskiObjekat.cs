using Microsoft.Data.SqlClient;

namespace Domain
{
    public abstract class OpstiDomenskiObjekat
    {
        public abstract String TableName { get;  }
        public abstract String InsertColumns { get;  }
        // Vrednosti za unos, istim redosledom kao InsertColumns (npr. "'Beograd'")
        public abstract String InsertValues { get;  }
        // SET deo za izmenu (npr. "naziv = 'Beograd'")
        public abstract String UpdateValues { get;  }
        // Uslov za pronalazak JEDNOG sloga po primarnom ključu (npr. "idGrad = 1")
        public abstract String PrimaryKeyCondition { get;  }
        public abstract String JoinCondition { get;  }
        // Uslov za pretragu po kriterijumu — postavlja se spolja pre pretrage
        public String SearchCondition { get; set; } = "";
        // Sklapanje liste objekata iz rezultata upita (SqlDataReader)
        public abstract List<OpstiDomenskiObjekat> GetObjectsFromReader(SqlDataReader reader);


    }
}

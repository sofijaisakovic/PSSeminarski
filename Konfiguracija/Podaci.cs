namespace Konfiguracija
{
    public class Podaci
    {
        public string KonekcijaString {  get; set; }
        public static Podaci UcitajPodatke()
        {
            Podaci podaci = new Podaci
            {
                KonekcijaString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EvidencijaTreninga;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30"
            };
            return podaci;
        }
    }
}

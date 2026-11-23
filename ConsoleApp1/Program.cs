using DbBroker;
using Domain;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Broker broker = new Broker();
            broker.OtvoriKonekciju();
            Grad noviGrad = new Grad { Naziv = "Beograd" };
            int noviId = broker.Add(noviGrad);
            Console.WriteLine($"Dodat grad, baza mu je dodelila id = {noviId}");


            Console.WriteLine("\nSvi gradovi u bazi:");
            List<OpstiDomenskiObjekat> gradovi = broker.GetAll(new Grad());
            foreach(OpstiDomenskiObjekat odo in gradovi)
            {
                Grad g = (Grad)odo;
                Console.WriteLine($"{g.Naziv} {g.IdGrad}");
            }

            broker.ZatvoriKonekciju();

        }
    }
}

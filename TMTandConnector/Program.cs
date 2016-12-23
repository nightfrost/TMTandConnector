using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TMTandConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            string yesno;
            string connectionString = "Data Source = " + args[0] + "; Initial Catalog = " + args[1] + "; User ID = " + args[2] + "; Password = " + args[3];
            string queryStatement;

            Console.WriteLine("Følgende parametre er blevet givet:");
            args.ToList().ForEach(Console.WriteLine);
            Console.WriteLine("Passer ovenstående? Y/N");
            yesno = Console.ReadLine().ToUpper();

            if (yesno == "N")
            {
                Environment.Exit(0);
            }

            Console.WriteLine("Fortsætter med at oprette forbindelse til Database.");
            Thread.Sleep(2000);

            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    _con.Open();
                    do
                    {
                        Console.WriteLine("Indtast query:\n");
                        queryStatement = Console.ReadLine();
                        Console.WriteLine("Query som bliver brugt: {0}", queryStatement);
                        using (SqlCommand command = new SqlCommand(queryStatement, _con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.WriteLine(reader.GetValue(i));
                                    }
                                    Console.WriteLine();
                                }
                            }
                        }

                    } while (true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

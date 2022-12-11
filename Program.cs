using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XmlReader;

namespace sqltest
{
    class Program
    {
        public static List<Customer> customerList = new List<Customer>();  
        static void Main(string[] args)
        {
            const string connString = "Server=(localdb)\\mssqllocaldb;Database=BankClientsAPIDb;Trusted_Connection=True;MultipleActiveResultSets=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    Console.WriteLine("\nGetting customer data:");
                    Console.WriteLine("=========================================\n");
                    Console.OutputEncoding = new UTF8Encoding();


                    connection.Open();

                    string sql = "SELECT * FROM [dbo].[Clients]";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 
                                    reader.GetDateTime(3), reader.GetString(4), reader.GetInt32(5), 
                                    reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8));
                                customerList.Add(new Customer
                                {
                                    Id = reader.GetInt32(0),
                                    HebrewName = reader.GetString(1),
                                    EnglishName = reader.GetString(2),
                                    DateOfBirth = reader.GetDateTime(3),
                                    SSN = reader.GetString(4),
                                    CityId = reader.GetInt32(5),
                                    BankBranch = reader.GetInt32(6),
                                    AccountNumber = reader.GetString(7),
                                    BankCode = reader.GetInt32(8)
                                }) ;

                            } 
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            WriteXML(customerList);
            ReadXML();
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine();
        }
        public static void WriteXML(List<Customer> customers)
        {
            var serializer = new XmlSerializer(typeof(List<Customer>));
            using (var writer = new StreamWriter("customers.xml"))
            {
                serializer.Serialize(writer, customers);
            }
        }

        public static void ReadXML()
        {
            Console.WriteLine("customers:");
            foreach (XElement xElement in XElement.Load(@"customers.xml").Elements("Customer"))
            {
                Console.WriteLine("Id of the Customer is : " + xElement.Element("Id").Value);
                Console.WriteLine("Name of the Employee is : " + xElement.Element("EnglishName").Value);
                Console.WriteLine();
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReader
{
    public class Customer
    {
        public int Id { get; set; }
        public string HebrewName { get; set; }

        public string EnglishName{ get; set; }
        public DateTime DateOfBirth{ get; set; }
        public string SSN{ get; set; }
        public int CityId { get; set; }
        public int BankBranch{ get; set; }
        public string AccountNumber { get; set; }
        public int BankCode { get; set; }

    }
}

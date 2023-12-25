using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneModel.Models
{
    public class Country
    {
        public string CountryName { get; set; }
        public string Code { get; set; }
    }
    public class CountryData
    {
        public CountryName[] CountryNames { get; set; }
        public string Code { get; set; }
    }
    public class CountryName
    {
        public string Text { get; set; }
        public string Language { get; set; }
    }
}

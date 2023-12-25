using PhoneModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ManufactureModels
{
    public class Manufacture
    {
        public string ManufactureMail { get; set; }
        public string Password { get; set; }
        public Guid ManufactureId { get; set; }
        public Description ManufactureDescription { get; set; }
        public Phone ManufacturePhone { get; set; }
    }

    public class Description
    {
        public string ManufactureName { get; set; }
        public string ManufactureDescription { get; set; }
        public string ManufactureContactName { get; set; }
        public string Language { get; set; }
    }
}

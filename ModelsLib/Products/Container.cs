using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLib.Products
{
    public class Container
    {
        public Guid ContainerId { get; set; }
        public double Сapacity { get; set; }
        public ContainerText[] ContainerTexts { get; set; }
    }

    public class ContainerText
    {
        public string ContainerName { get; set; }
        public string Language { get; set; }
    }
}

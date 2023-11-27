using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ModelsLib.Products.ProductsServices
{
    public class ContainersReader
    {
        public ContainersReader() { }
        public List<Container> Containers()
        {
            return JsonConvert.DeserializeObject<List<Container>>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "containers.json")));
        }
    }
}

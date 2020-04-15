using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Business_Layer
{
    public class Category : BuisnessObject
    {
        public Category(string name)
        {
            this.name = name;
            Products = new SortedList<string, Product>();
        }

        public string name { get; private set; }
        public string description { get; private set; }
        public SortedList<string, Product> Products { get; set; }
        public IList<Product> GetProducts
        {
            get { return Products.Values; }
        }
    }
}

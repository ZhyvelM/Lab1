using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Business_Layer
{
    public class Meal :BuisnessObject
    {
        public Meal(string name)
        {
            this.name = name;
            Products = new SortedList<string, Product>();
        }

        public string name { get; set; }
        public SortedList<string, Product> Products { get;set;}
        public IList<Product> GetProducts
        {
            get { return Products.Values; }
        }
    }
}

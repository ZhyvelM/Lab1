using Lab1.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Data_Access_Layer
{
    public interface IProduct
    {
        Category getCategory(Product product);
        Category getCategory(string name);
        Product ProductCalculations(Product product, int gramms);
    }
}

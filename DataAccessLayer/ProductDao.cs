using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Business_Layer;

namespace Lab1.Data_Access_Layer
{
    public class ProductDao : IProduct
    {
        private Db db;

        public ProductDao()
        {
            db = Db.GetInstance();
        }

        public Category getCategory(Product product)
        {
            return product.Category;
        }

        public Category getCategory(string name)
        { 
            IList<Category> arr = db.Categories.Values;
            foreach (Category c in arr)
            {
                if (c.Products.ContainsKey(name))
                {
                    return c;
                }
            }
            return null;
        }

        public Product ProductCalculations(Product product, int gramms)
        {
            double coef = gramms / 1.0 / product.Gramms  ;
            double protein = product.Protein * coef;
            double fats = product.Fats * coef;
            double carbs = product.Carbs * coef;
            double calories = product.Calories * coef;
            return new Product(product.Category, product.Name, gramms, protein, fats, carbs, calories); 
        }
    }
}

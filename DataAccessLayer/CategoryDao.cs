using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Business_Layer;

namespace Lab1.Data_Access_Layer
{
    public class CategoryDao : ICategory
    {
        private Db db;

        public CategoryDao()
        {
            db = Db.GetInstance();
        }

        public void addCategory(Category category)
        {
            if (!db.Categories.ContainsKey(category.name))
            {
                db.Categories.Add(category.name, category);
            }
        }

        public void addProduct(Product product)
        {
            if (!product.Category.Products.ContainsKey(product.Name) && BuisnessObject.Check(product))
            {
                product.Category.Products.Add(product.Name, product);
            }
        }

        public SortedList<string, Category> getCategories()
        {
            return db.Categories;
        }

        public Category GetCategory(string name)
        {
            foreach (Category c in db.Categories.Values)
            {
                if (c.name == name)
                {
                    return c;
                }
            }
            return null;
        }

        public SortedList<string, Product> getProducts(Category category)
        {
            return category.Products;
        }

        public SortedList<string, Product> getProducts(string name)
        {
            return db.Categories[name].Products;
        }

        public void removeCategory(Category category)
        {
            db.Categories.Remove(category.name);
        }

        public void removeCategory(string name)
        {
            db.Categories.Remove(name);
        }

        public void removeProduct(Category category, Product product)
        {
            category.Products.Remove(product.Name);
        }

        public void removeProduct(Category category, string name)
        {
            category.Products.Remove(name);
        }

        public SortedList<string, Category> search(string word)
        {
            SortedList<string, Category> Show = new SortedList<string, Category>();
            foreach (Category c in db.Categories.Values)
            {
                bool flagF = true;
                foreach (Product p in c.Products.Values)
                {
                    string pr = p.Name;
                    pr = pr.ToLower();
                    word = word.ToLower();
                    if (pr.Contains(word))
                    {
                        if (flagF)
                        {
                            Show.Add(c.name, new Category(c.name));
                            flagF = false;
                        }
                        Show[c.name].Products.Add(p.Name, p);
                    }
                }
            }
            return Show;
        }
    }
}

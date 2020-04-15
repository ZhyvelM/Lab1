using Lab1.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Data_Access_Layer
{
    public interface ICategory
    {
        SortedList<string, Product> getProducts(Category category);
        SortedList<string, Product> getProducts(string name);
        SortedList<string, Category> getCategories();
        SortedList<string, Category> search(string word);
        Category GetCategory(string name);
        void addCategory(Category category);
        void removeCategory(Category category);
        void removeCategory(string name);
        void addProduct(Product product);
        void removeProduct(Category category, Product product);
        void removeProduct(Category category, string name);
    }
}

using Lab1.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ServiceLayer
{
    public interface IService
    {
        IList<Product> GetProducts(Category category);
        IList<Category> GetCategories();
        IList<Meal> GetMeals();
        User GetUser();
        Category GetCategory(string name);
        double GetCalories();
        double GetDCR(string name, int age, int hieght, int weight, Activity activity);

        void RemoveProduct(Product product);
        void RemoveCategory(Category category);
        void AddProduct(Category category, string name, int gramms, double protein, double fat, double carbs, double calories);
        void AddCategory(string name);
        void AddProduct(Meal meal, Category category, string name, int gramms, double protein, double fat, double carbs, double calories);
        void AddProduct(Meal meal, Product product);
        void AddMeal(string name);
        void RemoveMeal(Meal meal);
        void RemoveMeal(string name);
        void RemoveProduct(Meal meal, Product product);
        void RemoveProduct(Meal meal, string name);

        void Export(string norm, string total);
        IList<Category> Search(string Word);
        Product productCalc(Product product, int gramms);
    }
}

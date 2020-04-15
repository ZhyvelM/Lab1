using Lab1.Business_Layer;
using Lab1.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ServiceLayer
{
    public class Service : IService
    {
        static readonly ICategory CatDao = new CategoryDao();
        static readonly IProduct ProdDao = new ProductDao();
        static readonly IMeal MealDao = new MealDao();
        static readonly IUser UserDao = new UserDao();

        public Service()
        {
        }

        public void AddCategory(string name)
        {
            CatDao.addCategory(new Category(name)); 
        }

        public void AddMeal(string name)
        {
            MealDao.addMeal(new Meal(name));
        }

        public void AddProduct(Category category, string name, int gramms, double protein, double fat, double carbs, double calories)
        {
            CatDao.addProduct(new Product(category, name, gramms, protein, fat, carbs, calories));
        }

        public void AddProduct(Meal meal, Category category, string name, int gramms, double protein, double fat, double carbs, double calories)
        {
            MealDao.addProduct(meal, new Product(category, name, gramms, protein, fat, carbs, calories));
        }

        public void AddProduct(Meal meal, Product product)
        {
            MealDao.addProduct(meal, product);
        }

        public void Export(string norm, string total)
        {
            Db.Export(norm,total);
        }

        public double GetCalories()
        {
            return MealDao.getCalories();
        }

        public IList<Category> GetCategories()
        {
            return CatDao.getCategories().Values;
        }

        public Category GetCategory(string name)
        {
            return CatDao.GetCategory(name);
        }

        public double GetDCR(string name, int age, int hieght, int weight, Activity activity)
        {
            UserDao.SetUser(name,age,hieght,weight,activity);
            return UserDao.GetDailyCaloriesRate();
        }

        public IList<Meal> GetMeals()
        {
            return MealDao.getMeals().Values;
        }

        public IList<Product> GetProducts(Category category)
        {
            return CatDao.getProducts(category).Values;
        }

        public User GetUser()
        {
            return UserDao.GetUser();
        }

        public Product productCalc(Product product, int gramms)
        {
            return ProdDao.ProductCalculations(product,gramms);
        }

        public void RemoveCategory(Category category)
        {
            CatDao.removeCategory(category);
        }

        public void RemoveMeal(Meal meal)
        {
            MealDao.removeMeal(meal);
        }

        public void RemoveMeal(string name)
        {
            MealDao.removeMeal(name);
        }

        public void RemoveProduct(Product product)
        {
            CatDao.removeProduct(product.Category, product);
        }

        public void RemoveProduct(Meal meal, Product product)
        {
            MealDao.removeProduct(meal, product);
        }

        public void RemoveProduct(Meal meal, string name)
        {
            MealDao.removeProduct(meal, name);
        }

        public IList<Category> Search(string Word)
        {
            return CatDao.search(Word).Values;
        }
    }
}

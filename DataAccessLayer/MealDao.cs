using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Business_Layer;

namespace Lab1.Data_Access_Layer
{
    public class MealDao : IMeal
    {
        private Db db;

        public MealDao()
        {
            db = Db.GetInstance();
        }

        public void addMeal(Meal meal)
        {
                db.Meals.Add(meal.name, meal);
        }

        public void addProduct(Meal meal, Product product)
        {
                meal.Products.Add(product.Name, product);
        }

        public double getCalories()
        {
            double result = 0;
            foreach (Meal m in db.Meals.Values)
            {
                foreach (Product p in m.Products.Values)
                {
                    result += p.Calories;
                }
            }
            return result;
        }

        public SortedList<string, Product> getMeal(Meal meal)
        {
            return meal.Products;
        }

        public SortedList<string, Product> getMeal(string name)
        {
            return db.Meals[name].Products;
        }

        public SortedList<string, Meal> getMeals()
        {
            return db.Meals;
        }

        public void removeMeal(Meal meal)
        {
            db.Meals.Remove(meal.name);
        }

        public void removeMeal(string name)
        {
            db.Meals.Remove(name);
        }

        public void removeProduct(Meal meal, Product product)
        {
            meal.Products.Remove(product.Name);
        }

        public void removeProduct(Meal meal, string name)
        {
            meal.Products.Remove(name);
        }
    }
}
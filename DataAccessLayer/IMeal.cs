using Lab1.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Data_Access_Layer
{
    public interface IMeal
    {
        SortedList<string, Product> getMeal(Meal meal);
        SortedList<string, Product> getMeal(string name);
        SortedList<string, Meal> getMeals();
        void addMeal(Meal meal);
        void removeMeal(Meal meal);
        void removeMeal(string name);
        void addProduct(Meal meal, Product product);
        void removeProduct(Meal meal, Product product);
        void removeProduct(Meal meal, string name);
        double getCalories();
    }
}

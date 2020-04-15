using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Business_Layer
{
    public class BuisnessObjectRule
    {
        public static bool ProductCheck(Product product)
        {
            bool result = true;
            if (product.Category == null || product.Name == "" || product.Gramms < 1 || product.Protein < 0 || product.Fats < 0 || product.Carbs < 0 || product.Calories < 1)
            {
                result = false;
            }
            return result;
        }

        public static bool CategoryCheck(Category category)
        {
            bool result = false;
            if (category.name != "")
            {
                IList<string> arr = category.Products.Keys;
                for (int i = 0; i < category.Products.Count; i++)
                {
                    if (!ProductCheck(category.Products[arr[i]]))
                    {
                        category.Products.Remove(arr[i]);
                        i--;
                    }
                }
                result = true;
            }
            return result;
        }

        public static bool MealCheck(Meal meal)
        {
            bool result = false;
            if (meal.name != "")
            {
                IList<string> arr = meal.Products.Keys;
                for (int i = 0; i < meal.Products.Count; i++)
                {
                    if (!ProductCheck(meal.Products[arr[i]]))
                    {
                        meal.Products.Remove(arr[i]);
                        i--;
                    }
                }
                result = true;
            }
            return result;
        }

        public static bool UserCheck(User user)
        {
            bool result = true;
            if (user.name == "" || user.weight < 1 || user.height < 1 || user.age < 1)
            {
                result = false;
            }
            return result;
        } 
           
    }
}

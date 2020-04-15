using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Business_Layer
{
    public class BuisnessObject
    {
        public static bool Check(Product product)
        {
            return BuisnessObjectRule.ProductCheck(product);
        }

        public static bool Check(Category category)
        {
            return BuisnessObjectRule.CategoryCheck(category);
        }

        public static bool Check(User user)
        {
            return BuisnessObjectRule.UserCheck(user);
        }

        public static bool Check(Meal meal)
        {
            return BuisnessObjectRule.MealCheck(meal);
        }
    }
}

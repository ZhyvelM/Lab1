using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Business_Layer
{
    public class Product : BuisnessObject
    {
        public Product(Category category, string name, int gramms, double protein, double fats, double carbs, double calories)
        {
            meal = null;
            Category = category;
            Name = name;
            Gramms = gramms;
            Protein = protein;
            Fats = fats;
            Carbs = carbs;
            Calories = calories;
        }

        public Meal meal { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public int Gramms { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }
        
        public string getInfo
        {
            get
            {
                return
                    $"Вес: {Gramms}\n" +
                    $"Белки: {Protein}\n" +
                    $"Жиры: {Fats}\n" +
                    $"Углеводы: {Carbs}\n" +
                    $"Калории: {Calories}";
            }
        }
    }
}

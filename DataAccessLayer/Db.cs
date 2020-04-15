using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lab1.Business_Layer
{
    public class Db
    {
        public SortedList<string, Category> Categories;
        public SortedList<string, Meal> Meals;
        public User user;

        private static Db instance;

        private Db()
        {
            MealsInit();
            UserInit();
            ParseFile();            
        }

        public static Db GetInstance()
        {
            if (instance == null)
            {
                instance = new Db();
            }
            return instance;
        }

        private void MealsInit()
        {
            Meals = new SortedList<string, Meal>();
            Meals.Add("Завтрак", new Meal("Завтрак"));
            Meals.Add("Обед", new Meal("Обед"));
            Meals.Add("Ужин", new Meal("Ужин"));
        }

        private void UserInit()
        {
            user = new User();
            user.age = 18;
            user.height = 185;
            user.weight = 73;
            user.name = "Живель Максим";
            user.activity = Activity.normal;
        }

        private void ParseFile()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"D:\Projects\OOP\Lab1\Lab1\Resources\FoodProducts.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            Categories = new SortedList<string, Category>();
            foreach (XmlNode CategoryF in xRoot)
            {

                string name = "";
                if (CategoryF.Attributes.Count > 0)
                {
                    name = CategoryF.Attributes.GetNamedItem("name").InnerText;
                }
                Category cater = new Category(name);
                foreach (XmlNode product in CategoryF.ChildNodes)
                {
                    string Name = "";
                    int gramms = -1;
                    double protein = -1;
                    double fats = -1;
                    double carbs = -1;
                    double calories = -1;
                    Name = product.ChildNodes[0].InnerText;
                    int.TryParse(product.ChildNodes[1].InnerText, out gramms);
                    double.TryParse(product.ChildNodes[2].InnerText, out protein);
                    double.TryParse(product.ChildNodes[3].InnerText, out fats);
                    double.TryParse(product.ChildNodes[4].InnerText, out carbs);
                    double.TryParse(product.ChildNodes[5].InnerText, out calories);
                    cater.Products.Add(Name, new Product(cater, Name, gramms, protein, fats, carbs, calories));
                }
                Categories.Add(name, cater);
            }
            foreach(Category cater in Categories.Values)
            {
                BuisnessObject.Check(cater);
            }
        }

        public static void Export(string norm, string total)
        {

            XFont small = new XFont("Comic Sans MS", 14, XFontStyle.Regular);
            XFont middle = new XFont("Comic Sans MS", 18, XFontStyle.Regular);
            XFont xlarge = new XFont("Comic Sans MS", 24, XFontStyle.Regular);
            int x = 40, y = 0;

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Рацион";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            gfx.DrawString("РАЦИОН",xlarge, XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.TopCenter);
            y += 35;
            gfx.DrawString("Информация о пользователе", middle, XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.TopCenter);
            y += 20;
            gfx.DrawString($"Имя: {instance.user.name}", small, XBrushes.Black, new XRect(x + 20, y, page.Width, page.Height), XStringFormats.TopLeft);
            y += 20;
            gfx.DrawString($"Возраст: {instance.user.age}", small, XBrushes.Black, new XRect(x + 20, y, page.Width, page.Height), XStringFormats.TopLeft);
            y += 20;
            gfx.DrawString($"Рост: {instance.user.height}", small, XBrushes.Black, new XRect(x + 20, y, page.Width, page.Height), XStringFormats.TopLeft);
            y += 20;
            gfx.DrawString($"Вес: {instance.user.weight}", small, XBrushes.Black, new XRect(x + 20, y, page.Width, page.Height), XStringFormats.TopLeft);
            y += 20;
            gfx.DrawString($"Активность: {instance.user.activity}", small, XBrushes.Black, new XRect(x + 20, y, page.Width, page.Height), XStringFormats.TopLeft);
            y += 30;
            gfx.DrawString($"Дневная норма: {norm}", xlarge, XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.TopCenter);
            foreach (Meal meal in instance.Meals.Values)
            {
                y += 35;
                if (y + 35 > page.Height)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = 0;
                }
                gfx.DrawString(meal.name, middle, XBrushes.Green, new XRect(x + 20, y, page.Width, page.Height), XStringFormats.TopLeft);
                foreach (Product product in meal.Products.Values)
                {
                    y += 25;
                    if (y + 25 > page.Height)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        y = 0;
                    }
                    gfx.DrawString($"{product.Name}: {product.Gramms}г", small, XBrushes.Black, new XRect(x + 25, y, page.Width, page.Height), XStringFormats.TopLeft);
                }
            }
            y += 50;
            if (y > page.Height)
            {
                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                y = 0;
            }
            gfx.DrawString($"Итог: {total}кал", xlarge, XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.TopCenter);
            document.Save("DailyRation.pdf");
        }
    }
}
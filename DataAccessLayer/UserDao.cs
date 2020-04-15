using Lab1.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Data_Access_Layer
{
    public class UserDao : IUser
    {
        private Db db;

        public UserDao()
        {
            db = Db.GetInstance();
        }

        public void SetUser(string name, int age, int height, int weight, Activity activity)
        {
            db.user.name = name;
            db.user.age = age;
            db.user.height = height;
            db.user.weight = weight;
            db.user.activity = activity;
        }

        public double GetDailyCaloriesRate()
        {
            double BMR = 447.593 + 9.247 * db.user.weight + 3.098 * db.user.height - 4.330 * db.user.age, ARM;
            if (db.user.activity == Activity.low)
            {
                ARM = 1.2;
            }else if (db.user.activity == Activity.normal)
            {
                ARM = 1.375;
            }
            else if (db.user.activity == Activity.average)
            {
                ARM = 1.55;
            }
            else
            {
                ARM = 1.725;
            }
            return BMR * ARM;
        }

        public User GetUser()
        {
            return db.user;
        }
    }
}

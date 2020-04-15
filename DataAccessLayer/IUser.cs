using Lab1.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Data_Access_Layer
{
    public interface IUser
    {
        void SetUser(string name, int age, int height, int weight, Activity activity);
        User GetUser();
        double GetDailyCaloriesRate();        
    }
}

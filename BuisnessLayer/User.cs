using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Business_Layer
{
    public enum Activity
    {
        low,
        normal,
        average,
        high
    }

    public class User : BuisnessObject
    {
        public string name { get; set; }
        public int age { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public Activity activity = new Activity();
    }
}

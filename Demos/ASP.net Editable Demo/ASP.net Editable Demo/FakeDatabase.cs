using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.net_Editable_Demo
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public DateTime DOB { get; set; }
        public string Status { get; set; }
    }

    public static class FakeDatabase
    {
        public static Dictionary<int, Employee> db = new Dictionary<int, Employee>();
        public static int keyIndex = 1;

        static FakeDatabase()
        { 
            //add two fake rows to our fake db
            Employee e = new Employee
            {
                EmpId = 1,
                FirstName = "Jake",
                LastName = "Drew",
                JobTitle = "Programmer",
                DOB = new DateTime(1980,12,23),
                Status = "Active"
            };
            Employee e2 = new Employee
            {
                EmpId = 2,
                FirstName = "Victoria",
                LastName = "McDowell-Drew",
                JobTitle = "Queen",
                DOB = new DateTime(1980, 3, 23),
                Status = "Inactive"
            };
            db.Add(db.Count + 1, e);
            keyIndex++;
            db.Add(db.Count + 1, e2);
            keyIndex++;
        }


        public static int add(Dictionary<string, string> data)
        {
            Employee e = new Employee 
            {
                EmpId = keyIndex,
                FirstName = data["First Name"],
                LastName = data["Last Name"],
                JobTitle = data["Job Title"],
                DOB = DateTime.Parse(data["DOB"]),
                Status = data["Status"]
            };
            db.Add(keyIndex, e);
            keyIndex++;
            return db.Count;
        }

        public static void delete(Dictionary<string, string> data)
        {
            int id = int.Parse(data["EmpId"]);
            db.Remove(id);
        }

        public static void update(Dictionary<string, string> data)
        {
            int id = int.Parse(data["EmpId"]);
            string fname = data["updatedFieldName"].Replace(" ", "");
            string fval = data["updatedFieldValue"];

            Employee e = db[id];

            //find and set the location property in loc name.
            if(fname == "DOB")
                e.GetType().GetProperty(fname).SetValue(e, Convert.ToDateTime(fval), null);
            else
                e.GetType().GetProperty(fname).SetValue(e, fval, null);

            db[id] = e;
        }

        public static List<Employee> select(Dictionary<string, string> data)
        {
            return db.Values.ToList();
        }
    }
}
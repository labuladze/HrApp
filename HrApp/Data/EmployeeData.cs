using HrApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HrApp.Data
{
    public class EmployeeData : IData
    {
        public bool CreateEmpl(Employee empl)
        {
            using (var client = new HttpClient())
            {
                var result = client.PostAsJsonAsync<Employee>("http://localhost:5000/api/employee", empl).Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public bool DeleteEmpl(int id)
        {
            using (var client = new HttpClient())
            {
                var result = client.DeleteAsync($"http://localhost:5000/api/employee/{id}").Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            IEnumerable<Employee> res = null;
            using (var client = new HttpClient())
            {
                var result =  client.GetAsync("http://localhost:5000/api/employee").Result;
                if (result.IsSuccessStatusCode)
                {
                     res =  result.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                }
            }
            return res;
        }

        public Employee GetById(int id)
        {
            Employee res = null;
            using (var client = new HttpClient())
            {
                var result = client.GetAsync($"http://localhost:5000/api/employee/{id}").Result;
                if (result.IsSuccessStatusCode)
                {
                    res = result.Content.ReadAsAsync<Employee>().Result;
                }
            }
            return res;
        }

        public bool UpdateEmpl(int id, Employee empl)
        {
            using (var client = new HttpClient())
            {
                var result = client.PutAsJsonAsync<Employee>($"http://localhost:5000/api/employee/{id}", empl).Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}

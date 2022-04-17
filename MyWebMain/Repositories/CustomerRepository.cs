using MyWebMain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyWebMain.Repositories
{
    public class CustomerRepository
    {
        private string ApiUrl = "https://localhost:44305/api/Customers";
        private HttpClient _client;

        public CustomerRepository()
        {
            _client = new HttpClient();
        }

        public List<Customer> GetAllCustomers(string token)
        {

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = _client.GetStringAsync(ApiUrl).Result;
            List<Customer> Customers = JsonConvert.DeserializeObject<List<Customer>>(result);

            return Customers;
        }

        public Customer GetCustomerById(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = _client.GetStringAsync(ApiUrl + "/" + id).Result;
            Customer customer = JsonConvert.DeserializeObject<Customer>(result);
            return customer;

        }

       public void InsertCustomer(Customer customer, string token)
        {
            //string JsonCustomer = JsonConvert.SerializeObject(customer);
            //StringContent content = new StringContent(JsonCustomer, Encoding.UTF8, "application/json");
            //_client.PostAsync(ApiUrl, content);

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _client.PostAsJsonAsync<Customer>(ApiUrl, customer);

        }

        public void UpdateCustomer(Customer customer, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var id = customer.CustomerId;
            _client.PutAsJsonAsync<Customer>(ApiUrl + "/" + id, customer);

        }

        public void DeleteCustomer(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _client.DeleteAsync(ApiUrl + "/" + id);

        }

    }
}

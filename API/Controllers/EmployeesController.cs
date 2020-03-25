using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace API.Controllers
{
    public class EmployeesController : ApiController
    {

        public class Results
        {
            public string name { get; set; }
            public string department { get; set; }
            public string address { get; set; }
            public string number { get; set; }

            public Results(string name, string department, string address, string number)
            {
                this.name = name;
                this.department = department;
                this.address = address;
                this.number = number;
            }

        }

        // GET employees
        public List<Results> Get()
        {
            MySqlConnection connection = WebApiConfig.Connection();

            MySqlCommand query = connection.CreateCommand();

            query.CommandText = "SELECT * FROM data.employees;";

            var Results = new List<Results>();

            try
            {
                connection.Open();
            }
            catch (MySqlException Exception)
            {
                throw Exception;
            }

            MySqlDataReader fetchQuery = query.ExecuteReader();

            while ( fetchQuery.Read())
            {
                Results.Add(new Results(
                    fetchQuery["Name"].ToString(), 
                    fetchQuery["Department"].ToString(), 
                    fetchQuery["Address"].ToString(), 
                    fetchQuery["Number"].ToString())
                    );
            }

            return Results;
        }
    }
}

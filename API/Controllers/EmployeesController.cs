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
            public string title { get; set; }
            public string surname { get; set; }
            public string employeeNr { get; set; }
            public string country { get; set; }

            public Results(string name, string surname, string department, string address, string number, string title, string employeeNr, string country)
            {
                this.name = name;
                this.department = department;
                this.address = address;
                this.number = number;
                this.surname = surname;
                this.title = title;
                this.employeeNr = employeeNr;
                this.country = country;

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
                    fetchQuery["SurName"].ToString(),
                    fetchQuery["Number"].ToString(),
                    fetchQuery["Address"].ToString(), 
                    fetchQuery["Department"].ToString(),
                    fetchQuery["Title"].ToString(),
                    fetchQuery["EmployeeNR"].ToString(),
                    fetchQuery["Country"].ToString())
                    );
            }

            return Results;
        }

        // GET the x first employees
        public List<Results> Get(int amount)
        {
            MySqlConnection connection = WebApiConfig.Connection();

            MySqlCommand query = connection.CreateCommand();

            query.CommandText = "SELECT * FROM data.employees LIMIT @amount;";

            query.Parameters.AddWithValue("@amount", amount);

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

            while (fetchQuery.Read())
            {
                Results.Add(new Results(
                    fetchQuery["Name"].ToString(),
                    fetchQuery["SurName"].ToString(),
                    fetchQuery["Number"].ToString(),
                    fetchQuery["Address"].ToString(),
                    fetchQuery["Department"].ToString(),
                    fetchQuery["Title"].ToString(),
                    fetchQuery["EmployeeNR"].ToString(),
                    fetchQuery["Country"].ToString())
                    );
            }


            connection.Close();
            return Results;
        }
    }
}

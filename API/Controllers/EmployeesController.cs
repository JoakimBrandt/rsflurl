using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class EmployeesController : ApiController
    {
        public class Result
        {
            public string name { get; set; }
            public string department { get; set; }
            public string address { get; set; }
            public string number { get; set; }
            public string title { get; set; }
            public string surname { get; set; }
            public string employeeNr { get; set; }
            public string country { get; set; }

            public Result(string name, string surname, string department, string address, string number, string title, string employeeNr, string country)
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

        // GET the x first employees
        public string Get(int amount)
        {

            MySqlConnection connection = WebApiConfig.Connection();

            MySqlCommand query = connection.CreateCommand();

            establishConnection(connection);

            query.CommandText = "SELECT * FROM data.employees LIMIT @amount;";

            query.Parameters.AddWithValue("@amount", amount);

            var Results = new List<Result>();

            MySqlDataReader fetchQuery = query.ExecuteReader();

            while (fetchQuery.Read())
            {
                Results.Add(new Result(
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

            closeConnection(connection);

            return JsonConvert.SerializeObject(Results);
        }
        
        
        static void establishConnection(MySqlConnection connection)
        { 
            try
            {
                connection.Open();
            }
            catch (MySqlException Exception)
            {
                throw Exception;
            }
        }
        static void closeConnection(MySqlConnection connection)
        {
            connection.Close();
        }
    }
}

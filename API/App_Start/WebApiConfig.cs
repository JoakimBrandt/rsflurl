using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace API
{
    public static class WebApiConfig
    {


        public static MySqlConnection Connection()
        {
            string connectionString = "server=localhost;port=3306;database=data;username=root;password=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }



        public static void Register(HttpConfiguration config)
        {

            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(item: new MediaTypeHeaderValue("application/json"));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{amount}",
                defaults: new { amount = RouteParameter.Optional }
            );
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using SportoloDolgozat.Models;
using SportoloDolgozat.Models.DTOs;

namespace SportoloDolgozat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EredmenyController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=sportolo13b;";
        [HttpGet]
        public List<Eredmeny> GetEredmenyek()
        {
            List<Eredmeny> eredmenyek = new List<Eredmeny>();

            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT * FROM eredmeny;";

            var cmd = new MySqlCommand(sql, connection);

            var data = cmd.ExecuteReader();

            while (data.Read())
            {
                var eredmeny = new Eredmeny
                {
                    Id = data.GetInt32("id"),
                    Competition = data.GetString("competition"),
                    Description = data.GetString("description"),
                    ResultTime = data.GetDateTime("resultTime"),
                    UpdateTime = data.GetDateTime("updateTime"),
                    SportoloId = data.GetInt32("id"),
                };
                eredmenyek.Add(eredmeny);
            }

            connection.Close();

            return eredmenyek;
        }

        [HttpGet("ByID")]
        public object GetEredmenyById(int id)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"SELECT * FROM `eredmeny` WHERE `Id` = @id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            var datareader = cmd.ExecuteReader();

            object? data = null;
            if (datareader.Read() == true)
            {

                var eredmeny = new Eredmeny
                {
                    Id = datareader.GetInt32("id"),
                    Competition = datareader.GetString("competition"),
                    Description = datareader.GetString("description"),
                    ResultTime = datareader.GetDateTime("resultTime"),
                    UpdateTime = datareader.GetDateTime("updateTime"),
                    SportoloId = datareader.GetInt32("id"),
                };

                data = new { message = "Sikeres lekérdezés", result = eredmeny };
            }
            else
            {
                data = new { message = "Sikertelen lekérdezés | Nincs ilyen ID-val rendelkező sportoló", result = "" };
            }


            connection.Close();

            return data;
        }


        [HttpPost]
        public object AddNewSportolo([FromBody] EredmenyPost addNewSportolo)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"INSERT INTO `eredmeny`(`Competition`,`Description`,`ResultTime`,`UpdateTime`) VALUES (@Competition,@Description,@ResultTime,@UpdateTime)";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@Competition", addNewSportolo.Competition);
            cmd.Parameters.AddWithValue("@Description", addNewSportolo.Description);
            cmd.Parameters.AddWithValue("@ResultTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdateTime", DateTime.Now);

            cmd.ExecuteNonQuery();

            connection.Close();

            return new { message = "Sikeres felvétel", result = addNewSportolo };
        }

    }
}

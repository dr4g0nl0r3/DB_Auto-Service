using Auto_Service.Pages.Spares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Cars
{
    public class CreateModel : PageModel
    {
		public CarInfo carInfo = new CarInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
        {
        }

        public void OnPost() 
        {
			carInfo.name = Request.Form["name"];
			carInfo.carnum = Request.Form["carnum"];
			carInfo.clphone = Request.Form["clphone"];

			if (carInfo.name.Length == 0 || carInfo.carnum.Length == 0 || carInfo.clphone.Length == 0)
			{
				errorMessage = "Все поля обязательны к заполнению!";
				return;
			}

			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "INSERT INTO cars" +
						"(name, carnum, clphone) VALUES" +
						"(@name, @carnum, @clphone)";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", carInfo.name);
						command.Parameters.AddWithValue("@carnum", carInfo.carnum);
						command.Parameters.AddWithValue("@clphone", carInfo.clphone);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			carInfo.name = "";
			carInfo.carnum = "";
			carInfo.clphone = "";
			successMessage = "Новый автомобиль добавлен успешно!";

			Response.Redirect("/Cars/Cars");
		}
    }
}

using Auto_Service.Pages.Spares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Dynamic;

namespace Auto_Service.Pages.Cars
{
    public class EditModel : PageModel
    {
		public CarInfo carInfo = new CarInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
        {
			String id = Request.Query["id"];

			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM cars WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								carInfo.id = "" + reader.GetInt32(0);
								carInfo.name = reader.GetString(1);
								carInfo.carnum = reader.GetString(2);
								carInfo.clphone = reader.GetString(3);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

		}

		public void OnPost()
		{
			carInfo.id = Request.Form["id"];
			carInfo.name = Request.Form["name"];
			carInfo.carnum = Request.Form["carnum"];
			carInfo.clphone = Request.Form["clphone"];

			if (carInfo.id.Length == 0 || carInfo.name.Length == 0 || carInfo.carnum.Length == 0 || carInfo.clphone.Length == 0)
			{
				errorMessage = "Все поля обязательны к заполнению!";
				return;
			}

			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "UPDATE cars " +
						"SET name=@name, carnum=@carnum, clphone=@clphone " +
						"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", carInfo.id);
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

			Response.Redirect("/Cars/Cars");
		}
	}
}

using Auto_Service.Pages.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Services
{
    public class EditModel : PageModel
    {
		public ServiceInfo serviceInfo = new ServiceInfo();
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
					String sql = "SELECT * FROM services WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								serviceInfo.id = "" + reader.GetInt32(0);
								serviceInfo.name = reader.GetString(1);
								serviceInfo.price = reader.GetString(2);
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
			serviceInfo.id = Request.Form["id"];
			serviceInfo.name = Request.Form["name"];
			serviceInfo.price = Request.Form["price"];

			if (serviceInfo.id.Length == 0 || serviceInfo.price.Length == 0 || serviceInfo.name.Length == 0)
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
					String sql = "UPDATE services " +
						"SET name=@name, price=@price " +
						"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", serviceInfo.id);
						command.Parameters.AddWithValue("@name", serviceInfo.name);
						command.Parameters.AddWithValue("@price", serviceInfo.price);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			Response.Redirect("/Services/Services");
		}
	}
}

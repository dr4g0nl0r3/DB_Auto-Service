using Auto_Service.Pages.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Clients
{
    public class EditModel : PageModel
    {
		public ClientInfo clientInfo = new ClientInfo();
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
					String sql = "SELECT * FROM clients WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								clientInfo.id = "" + reader.GetInt32(0);
								clientInfo.surname = reader.GetString(1);
								clientInfo.name = reader.GetString(2);
								clientInfo.phone = reader.GetString(3);
								clientInfo.type = reader.GetString(4);
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
			clientInfo.id = Request.Form["id"];
			clientInfo.surname = Request.Form["surname"];
			clientInfo.name = Request.Form["name"];
			clientInfo.phone = Request.Form["phone"];
			clientInfo.type = Request.Form["type"];

			if (clientInfo.id.Length == 0 || clientInfo.surname.Length == 0 || clientInfo.name.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.type.Length == 0)
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
					String sql = "UPDATE clients " +
						"SET surname=@surname, name=@name, phone=@phone, type=@type " +
						"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						
						command.Parameters.AddWithValue("@surname", clientInfo.surname);
						command.Parameters.AddWithValue("@name", clientInfo.name);
						command.Parameters.AddWithValue("@phone", clientInfo.phone);
						command.Parameters.AddWithValue("@type", clientInfo.type);
						command.Parameters.AddWithValue("@id", clientInfo.id);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			Response.Redirect("/Clients/Clients");
		}
	}
}

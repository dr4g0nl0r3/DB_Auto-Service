using Auto_Service.Pages.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Clients
{
	public class CreateModel : PageModel
	{
		public ClientInfo clientInfo = new ClientInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
		}

		public void OnPost()
		{
			clientInfo.surname = Request.Form["surname"];
			clientInfo.name = Request.Form["name"];
			clientInfo.phone = Request.Form["phone"];
			clientInfo.type = Request.Form["type"];

			if (clientInfo.surname.Length == 0 || clientInfo.name.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.type.Length == 0)
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
					String sql = "INSERT INTO clients" +
						"(surname, name, phone, type) VALUES" +
						"(@surname, @name, @phone, @type)";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@surname", clientInfo.surname);
						command.Parameters.AddWithValue("@name", clientInfo.name);
						command.Parameters.AddWithValue("@phone", clientInfo.phone);
						command.Parameters.AddWithValue("@type", clientInfo.type);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			clientInfo.surname = "";
			clientInfo.name = "";
			clientInfo.phone = "";
			clientInfo.type = "";
			successMessage = "Новый клиент добавлен успешно!";

			Response.Redirect("/Clients/Clients");
		}
	}
}

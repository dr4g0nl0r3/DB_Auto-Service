using Auto_Service.Pages.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Services
{
    public class CreateModel : PageModel
    {
		public ServiceInfo serviceInfo = new ServiceInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
        {
        }

		public void OnPost()
		{
			serviceInfo.name = Request.Form["name"];
			serviceInfo.price = Request.Form["price"];

			if (serviceInfo.name.Length == 0 || serviceInfo.price.Length == 0)
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
					String sql = "INSERT INTO services" +
						"(name, price) VALUES" +
						"(@name, @price)";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
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

			serviceInfo.name = "";
			serviceInfo.price = "";
			successMessage = "Новая услуга добавлена успешно!";

			Response.Redirect("/Services/Services");
		}
	}
}

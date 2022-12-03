using Auto_Service.Pages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Spares
{
    public class CreateModel : PageModel
    {
		public SpareInfo spareInfo = new SpareInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
        {

        }

        public void OnPost() 
        {
			spareInfo.name = Request.Form["name"];
			spareInfo.price = Request.Form["price"];

			if (spareInfo.name.Length == 0 || spareInfo.price.Length == 0)
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
					String sql = "INSERT INTO spares" +
						"(name, price) VALUES" +
						"(@name, @price)";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", spareInfo.name);
						command.Parameters.AddWithValue("@price", spareInfo.price);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			spareInfo.name = "";
			spareInfo.price = "";
			successMessage = "Новый товар добавлен успешно!";

			Response.Redirect("/Spares/Spares");
		}
	}
}


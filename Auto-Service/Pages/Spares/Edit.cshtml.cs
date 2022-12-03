using Auto_Service.Pages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Spares
{
    public class EditModel : PageModel
    {
		public SpareInfo spareInfo = new SpareInfo();
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
					String sql = "SELECT * FROM spares WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								spareInfo.id = "" + reader.GetInt32(0);
								spareInfo.name = reader.GetString(1);
								spareInfo.price = reader.GetString(2);
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
			spareInfo.id = Request.Form["id"];
			spareInfo.name = Request.Form["name"];
			spareInfo.price = Request.Form["price"];

			if (spareInfo.id.Length == 0 || spareInfo.price.Length == 0 || spareInfo.name.Length == 0)
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
					String sql = "UPDATE spares " +
						"SET name=@name, price=@price " +
						"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", spareInfo.id);
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

			Response.Redirect("/Spares/Spares");
		}
	}
}

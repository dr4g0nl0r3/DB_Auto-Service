using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Auto_Service.Pages.Staff
{
	public class EditModel : PageModel
	{
		public StaffInfo staffInfo = new StaffInfo();
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
					String sql = "SELECT * FROM staff WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								staffInfo.id = "" + reader.GetInt32(0);
								staffInfo.surname = reader.GetString(1);
								staffInfo.name = reader.GetString(2);
								staffInfo.email = reader.GetString(3);
								staffInfo.position = reader.GetString(4);
								staffInfo.status = reader.GetString(5);
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
			staffInfo.id = Request.Form["id"];
			staffInfo.surname = Request.Form["surname"];
			staffInfo.name = Request.Form["name"];
			staffInfo.email = Request.Form["email"];
			staffInfo.position = Request.Form["position"];
			staffInfo.status = Request.Form["status"];

			if (staffInfo.id.Length == 0 || staffInfo.surname.Length == 0 || staffInfo.name.Length == 0 || staffInfo.email.Length == 0 || staffInfo.position.Length == 0 || staffInfo.status.Length == 0)
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
					String sql = "UPDATE staff " +
						"SET surname=@surname, name=@name, email=@email, position=@position, status=@status " +
						"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", staffInfo.id);
						command.Parameters.AddWithValue("@surname", staffInfo.surname);
						command.Parameters.AddWithValue("@name", staffInfo.name);
						command.Parameters.AddWithValue("@email", staffInfo.email);
						command.Parameters.AddWithValue("@position", staffInfo.position);
						command.Parameters.AddWithValue("@status", staffInfo.status);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			Response.Redirect("/Staff/Index");
		}
	}
}

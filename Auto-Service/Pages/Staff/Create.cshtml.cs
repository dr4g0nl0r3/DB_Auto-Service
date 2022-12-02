using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Staff
{
	public class CreateModel : PageModel
	{
		public StaffInfo staffInfo = new StaffInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
		}

		public void OnPost()
		{
			staffInfo.surname = Request.Form["surname"];
			staffInfo.name = Request.Form["name"];
			staffInfo.email = Request.Form["email"];
			staffInfo.position = Request.Form["position"];
			staffInfo.status = Request.Form["status"];

			if (staffInfo.surname.Length == 0 || staffInfo.name.Length == 0 || staffInfo.email.Length == 0 || staffInfo.position.Length == 0 || staffInfo.status.Length == 0)
			{
				errorMessage = "Все поля обязательны к заполнению!";
				return;
			}

			//save new staff into DB
			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "INSERT INTO staff" +
						"(surname, name, email, position, status) VALUES" +
						"(@surname, @name, @email, @position, @status)";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
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

			staffInfo.surname = "";
			staffInfo.name = "";
			staffInfo.email = "";
			staffInfo.position = "";
			staffInfo.status = "";
			successMessage = "Новый работник добавлен успешно!";

			Response.Redirect("/Staff/Index");
		}
	}
}

using Auto_Service.Pages.Spares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Orders
{
    public class EditModel : PageModel
    {
		public OrderInfo orderInfo = new OrderInfo();
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
					String sql = "SELECT * FROM orders WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								orderInfo.id = "" + reader.GetInt32(0);
								orderInfo.date = reader.GetString(1);
								orderInfo.price = reader.GetString(2);
								orderInfo.service = reader.GetString(3);
								orderInfo.spare = reader.GetString(4);
								orderInfo.clphone = reader.GetString(5);
								orderInfo.carnum = reader.GetString(6);
								orderInfo.emstaff = reader.GetString(7);
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
			orderInfo.id = Request.Form["id"];
			orderInfo.date = Request.Form["date"];
			orderInfo.price = Request.Form["price"];
			orderInfo.service = Request.Form["service"];
			orderInfo.spare = Request.Form["spare"];
			orderInfo.clphone = Request.Form["clphone"];
			orderInfo.carnum = Request.Form["carnum"];
			orderInfo.emstaff = Request.Form["emstaff"];

			if (orderInfo.id.Length == 0 || orderInfo.date.Length == 0 || orderInfo.service.Length == 0 || orderInfo.spare.Length == 0 || orderInfo.clphone.Length == 0 || orderInfo.carnum.Length == 0 || orderInfo.emstaff.Length == 0)
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
					String sql = "UPDATE orders " +
						"SET date=@date, price=@price, service=@service, spare=@spare, clphone=@clphone, carnum=@carnum, emstaff=@emstaff " +
						"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", orderInfo.id);
						command.Parameters.AddWithValue("@date", orderInfo.date);
						command.Parameters.AddWithValue("@price", orderInfo.price);
						command.Parameters.AddWithValue("@service", orderInfo.service);
						command.Parameters.AddWithValue("@spare", orderInfo.spare);
						command.Parameters.AddWithValue("@clphone", orderInfo.clphone);
						command.Parameters.AddWithValue("@carnum", orderInfo.carnum);
						command.Parameters.AddWithValue("@emstaff", orderInfo.emstaff);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			Response.Redirect("/Orders/Orders");
		}
    }
}

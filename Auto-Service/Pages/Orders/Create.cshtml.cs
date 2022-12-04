using Auto_Service.Pages.Spares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Orders
{
    public class CreateModel : PageModel
    {
		public OrderInfo orderInfo = new OrderInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
        {
        }

		public void OnPost()
		{
			orderInfo.date = Request.Form["date"];
			orderInfo.price = Request.Form["price"];
			orderInfo.service = Request.Form["service"];
			orderInfo.spare = Request.Form["spare"];
			orderInfo.clphone = Request.Form["clphone"];
			orderInfo.carnum = Request.Form["carnum"];
			orderInfo.emstaff = Request.Form["emstaff"];

			if (orderInfo.date.Length == 0 || orderInfo.price.Length == 0 || orderInfo.service.Length == 0 || orderInfo.spare.Length == 0 || orderInfo.clphone.Length == 0 || orderInfo.carnum.Length == 0 || orderInfo.emstaff.Length == 0)
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
					String sql = "INSERT INTO orders " +
						"(date, price, service, spare, clphone, carnum, emstaff) VALUES " +
						"(@date, @price, @service, @spare, @clphone, @carnum, @emstaff)";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
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

			orderInfo.date = "";
			orderInfo.price = "";
			orderInfo.service = "";
			orderInfo.spare = "";
			orderInfo.clphone = "";
			orderInfo.carnum = "";
			orderInfo.emstaff = "";
			successMessage = "Новый товар добавлен успешно!";

			Response.Redirect("/Orders/Orders");
		}
	}
}

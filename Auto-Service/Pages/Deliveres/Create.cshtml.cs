using Auto_Service.Pages.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Dynamic;

namespace Auto_Service.Pages.Deliveres
{
    public class CreateModel : PageModel
    {
		public DeliverInfo deliverInfo = new DeliverInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
        { 
        }

		public void OnPost()
		{
			deliverInfo.date = Request.Form["date"];
			deliverInfo.spname = Request.Form["spname"];
			deliverInfo.quantity = Request.Form["quantity"];
			deliverInfo.price = Request.Form["price"];
			deliverInfo.sup = Request.Form["sup"];
			if (deliverInfo.date.Length == 0 || deliverInfo.spname.Length == 0 || deliverInfo.quantity.Length == 0 || deliverInfo.price.Length == 0 || deliverInfo.sup.Length == 0)
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
					String sql = "INSERT INTO deliveries" +
						"(date, spname, quantity, price, sup) VALUES" +
						"(@date, @spname, @quantity, @price, @sup)";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@date", deliverInfo.date);
						command.Parameters.AddWithValue("@spname", deliverInfo.spname);
						command.Parameters.AddWithValue("@quantity", deliverInfo.quantity);
						command.Parameters.AddWithValue("@price", deliverInfo.price);
						command.Parameters.AddWithValue("@sup", deliverInfo.sup);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			deliverInfo.date = "";
			deliverInfo.spname = "";
			deliverInfo.quantity = "";
			deliverInfo.price = "";
			deliverInfo.sup = "";
			successMessage = "Новый автомобиль добавлен успешно!";

			Response.Redirect("/Cars/Cars");
		}
	}
}

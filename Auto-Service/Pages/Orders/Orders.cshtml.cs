using Auto_Service.Pages.Spares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Orders
{
    public class OrdersModel : PageModel
    {
		public List<OrderInfo> listOrders = new List<OrderInfo>();
		public void OnGet()
        {
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM orders";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								OrderInfo orderInfo = new OrderInfo();
								orderInfo.id = "" + reader.GetInt32(0);
								orderInfo.date = reader.GetString(1);
								orderInfo.price = reader.GetString(2);
								orderInfo.service = reader.GetString(3);
								orderInfo.spare = reader.GetString(4);
								orderInfo.clphone = reader.GetString(5);
								orderInfo.carnum = reader.GetString(6);
								orderInfo.emstaff = reader.GetString(7);

								listOrders.Add(orderInfo);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
    }

	/*Хранение данных сервисов*/
	public class OrderInfo
	{
		public string id;
		public string date;
		public string price;
		public string service;
		public string spare;
		public string clphone;
		public string carnum;
		public string emstaff;
	}
}

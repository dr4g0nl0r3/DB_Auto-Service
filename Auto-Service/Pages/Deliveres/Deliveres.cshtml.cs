using Auto_Service.Pages.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Deliveres
{
    public class DeliveresModel : PageModel
    {
        public List<DeliverInfo> listDeliveres = new List<DeliverInfo>();
		public void OnGet()
        {
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM deliveries";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								DeliverInfo deliverInfo = new DeliverInfo();
								deliverInfo.id = "" + reader.GetInt32(0);
								deliverInfo.date = reader.GetString(1);
								deliverInfo.spname = reader.GetString(2);
								deliverInfo.quantity = reader.GetString(3);
								deliverInfo.price = reader.GetString(4);
								deliverInfo.sup = reader.GetString(5);

								listDeliveres.Add(deliverInfo);
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

	public class DeliverInfo
	{
		public string id;
		public string date;
		public string spname;
		public string quantity;
		public string price;
		public string sup;
	}
}

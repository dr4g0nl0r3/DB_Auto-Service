using Auto_Service.Pages.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Dynamic;

namespace Auto_Service.Pages.Deliveres
{
    public class EditModel : PageModel
    {
		public DeliverInfo deliverInfo = new DeliverInfo();
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
					String sql = "SELECT * FROM deliveries WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								deliverInfo.id = "" + reader.GetInt32(0);
								deliverInfo.date = reader.GetString(1);
								deliverInfo.spname = reader.GetString(2);
								deliverInfo.quantity = reader.GetString(3);
								deliverInfo.price = reader.GetString(4);
								deliverInfo.sup = reader.GetString(5);
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
    }
}

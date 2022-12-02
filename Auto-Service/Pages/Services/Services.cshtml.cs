using Auto_Service.Pages.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Services
{
    public class ServicesModel : PageModel
    {
		public List<ServiceInfo> listServices = new List<ServiceInfo>();
		public void OnGet()
        {
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM services";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								ServiceInfo serviceInfo = new ServiceInfo();
								serviceInfo.id = "" + reader.GetInt32(0);
								serviceInfo.name = reader.GetString(1);
								serviceInfo.price = reader.GetString(2);

								listServices.Add(serviceInfo);
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
	public class ServiceInfo
	{
		public string id;
		public string name;
		public string price;
	}
}

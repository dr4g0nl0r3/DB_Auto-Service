using Auto_Service.Pages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Spares
{
    public class SparesModel : PageModel
    {
		public List<SpareInfo> listSpares = new List<SpareInfo>();
		public void OnGet()
        {
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM spares";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								SpareInfo spareInfo = new SpareInfo();
								spareInfo.id = "" + reader.GetInt32(0);
								spareInfo.name = reader.GetString(1);
								spareInfo.price = reader.GetString(2);

								listSpares.Add(spareInfo);
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
	public class SpareInfo
	{
		public string id;
		public string name;
		public string price;
	}
}

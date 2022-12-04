using Auto_Service.Pages.Spares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Cars
{
    public class CarsModel : PageModel
    {
		public List<CarInfo> listCars = new List<CarInfo>();
		public void OnGet()
        {
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM cars";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								CarInfo carInfo = new CarInfo();
								carInfo.id = "" + reader.GetInt32(0);
								carInfo.name = reader.GetString(1);
								carInfo.carnum = reader.GetString(2);
								carInfo.clphone = reader.GetString(3);

								listCars.Add(carInfo);
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

	public class CarInfo
	{
		public string id;
		public string name;
		public string carnum;
		public string clphone;
	}
}

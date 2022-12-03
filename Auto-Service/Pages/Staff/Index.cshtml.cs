using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Staff
{
	public class IndexModel : PageModel
	{
		public List<StaffInfo> listStaff = new List<StaffInfo>();
		public void OnGet()
		{
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM staff";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								StaffInfo staffInfo = new StaffInfo();
								staffInfo.id = "" + reader.GetInt32(0);
								staffInfo.surname = reader.GetString(1);
								staffInfo.name = reader.GetString(2);
								staffInfo.age = reader.GetString(3);
								staffInfo.email = reader.GetString(4);
								staffInfo.position = reader.GetString(5);
								staffInfo.status = reader.GetString(6);

								listStaff.Add(staffInfo);
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

	/*Хранение данных сотрудников*/
	public class StaffInfo
	{
		public string id;
		public string surname;
		public string name;
		public string age;
		public string email;
		public string position;
		public string status;
	}
}

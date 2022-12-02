using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Service.Pages.Clients
{
    public class ClientsModel : PageModel
    {
		public List<ClientInfo> listClients = new List<ClientInfo>();
		public void OnGet()
        {
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=auto-service;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM clients";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								ClientInfo clientInfo = new ClientInfo();
								clientInfo.id = "" + reader.GetInt32(0);
								clientInfo.surname = reader.GetString(1);
								clientInfo.name = reader.GetString(2);
								clientInfo.phone = reader.GetString(3);
								clientInfo.type = reader.GetString(4);

								listClients.Add(clientInfo);
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

	/*Хранение данных клиентов*/
	public class ClientInfo
	{
		public string id;
		public string surname;
		public string name;
		public string phone;
		public string type;
	}
}

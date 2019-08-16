using Microsoft.VisualBasic;
using System.Data;
namespace ToyTron
{
	//This class is a wrapper for the database to function well with the Emulator class
	public class Program : Emulator
	{
		#region "Private Variables"
		private string _Name;
		private int _ID;
			#endregion
		private string _Description;
		#region "Public Functions"
		public void fillFromDatabase(System.Data.SqlClient.SqlDataReader dr)
		{
			_Name = dr["Name"].ToString();
			_ID = (int)dr["ProgramID"];
			_Description = dr["Description"].ToString();
		}
		#endregion
		#region "Public Property"
		public string Name {
			get { return _Name; }
			set { _Name = value; }
		}
		public int ID {
			get { return _ID; }
			set { _ID = value; }
		}
		public string Description {
			get { return _Description; }
			set { _Description = value; }
		}
		#endregion
	}
}
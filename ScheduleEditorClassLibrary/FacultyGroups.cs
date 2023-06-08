using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScheduleEditorClassLibrary
{
    public class FacultyGroups
    {
        public List<Group> Groups { get; set; }

        public FacultyGroups()
        {
            Groups = new List<Group>();
        }

        [JsonConstructor]
        public FacultyGroups(List<Group> groups)
        {
            Groups = groups;
        }

        public void Add(Group group)
        {
            Groups.Add(group);
        }

        public void Fill(string connectionString)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(connectionString);
            string databaseName = builder.Database;

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Groups = FillGroupes(connection, databaseName);
                //foreach (var group in Groups)
                //{
                //    group.FillAcademicClasses(connection);
                //}
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private List<Group> FillGroupes(MySqlConnection connection, string databaseName)
        {
            string query = "";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            return new List<Group>();
        }
    }
}
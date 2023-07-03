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
        public string Year { get; set; }
        public int Semester { get; set; }
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

        public Group this[string title]
        {
            get { return Groups.Find(group => group.Title == title); }
        }
        public static List<Teacher> GetTeachers(string connectionString)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string databaseName = connection.Database;
            string query = $"SELECT surname, name, patronimyc FROM {databaseName}.employees";
            MySqlCommand command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();
            var teachers = new List<Teacher>();

            string teacherName;
            while (reader.Read())
            {
                var surname = reader.GetValue(0);
                var name = reader.GetValue(1);
                var patronimyc = reader.GetValue(2);

                teacherName = string.Format("{0} {1} {2}", surname, name, patronimyc);
                teachers.Add(new Teacher(teacherName));
            }
            reader.Close();
            connection.Close();

            return teachers;
        }

        public void Fill(string connectionString)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(connectionString);
            string databaseName = builder.Database;

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                FillGroupes(connection, databaseName);
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

        private void FillGroupes(MySqlConnection connection, string databaseName)
        {
            string queryGetSchedule = $"SELECT * FROM {databaseName}.groups";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(queryGetSchedule, connection);
            DataSet dataSetSchedule = new DataSet();
            dataAdapter.Fill(dataSetSchedule);

            DataTable tableSchedule = dataSetSchedule.Tables[0];
            foreach (DataRow row in tableSchedule.Rows)
            {
                string groupTitle = row[1].ToString();
                Group group = new Group(groupTitle);
                group.FillAcademicClasses(connection, databaseName);
                Groups.Add(group);
            }
        }

        internal static int GetGroupId(string title, string databaseName, MySqlConnection connection)
        {
            string queryGetGroupId = $"SELECT id FROM {databaseName}.groups WHERE title='{title}' LIMIT 1";
            MySqlCommand command = new MySqlCommand(queryGetGroupId, connection);
            return (int)command.ExecuteScalar();
        }
    }
}
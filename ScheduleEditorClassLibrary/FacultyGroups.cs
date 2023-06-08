﻿using MySql.Data.MySqlClient;
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
            string queryGetSchedule = $"SELECT * FROM {databaseName}.groups";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(queryGetSchedule, connection);
            DataSet dataSetSchedule = new DataSet();
            dataAdapter.Fill(dataSetSchedule);

            List<Group> result = new List<Group>();

            DataTable tableSchedule = dataSetSchedule.Tables[0];
            foreach (DataRow row in tableSchedule.Rows)
            {
                string groupTitle = row[1].ToString();
                Group group = new Group(groupTitle);
                group.FillAcademicClasses(connection, databaseName);
                result.Add(group);
            }

            return result;
        }
    }
}
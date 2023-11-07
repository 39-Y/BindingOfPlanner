using System.Collections.Generic;
using System.Data;
using Action.VO;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Action
{
    public class ActionService
    {
        private string dbname;
        private string connectionString;
        private IDbConnection dbConnection;
        private string table = "Action";
        
        public ActionService()
        {
            dbname = "/BidingOfPlanner_dev.db";
            connectionString = "URI=file:" + Application.streamingAssetsPath + dbname;
            dbConnection = new SqliteConnection(connectionString);
        }
        
        public void save()
        {
            dbConnection.Open();
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "INSERT INTO Action(title, content, is_completed, create_date) " +
                                    "VALUES ('추가 Action', 'content', 0, '2023-10-10')";
            dbCommand.ExecuteNonQuery();
            dbConnection.Dispose();
            dbConnection.Close();
        }

        public void updateDoDateById(long id)
        {
            
        }

        public List<ActionVO> FindByPeriod(string startDate, string endDate)
        {
            List<ActionVO> actionVos = new List<ActionVO>();
            
            dbConnection.Open();
            IDbCommand dbCommand = dbConnection.CreateCommand();
             string query = $"SELECT * FROM {table} " +
                                    $"WHERE do_date BETWEEN '{startDate}' AND '{endDate}' " +
                                    $"ORDER BY do_date";
            Debug.Log($"query:{query}");
             dbCommand.CommandText = query;
            IDataReader dataReader = dbCommand.ExecuteReader();
            
            
            while (dataReader.Read())
            {
                long id = dataReader.GetInt32(0);
                string title = dataReader.GetString(1);
                string content = dataReader.GetString(2);
                bool isCompleted = dataReader.GetBoolean(3);
                string createDate = dataReader.GetString(4);
                string doDate = dataReader.GetString(5);
                actionVos.Add(new ActionVO(id, title, content, isCompleted, createDate, doDate));
            }

            dbConnection.Close();
            
            return actionVos;
        }
        
        public List<ActionVO> FindAll()
        {
            List<ActionVO> actionVos = new List<ActionVO>();
            
            dbConnection.Open();
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = $"SELECT * FROM {table} order by create_date";
            IDataReader dataReader = dbCommand.ExecuteReader();
            
            
            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                string title = dataReader.GetString(1);
                string content = dataReader.GetString(2);
                bool isCompleted = dataReader.GetBoolean(3);
                string createDate = dataReader.GetString(4);
                string doDate = dataReader.GetString(5);
                actionVos.Add(new ActionVO(id, title, content, isCompleted, createDate, doDate));
            }

            dbConnection.Close();
            
            return actionVos;
        }
    }
}
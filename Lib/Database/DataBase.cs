using Microsoft.Data.Sqlite;

namespace QQBot_Jump.Lib.Database;

public abstract class Database
{
    private const string ConnectionString = "Data Source=app.db";

    private static SqliteConnection GetConnection()
    {
        var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        return conn;
    }

    /// <summary>
    /// 执行SQL语句,只适用于执行非查询操作，INSERT/UPDATE/DELETE
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    protected static int Execute(string sql, Dictionary<string, object> parameters)
    {
        using var conn = GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        foreach (var param in parameters)
        {
            cmd.Parameters.AddWithValue(param.Key, param.Value);
        }

        return cmd.ExecuteNonQuery();
    }
    
    /// <summary>
    /// 执行SQL语句,只适用于执行查询操作，SELECT
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    protected static List<Dictionary<string, object>> Query(string sql, Dictionary<string, object> parameters)
    {
        var results = new List<Dictionary<string, object>>();

        using var conn = GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        foreach (var param in parameters)
        {
            cmd.Parameters.AddWithValue(param.Key, param.Value);
        }

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var row = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] = reader.GetValue(i);
            }
            results.Add(row);
        }

        return results;
    }
}
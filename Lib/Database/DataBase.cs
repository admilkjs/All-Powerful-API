using Microsoft.Data.Sqlite;

namespace QQBot_Jump.Lib.Database;

public abstract class Database
{
    protected const string ConnectionString = "Data Source=app.db";

    protected SqliteConnection GetConnection()
    {
        var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        return conn;
    }

    protected int Execute(string sql, Dictionary<string, object> parameters)
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

    protected List<T> Query<T>(string sql, Func<SqliteDataReader, T> map)
    {
        var result = new List<T>();

        using var conn = GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            result.Add(map(reader));
        }

        return result;
    }
}
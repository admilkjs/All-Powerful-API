using QQBot_Jump.Lib.Database;
using Microsoft.Data.Sqlite;

namespace QQBot_Jump.Data;

public class UserRepository : Database
{
    public UserRepository()
    {
        EnsureUserTableExists();
    }

    private void EnsureUserTableExists()
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserName TEXT NOT NULL,
                Token TEXT NOT NULL,
                Role TEXT NOT NULL
            );
        ";

        Execute(sql, new Dictionary<string, object>());
    }

    public void AddUser(string name, string token, string role)
    {
        const string sql = "INSERT INTO Users (UserName, Token, Role) VALUES ($name, $token, $role)";

        var parameters = new Dictionary<string, object>
        {
            ["$name"] = name,
            ["$token"] = token,
            ["$role"] = role
        };

        Execute(sql, parameters);
    }
}
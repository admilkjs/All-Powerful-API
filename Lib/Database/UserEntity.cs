using QQBot_Jump.Lib.Database;

namespace QQBot_Jump.Data;

public class UserRepository : Database
{
    public UserRepository()
    {
        EnsureUserTableExists();
    }

    /// <summary>
    /// 创建用户表
    /// </summary>
    private static void EnsureUserTableExists()
    {
        const string sql = "CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY AUTOINCREMENT,username TEXT NOT NULL,token TEXT NOT NULL,role TEXT NOT NULL);";

        Execute(sql, new Dictionary<string, object>());
    }

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="name"></param>
    /// <param name="role"></param>
    public static void AddUser(string name, string role)
    {
        
        const string sql = "INSERT INTO Users (username, token, role) VALUES ($name, $token, $role)";

        var parameters = new Dictionary<string, object>
        {
            ["$name"] = name,
            ["$token"] = GetNewUserToken(),
            ["$role"] = role
        };

        Execute(sql, parameters);
    }
    /// <summary>
    /// 生成一个新的用户token
    /// </summary>
    /// <returns></returns>
    private static string GetNewUserToken()
    {
      var random = new Random();
      const string str = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
      var token = "";
      for (var i = 0; i<=64; i++)
      {
          token += str[random.Next(str.Length)];
      }

      return token;
    }

    public static string? GetUserRole(string username)
    {
        const string sql = "SELECT role FROM Users WHERE username = $username";
        var parameters = new Dictionary<string, object>
        {
            ["$username"] = username
        };
        var results = Query(sql,parameters);
        return results[0]["role"].ToString() ?? null;
    }
}
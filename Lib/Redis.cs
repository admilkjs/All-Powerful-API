using StackExchange.Redis;

namespace QQBot_Jump.Lib;

public class Redis
{
    private readonly IDatabase _db;
    
    public Redis(IConnectionMultiplexer redis)
    {
        // 获取数据库,默认连接 db0
        _db = redis.GetDatabase();
    }
    
    /// <summary>
    /// 设置字符串值
    /// </summary>
    /// <param name="key">Key值</param>
    /// <param name="value">Key的内容</param>
    /// <param name="expiry">过期时间,默认不过期</param>
    /// <returns></returns>
    public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null)
    {
        return await _db.StringSetAsync(key, value, expiry);
    }

    /// <summary>
    /// 获取字符串值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<string?> GetStringAsync(string key)
    {
        return await _db.StringGetAsync(key);
    }
    
    /// <summary>
    /// 删除Key值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<bool> DeleteKeyAsync(string key)
    {
        return await _db.KeyDeleteAsync(key);
    }
    
    /// <summary>
    /// 判断Key是否存在
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<bool> KeyExistsAsync(string key)
    {
        return await _db.KeyExistsAsync(key);
    }
    
    // TODO: 以后要用再加吧,目前没有Hash存储需求
}
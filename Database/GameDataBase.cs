using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
namespace OursGalaxy.DataBase;

/// <summary>
/// 静态数据库类
/// </summary>
internal partial class GameDataBase {
	/// <summary>
	/// 设定密码，后续连接一直使用
	/// </summary>
	/// <param name="password"></param>
	public static void SetPassword(string? password = null) {
		connectCmd = new SqliteConnectionStringBuilder() {
			DataSource = "/home/Ervn/Projects/OursGalaxy/DataBase/OursGalaxyDB.db",
			Mode = SqliteOpenMode.ReadOnly,
			Password = password
		}.ToString();
	}

	/// <summary>
	/// 手动确保连接
	/// </summary>
	public static void Connect() {
		if (Connection is not null && Connection.State == System.Data.ConnectionState.Open) {
			return;
		}
		Connection?.Dispose();
		Connection = new SqliteConnection(connectCmd);
		Connection.Open();
	}

	/// <summary>
	/// 手动确保断连
	/// </summary>
	public static void Disconnet() {
		if (Connection == null) {
			return;
		}
		if (Connection.State != System.Data.ConnectionState.Closed) {
			Connection.Close();
		}
		Connection.Dispose();
		Connection = null;
	}

	/// <summary>
	/// 查询数据，以string返回
	/// </summary>
	/// <param name="table">表名</param>
	/// <param name="name">物品名</param>
	/// <param name="key">查询数据项名称</param>
	/// <returns>查询结果，如果没有有效值返回string.Empty</returns>
	public static string QueryString(string table, string name, string key) {
		Connect();
		using var cmd = Connection!.CreateCommand();
		cmd.CommandText = "SELECT Key FROM Table WHERE \"Code\" = Name";
		cmd.Parameters.AddWithValue("@Key", key);
		cmd.Parameters.AddWithValue("@Table", table);
		cmd.Parameters.AddWithValue("@Name", name);
		var obj = cmd.ExecuteScalar();
		return obj switch {
			DBNull => string.Empty,
			null => string.Empty,
			_ => obj?.ToString() ?? string.Empty,
		};
	}

	/// <summary>
	/// 查询数据，以int返回
	/// </summary>
	/// <param name="table">表名</param>
	/// <param name="name">物品名</param>
	/// <param name="key">查询数据项名称</param>
	/// <returns>查询结果，如果没有有效值则抛出异常</returns>
	public static int QueryInt(string table, string name, string key) {
		if (int.TryParse(QueryString(table, name, key), out var result)) {
			return result;
		}
		throw new ArgumentException("not a int");
	}

	public static float QueryFloat(string table, string name, string key) {
		if (float.TryParse(QueryString(table, name, key), out var result)) {
			return result;
		}
		throw new ArgumentException("not a float");
	}

	public static bool QueryBool(string table, string name, string key) {
		if (bool.TryParse(QueryString(table, name, key), out var result)) {
			return result;
		}
		throw new ArgumentException("not a bool");
	}

	/// <summary>
	/// 查询多个项。结果以key=>value的字典返回
	/// </summary>
	/// <param name="table">表名</param>
	/// <param name="name">物品名</param>
	/// <param name="keys">项名列表</param>
	/// <returns>返回keys=>value的字典</returns>
	public static System.Collections.Generic.Dictionary<string, string?> Query(string table, string name, params string[] keys) {
		Connect();
		using var cmd = Connection!.CreateCommand();
		cmd.CommandText = $"SELECT * FROM Table WHERE \"Code\" = Name";
		cmd.Parameters.AddWithValue("@Table", table);
		cmd.Parameters.AddWithValue("@Name", name);
		using var reader = cmd.ExecuteReader();
		var result = new Dictionary<string, string?>();
		if (reader.Read()) {
			foreach (var key in keys) {
				var dbobj = reader.IsDBNull(reader.GetOrdinal(key));
				var singleItem = dbobj ? null : reader.GetString(reader.GetOrdinal(key));
				result.Add(key, singleItem);
			}
		}
		return result;
	}

	/// <summary>
	/// 查询是否包含键
	/// </summary>
	/// <param name="table">表名</param>
	/// <param name="keys">键名</param>
	/// <returns>是否包含</returns>
	public static bool ContainsKeys(string table, params string[] keys) {
		Connect();
		using var cmd = new SqliteCommand($"PRAGMA table_info({table})", Connection);
		using var reader = cmd.ExecuteReader();
		var cols = new HashSet<string>();
		while (reader.Read()) {
			cols.Add(reader.GetString(1));
		}
		return keys.All(key => cols.Contains(key));
	}

	private static SqliteConnection? Connection = new();
	private static string connectCmd = new SqliteConnectionStringBuilder() {
		DataSource = "/home/Ervn/Projects/OursGalaxy/DataBase/OursGalaxyDB.db",
		Mode = SqliteOpenMode.ReadOnly,
	}.ToString();
}

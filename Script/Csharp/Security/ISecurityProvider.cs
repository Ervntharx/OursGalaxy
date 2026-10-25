namespace OursGalaxy;

/// <summary>
/// 为校验防作弊和加密提供的安全设施需要满足此接口
/// </summary>
public interface ISecurityProvider {
	/// <summary>
	/// 安全设施可用
	/// </summary>
	public bool IsAvailable { get; }

	/// <summary>
	/// 取得校验码
	/// </summary>
	/// <typeparam name="T">原始数据类型</typeparam>
	/// <param name="data">原始数据</param>
	/// <returns>校验码</returns>
	string? GetChecksum<T>(T data);

	/// <summary>
	/// 加密.不可用时返回null
	/// </summary>
	/// <param name="plainText">原文</param>
	/// <returns>密文</returns>
	byte[]? Encrypt(byte[] plainText);

	/// <summary>
	/// 解密
	/// </summary>
	/// <param name="cipherText">密文</param>
	/// <returns>原文</returns>
	byte[]? Decrypt(byte[] cipherText);
}

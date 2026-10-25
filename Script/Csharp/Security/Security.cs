namespace OursGalaxy;

/// <summary>
/// 静态安全服务器，用于提供一些安全设施
/// </summary>
internal class SecurityServer {
	public static bool TryRegisterOnlineService(ISecurityProvider ISPr) {
		if (onlineISPr is not null) {
			return false;
		}
		onlineISPr = ISPr;
		return true;
	}
	/// <summary>
	/// 安全服务可用
	/// </summary>
	public static bool IsAvailable => onlineISPr is not null && onlineISPr.IsAvailable;

	/// <summary>
	/// 取得校验码
	/// </summary>
	/// <typeparam name="T">原始数据类型</typeparam>
	/// <param name="data">原始数据</param>
	/// <returns>校验码</returns>
	public static string? GetChecksum<T>(T data) => onlineISPr?.GetChecksum(data);

	/// <summary>
	/// 加密.不可用时返回null
	/// </summary>
	/// <param name="plainText">原文</param>
	/// <returns>密文</returns>
	public static byte[]? Encrypt(byte[] plainText) => onlineISPr?.Encrypt(plainText);

	/// <summary>
	/// 解密
	/// </summary>
	/// <param name="cipherText">密文</param>
	/// <returns>原文</returns>
	public static byte[]? Decrypt(byte[] cipherText) => onlineISPr?.Decrypt(cipherText);

	private static ISecurityProvider? onlineISPr;
}

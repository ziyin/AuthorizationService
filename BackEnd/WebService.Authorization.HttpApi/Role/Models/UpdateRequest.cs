namespace WebService.Authorization.HttpApi.Role.Models;

/// <summary>
/// 更新角色的Request
/// </summary>
public class UpdateRequest
{
    /// <summary>
    /// 角色名稱
    /// </summary>
    public string RoleName { get; set; } = null!;
    /// <summary>
    /// 是否啟用
    /// </summary>
    public bool IsEnable { get; set; }
}
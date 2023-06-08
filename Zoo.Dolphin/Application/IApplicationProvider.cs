namespace Zoo.Dolphin.Application;

public interface IApplicationProvider
{
    /// <summary>
    /// 获取应用信息
    /// </summary>
    /// <returns></returns>
    ApplicationOption GetAppInfo();
}

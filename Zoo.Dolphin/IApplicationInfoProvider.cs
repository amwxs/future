namespace Zoo.Dolphin
{
    public interface IApplicationInfoProvider
    {
        /// <summary>
        /// 获取应用信息
        /// </summary>
        /// <returns></returns>
        ApplicationOption GetInfo();
    }
}

#region

using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

#endregion

namespace AE_ACR_DRK_Setting;

/// <summary>
///     配置文件适合放一些一般不会在战斗中随时调整的开关数据
///     如果一些开关需要在战斗中调整 或者提供给时间轴操作 那就用QT
///     非开关类型的配置都放配置里 比如诗人绝峰能量配置
/// </summary>
public class DKSettings
{
    public static DKSettings Instance;


    public int 保留蓝量 = 3000;
    public int 爆发目标血量 = 20;
    public float 能力技爆发延时 = 5f;
    public float GCD爆发延时 = 7f;
    public float 伤残阈值 = 10f;
    public bool 日常模式 = false;
    public bool 挑衅 = true;
    public bool 日常模式_残血不打爆发 = true;
    
    public JobViewSave JobViewSave = new(); // QT设置存档

    public int get爆发目标血量()
    {
        return 爆发目标血量 * 10000;
    }

    #region 标准模板代码 可以直接复制后改掉类名即可

    private static string path;
 

    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, nameof(DKSettings) + "DK.json");
        if (!File.Exists(path))
        {
            Instance = new DKSettings();
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<DKSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new DKSettings();
            LogHelper.Error(e.ToString());
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }

    #endregion
}
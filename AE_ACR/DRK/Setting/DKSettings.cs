#region

using AE_ACR_DRK;
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
    public bool 日常模式 = false;
    public bool 日常模式_残血不打爆发 = false;
    public float 伤残阈值 = 6f;
    public bool 挑衅 = true;
    public float GCD爆发延时 = 7f;
    public bool 起手突进 = true;
    public bool 只在高难模式使用起手序列 = true;
    public bool 自动黑盾 = true;
    public float 近战最大攻击距离 = 2.99f;


    public bool 上天血乱 = false;
    public float 上天血乱开始时间 = 30;
    public float 上天血乱结束时间 = 60 * 5 + 30;
    
    public bool AOE雪仇 = true;
    public bool AOE步道 = true;

    public JobViewSave JobViewSave = new(); // QT设置存档
    public Dictionary<string, bool> MyQtDict = new();
    public List<string> QtUnVisibleList = new();

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

        if (DRKRotationEntry.QT != null)
        {
            string[] qtArray = DRKRotationEntry.QT.GetQtArray();
            foreach (var qtName in qtArray)
            {
                var qtValue = DRKRotationEntry.QT.GetQt(qtName);
                MyQtDict[qtName] = qtValue;
            }
        }

        if (JobViewSave.QtUnVisibleList.Any())
        {
            QtUnVisibleList.Clear();
            foreach (string hideQt in JobViewSave.QtUnVisibleList)
            {
                QtUnVisibleList.Add(hideQt);
            }
        }


        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }

    #endregion
}
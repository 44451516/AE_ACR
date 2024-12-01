#region

using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

#endregion

namespace AE_ACR.PLD.Setting;

public class PLDSettings
{
    public static PLDSettings Instance;
    public float 调停保留层数 = 0f;

    public bool 日常模式 = false;
    public bool 日常模式_残血不打爆发 = false;
    public bool 挑衅 = true;
    public float 投盾阈值 = 5f;
    public float 远程圣灵阈值 = 5f;
    public float 近战最大攻击距离 = 2.99f;
    public bool 起手突进 = true;
    public float 最大突进距离 = 20f;
    public bool 起手幕帘 = false;
    public int 起手幕帘阈值 = 5000;
    public int 起手圣灵阈值 = 1500;
    public int USE_AOE = 3;

    public JobViewSave JobViewSave = new(); // QT设置存档
    public Dictionary<string, bool> MyQtDict = new();
    public List<string> QtUnVisibleList = new();

    #region 标准模板代码 可以直接复制后改掉类名即可

    private static string path;

    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, nameof(PLDSettings) + "PLD.json");
        if (!File.Exists(path))
        {
            Instance = new PLDSettings();
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<PLDSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new PLDSettings();
            LogHelper.Error(e.ToString());
        }
    }

    public void Save()
    {
        if (PLDRotationEntry.QT != null)
        {
            string[] qtArray = PLDRotationEntry.QT.GetQtArray();
            foreach (var qtName in qtArray)
            {
                var qtValue = PLDRotationEntry.QT.GetQt(qtName);
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
#region

using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

#endregion

namespace AE_ACR.WAR;

#if DEBUG
public class WARSettings
{
    public static WARSettings Instance;

    public JobViewSave JobViewSave = new(); // QT设置存档

    #region 标准模板代码 可以直接复制后改掉类名即可

    private static string path;

    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "WAR.json");
        if (!File.Exists(path))
        {
            Instance = new WARSettings();
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<WARSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new WARSettings();
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
#endif
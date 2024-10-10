#region

#endregion

using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace AE_ACR.VIP;

#if DEBUG
public class VIPSettings
{
    public static VIPSettings Instance;

    public JobViewSave JobViewSave = new(); // QT设置存档

    #region 标准模板代码 可以直接复制后改掉类名即可

    private static string path;

    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "VIP.json");
        if (!File.Exists(path))
        {
            Instance = new VIPSettings();
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<VIPSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new VIPSettings();
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
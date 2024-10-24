﻿#region

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
    public bool 日常模式_残血不打爆发 = true;
    public bool 挑衅 = true;
    public float 投盾阈值 = 5f;
    public float 远程圣灵阈值 = 5f;
    public float 近战最大攻击距离 = 2.99f;
    public bool 起手突进 = true;

    public JobViewSave JobViewSave = new(); // QT设置存档

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
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }

    #endregion
}
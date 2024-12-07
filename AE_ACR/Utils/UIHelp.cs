using AE_ACR.PLD.Setting;
using AEAssist.CombatRoutine.View.JobView;
using Dalamud.Utility;
using ImGuiNET;

namespace AE_ACR.utils;

public static class UIHelp
{
    public static void Feedback(JobViewWindow obj)
    {
        ImGui.Text("请先提前组织好语言");
        ImGui.Text("请先提前组织好语言");
        ImGui.Text("请先提前组织好语言");
        if (ImGui.Button("反馈建议"))
        {
            Util.OpenLink("https://docs.qq.com/sheet/DT0NkbkNKd0NXTWRM");
        }
    }


    public static void MyAddQt(this JobViewWindow jobView, Dictionary<string, bool> MyQtDict, string name, bool qtValueDefault, string toolTip = "")
    {
        var qtArray = MyQtDict;

        if (qtArray.TryGetValue(name, out var qt))
        {
            jobView.AddQt(name, qt, toolTip);
        }
        else
        {
            jobView.AddQt(name, qtValueDefault, toolTip);
        }
    }


    public static void MyAddQt(this JobViewWindow jobView, Dictionary<string, bool> MyQtDict, string name, bool qtValueDefault, Action<bool> action, string toolTip = "")
    {
        var qtArray = MyQtDict;

        if (qtArray.TryGetValue(name, out var qt))
        {
            jobView.AddQt(name, qt, action);
        }
        else
        {
            jobView.AddQt(name, qtValueDefault, action);
        }
    }
}
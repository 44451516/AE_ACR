using AEAssist.CombatRoutine.View.JobView;
using Dalamud.Utility;
using ImGuiNET;

namespace AE_ACR.utils;

public class UIHelp
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
}
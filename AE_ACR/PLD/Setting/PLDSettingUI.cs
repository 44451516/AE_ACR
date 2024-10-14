using ImGuiNET;

namespace AE_ACR.PLD.Setting;

public class PLDSettingUI
{
    public static PLDSettingUI Instance = new();
    public PLDSettings PldSettings => PLDSettings.Instance;

    public void Draw()
    {
        var pldSettings = PLDSettings.Instance;
        ImGui.Text("这里是日常模式的设置\n日常模式会持续开盾，和自动减伤");
        ImGui.SetNextItemWidth(150f);
        ImGui.Checkbox("启用", ref pldSettings.日常模式);
        ImGui.SetNextItemWidth(150f);
        ImGui.Checkbox("使用挑衅", ref pldSettings.挑衅);
        // ImGui.SetNextItemWidth(150f);
        ImGui.Checkbox("日常模式-残血不打爆发[测试中]", ref pldSettings.日常模式_残血不打爆发);
    }
}
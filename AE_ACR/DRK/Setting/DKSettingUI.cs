#region

using ImGuiNET;

#endregion

namespace AE_ACR_DRK_Setting;

public class DKSettingUI
{
    public static DKSettingUI Instance = new();
    public DKSettings DkSettings => DKSettings.Instance;

    public void Draw()
    {
        BaseDraw();
    }


    public static void BaseDraw()
    {
        var dkSettings = Instance.DkSettings;
        ImGui.SetNextItemWidth(150f);
        ImGui.InputInt("保留蓝量", ref dkSettings.保留蓝量, 0, 10000);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputInt("目标小于多少血打完所有资源[单位万]", ref dkSettings.爆发目标血量, 0, 100);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputFloat("能力技爆发延时", ref dkSettings.能力技爆发延时);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputFloat("GCD爆发延时", ref dkSettings.GCD爆发延时);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputFloat("伤残阈值", ref dkSettings.伤残阈值);
        ImGui.Checkbox("起手突进",ref dkSettings.起手突进);
        ImGui.Checkbox("只在高难模式使用起手序列",ref dkSettings.只在高难模式使用起手序列);
        if (ImGui.Button("Save[保存]"))
            DKSettings.Instance.Save();
    }
}
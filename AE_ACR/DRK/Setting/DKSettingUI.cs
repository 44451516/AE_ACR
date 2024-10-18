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
        BaseDarw();
    }


    public static void BaseDarw()
    {
        var dkSettings = DKSettingUI.Instance.DkSettings;
        ImGui.SetNextItemWidth(150f);
        ImGui.InputInt("保留蓝量", ref dkSettings.保留蓝量, 0, 10000);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputInt("目标小于多少血打完所有资源[单位万]", ref dkSettings.爆发目标血量, 0, 100);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputFloat("能力技爆发延时", ref dkSettings.能力技爆发延时);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputFloat("GCD爆发延时", ref dkSettings.GCD爆发延时);
        ImGui.Checkbox("起手突进",ref dkSettings.起手突进);
        if (ImGui.Button("Save[保存]"))
            DKSettings.Instance.Save();
    }
}
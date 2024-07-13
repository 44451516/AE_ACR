using AEAssist.GUI;
using ImGuiNET;

namespace AE_ACR_DRK_Setting;

public class DKSettingUI
{
    public static DKSettingUI Instance = new();
    public DKSettings DkSettings => DKSettings.Instance;

    public void Draw()
    {
        // ImGui.Checkbox("使用速行", ref DKSettings.Instance.UsePeloton);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputInt("保留蓝量", ref DkSettings.保留蓝量, 0, 10000);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputInt("目标小于多少血打完所有资源[单位万]", ref DkSettings.爆发目标血量, 0, 100);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputFloat("能力技爆发延时", ref DkSettings.能力技爆发延时);
        ImGui.SetNextItemWidth(150f);
        ImGui.InputFloat("GCD爆发延时", ref DkSettings.GCD爆发延时);


        // ImGuiHelper.LeftInputInt("保留蓝量", ref DKSettings.Instance.保留蓝量, 0, 10000);
        // ImGuiHelper.LeftInputInt("目标小于多少血打完所有资源[单位万]", ref DKSettings.Instance.爆发目标血量, 0, 100);
        // ImGuiHelper.LeftInputFloat("能力技爆发延时", ref DKSettings.Instance.能力技爆发延时);
        // ImGuiHelper.LeftInputFloat("GCD爆发延时", ref DKSettings.Instance.GCD爆发延时);
        ImGui.Checkbox("123不打血溅", ref DKSettings.Instance.留资源);

        if (ImGui.Button("Save[保存]"))
        {
            DKSettings.Instance.Save();
        }
    }
}
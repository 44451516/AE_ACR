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
        ImGui.DragFloat("近战最大攻击距离", ref dkSettings.近战最大攻击距离, 0.1f, 2.5f, 15f);
        
        ImGui.Spacing();
        ImGui.Checkbox("上天血乱", ref dkSettings.上天血乱);
        ImGui.DragFloat("上天血乱开始时间", ref dkSettings.上天血乱开始时间, 1f, 30f, 30 * 20f);
        ImGui.DragFloat("上天血乱结束时间", ref dkSettings.上天血乱结束时间, 1f, 30f, 30 * 20f);
        ImGui.Spacing();
        
        // ImGui.Checkbox("起手盾姿",ref dkSettings.起手盾姿);
        
        
        ImGui.Checkbox("起手突进",ref dkSettings.起手突进);
        ImGui.Checkbox("起手关盾",ref dkSettings.起手关盾);
        ImGui.Checkbox("只在高难模式使用起手序列",ref dkSettings.只在高难模式使用起手序列);
        ImGui.Spacing();
        ImGui.Checkbox("绝伊甸设置", ref dkSettings.绝伊甸设置);
        ImGui.Checkbox("M6S设置", ref dkSettings.M6S设置);
        ImGui.SameLine();
        ImGui.Text("圣灵优先打人马/猫/鱼,自动下踢炸脖龙");
        ImGui.Checkbox("M7S设置", ref dkSettings.M7S设置);
        ImGui.SameLine();
        ImGui.Text("自动沉默，自动拉小怪");
        ImGui.Spacing();
        ImGui.Spacing();
        
        if (ImGui.Button("Save[保存]"))
            DKSettings.Instance.Save();
    }
}
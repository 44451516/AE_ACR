using ImGuiNET;

namespace AE_ACR.PLD.Setting;

public class PLDSettingUI
{
    public static PLDSettingUI Instance = new();
    public PLDSettings PldSettings => PLDSettings.Instance;

    public void Draw()
    {
        BaseDraw();
    }

    public static void BaseDraw()
    {
        var pldSettings = Instance.PldSettings;
        ImGui.DragFloat("投盾阈值", ref pldSettings.投盾阈值, 0.1f, 5, 20f);
        ImGui.DragFloat("远程圣灵阈值", ref pldSettings.远程圣灵阈值, 0.1f, 5, 20f);
        ImGui.Text("如果想随时保留有一层【调停】的话设置为1.99");
        ImGui.DragFloat("调停充能释放时期", ref pldSettings.调停保留层数, 0.1f, 0, 2);
        ImGui.DragFloat("近战最大攻击距离", ref pldSettings.近战最大攻击距离, 0.1f, 2.5f, 15f);
        ImGui.DragInt("附近5M多少怪物放AOE", ref pldSettings.USE_AOE, 1, 1, 10);

        ImGui.DragFloat("最大突进距离(超过就不用了)", ref pldSettings.最大突进距离, 0.1f, 0f, 25f);
        // ImGui.Checkbox("最优面向大翅膀只在战斗中", ref pldSettings.最优面向大翅膀只在战斗中);

        ImGui.Spacing();
        ImGui.Checkbox("上天战逃", ref pldSettings.上天战逃);
        ImGui.DragFloat("上天战逃开始时间", ref pldSettings.上天战逃开始时间, 1f, 30f, 30 * 20f);
        ImGui.DragFloat("上天战逃结束时间", ref pldSettings.上天战逃结束时间, 1f, 30f, 30 * 20f);
        ImGui.Spacing();

        ImGui.Text("起手设置");
        ImGui.Checkbox("起手突进", ref pldSettings.起手突进);
        ImGui.Checkbox("起手幕帘", ref pldSettings.起手幕帘);
        ImGui.Checkbox("起手关盾", ref pldSettings.起手关盾);
        ImGui.Text("在倒计时多少的是使用幕帘，单位是毫秒，倒计时5秒填5000");
        ImGui.DragInt("起手幕帘阈值", ref pldSettings.起手幕帘阈值, 1000, 300, 30_000);
        ImGui.DragInt("起手圣灵阈值", ref pldSettings.起手圣灵阈值, 1000, 500, 2000);
        ImGui.Spacing();
        ImGui.Checkbox("绝伊甸设置", ref pldSettings.绝伊甸设置);
        ImGui.Checkbox("M6S设置", ref pldSettings.M6S设置);
        ImGui.SameLine();
        ImGui.Text("圣灵优先打人马/猫/鱼,自动下踢炸脖龙");
        ImGui.Checkbox("M7S设置", ref pldSettings.M7S设置);
        ImGui.SameLine();
        ImGui.Text("自动沉默，自动拉小怪");
        // ImGuiEx.H
        
        ImGui.Spacing();
        ImGui.Spacing();
        if (ImGui.Button("Save[保存]"))
        {
            PLDSettings.Instance.Save();
        }
    }
}
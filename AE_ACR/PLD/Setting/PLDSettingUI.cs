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
        ImGui.DragFloat("调停保留层数", ref pldSettings.调停保留层数, 0.1f, 0, 2);
        ImGui.DragFloat("近战最大攻击距离", ref pldSettings.近战最大攻击距离, 0.1f, 2.5f, 15f);
        ImGui.Checkbox("起手突进",ref pldSettings.起手突进);
        if (ImGui.Button("Save[保存]"))
        {
            PLDSettings.Instance.Save();
        }
    }
}
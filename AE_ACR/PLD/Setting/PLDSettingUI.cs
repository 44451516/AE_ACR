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
        ImGui.DragInt("附近5M多少怪物放AOE", ref pldSettings.USE_AOE, 1, 1, 10);
        
        ImGui.Checkbox("起手幕帘",ref pldSettings.起手幕帘);
        ImGui.Text("在倒计时多少的是使用幕帘，单位是毫秒，倒计时5秒填5000");
        ImGui.DragInt("起手幕帘阈值", ref pldSettings.起手幕帘阈值, 1000, 100, 30_000);
        ImGui.DragFloat("最大突进距离(超过就不用了)", ref pldSettings.最大突进距离, 0.1f, 0f, 25f);
        
        if (ImGui.Button("Save[保存]"))
        {
            PLDSettings.Instance.Save();
        }
    }
}
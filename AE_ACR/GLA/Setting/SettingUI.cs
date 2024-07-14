using ImGuiNET;

namespace AE_ACR.GLA.Setting;

public class SettingUI
{
    public static SettingUI Instance = new();
    public Settings Settings => Settings.Instance;

    public void Draw()
    {
    }
}
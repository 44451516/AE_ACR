using AE_ACR.GLA.Setting;

namespace AE_ACR.AST.Setting;

public class ASTSettingUI
{
    public static ASTSettingUI Instance = new();
    public Settings Settings => Settings.Instance;

    public void Draw()
    {
    }
}
namespace AE_ACR.AST.Setting;

public class ASTSettingUI
{
    public static ASTSettingUI Instance = new();
    public GLA.Setting.Settings Settings => GLA.Setting.Settings.Instance;

    public void Draw()
    {
    }
}
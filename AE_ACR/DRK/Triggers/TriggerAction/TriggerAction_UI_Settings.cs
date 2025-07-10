#region

using System.Numerics;
using AE_ACR_DRK_Setting;
using AE_ACR_DRK;
using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;

#endregion

namespace AE_ACR.DRK.Triggers.TriggerAction;

internal class TriggerAction_UI_Settings : ITriggerAction
{
    public string DisplayName { get; } = "黑骑UI_设置";
    public string? Remark { get; set; }
    

    public TriggerAction_UI_Settings()
    {
    }


    public bool Draw()
    {
        DKSettingUI.Instance.Draw();
        return true;
    }

    public bool Handle()
    {
        DKSettings.Instance.Save();
        return true;
    }
}
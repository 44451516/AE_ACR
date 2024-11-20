using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;

namespace AE_ACR.utils.Triggers;

public class TriggerAction_M1S_Rot : ITriggerAction
{
   
    public string DisplayName { get; } = "M1S_击飞面向调整 by ken";


    public string Remark { get; set; }

    public bool Draw()
    {
        ImGui.Checkbox("启用面对安全点", ref this.tempStart);
        return true;
    }

    public bool Handle()
    {
        TriggerAction_M1S_Rot.Start = this.tempStart;
        return true;
    }

    public static bool Start;

    private bool tempStart;
}
using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;

namespace AE_ACR.PLD.Triggers;

public class TriggerAction_大翅膀_Rot : ITriggerAction
{
   
    public string DisplayName { get; } = "骑士大翅膀自动面向";


    public string Remark { get; set; }

    public bool Draw()
    {
        ImGui.Checkbox("启用大翅膀自动面向", ref this.tempStart);
        return true;
    }

    public bool Handle()
    {
        Start = this.tempStart;
        return true;
    }

    public static bool Start;

    private bool tempStart;
}
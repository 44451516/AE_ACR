using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.JobApi;
using ImGuiNET;

namespace AE_ACR.PLD.Triggers;

public class ITriggerCond_PLD调停充能 : ITriggerCond
{
    public float 自定义调停充能 = 1;

    public bool Draw()
    {
        ImGui.Text(">=自定义调停充能回真");
        ImGui.Text("1为调停可用,2是两层");
        ImGui.DragFloat("自定义调停充能", ref 自定义调停充能, 0.1f, 0, 2);
        return true;
    }


    public bool Handle(ITriggerCondParams condParams = null)
    {
        int currentOath = Core.Resolve<JobApi_Paladin>().Oath;
        if (自定义调停充能 >= currentOath)
        {
            return true;
        }
        return false;
    }

    public string DisplayName { get; } = "骑士调停充能";
    public string Remark { get; set; }
}
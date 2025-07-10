using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.JobApi;
using ImGuiNET;

namespace AE_ACR.DRK.Triggers;

public class ITriggerCond_DRK暗血值 : ITriggerCond
{
    public int 暗血值;

    public bool Draw()
    {
        ImGui.Text(">=自定义暗血值返回真");
        ImGui.DragInt("自定义暗血值", ref 暗血值, 10, 0, 100);
        return true;
    }


    public bool Handle(ITriggerCondParams condParams = null)
    {
        int currentOath = Core.Resolve<JobApi_DarkKnight>().Blood;
        if (暗血值 >= currentOath)
        {
            return true;
        }
        return false;
    }

    public string DisplayName { get; } = "黑骑暗血值检测";
    public string Remark { get; set; }
}
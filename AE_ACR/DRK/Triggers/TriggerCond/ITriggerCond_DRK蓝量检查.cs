using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.JobApi;
using ImGuiNET;

namespace AE_ACR.DRK.Triggers;

public class ITriggerCond_DRK蓝量检查 : ITriggerCond
{
    public int 蓝量;

    public bool Draw()
    {
        ImGui.Text(">=自定义蓝量返回真");
        ImGui.DragInt("自定义蓝量", ref 蓝量, 1000, 0, 10000);
        return true;
    }


    public bool Handle(ITriggerCondParams condParams = null)
    {
        uint currentOath = Core.Me.CurrentMp;
        if (蓝量 >= currentOath)
        {
            return true;
        }
        return false;
    }

    public string DisplayName { get; } = "黑骑蓝量检测";
    public string Remark { get; set; }
}
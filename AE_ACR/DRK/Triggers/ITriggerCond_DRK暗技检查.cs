using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.JobApi;
using ImGuiNET;

namespace AE_ACR.DRK.Triggers;

public class ITriggerCond_DRK暗技检查 : ITriggerCond
{
    public int 蓝量;

    public bool Draw()
    {
        ImGui.Text("如果存在暗技返回True");
        return true;
    }


    public bool Handle(ITriggerCondParams condParams = null)
    {
        return Core.Resolve<JobApi_DarkKnight>().HasDarkArts;
    }

    public string DisplayName { get; } = "黑骑暗技检测";
    public string Remark { get; set; }
}
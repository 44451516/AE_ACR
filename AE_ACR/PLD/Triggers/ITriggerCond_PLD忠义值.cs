using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.JobApi;
using ImGuiNET;

namespace AE_ACR.PLD.Triggers;

public class ITriggerCond_PLD忠义值 : ITriggerCond
{
    public int 自定义忠义值;

    public bool Draw()
    {
        ImGui.Text(">=自定义忠义值返回真");
        ImGui.DragInt("自定义忠义值", ref 自定义忠义值, 10, 0, 100);
        return true;
    }


    public bool Handle(ITriggerCondParams condParams = null)
    {
        int currentOath = Core.Resolve<JobApi_Paladin>().Oath;
        if (自定义忠义值 >= currentOath)
        {
            return true;
        }
        return false;
    }

    public string DisplayName { get; } = "骑士忠义值检测";
    public string Remark { get; set; }
}
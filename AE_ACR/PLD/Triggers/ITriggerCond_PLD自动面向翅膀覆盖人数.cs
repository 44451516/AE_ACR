using System.Numerics;
using AE_ACR.DRK.SlotResolvers;
using AE_ACR.Utils;
using AEAssist;
using AEAssist.Avoid;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using ImGuiNET;

namespace AE_ACR.PLD.Triggers;

public class ITriggerCond_PLD自动面向翅膀覆盖人数 : ITriggerCond
{
    public int 人数 = 4;

    public bool Draw()
    {
        ImGui.Text(">=PLD翅膀可以覆盖人数返回真");
        ImGui.Text("当前位置自动面向释放大翅膀能覆盖的人数");
        ImGui.DragInt("PLD最优翅膀可以覆盖人数", ref 人数, 1f, 0, 7);
        return true;
    }


    public bool Handle(ITriggerCondParams condParams = null)
    {
        if (人数 >= 获取人数())
        {
            return true;
        }
        return false;
    }


    public string DisplayName { get; } = "骑士自动面向大翅膀覆盖人数";
    public string Remark { get; set; }


    public int 获取人数()
    {
        int count = 0;
        float step = MathF.PI / 360;

        var 新面向 = false;

        // 从 MathF.PI 到 -MathF.PI 遍历
        for (float angle = MathF.PI; angle >= -MathF.PI; angle -= step)
        {
            var playerCount = RotUtil.GetPlayersInFanShape(PartyHelper.CastableAlliesWithin10, angle);
            if (playerCount > count)
            {
                count = playerCount;
            }
        }

        return count;
    }
}
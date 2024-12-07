using System.Numerics;
using AE_ACR.DRK.SlotResolvers;
using AEAssist;
using AEAssist.Avoid;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using ImGuiNET;

namespace AE_ACR.PLD.Triggers;

public class ITriggerCond_PLD翅膀覆盖人数 : ITriggerCond
{
    public int 人数 = 4;

    public bool Draw()
    {
        ImGui.Text(">=PLD翅膀可以覆盖人数返回真");
        ImGui.Text("[写的有问题不一定准]");
        ImGui.Text("当前位置和面向释放大翅膀能覆盖的人数");
        ImGui.DragInt("PLD翅膀可以覆盖人数", ref 人数, 1f, 0, 7);
        return true;
    }


    public bool Handle(ITriggerCondParams condParams = null)
    {
        if (人数 >= GetPlayersInFanShape(PartyHelper.CastableAlliesWithin10, Core.Me.Rotation))
        {
            return true;
        }
        return false;
    }


    public string DisplayName { get; } = "骑士翅膀覆盖人数";
    public string Remark { get; set; }




    public static List<string> 测试列表 = new List<string>();

    public static int GetPlayersInFanShape(List<IBattleChara> list, float Rotation)
    {
        测试列表.Clear();
        int count = 0;
        SectorShape sectorShape = new SectorShape()
        {
            Origin = Core.Me.Position, Angle = 90, Dir = -Rotation.GetDir(), Radius = 8f
        };

        foreach (var battleChara in list)
        {
            //去掉自己
            if (battleChara.GameObjectId == Core.Me.GameObjectId)
            {
                continue;
            }

            var distance = Core.Me.Distance(battleChara);
            if (distance > 8)
            {
                continue;
            }

            if (sectorShape.IsInShape(battleChara.Position))
            {
                测试列表.Add(battleChara.Name.TextValue);
                count++;
            }
        }

        return count;
    }
}
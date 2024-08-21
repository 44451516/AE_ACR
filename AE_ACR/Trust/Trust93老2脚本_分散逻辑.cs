using System.Numerics;
using AE_ACR.utils;
using AEAssist;
using AEAssist.Avoid;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;

//小怪钢铁
public class Trust93老2脚本_分散逻辑 : IResolverScript
{
    public void OnActive(DungeonController dungeonController, ResolverCondParams spellCondParams)
    {

    }

    public void OnUpdate(DungeonController dungeonController)
    {
        // 先拿到当前场景的地图管理
        var mapBaseName = Core.Resolve<MemApiZoneInfo>().GetCurrZoneInfo().MapBaseName;
        if (!AvoidManager.Instance.GetMap(mapBaseName, out var map))
        {

            return;
        }
        
        if (AI.Instance.BattleData.CurrBattleTimeInMs >= 50 * 1000)
        {
            //spl x=-53.139,y=-56.850,z=323.000f
            var vector3 = new Vector3(-53.139f, 323.000f, -56.850f);
            Core.Me.SetPos(vector3);
        }
        else
        {
            var battleChara = PartyHelper.Party[2];
            Core.Me.SetPos(battleChara.Position);
        }
    }
}